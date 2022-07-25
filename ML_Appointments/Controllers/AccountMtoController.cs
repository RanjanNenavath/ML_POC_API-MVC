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
    public class AccountMtoController : ControllerBase
    {
        private readonly IMtoServices _companyServices;

        public AccountMtoController(IMtoServices companyServices)
        {
            this._companyServices = companyServices;
        }
        [HttpPost("AddMtoUsers")]
        public async Task<ActionResult> AddMtoData([FromBody] Mto mto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mto.M_Id = Guid.NewGuid().ToString();
                    await _companyServices.AddCompanyDataAsync(mto);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Some error occured while inserting data:");
            }
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateData(Mto mto)
        {
            if (ModelState.IsValid)
            {
                await _companyServices.UpdateAsync(mto.M_Id, mto);
            }
            return Ok();
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetbyQuery(string userName, string password)
        {
            var data = await _companyServices.GetAsyncQuery(userName, password);
            if (data.Count > 0)
            {
                return Ok(data);
            }
            else
                return NotFound();
        }
        //[HttpPost]
        //public async Task<ActionResult> AddCompanySlotData([FromBody] Companys company)
        //{
        //    company.Id = Guid.NewGuid().ToString();
        //    await _companyServices.AddCompanySlotsDataAsync(company);
        //    return Ok();
        //}
    }
}
