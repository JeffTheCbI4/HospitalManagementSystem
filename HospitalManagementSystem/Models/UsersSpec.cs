using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class UsersSpec
    {
        public int UsersSpecsId { get; set; }
        public int UserId { get; set; }
        public int SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
