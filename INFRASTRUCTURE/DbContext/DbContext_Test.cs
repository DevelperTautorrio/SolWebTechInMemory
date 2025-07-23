using Microsoft.EntityFrameworkCore;
using DOMAIN.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INFRASTRUCTURE
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<E_User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("AppInMemoryDB");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<E_User>(entity =>
            {
                
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.UserID)
                      .IsRequired()
                      .ValueGeneratedOnAdd(); 

                
                entity.Property(e => e.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.PaternalSurname)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.MaternalSurname)
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.Nickname)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.ProfilePicture)
                      .HasMaxLength(500);

                entity.Property(e => e.RecoveryToken)
                      .HasMaxLength(255);

                entity.Property(e => e.Phone)
                      .HasMaxLength(20);

                entity.Property(e => e.Biography)
                      .HasMaxLength(1000);

                entity.HasIndex(e => e.Email)
                      .IsUnique();

                entity.HasIndex(e => e.Nickname)
                      .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}