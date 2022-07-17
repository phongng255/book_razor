using Book.Areas.Identity.Data;

namespace Book.Model
{
    public class withlist
    {
        public int id { get; set; }

        public virtual int bookId { get; set; }
        public virtual _Book Book { get; set; }
        public string IdentityUserId { get; set; }
        public virtual ApplicationUser IdentityUser { get; set; }

    }
}
