using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Prescription
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public int AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; } = null!;
        public virtual Medicine Medicine { get; set; } = null!;
    }
}
