using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Torchlight.Mvc5.Common.Libs.ModelAssets.IdentityAssets
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("IdentityDb") { }

        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(new IdentityDbInit());
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
