using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GameOfDrones.API.Models
{
    public partial class game_of_dronesContext : DbContext
    {
        public game_of_dronesContext()
        {
        }

        public game_of_dronesContext(DbContextOptions<game_of_dronesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<AuditAction> AuditAction { get; set; }
        public virtual DbSet<GameStatistics> GameStatistics { get; set; }
        public virtual DbSet<User> User { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseSqlServer("Server=gameofdronesuruit.database.windows.net,1433;Initial Catalog=game_of_drones;Persist Security Info=False;User ID=javiseeker;Password=Javier.123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.HasKey(e => e.AUid)
                    .HasName("PK__Audit__C3F6E1B2C6144EB2");

                entity.ToTable("Audit", "game_of_drones");

                entity.Property(e => e.AUid).HasColumnName("A_UID");

                entity.Property(e => e.AAaUid).HasColumnName("A_AA_UID");

                entity.Property(e => e.ADateCreated)
                    .HasColumnName("A_Date_Created")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ADescription)
                    .IsRequired()
                    .HasColumnName("A_Description")
                    .IsUnicode(false);

                entity.HasOne(d => d.AAaU)
                    .WithMany(p => p.Audit)
                    .HasForeignKey(d => d.AAaUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_audit_auditactions");
            });

            modelBuilder.Entity<AuditAction>(entity =>
            {
                entity.HasKey(e => e.AaUid)
                    .HasName("PK__Audit_Ac__5D263AD3047FAD54");

                entity.ToTable("Audit_Action", "game_of_drones");

                entity.Property(e => e.AaUid).HasColumnName("AA_UID");

                entity.Property(e => e.AaName)
                    .IsRequired()
                    .HasColumnName("AA_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GameStatistics>(entity =>
            {
                entity.HasKey(e => e.GsUid)
                    .HasName("PK__Game_Sta__08792841D03EF001");

                entity.ToTable("Game_Statistics", "game_of_drones");

                entity.Property(e => e.GsUid).HasColumnName("GS_UID");

                entity.Property(e => e.GsScore).HasColumnName("GS_Score");

                entity.Property(e => e.GsUUid).HasColumnName("GS_U_UID");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UUid)
                    .HasName("PK__User__058630DF23F3FFF2");

                entity.ToTable("User", "game_of_drones");

                entity.Property(e => e.UUid).HasColumnName("U_UID");

                entity.Property(e => e.UName)
                    .IsRequired()
                    .HasColumnName("U_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
