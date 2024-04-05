using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Data;

public partial class HrContext : DbContext
{
    public HrContext(){}
    public HrContext(DbContextOptions<HrContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BenefitPlan> BenefitPlans { get; set; }

    public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }

    public virtual DbSet<Employment> Employments { get; set; }

    public virtual DbSet<JobHistory> JobHistories { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KODO\\SQLEXPRESS;Initial Catalog=HR;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BenefitPlan>(entity =>
        {
            entity.HasKey(e => e.BenefitPlanId).HasName("PK_dbo.Benefit_Plans");

            entity.ToTable("Benefit_Plans");

            entity.Property(e => e.BenefitPlanId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Benefit_Plan_ID");
            entity.Property(e => e.Deductable).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.PercentageCoPay).HasColumnName("Percentage_CoPay");
            entity.Property(e => e.PlanName)
                .HasMaxLength(50)
                .HasColumnName("Plan_Name");
        });

        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_dbo.Emergency_Contacts");

            entity.ToTable("Emergency_Contacts");

            entity.HasIndex(e => e.EmployeeId, "IX_Employee_ID");

            entity.Property(e => e.EmployeeId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Employee_ID");
            entity.Property(e => e.EmergencyContactName)
                .HasMaxLength(50)
                .HasColumnName("Emergency_Contact_Name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.Relationship).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithOne(p => p.EmergencyContact)
                .HasForeignKey<EmergencyContact>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.Emergency_Contacts_dbo.Personal_Employee_ID");
        });

        modelBuilder.Entity<Employment>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_dbo.Employment");

            entity.ToTable("Employment");

            entity.HasIndex(e => e.EmployeeId, "IX_Employee_ID");

            entity.Property(e => e.EmployeeId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Employee_ID");
            entity.Property(e => e.EmploymentStatus)
                .HasMaxLength(50)
                .HasColumnName("Employment_Status");
            entity.Property(e => e.HireDate)
                .HasColumnType("datetime")
                .HasColumnName("Hire_Date");
            entity.Property(e => e.LastReviewDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Review_Date");
            entity.Property(e => e.RehireDate)
                .HasColumnType("datetime")
                .HasColumnName("Rehire_Date");
            entity.Property(e => e.TerminationDate)
                .HasColumnType("datetime")
                .HasColumnName("Termination_Date");
            entity.Property(e => e.WorkersCompCode)
                .HasMaxLength(50)
                .HasColumnName("Workers_Comp_Code");

            entity.HasOne(d => d.Employee).WithOne(p => p.Employment)
                .HasForeignKey<Employment>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.Employment_dbo.Personal_Employee_ID");
        });

        modelBuilder.Entity<JobHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Job_History");

            entity.ToTable("Job_History");

            entity.HasIndex(e => e.EmployeeId, "IX_Employee_ID");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DepartmenCode)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Departmen_Code");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.Division).HasMaxLength(50);
            entity.Property(e => e.EmployeeId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Employee_ID");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");
            entity.Property(e => e.HazardousTraining).HasColumnName("Hazardous_Training");
            entity.Property(e => e.HoursPerWeek)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Hours_per_Week");
            entity.Property(e => e.JobCategory)
                .HasMaxLength(50)
                .HasColumnName("Job_Category");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .HasColumnName("Job_Title");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.PayPeriod)
                .HasMaxLength(50)
                .HasColumnName("Pay_Period");
            entity.Property(e => e.SalaryType)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Salary_Type");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");
            entity.Property(e => e.Supervisor).HasColumnType("numeric(18, 0)");

            entity.HasOne(d => d.Employee).WithMany(p => p.JobHistories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.Job_History_dbo.Personal_Employee_ID");
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

            entity.ToTable("__MigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ContextKey).HasMaxLength(300);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_dbo.Personal");

            entity.ToTable("Personal");

            entity.HasIndex(e => e.BenefitPlans, "IX_Benefit_Plans");

            entity.Property(e => e.EmployeeId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Employee_ID");
            entity.Property(e => e.Address1).HasMaxLength(50);
            entity.Property(e => e.Address2).HasMaxLength(50);
            entity.Property(e => e.BenefitPlans)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Benefit_Plans");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.DriversLicense)
                .HasMaxLength(50)
                .HasColumnName("Drivers_License");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Ethnicity).HasMaxLength(50);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.MiddleInitial)
                .HasMaxLength(50)
                .HasColumnName("Middle_Initial");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.ShareholderStatus).HasColumnName("Shareholder_Status");
            entity.Property(e => e.SocialSecurityNumber)
                .HasMaxLength(50)
                .HasColumnName("Social_Security_Number");
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.Zip).HasColumnType("numeric(18, 0)");

            entity.HasOne(d => d.BenefitPlansNavigation).WithMany(p => p.Personals)
                .HasForeignKey(d => d.BenefitPlans)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_dbo.Personal_dbo.Benefit_Plans_Benefit_Plans");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
