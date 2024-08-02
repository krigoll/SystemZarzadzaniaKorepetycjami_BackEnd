using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
public partial class SZKContext : DbContext
{
    public virtual DbSet<Administrator> Administrator { get; set; }
    public virtual DbSet<Calendar> Calendar { get; set; }
    public virtual DbSet<Lesson> Lesson { get; set; }
    public virtual DbSet<LessonStatus> LessonStatus { get; set; }
    public virtual DbSet<Mark> Mark { get; set; }
    public virtual DbSet<Message> Message { get; set; }
    public virtual DbSet<Opinion> Opinion { get; set; }
    public virtual DbSet<Person> Person { get; set; }
    public virtual DbSet<Report> Report { get; set; }
    public virtual DbSet<Student> Student { get; set; }
    public virtual DbSet<StudentAnswer> StudentAnswer { get; set; }
    public virtual DbSet<Subject> Subject { get; set; }
    public virtual DbSet<SubjectCategory> SubjectCategory { get; set; }
    public virtual DbSet<SubjectLevel> SubjectLevel { get; set; }
    public virtual DbSet<Task> Task { get; set; }
    public virtual DbSet<TaskType> TaskType { get; set; }
    public virtual DbSet<Teacher> Teacher { get; set; }
    public virtual DbSet<TeacherSalary> TeacherSalary { get; set; }
    public virtual DbSet<Test> Test { get; set; }
    public virtual DbSet<TestForStudent> TestForStudent { get; set; }

public SZKContext()
{
}

public SZKContext(DbContextOptions<SZKContext> options) : base(options)
{
}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.IdAdministrator)
                    .HasName("Administrator_pk");

                entity.Property(e => e.IdAdministrator).ValueGeneratedNever();

                entity.HasOne(d => d.IdAdministratorNavigation)
                    .WithOne(p => p.Administrator)
                    .HasForeignKey<Administrator>(d => d.IdAdministrator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Administrator_Person");
            });

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.HasKey(e => new { e.IdTeacher, e.Date })
                    .HasName("Calendar_pk");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.Calendar)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Calendar_Teacher");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.IdLesson)
                    .HasName("Lesson_pk");

                entity.HasOne(d => d.IdLessonStatusNavigation)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.IdLessonStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Lesson_Lesson_Status");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Lesson_Student");

                entity.HasOne(d => d.IdSubjectLevelNavigation)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.IdSubjectLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Lesson_Subject");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Lesson_Teacher");
            });

            modelBuilder.Entity<LessonStatus>(entity =>
            {
                entity.HasKey(e => e.IdLessonStatus)
                    .HasName("Lesson_Status_pk");

                entity.ToTable("Lesson_Status");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.HasKey(e => e.IdMark)
                    .HasName("Mark_pk");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.IdMessage)
                    .HasName("Message_pk");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.ReceiverNavigation)
                    .WithMany(p => p.MessageReceiverNavigation)
                    .HasForeignKey(d => d.Receiver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Message_Reciever");

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.MessageSenderNavigation)
                    .HasForeignKey(d => d.Sender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Message_Sender");
            });

            modelBuilder.Entity<Opinion>(entity =>
            {
                entity.HasKey(e => e.IdOpinion)
                    .HasName("Opinion_pk");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Opinion)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Recenzja_Student");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.Opinion)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Recenzja_Teacher");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdPerson)
                    .HasName("Person_pk");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.IdReport)
                    .HasName("Report_pk");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.Report)
                    .HasForeignKey(d => d.Sender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Complaint_Person");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStudent)
                    .HasName("Student_pk");

                entity.Property(e => e.IdStudent).ValueGeneratedNever();

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Person");
            });

            modelBuilder.Entity<StudentAnswer>(entity =>
            {
                entity.HasKey(e => e.IdStudentAnswer)
                    .HasName("Student_Answer_pk");

                entity.ToTable("Student_Answer");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.IdMarkNavigation)
                    .WithMany(p => p.StudentAnswer)
                    .HasForeignKey(d => d.IdMark)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Answer_Mark");

                entity.HasOne(d => d.IdTaskNavigation)
                    .WithMany(p => p.StudentAnswer)
                    .HasForeignKey(d => d.IdTask)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Answer_Task");

                entity.HasOne(d => d.IdTestForStudentNavigation)
                    .WithMany(p => p.StudentAnswer)
                    .HasForeignKey(d => d.IdTestForStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Answer_Test_For_Student");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.IdSubject)
                    .HasName("Subject_pk");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubjectCategory>(entity =>
            {
                entity.HasKey(e => e.IdSubjectCategory)
                    .HasName("Subject_Category_pk");

                entity.ToTable("Subject_Category");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.SubjectCategory)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Subject_Category_Subject");
            });

            modelBuilder.Entity<SubjectLevel>(entity =>
            {
                entity.HasKey(e => e.IdSubjectLevel)
                    .HasName("Subject_Level_pk");

                entity.ToTable("Subject_Level");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSubjectCategoryNavigation)
                    .WithMany(p => p.SubjectLevel)
                    .HasForeignKey(d => d.IdSubjectCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Subject_Level_Subject_Category");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.IdTask)
                    .HasName("Task_pk");

                entity.Property(e => e.Answer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSubjectLevelNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.IdSubjectLevel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_Subject");

                entity.HasOne(d => d.IdTaskTypeNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.IdTaskType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_Task_Type");
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.HasKey(e => e.IdTaskType)
                    .HasName("Task_Type_pk");

                entity.ToTable("Task_Type");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.IdTeacher)
                    .HasName("Teacher_pk");

                entity.Property(e => e.IdTeacher).ValueGeneratedNever();

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Teacher_Person");
            });

            modelBuilder.Entity<TeacherSalary>(entity =>
            {
                entity.ToTable("Teacher_Salary");

                entity.Property(e => e.HourlyRate).HasColumnType("numeric(7, 2)");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.TeacherSalary)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Teacher_Salary_Subject");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.TeacherSalary)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Teacher_Salary_Teacher");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.IdTest)
                    .HasName("Test_pk");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Test_Teacher");

                entity.HasMany(d => d.IdTask)
                    .WithMany(p => p.IdTest)
                    .UsingEntity<Dictionary<string, object>>(
                        "TaskOnTest",
                        l => l.HasOne<Task>().WithMany().HasForeignKey("IdTask").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Task_On_Test_Task"),
                        r => r.HasOne<Test>().WithMany().HasForeignKey("IdTest").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Task_On_Test_Test"),
                        j =>
                        {
                            j.HasKey("IdTest", "IdTask").HasName("Task_On_Test_pk");

                            j.ToTable("Task_On_Test");
                        });
            });

            modelBuilder.Entity<TestForStudent>(entity =>
            {
                entity.HasKey(e => e.IdTestForStudent)
                    .HasName("Test_For_Student_pk");

                entity.ToTable("Test_For_Student");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.TestForStudent)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Test_For_Student_Student");

                entity.HasOne(d => d.IdTestNavigation)
                    .WithMany(p => p.TestForStudent)
                    .HasForeignKey(d => d.IdTest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Test_For_Student_Test");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
}
