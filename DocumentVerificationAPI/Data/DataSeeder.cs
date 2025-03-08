using DocumentVerificationAPI.Models;

namespace DocumentVerificationAPI.Data
{
    public class DataSeeder
    {
        public static void Initialize(AppDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if there are  users in the database, no need to seed
            if (context.Users.Any())
            {
                return;
            }
            // Seed Users
            var users = new[]
            {
                new User
                {
                    Name = "Admin User",
                    Email = "admin@example.com",
                    Password = "124qwe", // In a real app, hash this password!
                    Role = "Admin"
                },
                new User
                {
                    Name = "Regular User",
                    Email = "user@example.com",
                    Password = "456qwe", // In a real app, hash this password!
                    Role = "User"
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
            var documents = new[]
           {
                new Document
                {
                    Title = "Document 1",
                    FilePath = "/Uploads/test-admin.pdf",
                    VerificationCode = Guid.NewGuid().ToString(),
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    UserId = users[0].Id 
                },
                new Document
                {
                    Title = "Document 2",
                    FilePath = "/Uploads/test-user.pdf",
                    VerificationCode = Guid.NewGuid().ToString(),
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    UserId = users[1].Id 
                }
            };

            context.Documents.AddRange(documents); // save to db
            context.SaveChanges();

            var verificationLogs = new[]
            {
                new VerificationLog
                {
                    Timestamp = DateTime.UtcNow,
                    Status = "Verified",
                    DocumentId = documents[0].Id,
                    VerifiedByUserId = users[0].Id // by Admin User
                }
            };

            context.VerificationLogs.AddRange(verificationLogs);
            context.SaveChanges();


        }
    }
}
