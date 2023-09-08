using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class RoomOccupation
    {
        public int RoomOccupation1 { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
