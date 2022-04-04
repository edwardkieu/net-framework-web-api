using System.Threading;

namespace Common.Helpers
{
    public static class IdentityHelper
    {
        public static string GetClaimType(string type)
        {
            return ((System.Security.Claims.ClaimsPrincipal)Thread.CurrentPrincipal).FindFirst(c => c.Type == type).Value ?? null;
        }
    }
}