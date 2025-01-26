using Microsoft.EntityFrameworkCore;
using PreschoolApp.Models;

namespace PreschoolApp.Data;

public class PreschoolDbContext: DbContext
{
    public PreschoolDbContext(DbContextOptions<PreschoolDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Review> Reviews { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Student>()
            .HasOne(c => c.Parent)
            .WithMany()
            .HasForeignKey(c => c.ParentId);

        modelBuilder.Entity<StudentGroup>()
            .HasKey(sg => new { sg.StudentId, sg.GroupId });

        modelBuilder.Entity<StudentGroup>()
            .HasOne(sg => sg.Student)
            .WithMany(s => s.StudentGroups)
            .HasForeignKey(sg => sg.StudentId);

        modelBuilder.Entity<StudentGroup>()
            .HasOne(sg => sg.Group)
            .WithMany(g => g.StudentGroups)
            .HasForeignKey(sg => sg.GroupId);
        
        modelBuilder.Entity<TeacherSubject>()
            .HasKey(ts => new { ts.TeacherId, ts.SubjectId });

        modelBuilder.Entity<TeacherGroup>()
            .HasKey(tg => new { tg.TeacherId, tg.GroupId });
        
        modelBuilder.Entity<TeacherGroup>()
            .HasOne(tg => tg.Teacher)
            .WithMany()
            .HasForeignKey(tg => tg.TeacherId);

        modelBuilder.Entity<TeacherGroup>()
            .HasOne(tg => tg.Group)
            .WithMany(g => g.TeacherGroups)
            .HasForeignKey(tg => tg.GroupId);
        
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Student)
            .WithMany(s => s.Reviews)
            .HasForeignKey(r => r.StudentId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.SentMessages)
            .WithOne(m => m.Sender)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.ReceivedMessages)
            .WithOne(m => m.Receiver)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}