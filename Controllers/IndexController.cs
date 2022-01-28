using Furniture_Land_Web_store.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Furniture_Land_Web_store.Controllers
{

    public class IndexController : Controller
    {
        string connectionString = DBconnectionModel.GetConnectionString();

        //To get images from assets file
        //Note: Only 3 files are taken
        public JsonResult GetPhotos()
        {
            List<ImageModel> imagesList= new List<ImageModel>();
            string path = AppDomain.CurrentDomain.BaseDirectory+"\\Assets\\";
            

            DirectoryInfo Folder;
            FileInfo[] Images;

            Folder = new DirectoryInfo(path);
            Images = Folder.GetFiles();
            
            for (int i = 0; i < 3; i++)
            {
                ImageModel imgmodel = new ImageModel();
                imgmodel.ImageName=(String.Format(@"/Assets/{0}", Images[i].Name));
                imagesList.Add(imgmodel);
            }
           return Json(imagesList, JsonRequestBehavior.AllowGet);


        }
        public JsonResult GetProducts()
        {
            List<Inventory> inventorylst = new List<Inventory>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            string productquery = $"Select * from Inventory where deletedflag='N'";
            MySqlCommand cmd = new MySqlCommand(productquery, conn);
            conn.Open();
            MySqlDataReader rdr = cmd.ExecuteReader();
            try
            {
                while (rdr.Read())
                {
                    Inventory inventory = new Inventory();

                    inventory.InventoryID = int.Parse(rdr["InventoryID"].ToString());
                    inventory.Name = rdr["Name"].ToString();
                    inventory.Type = rdr["Type"].ToString();
                    inventory.Description = rdr["Description"].ToString();
                    inventory.Size = rdr["Size"].ToString();
                    inventory.Weight = int.Parse(rdr["Weight"].ToString());
                    inventory.DeletedFlag = rdr["DeletedFlag"].ToString();
                    inventory.InventoryDetails=new List<InventoryDetails>();
                    string productdetailquery = "Select * from InventoryDetails where InventoryID='" + inventory.InventoryID + "' LIMIT 1 ";
                    MySqlConnection conn1 = new MySqlConnection(connectionString);

                    MySqlCommand cmd1 = new MySqlCommand(productdetailquery, conn1);
                   conn1.Open();
                    MySqlDataReader rdr1 = cmd1.ExecuteReader();
                    while (rdr1.Read())
                    {
                        InventoryDetails details = new InventoryDetails();
                        details.InventoryDetailID = int.Parse(rdr1["InventDetailID"].ToString());
                        details.Color = rdr1["Color"].ToString();
                        details.Quantity = rdr1["AvailableStock"].ToString();
                        details.Price = int.Parse(rdr1["Price"].ToString());
                        details.StockUpdated = DateTime.Parse(rdr1["StockUpdated"].ToString());
                        details.ImageUrl = rdr1["ImageUrl"].ToString();
                        inventory.InventoryDetails.Add(details);
                    }
                    conn1.Close();
                    inventorylst.Add(inventory);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                inventorylst = null;
            }
            return Json(inventorylst, JsonRequestBehavior.AllowGet); ;
        }
    }
    
    public class ImageModel
    {
        public string ImageName { get; set; }
    }
}