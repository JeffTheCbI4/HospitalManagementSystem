using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        public int AppointmentId { get; set; }
        public int AppointmentTypeId { get; set; }
        public int DoctorUserId { get; set; }
        public int PatientUserId { get; set; }
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public string DiagnosisDescription { get; set; } = null!;

        public virtual AppointmentType AppointmentType { get; set; } = null!;
        public virtual User DoctorUser { get; set; } = null!;
        public virtual User PatientUser { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
