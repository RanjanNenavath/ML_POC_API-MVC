using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace ML_Appointments.Models
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
        //[NotMapped]
        //public string CompanyName { get; set; }
        //[NotMapped]
        //public string TerminalName { get; set; }
        [JsonIgnore]
        public virtual Company Company { get; set; }
        [JsonIgnore]
        public virtual Terminal Terminal { get; set; }
    }

    public class AppointViewModel
    {
        public int Appoint_Id { get; set; }
        public int? Terminal_Id { get; set; }
        public int? Company_Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FromDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }
        public int? Slots { get; set; }
        public long? SlotsRefrenceNo { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string TerminalName { get; set; }
    }
}
