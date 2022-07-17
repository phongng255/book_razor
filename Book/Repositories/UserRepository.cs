using Book.Areas.Identity.Data;
using Book.Core.Repositories;

namespace Book.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookContext _context;

        public UserRepository(BookContext context)
        {
            _context = context;
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
             _context.Update(user);
             _context.SaveChanges();

             return user;
        }
    }
}
