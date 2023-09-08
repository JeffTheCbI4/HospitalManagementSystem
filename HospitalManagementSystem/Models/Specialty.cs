using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            UsersSpecs = new HashSet<UsersSpec>();
        }

        public int SpecialtyId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<UsersSpec> UsersSpecs { get; set; }
    }
}
