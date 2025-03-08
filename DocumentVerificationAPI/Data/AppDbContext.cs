using DocumentVerificationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentVerificationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<VerificationLog> VerificationLogs { get; set; }

        // link relationship

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   // one - many relationship
            modelBuilder.Entity<Document>()
              .HasOne(d => d.User)
              .WithMany(u => u.Documents)
              .HasForeignKey(d => d.UserId)
              .OnDelete(DeleteBehavior.Cascade); // Delete documents if a user is deleted

            modelBuilder.Entity<VerificationLog>()
               .HasOne(vl => vl.Document)
               .WithMany(d => d.VerificationLogs)
               .HasForeignKey(vl => vl.DocumentId)
               .OnDelete(DeleteBehavior.Cascade); // Delete logs if a document is deleted

            modelBuilder.Entity<VerificationLog>()
                .HasOne(vl => vl.VerifiedByUser)
                .WithMany()
                .HasForeignKey(vl => vl.VerifiedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

