using ML_Appointments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML_Appointments.Services
{
    public interface IManagementServices
    {
        Task<int> Postslots(Slot slot);
        Task<List<Company>> GetCompany();
        Task<List<Terminal>> GetTerminals();
        Task<List<AppointViewModel>> GetAppointments();
        Task<AppointViewModel> GetAppointmentById(int id);
        Task<int> PostAppointments(Appointment appointment);
        Task UpdateAppointment(Appointment appointment);
        Task<int> DeleteAppointment(int? Id);
    }
}
