using Microsoft.EntityFrameworkCore;
using SubmissionManager.Data.Entities;

namespace SubmissionManager.Data;

public class SubmissionContext : DbContext
{
    public SubmissionContext() : base()
    {
        Database.EnsureCreated();
    }

    public DbSet<Submission> Submissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder opt)
    {
        opt.UseSqlite("Data Source=Submissions.db");
    }

    public async Task<Submission> GetByIdAsync(int id)
    {
        var submission = await Submissions.Include(s => s.Document)
                                          .FirstOrDefaultAsync(s => s.Id == id);

        return submission;
    }
}
