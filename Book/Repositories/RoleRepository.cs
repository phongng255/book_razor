using Book.Areas.Identity.Data;
using Book.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Book.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BookContext _context;

        public RoleRepository(BookContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
