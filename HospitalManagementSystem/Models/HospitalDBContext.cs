using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HospitalManagementSystem.Models
{
    public partial class HospitalDBContext : DbContext
    {
        public HospitalDBContext()
        {
        }

        public HospitalDBContext(DbContextOptions<HospitalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<AppointmentType> AppointmentTypes { get; set; } = null!;
        public virtual DbSet<Medicine> Medicines { get; set; } = null!;
        public virtual DbSet<Prescription> Prescriptions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomOccupation> RoomOccupations { get; set; } = null!;
        public virtual DbSet<RoomType> RoomTypes { get; set; } = null!;
        public virtual DbSet<Specialty> Specialties { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UsersRole> UsersRoles { get; set; } = null!;
        public virtual DbSet<UsersSpec> UsersSpecs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["ConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.AppointmentTypeId).HasColumnName("appointment_type_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.DiagnosisDescription)
                    .HasMaxLength(500)
                    .HasColumnName("diagnosis_description");

                entity.Property(e => e.DoctorUserId).HasColumnName("doctor_user_id");

                entity.Property(e => e.PatientUserId).HasColumnName("patient_user_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.HasOne(d => d.AppointmentType)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AppointmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Appointment_Type");

                entity.HasOne(d => d.DoctorUser)
                    .WithMany(p => p.AppointmentDoctorUsers)
                    .HasForeignKey(d => d.DoctorUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Doctor_User");

                entity.HasOne(d => d.PatientUser)
                    .WithMany(p => p.AppointmentPatientUsers)
                    .HasForeignKey(d => d.PatientUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Patient_User");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Room");
            });

            modelBuilder.Entity<AppointmentType>(entity =>
            {
                entity.ToTable("Appointment_Type");

                entity.HasIndex(e => e.Name, "UK_Appointment_Type_name")
                    .IsUnique();

                entity.Property(e => e.AppointmentTypeId).HasColumnName("appointment_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.ToTable("Medicine");

                entity.HasIndex(e => e.Name, "UK_Medicine_name")
                    .IsUnique();

                entity.Property(e => e.MedicineId).HasColumnName("medicine_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("Prescription");

                entity.Property(e => e.PrescriptionId).HasColumnName("prescription_id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.MedicineId).HasColumnName("medicine_id");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prescription_Appointment");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prescription_Medicine");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.Name, "UK_name")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.HasIndex(e => e.Name, "UK_Room_name")
                    .IsUnique();

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.RoomTypeId).HasColumnName("room_type_id");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Room_Type");
            });

            modelBuilder.Entity<RoomOccupation>(entity =>
            {
                entity.HasKey(e => e.RoomOccupation1);

                entity.ToTable("Room_Occupation");

                entity.Property(e => e.RoomOccupation1).HasColumnName("room_occupation");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomOccupations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Occupation_Room");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoomOccupations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Occupation_User");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("Room_Type");

                entity.HasIndex(e => e.Name, "UK_Room_Type_name")
                    .IsUnique();

                entity.Property(e => e.RoomTypeId).HasColumnName("room_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.ToTable("Specialty");

                entity.HasIndex(e => e.Name, "UK_Specialty_name")
                    .IsUnique();

                entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Login, "UK_login")
                    .IsUnique();

                entity.HasIndex(e => e.PasswordSalt, "UK_password_salt")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("lastName");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(100)
                    .HasColumnName("password_hash");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(50)
                    .HasColumnName("password_salt");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<UsersRole>(entity =>
            {
                entity.HasKey(e => e.UsersRolesId);

                entity.ToTable("Users_Roles");

                entity.Property(e => e.UsersRolesId).HasColumnName("users_roles_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UsersRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles_User");
            });

            modelBuilder.Entity<UsersSpec>(entity =>
            {
                entity.HasKey(e => e.UsersSpecsId);

                entity.ToTable("Users_Specs");

                entity.Property(e => e.UsersSpecsId).HasColumnName("users_specs_id");

                entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.UsersSpecs)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Specs_Specialty");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersSpecs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Specs_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
