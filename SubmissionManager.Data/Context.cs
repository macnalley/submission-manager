using Microsoft.EntityFrameworkCore;
using SubmissionManager.Data.Entities;

namespace SubmissionManager.Data;

public class SubmissionContext : DbContext
{
    DbSet<Submission> Submissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder opt)
    {
        opt.UseSqlite("Data Source=Submissions.db");
    }
}
