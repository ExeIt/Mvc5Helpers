using System.Collections.Generic;
using System.Security.Claims;

namespace Torchlight.Mvc5.Common.Libs.AspNetIdentity
{
    public static class LocationClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(ClaimsIdentity user)
        {
            //var claims = new List<Claim>();

            //if (user.Name.ToLower() == "admin")
            //{
            //    claims.Add(CreateClaim(ClaimTypes.PostalCode, "EX8 2BS"));
            //    claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "Devon"));
            //}
            //else
            //{
            //    claims.Add(CreateClaim(ClaimTypes.PostalCode, "HP11 2UP"));
            //    claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "Bucks"));
            //}

            return new List<Claim>();
        }

        //private static Claim CreateClaim(string type, string value)
        //{
        //    return new Claim(type, value, ClaimValueTypes.String, "RemoteClaims");
        //}
    }
}
