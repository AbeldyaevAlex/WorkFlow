using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asu.Web.Models
{
    public class OrderViewModel
    {
        public int OrderID
        {
            get;
            set;
        }

        public string CustomerID { get; set; }

        public string ContactName
        {
            get;
            set;
        }

        public decimal? Freight
        {
            get;
            set;
        }

        public string ShipAddress
        {
            get;
            set;
        }

        public DateTime? OrderDate
        {
            get;
            set;
        }

        public DateTime? ShippedDate
        {
            get;
            set;
        }

        public string ShipCountry
        {
            get;
            set;
        }

        public string ShipCity
        {
            get;
            set;
        }

        public string ShipName
        {
            get;
            set;
        }

        public int? EmployeeID
        {
            get;
            set;
        }
    }



    public class ProductViewModel
    {
        public string ProductID
        {
            get;
            set;
        }

        public string ProductName { get; set; }
    }

    public class OrderViewModel2
    {
        public string ShipName
        {
            get;
            set;
        }

    }
}
