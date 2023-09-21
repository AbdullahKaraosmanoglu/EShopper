using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EShopper.Layers
{
    public class ProductProcess
    {
        private readonly string _connectionString;

        public ProductProcess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }

        public List<ProductModel> GetAllProduct()
        {
            var productList = new List<ProductModel>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SpGetProducts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                var dataAdapter = new SqlDataAdapter(command);
                var dataProducts = new DataTable();

                connection.Open();
                dataAdapter.Fill(dataProducts);

                foreach (DataRow dr in dataProducts.Rows)
                {
                    productList.Add(new ProductModel
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
            return productList;
        }
    }
}
