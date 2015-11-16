using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Torchlight.Mvc5.Common.Libs.ModelAssets.IdentityAssets;

namespace Torchlight.Mvc5.Common.Libs.AspNetIdentity
{
    public class AppRoleManager : RoleManager<AppRole>
    {
        public AppRoleManager(RoleStore<AppRole> store) : base(store)
        {            
        }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            var db = context.Get<AppDbContext>();
            return new AppRoleManager(new RoleStore<AppRole>(db));
        }
    }      
}
