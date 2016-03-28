﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork.Products
{
    public sealed class UpdateProduct : OperationWithReturnValueAsync<Product>
    {
        private readonly string _name;
        private readonly bool _isPublic;
        private readonly string _siteUrl;
        private readonly string _landingPage;
        private readonly bool _isActive;

        public UpdateProduct(IIdentityServerContext identityServerContext, 
            string name, bool isActive, bool isPublic, string siteUrl, string landingPage)
            : base(identityServerContext)
        {
            _name = name;
            _isPublic = isPublic;
            _siteUrl = siteUrl;
            _landingPage = landingPage;
            _isActive = isActive;
        }

        public override Task<Product> Do()
        {
            throw new NotImplementedException();
        }
    }
}
