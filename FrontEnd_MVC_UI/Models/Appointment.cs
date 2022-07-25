using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FrontEnd_MVC_UI.Models
{
    public partial class Appointment
    {
        public int Appoint_Id { get; set; }
        public int? Terminal_Id { get; set; }
        public int? Company_Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Slots { get; set; }
        public long? SlotsRefrenceNo { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string TerminalName { get; set; }
        [JsonIgnore]
        public virtual Company Company { get; set; }
        [JsonIgnore]
        public virtual Terminal Terminal { get; set; }
    }
}
