﻿using Microsoft.EntityFrameworkCore;

namespace SystemZarzadzaniaKorepetycjami_BackEnd.Models
{
    public partial class SZKContext : DbContext
    {
        public SZKContext()
        {
        }

        public SZKContext(DbContextOptions<SZKContext> options) : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Assignment> Assignment { get; set; }
        public virtual DbSet<Availability> Availability { get; set; }
        public virtual DbSet<Ban> Ban { get; set; }
        public virtual DbSet<DayOfTheWeek> DayOfTheWeek { get; set; }
        public virtual DbSet<Lesson> Lesson { get; set; }
        public virtual DbSet<LessonStatus> LessonStatus { get; set; }
        public virtual DbSet<Mark> Mark { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Opinion> Opinion { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<ResetPassword> ResetPassword { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswer { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<SubjectCategory> SubjectCategory { get; set; }
        public virtual DbSet<SubjectLevel> SubjectLevel { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<TeacherSalary> TeacherSalary { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestForStudent> TestForStudent { get; set; }
        public virtual DbSet<TestForStudentStatus> TestForStudentStatus { get; set; }

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

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(e => e.IdAssignment)
                    .HasName("Assignment_pk");

                entity.Property(e => e.Answer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTestNavigation)
                    .WithMany(p => p.Assignment)
                    .HasForeignKey(d => d.IdTest)
                    .HasConstraintName("Assignment_Test");
            });

            modelBuilder.Entity<Availability>(entity =>
            {
                entity.HasKey(e => new { e.IdTeacher, e.IdDayOfTheWeek })
                    .HasName("Availability_pk");

                entity.Property(e => e.EndTime).HasPrecision(0);

                entity.Property(e => e.StartTime).HasPrecision(0);

                entity.HasOne(d => d.IdDayOfTheWeekNavigation)
                    .WithMany(p => p.Availability)
                    .HasForeignKey(d => d.IdDayOfTheWeek)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Calendar_Day_Of_The_Weak");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.Availability)
                    .HasForeignKey(d => d.IdTeacher)
                    .HasConstraintName("Calendar_Teacher");
            });

            modelBuilder.Entity<Ban>(entity =>
            {
                entity.HasKey(e => e.IdBan)
                    .HasName("Ban_pk");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.Ban)
                    .HasForeignKey(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ban_Person");
            });

            modelBuilder.Entity<DayOfTheWeek>(entity =>
            {
                entity.HasKey(e => e.IdDayOfTheWeek)
                    .HasName("Day_Of_The_Week_pk");

                entity.ToTable("Day_Of_The_Week");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.IdLesson)
                    .HasName("Lesson_pk");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdLessonStatusNavigation)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.IdLessonStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Lesson_Lesson_Status");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.IdStudent)
                    .HasConstraintName("Lesson_Student");

                entity.HasOne(d => d.IdSubjectLevelNavigation)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.IdSubjectLevel)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Lesson_Subject");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.Lesson)
                    .HasForeignKey(d => d.IdTeacher)
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
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdStudentAnswerNavigation)
                    .WithMany(p => p.Mark)
                    .HasForeignKey(d => d.IdStudentAnswer)
                    .HasConstraintName("Mark_Student_Answer");
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
                    .WithMany(p => p.OpinionIdStudentNavigation)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Opinion_Student");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.OpinionIdTeacherNavigation)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Opinion_Teacher");
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

                entity.Property(e => e.IsDeleted).HasDefaultValue(false);

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

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.IdJwt)
                    .HasName("Refresh_Token_pk");

                entity.ToTable("Refresh_Token");

                entity.Property(e => e.IdJwt).HasColumnName("IdJWT");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.RefreshToken)
                    .HasForeignKey(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("JWT_Person");
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

            modelBuilder.Entity<ResetPassword>(entity =>
            {
                entity.HasKey(e => e.IdResetPassword)
                    .HasName("Reset_Password_pk");

                entity.ToTable("Reset_Password");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.ResetPassword)
                    .HasForeignKey(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RessetPassword_Person");
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

                entity.HasOne(d => d.IdAssignmentNavigation)
                    .WithMany(p => p.StudentAnswer)
                    .HasForeignKey(d => d.IdAssignment)
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
                    .HasConstraintName("Subject_Level_Subject_Category");
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
                entity.HasKey(e => e.IdTeacherSalary)
                    .HasName("Teacher_Salary_pk");

                entity.ToTable("Teacher_Salary");

                entity.Property(e => e.HourlyRate).HasColumnType("numeric(7, 2)");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.TeacherSalary)
                    .HasForeignKey(d => d.IdSubject)
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

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Test_Teacher");
            });

            modelBuilder.Entity<TestForStudent>(entity =>
            {
                entity.HasKey(e => e.IdTestForStudent)
                    .HasName("Test_For_Student_pk");

                entity.ToTable("Test_For_Student");

                entity.Property(e => e.DateOfCreation).HasColumnType("datetime");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.TestForStudent)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Test_For_Student_Student");

                entity.HasOne(d => d.IdTestNavigation)
                    .WithMany(p => p.TestForStudent)
                    .HasForeignKey(d => d.IdTest)
                    .HasConstraintName("Test_For_Student_Test");

                entity.HasOne(d => d.IdTestForStudentStatusNavigation)
                    .WithMany(p => p.TestForStudent)
                    .HasForeignKey(d => d.IdTestForStudentStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Test_For_Student_Test_For_Student_Status");
            });

            modelBuilder.Entity<TestForStudentStatus>(entity =>
            {
                entity.HasKey(e => e.IdTestForStudentStatus)
                    .HasName("Test_For_Student_Status_pk");

                entity.ToTable("Test_For_Student_Status");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}