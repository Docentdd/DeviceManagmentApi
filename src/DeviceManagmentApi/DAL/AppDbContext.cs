using DeviceManagementApi.Models;
using DeviceManagmentApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagmentApi.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Account { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Position> Position { get; set; }
    public DbSet<DeviceType> DeviceType { get; set; } = null!;
    public DbSet<Device> Device { get; set; } = null!;
    public DbSet<DeviceEmployee> DeviceEmployee { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<DeviceEmployee>()
            .HasKey(de => new { de.DeviceId, de.EmployeeId });

      
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "User" }
        );

   
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Person)
            .WithMany()
            .HasForeignKey(e => e.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Position)
            .WithMany(p => p.Employees)
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Device>()
            .HasOne(d => d.DeviceType)
            .WithMany(dt => dt.Devices)
            .HasForeignKey(d => d.DeviceTypeId);

        modelBuilder.Entity<DeviceEmployee>()
            .HasOne(de => de.Device)
            .WithMany(d => d.DeviceEmployees)
            .HasForeignKey(de => de.DeviceId);

        modelBuilder.Entity<DeviceEmployee>()
            .HasOne(de => de.Employee)
            .WithMany(e => e.DeviceEmployees)
            .HasForeignKey(de => de.EmployeeId);
    }
}