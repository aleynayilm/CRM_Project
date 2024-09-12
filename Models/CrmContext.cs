using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRMV2.Models;

public partial class CrmContext : DbContext
{
    public CrmContext()
    {
    }

    public CrmContext(DbContextOptions<CrmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<ContactPerson> ContactPeople { get; set; }

    public virtual DbSet<ContactsType> ContactsTypes { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-CPS5QP0;Initial Catalog=CRM;User ID=sa;Password=9984;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Adress).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<ContactPerson>(entity =>
        {
            entity.ToTable("ContactPerson");

            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.LastName).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.Contract).WithMany(p => p.ContactPeople)
                .HasForeignKey(d => d.ContractId)
                .HasConstraintName("FK_ContactPerson_Contracts");
        });

        modelBuilder.Entity<ContactsType>(entity =>
        {
            entity.ToTable("ContactsType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.Property(e => e.BeginDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(250);

            entity.HasOne(d => d.Company).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contracts_Companies");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK_Contracts_ContactsType");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Log__3214EC07A51F00C1");

            entity.ToTable("Log");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.Logger).HasMaxLength(255);
            entity.Property(e => e.Thread).HasMaxLength(255);
            modelBuilder.Entity<Log>()
            .HasOne(log => log.User)
            .WithMany()
            .HasForeignKey(log => log.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
