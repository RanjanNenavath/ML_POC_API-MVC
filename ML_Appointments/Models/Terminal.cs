using System;
using System.Collections.Generic;

#nullable disable

namespace ML_Appointments.Models
{
    public partial class Terminal
    {
        public Terminal()
        {
            Appointments = new HashSet<Appointment>();
            Slots = new HashSet<Slot>();
        }

        public int T_Id { get; set; }
        public int? Company_Id { get; set; }
        public string TerminalName { get; set; }
        public bool IsActive { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Slot> Slots { get; set; }
    }
}
