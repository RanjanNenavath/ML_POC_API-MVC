using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FrontEnd_MVC_UI.Models;
using FrontEnd_MVC_UI.Helpers;
using Microsoft.Extensions.Configuration;

namespace FrontEnd_MVC_UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        // Post: LoginController/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginUsers user)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, ConstString.StringContect);
                    using (var response = await httpClient.GetAsync(ApiBaseUrl.GetLoginApi(_configuration, user.UserName, user.Password)))
                    {
                        var apiContent = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode == true)
                        {
                            List<LoginUsers> result = JsonConvert.DeserializeObject<List<LoginUsers>>(apiContent.Trim());
                            if (result[0].UserName == "admin@emodal.com" && result[0].Password == "Advent@123" && result[0].IsActive == true)
                            {
                                return Redirect("~/TerminalSlots/Index");
                            }
                            else if (result.Count > 0)
                            {
                                return Redirect("~/Appointment/GetAppointmentList");
                            }
                        }
                        else
                        {
                            return BadRequest("User Not Found");
                        }
                        return BadRequest("Not Found");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.StackTrace);
                }
                finally
                {
                    user = null;
                }
            }
        }
    }
}
