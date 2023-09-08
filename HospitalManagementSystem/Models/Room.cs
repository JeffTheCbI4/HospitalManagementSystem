using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class Room
    {
        public Room()
        {
            Appointments = new HashSet<Appointment>();
            RoomOccupations = new HashSet<RoomOccupation>();
        }

        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }

        public virtual RoomType RoomType { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<RoomOccupation> RoomOccupations { get; set; }
    }
}
