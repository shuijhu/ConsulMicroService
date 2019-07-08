using HeLian.Xiaoyi.Indetity.Host.Service;
using HeLian.Xiaoyi.ViewModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HeLian.Xiaoyi.Indetity.Host
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private ILoginUserService _loginUserService;

        public ResourceOwnerPasswordValidator(ILoginUserService loginUserService)
        {
            _loginUserService = loginUserService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            UserMo user = await _loginUserService.ValidateUser(context.UserName, context.Password);
            if (user != null)
            {
                context.Result = new GrantValidationResult(
                    subject: context.UserName,
                    authenticationMethod: "custom",
                    claims: new Claim[] {
                        new Claim("Name", context.UserName),
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    }
                );
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid client credential");
            }
        }
    }
    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = context.Subject.Claims.ToList();
            context.IssuedClaims = claims.ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
