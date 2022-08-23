using System.ComponentModel.DataAnnotations;
using SubmissionManager.Data.Entities;

public class SubmissionModel
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Email { get; set; }
    public Status Status { get; set; }
    public int QueuePosition { get; set; }
    public string Message { get; set; }
}