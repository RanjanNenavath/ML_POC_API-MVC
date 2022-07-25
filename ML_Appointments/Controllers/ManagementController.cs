using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML_Appointments.Models;
using ML_Appointments.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML_Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly IManagementServices _managementServices;

        public ManagementController(IManagementServices managementServices)
        {
            this._managementServices = managementServices;
        }
        [HttpPost("AddSlots")]
        public async Task<IActionResult> AddSlots([FromBody]Slot slot)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var con = await _managementServices.Postslots(slot);
                    if (con > 0)
                    {
                        return Ok(con);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
                finally
                {
                    slot = null;
                }
            }
            return BadRequest();
        }
        [HttpGet("GetAllCompany")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var com = await _managementServices.GetCompany();
                if (com == null)
                {
                    return NotFound();
                }
                return Ok(com);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
            finally
            {
            }
        }
        [HttpGet("GetAllterminals")]
        public async Task<IActionResult> GetAllterminals()
        {
            try
            {
                var ter = await _managementServices.GetTerminals();
                if (ter == null)
                {
                    return NotFound();
                }
                return Ok(ter);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
            finally
            {
            }
        }
        [HttpGet("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointment()
        {
            try
            {
                var app = await _managementServices.GetAppointments();
                if (app == null)
                {
                    return NotFound();
                }
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
            finally
            {
            }
        }

        [HttpGet("GetAppointmentById")]
        public async Task<IActionResult> GetAppointById(int AppId)
        {
            try
            {
                var app = await _managementServices.GetAppointmentById(AppId);
                if (app == null)
                {
                    return NotFound();
                }
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
            finally
            {
                AppId = 0;
            }
        }
        [HttpPost("AddAppointments")]
        public async Task<IActionResult> AddAppointment([FromBody] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var app = await _managementServices.PostAppointments(appointment);
                    if (app > 0)
                    {
                        return Ok(app);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
                finally
                {
                    appointment = null;
                }
            }
            return BadRequest();
        }
        [HttpPut("UpdateAppointmentbyId")]
        public async Task<IActionResult> UpdateAppoint([FromBody] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _managementServices.UpdateAppointment(appointment);
                    return Ok("Updated Sucessfully");
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest(new { ex.Message });
                }
                finally
                {
                    appointment = null;
                }
            }
            return BadRequest();
        }

        [HttpDelete("DeleteAppointmentById")]
        public async Task<IActionResult> DeleteAppoint(int? Id)
        {
            int result = 0;
            if (Id == null)
            {
                return BadRequest();
            }

            try
            {
                result = await _managementServices.DeleteAppointment(Id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok("Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
            finally
            {
            }
        }

    }
}
