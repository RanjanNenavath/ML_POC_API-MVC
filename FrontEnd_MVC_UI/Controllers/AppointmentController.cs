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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd_MVC_UI.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public AppointmentController(HttpClient httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
        }
        public async Task<IActionResult> GetAppointmentList()
        {
            try
            {
                var app = await GetAppointments();
                if (app == null)
                {
                    return BadRequest("Exception while consuming API");
                }
                return View(app);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.StackTrace });
            }
            finally
            {
            }
        }

        [HttpGet]
        public async Task<List<Appointment>> GetAppointments()
        {
            try
            {
                using (var response = await _httpClient.GetAsync(ApiBaseUrl.GetAppointmentApi(_configuration)))
                {
                    var result = JsonConvert.DeserializeObject<List<Appointment>>(await response.Content.ReadAsStringAsync());
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
        public async Task<IActionResult> CreateAppointment()
        {
            List<Company> companyList = await GetCompanyList();
            ViewBag.Company = new SelectList(companyList, "C_Id", "CompanyName");
            List<Terminal> TerminalList = await GetTerminalList();
            ViewBag.Terminal = new SelectList(TerminalList, "T_Id", "TerminalName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAppointment(Appointment appointment)
        {
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, ConstString.StringContect);
                using (HttpResponseMessage response = _httpClient.PostAsync(ApiBaseUrl.PostAppointmentApi(_configuration), content).Result)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
                return RedirectToAction(nameof(GetAppointmentList));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.StackTrace });
            }
            finally
            {
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditAppointment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                using (var response = await _httpClient.GetAsync(ApiBaseUrl.GetAppointByIdApi(_configuration) + id))
                {
                    var result = JsonConvert.DeserializeObject<Appointment>(await response.Content.ReadAsStringAsync());

                    if (result == null)
                        return NotFound();
                    else
                        return View(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.StackTrace });
            }
            finally
            {
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAppointment(int id, Appointment appointment)
        {
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, ConstString.StringContect);
                using (var response = await _httpClient.PutAsync(ApiBaseUrl.UpdateAppointApi(_configuration), content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
                return RedirectToAction(nameof(GetAppointmentList));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.StackTrace });
            }
            finally
            {
            }
        }

        public async Task<IActionResult> DeleteAppointment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                using (var response = await _httpClient.GetAsync(ApiBaseUrl.GetAppointByIdApi(_configuration) + id))
                {
                    var result = JsonConvert.DeserializeObject<Appointment>(await response.Content.ReadAsStringAsync());

                    if (result == null)
                        return NotFound();
                    else
                        return View(result);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.StackTrace });
            }
            finally
            {
            }
        }

        [HttpPost, ActionName("DeleteAppointment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                using (var response = await _httpClient.DeleteAsync(ApiBaseUrl.DeleteAppointApi(_configuration) + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
                return RedirectToAction(nameof(GetAppointmentList));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.StackTrace });
            }
            finally
            {
            }
        }

        public async Task<IActionResult> DetailAppointment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                using (var response = await _httpClient.GetAsync(ApiBaseUrl.GetAppointByIdApi(_configuration) + id))
                {
                    var result = JsonConvert.DeserializeObject<Appointment>(await response.Content.ReadAsStringAsync());

                    if (result == null)
                        return NotFound();
                    else
                        return View(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.StackTrace });
            }
            finally
            {
            }
        }
        [HttpPost]
        public async Task<List<Company>> GetCompanyList()
        {
            try
            {
                using (var response = await _httpClient.GetAsync(ApiBaseUrl.CompanyListApi(_configuration)))
                {
                    var apiContext = await response.Content.ReadAsStringAsync();
                    List<Company> result = JsonConvert.DeserializeObject<List<Company>>(apiContext);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }

        [HttpPost]
        public async Task<List<Terminal>> GetTerminalList()
        {
            try
            {
                using (var response = await _httpClient.GetAsync(ApiBaseUrl.TerminalListApi(_configuration)))
                {
                    List<Terminal> result = JsonConvert.DeserializeObject<List<Terminal>>(await response.Content.ReadAsStringAsync());
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
    }
}
