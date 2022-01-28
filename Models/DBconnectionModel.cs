using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Furniture_Land_Web_store.Models
{
    public  class DBconnectionModel
    {

        public static string Server { get; set; }
        public static string Database { get; set; }
        public static string UserID { get; set; }
        public static string Password { get; set; }


        public static string   GetConnectionString()
        {
            return "Server=" + Server + ";Port=3306;Database=" + Database + ";Uid=" + UserID + ";Pwd=" + Password + ";";
        }
    }
}