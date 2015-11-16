using System.Collections.Generic;

namespace Torchlight.Mvc5.Common.Libs.ModelAssets.IdentityAssets
{
    public class RoleEditViewModel
    {
        public AppRole Role { get; set; }

        public IEnumerable<AppUser> Members { get; set; }

        public IEnumerable<AppUser> NonMembers { get; set; }
    }
}
