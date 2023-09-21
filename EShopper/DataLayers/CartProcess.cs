using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EShopper.Layers
{
    public class CartProcess
    {
        private readonly string _connectionString;

        public CartProcess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public bool AddCart(string productId, string userId)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpAddCartByUserId", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", userId).DbType = DbType.Int32;
                com.Parameters.AddWithValue("@ProductId", productId).DbType = DbType.Int32;

                con.Open();
                var result = com.ExecuteScalar();
                return result != null && (int)result == 1;
            }
        }

        public bool DeleteItemCart(string productId, string userId)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpDeleteCartItemByProductId", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", userId).DbType = DbType.Int32;
                com.Parameters.AddWithValue("@ProductId", productId).DbType = DbType.Int32;

                con.Open();
                var result = com.ExecuteScalar();
                return result != null && (int)result == 1;
            }
        }

        public List<ProductModel> GetCarts(string userId)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpGetCarts", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", userId).DbType = DbType.Int32;

                var cartList = new List<ProductModel>();
                con.Open();

                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cartList.Add(new ProductModel
                        {
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName = reader["ProductName"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Stock = Convert.ToInt32(reader["Stock"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            ProductGuid = reader["ProductGuid"].ToString(),
                            ImagePath = reader["ImagePath"].ToString(),
                        });
                    }
                }

                return cartList;
            }
        }
    }
}
