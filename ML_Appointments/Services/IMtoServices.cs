using ML_Appointments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML_Appointments.Services
{
    public interface IMtoServices
    {
        Task AddCompanyDataAsync(Mto mto);
        Task<IList<Mto>> GetAsyncQuery(string userName, string password);
        Task UpdateAsync(string id, Mto mto);
        Task<Mto> GetAsync(string id);
        Task DeleteAsync(string id);
    }
}
