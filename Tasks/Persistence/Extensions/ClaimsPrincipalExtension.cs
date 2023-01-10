using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace Tasks.Persistence.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal model)
        {
            return model.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
