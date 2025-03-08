namespace DocumentVerificationAPI.Models
{
    public class VerificationLog
    {
        public int Id { get; set; }  // pk
        public int DocumentId { get; set; } // fk


        public int VerifiedByUserId { get; set; } // fk to user
        public User VerifiedByUser { get; set; } // reference to  user

        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Status { get; set; }

        public Document Document { get; set; } // reference document obj 
    }
}
