using Dapper;
using Microsoft.EntityFrameworkCore;
using ML_Appointments.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ML_Appointments.Services
{
    public class ManagementServices : IManagementServices
    {
        private readonly ML_MasterDBContext _mL_MasterDBContext;
        private readonly IQueueService _queueService;
        public const string queueName = "SlotQueue";
        private readonly IDapperDbContext _dappercontext;

        public ManagementServices(ML_MasterDBContext mL_MasterDBContext, IQueueService queueService, IDapperDbContext dapperDbContext)
        {
            this._mL_MasterDBContext = mL_MasterDBContext;
            this._queueService = queueService;
            this._dappercontext = dapperDbContext;
        }
        public async Task<int> Postslots(Slot slot)
        {
            var c_Slot = _mL_MasterDBContext.Companies.FromSqlRaw($"select * from A00.Company (NoLock) where C_Id='{slot.Company_Id}' ").FirstOrDefault();
            var t_slot = _mL_MasterDBContext.Terminals.FromSqlRaw($"select * from A00.Terminal (NoLock) where T_Id='{slot.Terminal_Id}' ").FirstOrDefault();
            var slots = new Slot()
            {
                S_Id = slot.S_Id,
                Company_Id = Convert.ToInt32(c_Slot.C_Id),
                Terminal_Id = Convert.ToInt32(t_slot.T_Id),
                Date = slot.Date,
                SlotAvailble = slot.SlotAvailble,
                IsActive = true
            };
            await _mL_MasterDBContext.Slots.AddAsync(slots);
            await _mL_MasterDBContext.SaveChangesAsync();
            //sending availablity slots
            if (slots.SlotAvailble != null)
            {
                string message = $"The Number of slots available in {c_Slot.CompanyName} Company in {t_slot.TerminalName} Terminal - Dated {slots.Date} available Slots are : {slots.SlotAvailble}.";
                await _queueService.SendMessageAsync(message, queueName);
            }
            return slots.S_Id;
        }
        public async Task<List<Company>> GetCompany()
        {
            if (_mL_MasterDBContext != null)
            {
                var com = await _mL_MasterDBContext.Companies
                    //.Include(e => e.Addresses)
                    //.Include(e => e.Contacts)
                    .ToListAsync();
                return com;
            }
            else
                return null;
        }

        public async Task<List<Terminal>> GetTerminals()
        {
            if (_mL_MasterDBContext != null)
            {
                return await _mL_MasterDBContext.Terminals.ToListAsync();
            }
            else
                return null;
        }

        //using dapper we can create a view model to get the value from another table we cant get it from EF so we go for dapper.
        public async Task<List<AppointViewModel>> GetAppointments()
        {
            using (IDbConnection conn = _dappercontext.Connection)
            {
                var query = $"select a.*,c.CompanyName,t.TerminalName from [A00].[Appointment] (NoLock) as a join [A00].[Company] (NoLock) as c on a.Company_Id=c.C_Id join [A00].[Terminal] (NoLock) as t on a.Terminal_Id=t.T_Id";
                var result = await conn.QueryAsync<AppointViewModel>(query);
                return result.ToList();
               // return await _mL_MasterDBContext.Appointments.FromSqlRaw(query).ToListAsync();
            }
        
        }
        public async Task<AppointViewModel> GetAppointmentById(int id)
        {
            using (IDbConnection conn = _dappercontext.Connection)
            {
                var query = $"select a.*,c.CompanyName,t.TerminalName from [A00].[Appointment](NoLock) as a join [A00].[Company] (NoLock) as c on a.Company_Id=c.C_Id join [A00].[Terminal] (NoLock) as t on a.Terminal_Id=t.T_Id where a.Appoint_Id={id}";
                var result = conn.QueryAsync<AppointViewModel>(query).GetAwaiter().GetResult().FirstOrDefault();
                return result;
            }
        }

        public async Task<int> PostAppointments(Appointment appointment)
        {
            var c_App = _mL_MasterDBContext.Companies.FromSqlRaw($"select * from A00.Company (NoLock) where C_Id='{appointment.Company_Id}' ").FirstOrDefault();
            var t_App = _mL_MasterDBContext.Terminals.FromSqlRaw($"select * from A00.Terminal (NoLock) where T_Id='{appointment.Terminal_Id}' ").FirstOrDefault();
            var app = new Appointment()
            {
                Appoint_Id = appointment.Appoint_Id,
                Company_Id = c_App.C_Id,
                Terminal_Id = t_App.T_Id,
                FromDate = appointment.FromDate,
                ToDate=appointment.ToDate,
                Slots=appointment.Slots,
                SlotsRefrenceNo=appointment.SlotsRefrenceNo,
                IsActive = true
            };
            await _mL_MasterDBContext.Appointments.AddAsync(app);
            await _mL_MasterDBContext.SaveChangesAsync();

            return app.Appoint_Id;
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            if (_mL_MasterDBContext != null)
            {
                _mL_MasterDBContext.Appointments.Update(appointment);
                await _mL_MasterDBContext.SaveChangesAsync();
            }
        }
        public async Task<int> DeleteAppointment(int? Id)
        {
            int result = 0;

            if (_mL_MasterDBContext != null)
            {
                //Find the post for specific post id
                var add = await _mL_MasterDBContext.Appointments.FirstOrDefaultAsync(x => x.Appoint_Id == Id);
                if (add != null)
                {
                    _mL_MasterDBContext.Appointments.Remove(add);

                    result = await _mL_MasterDBContext.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }
    }
}
