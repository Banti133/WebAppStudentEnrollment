using Microsoft.EntityFrameworkCore;

namespace WebApStudentEnrolment.Data
{
    public class StudentEnrolmentContext : DbContext
    {
        public StudentEnrolmentContext(DbContextOptions<StudentEnrolmentContext> options)
            : base(options)
        {
        }
        public DbSet<Models.Student> Students { get; set; } = null!;
        public DbSet<Models.Course> Courses { get; set; } = null!;
        public DbSet<Models.Enrolment> Enrolments { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration can be added here if needed
        }

    }
}
