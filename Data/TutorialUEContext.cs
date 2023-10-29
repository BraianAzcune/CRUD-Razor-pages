using Microsoft.EntityFrameworkCore;
using TutorialEU.Models;

namespace TutorialEU.Data;

public class TutorialUEContext : DbContext {
    public TutorialUEContext(DbContextOptions<TutorialUEContext> options) : base(options) {
    }

    public DbSet<Person> Person { get; set; }
    public DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>()
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("getutcdate()");

        modelBuilder.Entity<Producto>()
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("getutcdate()");

        modelBuilder.Entity<Producto>()
            .Property(p => p.ModifiedAt)
            .HasDefaultValueSql("getutcdate()")
            .ValueGeneratedOnAddOrUpdate();

        modelBuilder.Entity<Producto>().ToTable(tb => tb.HasTrigger("Productos_UPDATE"));


    }
}

