using Microsoft.AspNetCore.Identity;

namespace Book.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
