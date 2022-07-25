using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML_Appointments.Services
{
    public interface IQueueService
    {
        Task SendMessageAsync<T>(T serviceBusMessage, string queueName);
    }
}
