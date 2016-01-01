﻿using System;
using System.Collections.Generic;

namespace IdentityServer.DomainModel
{
    public class User : EntityBase
    {
        public User()
        {
            Policies = new HashSet<Policy>();
        }

        public string Email { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public bool IsPending { get; set; }

        public Guid PendingRegistrationId { get; set; }

        public bool IsExternal { get; set; }

        public string ExternalProviderName { get; set; }

        public string ExternalToken { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Policy> Policies { get; set; } 
    }
}
