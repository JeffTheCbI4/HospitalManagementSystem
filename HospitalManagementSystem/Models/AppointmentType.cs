using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class AppointmentType
    {
        public AppointmentType()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int AppointmentTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
