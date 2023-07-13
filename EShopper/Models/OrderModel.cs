using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EShopper.Models
{
    [Bind(Include = "UserId, TranDate, SubTotal, PaymentType, Address, Description, PhoneNumber, OrderStatus")]
    public class OrderModel
    {
        public int UserId { get; set; }
        public DateTime TranDate { get; set; }
        public decimal SubTotal { get; set; }
        public int PaymentType { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public int OrderStatus { get; set; }
        public string PaymentTypeText { get; set; }
        public string OrderStatusText { get; set; }
    }
}