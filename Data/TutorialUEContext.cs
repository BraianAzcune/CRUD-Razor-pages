using Microsoft.EntityFrameworkCore;
using TutorialEU.Models;

namespace TutorialEU.Data;

public class TutorialUEContext : DbContext {
    public TutorialUEContext(DbContextOptions<TutorialUEContext> options) : base(options) {
    }

    public DbSet<Person> Person { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>()
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("getutcdate()");
    }
}

