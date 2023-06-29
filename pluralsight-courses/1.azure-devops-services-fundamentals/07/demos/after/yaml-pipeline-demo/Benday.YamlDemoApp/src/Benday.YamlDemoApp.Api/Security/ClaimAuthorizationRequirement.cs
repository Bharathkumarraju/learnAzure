﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Benday.YamlDemoApp.Api.Security
{
    public class ClaimAuthorizationRequirement : IAuthorizationRequirement
    {
        public List<string> Roles { get; set; } = new List<string>();
        public List<string> PermissionNames { get; set; } = new List<string>();
    }
}
