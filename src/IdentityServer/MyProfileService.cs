using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class MyProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string strName = string.Empty;
            return;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var allowedClients = context.Subject.Claims.FirstOrDefault(c => c.Type == "allowedClients")?.Value;
            
            if (!string.IsNullOrEmpty(allowedClients) && allowedClients.Contains(context.Client.ClientId))
                context.IsActive = true;
            else
                context.IsActive = false;
        }
    }
}
