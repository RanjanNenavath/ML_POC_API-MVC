using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ML_Appointments.Services
{
    public interface IDapperDbContext : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
