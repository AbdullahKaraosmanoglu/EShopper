using System;
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

        /// <summary>
        /// web.config'den connection string'i alan SQL bağlantı yapısı...
        /// </summary>
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();
            con = new SqlConnection(constr);

        }
        /// <summary>
        /// Kullanıcıyı Kayıt Eden Insert  Yapısı ve Methodları...
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Kullanıcı Giriş yaparken Sistemde Hesabı Olup olmadığını dönen ve kontrol eden SELECT Yapısı ve Methodudur...
        /// </summary>
        /// <param name="usersModel"></param>
        /// <returns></returns>
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
            con.Close(); //burada kayıt varsa  
            if (sp.Equals("true"))
            {
                return true;
            }
            return false;
        }
    }
}

