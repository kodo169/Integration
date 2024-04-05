using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Integration.Models;

public partial class MydbContext : DbContext
{
    public MydbContext()
    {
    }

    public MydbContext(DbContextOptions<MydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<PayRate> PayRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=mydb;uid=root;pwd=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.3.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeNumber, e.PayRatesIdPayRates })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("employee")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.PayRatesIdPayRates, "fk_Employee_Pay Rates_idx");

            entity.Property(e => e.EmployeeNumber).HasColumnName("Employee_Number");
            entity.Property(e => e.PayRatesIdPayRates).HasColumnName("Pay_Rates_idPay_Rates");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("First_Name");
            entity.Property(e => e.IdEmployee).HasColumnName("idEmployee");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("Last_Name");
            entity.Property(e => e.PaidLastYear)
                .HasPrecision(2)
                .HasColumnName("Paid_Last_Year");
            entity.Property(e => e.PaidToDate)
                .HasPrecision(2)
                .HasColumnName("Paid_To_Date");
            entity.Property(e => e.PayRate)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("Pay_Rate");
            entity.Property(e => e.Ssn)
                .HasPrecision(10)
                .HasColumnName("SSN");
            entity.Property(e => e.VacationDays).HasColumnName("Vacation_Days");

            entity.HasOne(d => d.PayRatesIdPayRatesNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PayRatesIdPayRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Employee_Pay Rates");
        });

        modelBuilder.Entity<PayRate>(entity =>
        {
            entity.HasKey(e => e.IdPayRates).HasName("PRIMARY");

            entity
                .ToTable("pay_rates")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.IdPayRates)
                .ValueGeneratedNever()
                .HasColumnName("idPay_Rates");
            entity.Property(e => e.PayAmount)
                .HasPrecision(10)
                .HasColumnName("Pay_Amount");
            entity.Property(e => e.PayRateName)
                .HasMaxLength(45)
                .IsFixedLength()
                .HasColumnName("Pay_Rate_Name");
            entity.Property(e => e.PayType).HasColumnName("Pay_Type");
            entity.Property(e => e.PtLevelC)
                .HasPrecision(10)
                .HasColumnName("PT_Level_C");
            entity.Property(e => e.TaxPercentage)
                .HasPrecision(10)
                .HasColumnName("Tax_Percentage");
            entity.Property(e => e.Value).HasPrecision(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
