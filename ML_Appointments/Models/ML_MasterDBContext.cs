using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ML_Appointments.Models
{
    public partial class ML_MasterDBContext : DbContext
    {
        public ML_MasterDBContext()
        {
        }

        public ML_MasterDBContext(DbContextOptions<ML_MasterDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }
        public virtual DbSet<Terminal> Terminals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:api-ml-poc-microservices.database.windows.net,1433;Initial Catalog=ML_MasterDB;Persist Security Info=False;User ID=ML-microservices;Password=Prem@1997;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Appoint_Id);

                entity.ToTable("Appointment", "A00");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Company_Id)
                    .HasConstraintName("FK_Appointment_Company");

                entity.HasOne(d => d.Terminal)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.Terminal_Id)
                    .HasConstraintName("FK_Appointment_Terminal");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.C_Id);

                entity.ToTable("Company", "A00");

                entity.Property(e => e.Client_Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.HasKey(e => e.S_Id);

                entity.ToTable("Slots", "A00");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.Company_Id)
                    .HasConstraintName("FK_Slots_Company");

                entity.HasOne(d => d.Terminal)
                    .WithMany(p => p.Slots)
                    .HasForeignKey(d => d.Terminal_Id)
                    .HasConstraintName("FK_Slots_Terminal");
            });

            modelBuilder.Entity<Terminal>(entity =>
            {
                entity.HasKey(e => e.T_Id);

                entity.ToTable("Terminal", "A00");

                entity.Property(e => e.TerminalName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Terminals)
                    .HasForeignKey(d => d.Company_Id)
                    .HasConstraintName("FK_Terminal_Company");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
