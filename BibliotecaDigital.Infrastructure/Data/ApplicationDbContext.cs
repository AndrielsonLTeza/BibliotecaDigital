using BibliotecaDigital.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BibliotecaDigital.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired();

                // Armazenar Roles como JSON
                entity.Property(e => e.Roles)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                        v => JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions()));
            });

            // Configuração da entidade Genre
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // Configuração da entidade Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ISBN).IsRequired().HasMaxLength(20);
                entity.Property(e => e.PublishedYear).IsRequired();

                // Relacionamento com Genre
                entity.HasOne(e => e.Genre)
                      .WithMany(e => e.Books)
                      .HasForeignKey(e => e.GenreId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
