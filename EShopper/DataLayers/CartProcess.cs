using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EShopper.Layers
{
    public class CartProcess
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
            con = new SqlConnection(constr);

        }
        public bool AddCart(string productId, string userId)
        {

            connection();
            SqlCommand com = new SqlCommand("dbo.SpAddCartByUserId", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserId", userId).DbType = DbType.Int32;
            com.Parameters.AddWithValue("@ProductId", productId).DbType = DbType.Int32;

            con.Open();
            var sp = com.ExecuteScalar();
            int i = sp.GetHashCode();
            con.Close();
            if (i == 1)
                return true;
            else
                return false;
        }

        public bool DeleteItemCart(string productId, string userId)
        {
            connection();
            SqlCommand com = new SqlCommand("dbo.SpDeleteCartItemByProductId", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserId", userId).DbType = DbType.Int32;
            com.Parameters.AddWithValue("@ProductId", productId).DbType = DbType.Int32;

            con.Open();
            var sp = com.ExecuteScalar();
            int i = sp.GetHashCode();
            con.Close();
            if (i == 1)
                return true;
            else
                return false;
        }

        public List<ProductModel> GetCarts(string userId)
        {
            connection();
            List<ProductModel> CartList = new List<ProductModel>();

            using (SqlConnection sqlConnection = new SqlConnection())
            {
                SqlCommand com = new SqlCommand("dbo.SpGetCarts", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@UserId", userId).DbType = DbType.Int32;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow item in dt.Rows)
                {
                    CartList.Add(new ProductModel
                    {
                        ProductId = Convert.ToInt32(item["ProductId"]),
                        ProductName = item["ProductName"].ToString(),
                        Price = Convert.ToDecimal(item["Price"]),
                        Stock = Convert.ToInt32(item["Stock"]),
                        Quantity = Convert.ToInt32(item["Quantity"]),
                        ProductGuid = item["ProductGuid"].ToString(),
                        ImagePath = item["ImagePath"].ToString(),
                    }
                    );
                }
            }
            return CartList;
        }
    }
}


