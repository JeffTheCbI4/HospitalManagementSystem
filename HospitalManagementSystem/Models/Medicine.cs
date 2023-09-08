using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        public int MedicineId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
