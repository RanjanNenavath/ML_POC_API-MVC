using System;
using System.Collections.Generic;

#nullable disable

namespace ML_Appointments.Models
{
    public partial class Slot
    {
        public int S_Id { get; set; }
        public int? Terminal_Id { get; set; }
        public int? Company_Id { get; set; }
        public DateTime? Date { get; set; }
        public int? SlotAvailble { get; set; }
        public bool IsActive { get; set; }

        public virtual Company Company { get; set; }
        public virtual Terminal Terminal { get; set; }
    }
}
