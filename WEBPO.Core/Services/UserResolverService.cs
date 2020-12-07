using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;
using WEBPO.Core.Interfaces;
using WEBPO.Core.Persistances;

namespace WEBPO.Core.Services
{
    public class UserResolverService : IUserResolverService
    {
        public readonly IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public virtual bool IsAuthenticated => (_context.HttpContext.User != null && _context.HttpContext.User.Identity.IsAuthenticated);
        public virtual bool IsAdmin => _context.HttpContext.User.IsInRole(ROLE.Admin) || false;
        public virtual string RoleText => _context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        public virtual string UserName => _context.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        public virtual string UserID => _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        public virtual string Email => _context.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
        public virtual string CompanyName => _context.HttpContext.User.FindFirst("CompanyName").Value;
        public virtual string VendorCode => _context.HttpContext.User.FindFirst("VendorCode").Value;
    }
}
