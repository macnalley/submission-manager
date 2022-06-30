using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SubmissionManager.Data.Entities
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Document Document { get; set; }
        [Required]
        public string CoverLetter { get; set; }
        public Status Status { get; set; } = Status.New;
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
        public int WordCount { get; set; }
    }

    public enum Status 
    {
        New,
        Advanced,
        Rejected,
        Accepted
    }
}