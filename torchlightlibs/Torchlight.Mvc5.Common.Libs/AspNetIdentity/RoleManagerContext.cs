using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace Torchlight.Mvc5.Common.Libs.AspNetIdentity
{
    public class RoleManagerContext
    {
        public static AppRoleManager RoleManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>(); }
        }
    }
}
