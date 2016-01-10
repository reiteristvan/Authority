﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.DomainModel;

namespace IdentityServer.IntegrationTests
{
    public static class EntityGenerator
    {
        public static Developer Developer()
        {
            return new Developer
            {
                Created = DateTime.UtcNow,
                DisplayName = "alma",
                Email = "",
                IsActive = true,
                IsPending = false
            };
        }
    }
}
