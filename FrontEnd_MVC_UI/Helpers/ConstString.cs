using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd_MVC_UI.Helpers
{
    public class ConstString
    {
        public static string TokenName = "JWToken";
        public const string StringContect = "application/json";
    }

    public static class ApiBaseUrl
    {
        public static string GetLoginApi(IConfiguration _configuration,string userName, string password)
        {
            string apiUrl = $"{_configuration.GetValue<string>("WebApiBaseURL")}";
            var st = apiUrl + "AccountMto/GetUser?userName="+userName+"&password="+password;
            return st.Trim();
        } 
        public static string PostSlotBookiingApi(IConfiguration _configuration)
        {
            string apiUrl = $"{_configuration.GetValue<string>("WebApiBaseURL")}";
            var st = apiUrl + "Management/AddSlots";
            return st.Trim();
        }      
        public static string PostAppointmentApi(IConfiguration _configuration)
        {
            string apiUrl = $"{_configuration.GetValue<string>("WebApiBaseURL")}";
            var st = apiUrl + "Management/AddAppointments";
            return st.Trim();
        }
        public static string GetAppointmentApi(IConfiguration _configuration)
        {
            string apiUrl = $"{_configuration.GetValue<string>("WebApiBaseURL")}";
            var st = apiUrl + "Management/GetAllAppointments";
            return st.Trim();
        }
        public static string GetAppointByIdApi(IConfiguration _configuration)
        {
            string apiUrl = $"{_configuration.GetValue<string>("WebApiBaseURL")}";
            var st = apiUrl + "Management/GetAppointmentById?AppId=";
            return st.Trim();
        }
        public static string UpdateAppointApi(IConfiguration _configuration)
        {
            string apiUrl = $"{_configuration.GetValue<string>("WebApiBaseURL")}";
            var st = apiUrl + "Management/UpdateAppointmentbyId";
            return st.Trim();
        }
        public static string DeleteAppointApi(IConfiguration _configuration)
        {
            string apiUrl = $"{_configuration.GetValue<string>("WebApiBaseURL")}";
            var st = apiUrl + "Management/DeleteAppointmentById?Id=";
            return st.Trim();
        }
        public static string CompanyListApi(IConfiguration _configuration)
        {
            string apiUrl = $"{_configuration.GetValue<string>("WebApiBaseURL")}";
            var st = apiUrl + "Management/GetAllCompany";
            return st.Trim();
        }
        public static string TerminalListApi(IConfiguration _configuration)
        {
            string apiUrl = $"{_configuration.GetValue<string>("WebApiBaseURL")}";
            var st = apiUrl + "Management/GetAllterminals";
            return st.Trim();
        }
    }
}
