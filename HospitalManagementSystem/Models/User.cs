using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class User
    {
        public User()
        {
            AppointmentDoctorUsers = new HashSet<Appointment>();
            AppointmentPatientUsers = new HashSet<Appointment>();
            RoomOccupations = new HashSet<RoomOccupation>();
            UsersRoles = new HashSet<UsersRole>();
            UsersSpecs = new HashSet<UsersSpec>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;

        public virtual ICollection<Appointment> AppointmentDoctorUsers { get; set; }
        public virtual ICollection<Appointment> AppointmentPatientUsers { get; set; }
        public virtual ICollection<RoomOccupation> RoomOccupations { get; set; }
        public virtual ICollection<UsersRole> UsersRoles { get; set; }
        public virtual ICollection<UsersSpec> UsersSpecs { get; set; }
    }
}
