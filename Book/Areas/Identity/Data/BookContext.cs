using Book.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Book.Model;

namespace Book.Areas.Identity.Data;

public class BookContext : IdentityDbContext<ApplicationUser>
{
    public BookContext(DbContextOptions<BookContext> options)
        : base(options)
    {
    }
    public BookContext() { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Tacgia>()
            .HasMany<_Book>(g => g.Books)
            .WithOne(s => s.Tacgia)
            .HasForeignKey(s => s.tacgiaId);

        builder.Entity<The_Loais>()
           .HasMany<_Book>(g => g.Books)
           .WithOne(s => s.The_Loai)
           .HasForeignKey(s => s.the_loaiId);
    }
    public virtual DbSet<_Book> Books { get; set; }
    public virtual DbSet<Tacgia> Tacgias { get; set; }
    public virtual DbSet<The_Loais> The_Loais { get; set; }

    public virtual DbSet<withlist> Withlists { get; set; }
}
