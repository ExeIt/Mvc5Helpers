using System.ComponentModel.DataAnnotations;

namespace Torchlight.Mvc5.Common.Libs.ModelAssets.IdentityAssets
{
    public class RoleModificationViewModel
    {
        [Required]
        public string RoleName { get; set; }

        public string[] IdsToAdd { get; set; }

        public string[] IdsToDelete { get; set; }
    }
}
