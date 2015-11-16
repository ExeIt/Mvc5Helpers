using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Torchlight.Mvc5.Common.Libs.AspNetIdentity
{
    public class CustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string pass)
        {
            var result = await base.ValidateAsync(pass);

            if (pass.Contains("12345"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Passwords cannot contain numeric sequences");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}
