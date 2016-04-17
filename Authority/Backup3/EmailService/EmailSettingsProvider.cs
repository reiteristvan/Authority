using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.EmailService.cs;

namespace IdentityServer.Tests.EmailService
{
    public sealed class EmailSettingsProvider : IEmailSettingsProvider
    {
        public string TemplateFolderPath
        {
            get { throw new NotImplementedException(); }
        }
    }
}
