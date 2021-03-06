﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EmailService;
using Authority.EmailService.Models;
using Authority.EntityFramework;
using Authority.Operations.Account;
using IdentityServer.UnitOfWork.Account;

namespace IdentityServer.Services
{
    public interface IAccountService
    {
        Task<ValidationResult> ValidateProduct(Guid apiKey);
        Task RegisterUser(Guid productId, string email, string username, string password);
        Task ActivateUser(Guid productId, Guid activationCode);
        Task<bool> LogInUser(Guid productId, string email, string password);
        Task<List<Claim>> GetUserClaims(string email);
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public Guid ProductId { get; set; }
    }

    public sealed class AccountService : IAccountService
    {
        private readonly IAuthorityContext _authorityContext;
        private readonly IEmailService _emailService;

        public AccountService(IAuthorityContext authorityContext, IEmailService emailService)
        {
            if (authorityContext == null)
            {
                throw new ArgumentNullException("authorityContext");
            }

            if (emailService == null)
            {
                throw new ArgumentNullException("emailService");
            }

            _authorityContext = authorityContext;
            _emailService = emailService;
        }

        public async Task<ValidationResult> ValidateProduct(Guid apiKey)
        {
            ValidationResult result = new ValidationResult();
            Product product = await _authorityContext.Products.FirstOrDefaultAsync(p => p.ApiKey == apiKey);

            if (product != null)
            {
                result.ProductId = product.Id;
            }

            result.IsValid =  product != null && product.IsPublic && product.IsActive;
            return result;
        }

        public async Task RegisterUser(Guid productId, string email, string username, string password)
        {
            Product product = await _authorityContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            UserRegistration operation = new UserRegistration(_authorityContext, product.Id, email, username, password);
            User user = await operation.Do();
            await operation.CommitAsync();

            await _emailService.SendUserActivation(email, new UserActivationModel
            {
                ActivationUrl = string.Format("{0}/{1}", product.ActivationUrl, user.PendingRegistrationId),
                ProductName = product.Name,
                Sender = product.NotificationEmail
            });
        }

        public async Task ActivateUser(Guid productId, Guid activationCode)
        {
            UserActivation operation = new UserActivation(_authorityContext, productId, activationCode);
            await operation.Do();
            await operation.CommitAsync();
        }

        public async Task<bool> LogInUser(Guid productId, string email, string password)
        {
            UserLogIn operation = new UserLogIn(_authorityContext, productId, email, password);

            if (!await operation.Do())
            {
                return false;
            }

            return true;
        }

        public async Task<List<Claim>> GetUserClaims(string email)
        {
            User user = await _authorityContext.Users
                .Include(u => u.Policies)
                .Include(u => u.Policies.Select(po => po.Claims))
                .FirstAsync(u => u.Email == email);

            List<Claim> claims = user.Policies.SelectMany(u => u.Claims).ToList();

            HashSet<Guid> idList = new HashSet<Guid>();
            List<Claim> distinctClaims = new List<Claim>();

            foreach (Claim claim in claims)
            {
                if (!idList.Contains(claim.Id))
                {
                    idList.Add(claim.Id);
                    distinctClaims.Add(claim);
                }
            }

            return distinctClaims;
        }
    }
}
