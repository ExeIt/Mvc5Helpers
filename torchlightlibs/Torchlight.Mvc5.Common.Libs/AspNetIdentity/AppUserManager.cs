using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Torchlight.Mvc5.Common.Libs.ModelAssets.IdentityAssets;

namespace Torchlight.Mvc5.Common.Libs.AspNetIdentity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store) { }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var db = context.Get<AppDbContext>();
            var manager = new AppUserManager(new UserStore<AppUser>(db))
            {
                PasswordValidator = new CustomPasswordValidator
                {
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true
                }
            };

            manager.UserValidator = new CustomUserValidator(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };
            
            return manager;
        }
    }
}
