﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using EShopper.Models;
using Newtonsoft.Json;

namespace EShopper.Layers
{
    public class UserProcess
    {
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
            con = new SqlConnection(constr);

        }
        public bool AddUser(UsersModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("dbo.SpAddNewUsers", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name).DbType = DbType.String;
            com.Parameters.AddWithValue("@Surname", obj.Surname).DbType = DbType.String;
            com.Parameters.AddWithValue("@Email", obj.Email).DbType = DbType.String;
            com.Parameters.AddWithValue("@Password", obj.Password).DbType = DbType.String;
            com.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth).DbType = DbType.DateTime;
            com.Parameters.AddWithValue("@Gender", obj.Gender).DbType = DbType.Int32;
            com.Parameters.AddWithValue("@Address", obj.Address).DbType = DbType.String;

            con.Open();
            var sp = com.ExecuteScalar();
            int i = sp.GetHashCode();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<UsersModel> LoginControl(UsersModel usersModel)
        {
            connection();
            List<UsersModel> userModelList = new List<UsersModel>();

            SqlCommand com = new SqlCommand("dbo.SpLoginControl", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            com.Parameters.AddWithValue("@Email", usersModel.Email).DbType = DbType.String;
            com.Parameters.AddWithValue("@Password", usersModel.Password).DbType = DbType.String;

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {

                userModelList.Add(

                    new UsersModel
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        Name = Convert.ToString(dr["Name"]),
                        Surname = Convert.ToString(dr["Surname"]),
                        Email = Convert.ToString(dr["Email"]),
                        Password = Convert.ToString(dr["Password"]),
                        DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                        Gender = Convert.ToInt32(dr["Gender"]),
                        Address = Convert.ToString(dr["Address"])

                    }
                    );
            }

            return userModelList;
        }

        public Boolean SignUpControl(UsersModel usersModel)
        {
            connection();
            SqlCommand com = new SqlCommand("dbo.SpUsersControl", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            com.Parameters.AddWithValue("@Email", usersModel.Email).DbType = DbType.String;

            con.Open();
            var sp = com.ExecuteScalar();
            con.Close();
            if (sp.Equals("true"))
                return true;
            return false;
        }

        public UsersModel SelectUserModelByEmailAndPassword(string Email, string Password)
        {
            connection();
            UsersModel userModel = new UsersModel();


            SqlCommand com = new SqlCommand("dbo.SpGetUserModelByEmailAndPassword", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            com.Parameters.AddWithValue("@Email", Email).DbType = DbType.String;
            com.Parameters.AddWithValue("@Password", Password).DbType = DbType.String;

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            var dr = dt.Rows[0];

            userModel.UserId = Convert.ToInt32(dr["UserId"]);
            userModel.Name = Convert.ToString(dr["Name"]);
            userModel.Surname = Convert.ToString(dr["Surname"]);
            userModel.Email = Convert.ToString(dr["Email"]);
            userModel.Password = Convert.ToString(dr["Password"]);
            userModel.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
            userModel.Gender = Convert.ToInt32(dr["Gender"]);
            userModel.Address = Convert.ToString(dr["Address"]);

            return userModel;
        }
        public UsersModel GetUserModelByUserId(int UserId)
        {
            connection();
            UsersModel userModel = new UsersModel();


            SqlCommand com = new SqlCommand("dbo.SpGetUserModelByUserId", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            com.Parameters.AddWithValue("@UserId", UserId).DbType = DbType.Int32;

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            var dr = dt.Rows[0];

            userModel.UserId = Convert.ToInt32(dr["UserId"]);
            userModel.Name = Convert.ToString(dr["Name"]);
            userModel.Surname = Convert.ToString(dr["Surname"]);
            userModel.Email = Convert.ToString(dr["Email"]);
            userModel.Password = Convert.ToString(dr["Password"]);
            userModel.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
            userModel.Gender = Convert.ToInt32(dr["Gender"]);
            userModel.Address = Convert.ToString(dr["Address"]);

            return userModel;
        }

        public Boolean UpdateUsers(UsersModel usersModel)
        {
            connection();
            SqlCommand com = new SqlCommand("dbo.SpUpdateUsers", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserId", usersModel.UserId).DbType = DbType.Int32;
            com.Parameters.AddWithValue("@Name", usersModel.Name).DbType = DbType.String;
            com.Parameters.AddWithValue("@Surname", usersModel.Surname).DbType = DbType.String;
            com.Parameters.AddWithValue("@Email", usersModel.Email).DbType = DbType.String;
            com.Parameters.AddWithValue("@Password", usersModel.Password).DbType = DbType.String;
            com.Parameters.AddWithValue("@DateOfBirth", usersModel.DateOfBirth).DbType = DbType.DateTime;
            com.Parameters.AddWithValue("@Address", usersModel.Address).DbType = DbType.String;

            con.Open();
            var sp = com.ExecuteScalar();
            int i = sp.GetHashCode();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;

        }
    }
}
