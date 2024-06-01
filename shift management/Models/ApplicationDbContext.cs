using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;

namespace shift_management.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<InvitationLog> InvitationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvitationLog>()
            .HasKey(log => log.LogId);

           
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Worker)
                .WithMany(w => w.Enrollments)
                .HasForeignKey(e => e.WorkerId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Shift)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.ShiftId);

            modelBuilder.Entity<InvitationLog>()
                .HasOne(il => il.Worker)
                .WithMany(w => w.InvitationLogs)
                .HasForeignKey(il => il.WorkerId);

            modelBuilder.Entity<InvitationLog>()
                .HasOne(il => il.Shift)
                .WithMany(s => s.InvitationLogs)
                .HasForeignKey(il => il.ShiftId);
        }
    }
}
