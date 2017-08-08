using IdentityDemo3.Data;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo3.Models
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (string.IsNullOrEmpty(context.UserName) || string.IsNullOrEmpty(context.Password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "Invalid username or password");
                return;
            }

            var user = await _userManager.FindByEmailAsync(context.UserName);
            if(user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "Invalid username or password");
                return;
            }


            var result =await _signInManager.CheckPasswordSignInAsync(user, context.Password, false);
            if (result.Succeeded)
            {
                context.Result = new GrantValidationResult(context.UserName, context.Password); 
                return;
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient, "Invalid username or password");
                return;
            }
        }
    }
}
