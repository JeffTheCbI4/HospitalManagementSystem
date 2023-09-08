using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Role
    {
        public Role()
        {
            UsersRoles = new HashSet<UsersRole>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<UsersRole> UsersRoles { get; set; }
    }
}
