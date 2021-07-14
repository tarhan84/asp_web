using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SmartSense
{
    public partial class SmartSenseDB : DbContext
    {
        public SmartSenseDB()
        {
        }

        public SmartSenseDB(DbContextOptions<SmartSenseDB> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<ContentResim> ContentResims { get; set; }
        public virtual DbSet<Iletisim> Iletisims { get; set; }
        public virtual DbSet<Mesajlar> Mesajlars { get; set; }
        public virtual DbSet<SliderResim> SliderResims { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-T0CN7DN\\SQLEXPRESS;Initial Catalog=smartsense;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Ad).HasColumnName("ad");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Soyad).HasColumnName("soyad");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.ToTable("content");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Baslik)
                    .HasMaxLength(10)
                    .HasColumnName("baslik")
                    .IsFixedLength(true);

                entity.Property(e => e.Icerik)
                    .HasMaxLength(10)
                    .HasColumnName("icerik")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ContentResim>(entity =>
            {
                entity.ToTable("contentResim");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ContentId).HasColumnName("contentID");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("path");

                entity.HasOne(d => d.Content)
                    .WithMany(p => p.ContentResims)
                    .HasForeignKey(d => d.ContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_contentResim_content");
            });

            modelBuilder.Entity<Iletisim>(entity =>
            {
                entity.ToTable("iletisim");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("adres");

                entity.Property(e => e.Eposta)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("eposta");

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("telefon");
            });

            modelBuilder.Entity<Mesajlar>(entity =>
            {
                entity.ToTable("mesajlar");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("email");

                entity.Property(e => e.Konu)
                    .HasColumnType("ntext")
                    .HasColumnName("konu");

                entity.Property(e => e.Mesaj)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("mesaj");

                entity.Property(e => e.Okundu).HasColumnName("okundu");

                entity.Property(e => e.Tarih)
                    .HasColumnType("ntext")
                    .HasColumnName("tarih");
            });

            modelBuilder.Entity<SliderResim>(entity =>
            {
                entity.ToTable("sliderResim");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("path");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
