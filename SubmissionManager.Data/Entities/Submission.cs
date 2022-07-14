using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SubmissionManager.Data.Entities
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Author name is required.")]
        public string? Author { get; set; }
        [Required(ErrorMessage = "Email is required."), 
        RegularExpression(@"[^@]*.*\..*", ErrorMessage = "Must be a valid email.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Document is required.")]
        public Document? Document { get; set; }
        [Required(ErrorMessage = "Cover letter is required.")]
        public string? CoverLetter { get; set; }
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