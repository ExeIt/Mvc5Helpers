using Torchlight.Mvc5.Common.Libs.ModelAssets.IdentityAssets;

namespace Torchlight.Mvc5.Common.Libs.Interfaces
{
    public interface IUserContext
    {
        //void ResetSession();

        AppUser User { get; }
    }
}
