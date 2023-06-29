﻿using System;
using Microsoft.Extensions.Configuration;

namespace Benday.YamlDemoApp.Api.Security
{
    public class SecurityConfiguration : ISecurityConfiguration
    {
        public SecurityConfiguration(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), $"{nameof(configuration)} is null.");
            }

            AuthType = configuration.GetValue<string>("SecuritySettings:AuthType");
            LoginPath = configuration.GetValue<string>("SecuritySettings:LoginPath");
            LogoutPath = configuration.GetValue<string>("SecuritySettings:LogoutPath");
            PostLoginPath = configuration.GetValue<string>("SecuritySettings:PostLoginPath");
            PostLogoutPath = configuration.GetValue<string>("SecuritySettings:PostLogoutPath");
            UserAccountPath = configuration.GetValue<string>("SecuritySettings:UserAccountPath");
            RegisterPath = configuration.GetValue<string>("SecuritySettings:RegisterPath");
            DevelopmentMode = configuration.GetValue<bool>("SecuritySettings:DevelopmentMode");
            AzureActiveDirectory = configuration.GetValue<bool>("SecuritySettings:AzureActiveDirectory");
            Google = configuration.GetValue<bool>("SecuritySettings:Google");
            MicrosoftAccount = configuration.GetValue<bool>("SecuritySettings:MicrosoftAccount");
            Twitter = configuration.GetValue<bool>("SecuritySettings:Twitter");
            Facebook = configuration.GetValue<bool>("SecuritySettings:Facebook");
        }

        public string AuthType { get; private set; }
        public bool DevelopmentMode { get; private set; }
        public bool AzureActiveDirectory { get; private set; }
        public bool Google { get; private set; }
        public bool MicrosoftAccount { get; private set; }
        public bool Twitter { get; private set; }
        public bool Facebook { get; private set; }
        public string LoginPath { get; private set; }
        public string LogoutPath { get; private set; }
        public string RegisterPath { get; private set; }
        public string PostLoginPath { get; private set; }
        public string PostLogoutPath { get; private set; }
        public string UserAccountPath { get; private set; }
    }
}
