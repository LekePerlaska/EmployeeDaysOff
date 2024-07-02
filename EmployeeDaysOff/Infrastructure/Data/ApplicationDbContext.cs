using EmployeeDaysOff.Core.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(lt => lt.Id);
            entity.Property(lt => lt.Name).IsRequired().HasMaxLength(50);
            entity.HasData(
                new LeaveType { Id = 1, Name = "Annual", DefaultDays = 0, DateCreated = DateTime.Now },
                new LeaveType { Id = 2, Name = "Sick", DefaultDays = 20, DateCreated = DateTime.Now },
                new LeaveType { Id = 3, Name = "Replacement", DefaultDays = 0, DateCreated = DateTime.Now },
                new LeaveType { Id = 4, Name = "Unpaid", DefaultDays = 10, DateCreated = DateTime.Now }
            );
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(lr => lr.Id);
            entity.HasOne(lr => lr.RequestingEmployee)
                  .WithMany()
                  .HasForeignKey(lr => lr.RequestingEmployeeId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(lr => lr.LeaveType)
                  .WithMany()
                  .HasForeignKey(lr => lr.LeaveTypeId);
            entity.HasOne(lr => lr.ApprovedBy)
                  .WithMany()
                  .HasForeignKey(lr => lr.ApprovedById)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<LeaveAllocation>(entity =>
        {
            entity.HasKey(la => la.Id);
            entity.HasOne(la => la.Employee)
                  .WithMany()
                  .HasForeignKey(la => la.EmployeeId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(la => la.LeaveType)
                  .WithMany()
                  .HasForeignKey(la => la.LeaveTypeId);
		});
	}	
}