using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShopper.Models
{
    public class CartModel
    {
        [Key]
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
    }
}