using System.Web.Mvc;

namespace Torchlight.Mvc5.Common.Libs.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        //public AccessRights RequiredAccess { get; set; }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    var user = ObjectFactory.GetInstance<IUserContext>();

        //    if (user == null || user.Member == null)
        //        return false;

        //    return (user.Member.AccessLevel & (int) RequiredAccess) > 0;
        //}
    }
}
