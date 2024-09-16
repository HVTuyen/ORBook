using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ORBook.Models;

namespace ORBook.Data
{
    public class ORBookContext : DbContext
    {
        public ORBookContext (DbContextOptions<ORBookContext> options)
            : base(options)
        {
        }

        public DbSet<ORBook.Models.Book> Book { get; set; } = default!;
        public DbSet<ORBook.Models.Genre> Genre { get; set; } = default!;
        public DbSet<ORBook.Models.Volumn> Volumn { get; set; } = default!;
        public DbSet<ORBook.Models.User> User { get; set; } = default!;
        public DbSet<ORBook.Models.Notification> Notification { get; set; } = default!;
        public DbSet<ORBook.Models.BookGenre> BookGenre { get; set; } = default!;
        public DbSet<ORBook.Models.BookUser> BookUser { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId);

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.BookGenres)
                .HasForeignKey(bg => bg.GenreId);

            modelBuilder.Entity<BookUser>()
                .HasKey(bu => new { bu.BookId, bu.UserId });

            modelBuilder.Entity<BookUser>()
                .HasOne(bu => bu.Book)
                .WithMany(b => b.BookUsers)
                .HasForeignKey(bu => bu.BookId);

            modelBuilder.Entity<BookUser>()
                .HasOne(bu => bu.User)
                .WithMany(g => g.BookUsers)
                .HasForeignKey(bu => bu.UserId);
        }
    }
}
