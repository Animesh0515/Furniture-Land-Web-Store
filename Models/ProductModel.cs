using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Furniture_Land_Web_store.Models
{
    public class Inventory
    {

        public int InventoryID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public int Weight { get; set; }
        public string DeletedFlag { get; set; }
        public List<InventoryDetails> InventoryDetails { get; set; }
    }


    public class InventoryDetails
    {
        public int InventoryDetailID { get; set; }
        public string Color { get; set; }
        public string Quantity { get; set; }
        public int Price { get; set; }
        public DateTime StockUpdated { get; set; }
        public string ImageUrl { get; set; }
    }
}