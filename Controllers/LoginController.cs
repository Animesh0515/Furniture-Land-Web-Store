using Furniture_Land_Web_store.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture_Land_Web_store.Controllers
{
    public class LoginController : Controller
    {
        string connectionString = DBconnectionModel.GetConnectionString();

        [HttpPost]
        public string LogIn(string Username, string Password)
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            string query=$"Select UserID from users where username='{Username}' and password='{EncodePasswordToBase64(Password)}' and deletedflag='N'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            var result = cmd.ExecuteScalar();
            if(result==null)
            {
                return null;
            }
            else
            {
                return result.ToString();

            }

        }



        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}