using Microsoft.AspNet.Identity.EntityFramework;

namespace Torchlight.Mvc5.Common.Libs.ModelAssets.IdentityAssets
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base()
        {
        }
        
        public AppRole(string name) : base(name) { }
    }
}
