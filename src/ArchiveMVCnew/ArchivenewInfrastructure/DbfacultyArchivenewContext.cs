using System;
using System.Collections.Generic;
using ArchivenewDomain.Model;
using Microsoft.EntityFrameworkCore;

//namespace ArchivenewDomain.Model;
namespace ArchivenewInfrastructure;

public partial class DbfacultyArchivenewContext : DbContext
{
    public DbfacultyArchivenewContext()
    {
    }

    public DbfacultyArchivenewContext(DbContextOptions<DbfacultyArchivenewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Date> Dates { get; set; }

    public virtual DbSet<DateReference> DateReferences { get; set; }

    public virtual DbSet<Reference> References { get; set; }

    public virtual DbSet<SearchHistory> SearchHistories { get; set; }

    public virtual DbSet<SearchObject> SearchObjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserReference> UserReferences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=Denis\\SQLEXPRESS; Database=DBFacultyArchivenew; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Date>(entity =>
        {
            entity.ToTable("Date");

            entity.Property(e => e.DateId)
                .HasColumnName("date_id");
            entity.Property(e => e.Date1).HasColumnName("date");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.ExtentOfMaterial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("extent_of_material");
            entity.Property(e => e.Faculty)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("faculty");
            entity.Property(e => e.Format)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("format");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<DateReference>(entity =>
        {
            entity.HasKey(e => e.DaterefId);

            entity.ToTable("DateReference");

            entity.Property(e => e.DaterefId).HasColumnName("dateref_id");
            entity.Property(e => e.DateId).HasColumnName("date_id");
            entity.Property(e => e.ReferenceId).HasColumnName("reference_id");

            entity.HasOne(d => d.Date).WithMany(p => p.DateReferences)
                .HasForeignKey(d => d.DateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DateReference_Date");

            entity.HasOne(d => d.Reference).WithMany(p => p.DateReferences)
                .HasForeignKey(d => d.ReferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DateReference_Reference");
        });

        modelBuilder.Entity<Reference>(entity =>
        {
            entity.ToTable("Reference");

            entity.Property(e => e.ReferenceId)
                .ValueGeneratedNever()
                .HasColumnName("reference_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Searchable)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("searchable");
            entity.Property(e => e.SoId)
                .ValueGeneratedOnAdd()
                .HasColumnName("so_id");
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("SearchHistory");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.SearchDate).HasColumnName("search_date");
            entity.Property(e => e.SearchSuccess).HasColumnName("search_success");
            entity.Property(e => e.SoId).HasColumnName("so_id");

            entity.HasOne(d => d.So).WithMany(p => p.SearchHistories)
                .HasForeignKey(d => d.SoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SearchHistory_SearchObject");
        });

        modelBuilder.Entity<SearchObject>(entity =>
        {
            entity.HasKey(e => e.SoId);

            entity.ToTable("SearchObject");

            entity.Property(e => e.SoId).HasColumnName("so_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Faculty)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("faculty");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.ReferenceId).HasColumnName("reference_id");
            entity.Property(e => e.SearchTime).HasColumnName("search_time");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Reference).WithMany(p => p.SearchObjects)
                .HasForeignKey(d => d.ReferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SearchObject_Reference");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");

            entity.HasOne(d => d.UserNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_SearchHistory");
        });

        modelBuilder.Entity<UserReference>(entity =>
        {
            entity.HasKey(e => e.UserrefId);

            entity.ToTable("UserReference");

            entity.Property(e => e.UserrefId).HasColumnName("userref_id");
            entity.Property(e => e.ReferenceId).HasColumnName("reference_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Reference).WithMany(p => p.UserReferences)
                .HasForeignKey(d => d.ReferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserReference_Reference");

            entity.HasOne(d => d.User).WithMany(p => p.UserReferences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserReference_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
