using Furniture_Land_Web_store.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture_Land_Web_store.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        string connectionString = DBconnectionModel.GetConnectionString();
        [HttpPost]
        public bool PlaceOrder(OrderModel[] itemOrdered, int userId)
        {
            MySqlTransaction objTrans = null;
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                //inserting the order data
                MySqlConnection conn = new MySqlConnection(connectionString);                
                    conn.Open();
                objTrans = conn.BeginTransaction();
                string insertquery = "Insert into Orders (UserID, OrderedDate, DelieverdStatus, DeletedFlag) values (@Id,@Date, 'N', 'N')";
                    MySqlCommand cmd = new MySqlCommand(insertquery, conn);
                    cmd.Parameters.Add(new MySqlParameter("Id", MySqlDbType.Int32, 11)).Value = userId;
                    cmd.Parameters.Add(new MySqlParameter("Date", MySqlDbType.DateTime)).Value = dateTime;
                    string tmp = cmd.CommandText.ToString();
                    foreach (MySqlParameter param in cmd.Parameters)
                    {
                        tmp = tmp.Replace('@' + param.ParameterName.ToString(), "'" + param.Value.ToString() + "'");
                    }
                    cmd.ExecuteNonQuery();
                
                objTrans.Commit();
                conn.Close();

                //geting the order id that was created
                int OrderID =0;
                MySqlConnection conn1 = new MySqlConnection(connectionString);

                string selectQuery = $"Select OrderID from Orders where UserID='{userId}' and  OrderedDate='{dateTime}' ";
                conn1.Open();
                    MySqlCommand cmd1 = new MySqlCommand(selectQuery, conn1);                    
                    MySqlDataReader rdr = cmd1.ExecuteReader();
                    while (rdr.Read())
                    {
                        OrderID = int.Parse(rdr["OrderID"].ToString());
                    }
                
                conn1.Close();
                bool isInserted = InsertOrderedItem(itemOrdered, OrderID);
                if(!isInserted)
                {
                    objTrans.Rollback();
                    return false;

                }
                else
                {
                    return true;

                }








            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                return false;

            }


        }

        public bool InsertOrderedItem(OrderModel[] itemOrdered, int OrderID)
        {
            //insert the items ordered by the customer
            MySqlTransaction objTrans = null;
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                objTrans = conn.BeginTransaction();

                if (OrderID != 0)
                {
                    foreach (var item in itemOrdered)
                    {
                        string insertItemquery = "Insert into ProductOrdered (OrderID, Quantity, TotalPrice) values(@OrderID, @Quantity, @Total)";
                        MySqlCommand cmd3 = new MySqlCommand(insertItemquery, conn);
                        cmd3.Parameters.Add(new MySqlParameter("OrderID", MySqlDbType.Int32, 11)).Value = OrderID;
                        cmd3.Parameters.Add(new MySqlParameter("Quantity", MySqlDbType.Int32, 11)).Value = item.Quantity;
                        cmd3.Parameters.Add(new MySqlParameter("Total", MySqlDbType.Int32, 11)).Value = item.Item.InventoryDetails[0].Price * item.Quantity;
                        cmd3.ExecuteNonQuery();
                    }
                    objTrans.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                return false;
            }
        }


    }
}