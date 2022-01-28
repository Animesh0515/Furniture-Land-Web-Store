using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Furniture_Land_Web_store.Models
{
    public class OrderModel
    {

        public Inventory Item { get; set; }
        public int Quantity { get; set; }
        public List<InventoryDetails> InventoryDetails { get; set; }

        
    }
}