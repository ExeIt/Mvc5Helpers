using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Torchlight.Mvc5.Common.Libs.ModelAssets.IdentityAssets;

namespace Torchlight.Mvc5.Common.Libs.AspNetIdentity
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager mgr) : base(mgr)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            var result = await base.ValidateAsync(user);

            //if (!user.Email.ToLower().EndsWith("example.com"))
            //{
            //    var errors = result.Errors.ToList();
            //    errors.Add("Only example.com email addresses are allowed");
            //    result = new IdentityResult(errors);
            //}

            return result;
        }        
    }
}
