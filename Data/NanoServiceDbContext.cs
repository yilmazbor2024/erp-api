using System.Reflection;
using ErpMobile.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpMobile.Api.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ErpMobile.Api.Data;

public class NanoServiceDbContext : IdentityDbContext<User, Role, string>
{
    public NanoServiceDbContext(DbContextOptions<NanoServiceDbContext> options) : base(options)
    {
    }

    public DbSet<AuditLog> AuditLogs { get; set; } = null!;
    public DbSet<MenuItem> MenuItems { get; set; } = null!;
    public DbSet<TempCustomerToken> TempCustomerTokens { get; set; } = null!;
    public DbSet<UserGroup> UserGroups { get; set; } = null!;
    public DbSet<ModulePermission> ModulePermissions { get; set; } = null!;
    public DbSet<Database> Databases { get; set; } = null!;
    public DbSet<UserDatabase> UserDatabases { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Identity tablolarını dbo şemasına taşı
        builder.Entity<User>().ToTable("AspNetUsers", "dbo");
        builder.Entity<Role>().ToTable("AspNetRoles", "dbo");
        builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles", "dbo");
        builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims", "dbo");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims", "dbo");
        builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins", "dbo");
        builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens", "dbo");

        // MenuItem konfigürasyonu
        builder.Entity<MenuItem>(entity =>
        {
            entity.ToTable("MenuItems", "dbo");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
                
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.Icon)
                .HasMaxLength(50);
                
            entity.Property(e => e.Url)
                .HasMaxLength(200);
        });

        // AuditLog konfigürasyonu
        builder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("AuditLogs", "dbo");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.TableName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.OldValues)
                .HasColumnType("ntext");

            entity.Property(e => e.NewValues)
                .HasColumnType("ntext");

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });

        // UserGroup konfigürasyonu
        builder.Entity<UserGroup>(entity =>
        {
            entity.ToTable("UserGroups", "dbo");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);
        });

        // ModulePermission konfigürasyonu
        builder.Entity<ModulePermission>(entity =>
        {
            entity.ToTable("ModulePermissions", "dbo");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.ModuleName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.UserGroup)
                .WithMany(p => p.ModulePermissions)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Database konfigürasyonu
        builder.Entity<Database>(entity =>
        {
            entity.ToTable("Database", "dbo");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.DatabaseName)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(e => e.ConnectionString)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);
        });

        // UserDatabase konfigürasyonu
        builder.Entity<UserDatabase>(entity =>
        {
            entity.ToTable("UserDatabase", "dbo");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Database)
                .WithMany(p => p.UserDatabases)
                .HasForeignKey(d => d.DatabaseId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
