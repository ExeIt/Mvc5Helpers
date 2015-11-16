using System.Web;
using Microsoft.AspNet.Identity;
using Torchlight.Mvc5.Common.Libs.ModelAssets.IdentityAssets;

namespace Torchlight.Mvc5.Common.Libs.AspNetIdentity
{
    public class UserContext
    {
        public static AppUser CurrentUser
        {
            get { return UserManagerContext.UserManager.FindByName(HttpContext.Current.User.Identity.Name); }
        }
    }    
}
