using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EShopper.Layers
{
    public class OrdersProcess
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
            con = new SqlConnection(constr);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        public bool AddOrders(OrderModel orderModel, string userId)
        {
            bool result = false;
            connection();
            try
            {
                if (orderModel.TranDate == null || orderModel.TranDate.Year < DateTime.Now.Year)
                    orderModel.TranDate = DateTime.Now;

                SqlCommand com = new SqlCommand("dbo.SpAddOrdersByUserId", con);
                com.Parameters.Clear();
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", orderModel.UserId).DbType = DbType.Int32;
                com.Parameters.AddWithValue("@SubTotal", orderModel.SubTotal).DbType = DbType.Decimal;
                com.Parameters.AddWithValue("@PaymentType", orderModel.PaymentType).DbType = DbType.Int32;
                com.Parameters.AddWithValue("@Address", orderModel.Address).DbType = DbType.String;
                com.Parameters.AddWithValue("@Description", orderModel.Description).DbType = DbType.String;
                com.Parameters.AddWithValue("@PhoneNumber", orderModel.PhoneNumber).DbType = DbType.String;
                com.Parameters.AddWithValue("@CreditCardNumber", orderModel.CreditCardNumber).DbType = DbType.String;
                com.Parameters.AddWithValue("@CreditCardName", orderModel.CreditCardName).DbType = DbType.String;
                com.Parameters.AddWithValue("@CreditCartLastDate", orderModel.CreditCardLastDate).DbType = DbType.String;
                com.Parameters.AddWithValue("@CreditCardSecurityNumber", orderModel.CreditCardSecurityNumber).DbType = DbType.String;


                if (con.State != ConnectionState.Open)
                    con.Open();

                var sp = com.ExecuteScalar();
                int i = Convert.ToInt32(sp);
                con.Close();
                if (i >= 1)
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public List<OrderModel> GetOrder(string userId)
        {
            connection();
            List<OrderModel> OrderList = new List<OrderModel>();

            using (SqlConnection sqlConnection = new SqlConnection())
            {
                SqlCommand com = new SqlCommand("dbo.SpGetOrder", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@UserId", userId).DbType = DbType.Int32;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                foreach (DataRow item in dt.Rows)
                {
                    OrderList.Add(new OrderModel
                    {
                        UserId = Convert.ToInt32(item["UserId"]),
                        TranDate = Convert.ToDateTime(item["Trandate"]),
                        SubTotal = Convert.ToDecimal(item["Subtotal"]),
                        PaymentType = Convert.ToInt32(item["PaymentType"]),
                        Address = item["Address"].ToString(),
                        Description = item["Description"].ToString(),
                        PhoneNumber = item["PhoneNumber"].ToString(),
                        OrderStatus = Convert.ToInt32(item["OrderStatus"]),
                        PaymentTypeText = item["PaymentTypeText"].ToString(),
                        OrderStatusText = item["OrderStatusText"].ToString(),
                    }
                    );
                }
            }
            return OrderList;
        }
    }
}