using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using EShopper.Models;

namespace EShopper.Layers
{
    public class ProductProcess
    {
        string ConString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        public List<ProductModel> GetAllProduct()
        {
            List<ProductModel> ProductList = new List<ProductModel>();

            using (SqlConnection connection = new SqlConnection(ConString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SpGetProducts";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable DataProducts = new DataTable();

                connection.Open();
                sqlDataAdapter.Fill(DataProducts);
                connection.Close();

                foreach (DataRow dr in DataProducts.Rows)
                {
                    ProductList.Add(new ProductModel
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductCategoryId = Convert.ToInt32(dr["ProductCategoryId"]),
                        ProductBrandId = Convert.ToInt32(dr["ProductBrandId"]),
                        ProductName = dr["ProductName"].ToString(),
                        ProductGuid = dr["ProductGuid"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Stock = Convert.ToInt32(dr["Stock"]),
                        ImagePath = dr["ImagePath"].ToString(),
                        ProductBrandName = dr["ProductBrandName"].ToString(),
                        ProductCategoryName = dr["ProductCategoryName"].ToString()
                    });
                }
            }
            return ProductList;
        }
    }
}