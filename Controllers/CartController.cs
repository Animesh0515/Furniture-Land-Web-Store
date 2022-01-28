using Furniture_Land_Web_store.Models;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Furniture_Land_Web_store.Controllers
{
    public class CartController : Controller
{
        
        public ActionResult Cart()
        {

            
            return View();
        }
    }
}