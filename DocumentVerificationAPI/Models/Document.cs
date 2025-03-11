using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentVerificationAPI.Models
{
    public class Document
    {
        public int Id { get; set; } //primary key
        public int UserId { get; set; } // forigen key link user one-many

        public string Title { get; set; }
        public string FilePath { get; set; }
        public string VerificationCode { get; set; }

        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } //referencce to user object
        public ICollection<VerificationLog> VerificationLogs { get; set; }
        [NotMapped] // Exclude from database
        public IFormFile File { get; set; } // File for upload

    }
}
