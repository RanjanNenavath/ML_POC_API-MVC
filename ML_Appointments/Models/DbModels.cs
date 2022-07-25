using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ML_Appointments.Models
{
    //public class Mto
    //{
    //    public int Id { get; set; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public bool IsActive { get; set; }
    //}
    public class Mto
    {
        [JsonProperty(PropertyName = "id")]
        public string M_Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        //  public List <Termainal> termainals { get; set; }
    }
    //public class Termainal
    //{
    //    public int T_Id { get; set; }
    //    public string TermainalName { get; set; }
    //    public bool IsActive { get; set; }
    //}
    //public class Appointments
    //{
    //    public int Appoint_Id { get; set; }
    //    public DateTime AppointmentDate { get; set; }
    //    public int SLots { get; set; }
    //    public bool IsActive { get; set; }

    //}
    //public class SlotsCheck
    //{
    //    public int Slot_Id { get; set; }
    //    public DateTime SDate { get; set; }
    //    public int SNumber { get; set; }
    //    public bool IsActive { get; set; }

    //}
}
