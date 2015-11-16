using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace Torchlight.Mvc5.Common.Libs.AspNetIdentity
{
    public class UserManagerContext
    {
        public static AppUserManager UserManager
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.User != null)
                    return HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();

                return null;
            }
        }
    }
}
