namespace SubmissionManager.Data.Entities
{
    public class Submission
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string CoverLetter { get; set; }
        public Status Status { get; set; }
        public DateTime DateSubmitted { get; set; }
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