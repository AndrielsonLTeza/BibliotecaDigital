using BibliotecaDigital.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Genre entity
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // Configure Book entity
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ISBN).IsRequired().HasMaxLength(20);
                entity.Property(e => e.PublishedYear).IsRequired();

                // Configure the one-to-many relationship
                entity.HasOne(e => e.Genre)
                      .WithMany(e => e.Books)
                      .HasForeignKey(e => e.GenreId)
                      .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
            });
        }
    }
}