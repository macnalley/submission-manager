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
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
                    .HasData(new User 
                    { 
                        Id = 1, 
                        UserName = "Admin", 
                        Password = "SubmissionManagerPassword"
                    });
    }

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

    public async Task<Submission> GetByIdAndEmailAsync(int id, string email)
    {
        var submission = await Submissions.Where(s => s.Id == id && s.Email == email).SingleOrDefaultAsync();

        return submission;
    }

    public async Task<User> GetByUsernameAndPassword(string userName, string password)
    {
        var user = await Users.Where(u => u.UserName == userName && u.Password == password)
                              .SingleOrDefaultAsync();

        return user;
    }

    public async Task<List<Submission>> GetUnreadAsync()
    {
        var unreadSubmissions = await Submissions.Where(s => s.Status == Status.New)
                                                 .ToListAsync();

        return unreadSubmissions;
    }
}
