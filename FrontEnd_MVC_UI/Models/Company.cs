using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd_MVC_UI.Models
{
    public partial class Company
    {
        public Company()
        {
            Appointments = new HashSet<Appointment>();
            Slots = new HashSet<Slot>();
            Terminals = new HashSet<Terminal>();
        }

        public int C_Id { get; set; }
        public string Client_Id { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Slot> Slots { get; set; }
        public virtual ICollection<Terminal> Terminals { get; set; }
    }
}
