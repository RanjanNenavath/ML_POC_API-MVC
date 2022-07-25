using Azure.Messaging.ServiceBus;
using FrontEnd_MVC_UI.Helpers;
using FrontEnd_MVC_UI.Models;
using FrontEnd_MVC_UI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FrontEnd_MVC_UI.Controllers
{
    public class TerminalSlotsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IQueueReciverServices _queueReciverServices;

        public TerminalSlotsController(HttpClient httpClient, IConfiguration configuration, IQueueReciverServices queueReciverServices)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
            this._queueReciverServices = queueReciverServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookSlots(Slot slot)
        {
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(slot), Encoding.UTF8, ConstString.StringContect);
                using (HttpResponseMessage response = _httpClient.PostAsync(ApiBaseUrl.PostSlotBookiingApi(_configuration), content).Result)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
                return Redirect(nameof(SlotConfirmation));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.StackTrace });
            }
            finally
            {
            }
        }

        public async Task<IActionResult> SlotConfirmation()
        {
            var data = await _queueReciverServices.recivemessage();
            TempData["message"] = data;
            return View();
        }
       
    }
}
