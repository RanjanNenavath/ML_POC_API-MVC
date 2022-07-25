using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ML_Appointments.Services
{
    public class DapperDbContext : IDapperDbContext
    {
        private readonly IConfiguration config;

        public DapperDbContext(IConfiguration config)
        {
            this.config = config;
        }
        public IDbConnection Connection
        {
            get => new SqlConnection(config.GetConnectionString("AzureConnection"));
        }
        public void Dispose()
        {
            this.Connection.Close();
        }
    }
}
