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
        private readonly string _connectionString;

        public OrdersProcess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
        }

        private SqlConnection CreateConnection()
        {
            var con = new SqlConnection(_connectionString);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            return con;
        }

        public bool AddOrders(OrderModel orderModel, string userId)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpAddOrdersByUserId", con))
            {
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

                var result = com.ExecuteScalar();
                return result != null && Convert.ToInt32(result) >= 1;
            }
        }

        public List<OrderModel> GetOrder(string userId)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpGetOrder", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", userId).DbType = DbType.Int32;

                var orderList = new List<OrderModel>();

                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orderList.Add(new OrderModel
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            TranDate = Convert.ToDateTime(reader["Trandate"]),
                            SubTotal = Convert.ToDecimal(reader["Subtotal"]),
                            PaymentType = Convert.ToInt32(reader["PaymentType"]),
                            Address = reader["Address"].ToString(),
                            Description = reader["Description"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            OrderStatus = Convert.ToInt32(reader["OrderStatus"]),
                            PaymentTypeText = reader["PaymentTypeText"].ToString(),
                            OrderStatusText = reader["OrderStatusText"].ToString(),

                        });
                    }
                }

                return orderList;
            }
        }
    }
}
