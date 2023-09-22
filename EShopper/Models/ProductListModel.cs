using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopper.Models
{
    public class ProductListModel
    {
        public ProductModel ProductModel { get; set; }
        public int NikeProductsCount { get; set; }
        public int AdidasProductsCount { get; set; }
        //public List<ProductModel> NikeProducts { get; set; }
        //public List<ProductModel> ProductList { get; set; }
    }

}