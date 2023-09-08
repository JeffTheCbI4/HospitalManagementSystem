using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models
{
    public partial class RoomType
    {
        public RoomType()
        {
            Rooms = new HashSet<Room>();
        }

        public int RoomTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
