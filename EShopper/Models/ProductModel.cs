﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace EShopper.Models
{
    public class ProductModel
    {
        [Key]
        [Display(Name = "ProductId")]
        public int ProductId { get; set; }
        [Display(Name = "ProductCategoryId")]
        public int ProductCategoryId { get; set; }
        [Display(Name = "ProductBrandId")]
        public int ProductBrandId { get; set; }
        [Display(Name = "ProductName")]
        public string ProductName { get; set; }
        [Display(Name = "ProductGuid")]
        public  string ProductGuid { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Stock")]
        public int Stock { get; set; }
    }
}