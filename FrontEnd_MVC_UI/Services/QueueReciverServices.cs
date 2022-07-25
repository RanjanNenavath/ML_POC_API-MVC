using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FrontEnd_MVC_UI.Services
{
    public class QueueReciverServices : IQueueReciverServices
    {
        private readonly IConfiguration _config;
        public readonly static IQueueClient queueClient;
        const string queueName = "SlotQueue";
        public QueueReciverServices(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> recivemessage()
        {
            await using var client = new ServiceBusClient(_config.GetConnectionString("AzureServiceBus"));
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);
            ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();
            string body = receivedMessage.Body.ToString();
            return body;
        }
    }
}
