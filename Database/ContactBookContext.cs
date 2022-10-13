using Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class ContactBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<ContactInGroup> ContactInGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ContactInGroup>()
            .HasKey(x => new { x.ContactId, x.GroupId });
    }

    public ContactBookContext(DbContextOptions<ContactBookContext> options) : base(options)
    {
    }
}
