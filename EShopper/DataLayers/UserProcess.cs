using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace EShopper.Layers
{
    public class UserProcess
    {
        private readonly string _connectionString;

        public UserProcess()
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

        public bool AddUser(UsersModel obj)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpAddNewUsers", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Name", obj.Name).DbType = DbType.String;
                com.Parameters.AddWithValue("@Surname", obj.Surname).DbType = DbType.String;
                com.Parameters.AddWithValue("@Email", obj.Email).DbType = DbType.String;
                com.Parameters.AddWithValue("@Password", obj.Password).DbType = DbType.String;
                com.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth).DbType = DbType.DateTime;
                com.Parameters.AddWithValue("@Gender", obj.Gender).DbType = DbType.Int32;
                com.Parameters.AddWithValue("@Address", obj.Address).DbType = DbType.String;

                var sp = com.ExecuteScalar();
                return sp != null && sp.GetHashCode() >= 1;
            }
        }

        public List<UsersModel> LoginControl(UsersModel usersModel)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpLoginControl", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Email", usersModel.Email).DbType = DbType.String;
                com.Parameters.AddWithValue("@Password", usersModel.Password).DbType = DbType.String;

                var userModelList = new List<UsersModel>();

                using (var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userModelList.Add(new UsersModel
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Gender = Convert.ToBoolean(reader["Gender"]),
                            Address = reader["Address"].ToString()
                        });
                    }
                }

                return userModelList;
            }
        }

        public bool SignUpControl(UsersModel usersModel)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpUsersControl", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Email", usersModel.Email).DbType = DbType.String;

                var sp = com.ExecuteScalar();
                return sp != null && sp.Equals("true");
            }
        }

        public UsersModel SelectUserModelByEmailAndPassword(string Email, string Password)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpGetUserModelByEmailAndPassword", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Email", Email).DbType = DbType.String;
                com.Parameters.AddWithValue("@Password", Password).DbType = DbType.String;

                var userModel = new UsersModel();

                using (var reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userModel.UserId = Convert.ToInt32(reader["UserId"]);
                        userModel.RoleId = Convert.ToInt32(reader["RoleId"]);
                        userModel.Name = reader["Name"].ToString();
                        userModel.Surname = reader["Surname"].ToString();
                        userModel.Email = reader["Email"].ToString();
                        userModel.Password = reader["Password"].ToString();
                        userModel.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                        userModel.Gender = Convert.ToBoolean(reader["Gender"]);
                        userModel.Address = reader["Address"].ToString();
                    }
                }

                return userModel;
            }
        }

        public UsersModel GetUserModelByUserId(int UserId)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpGetUserModelByUserId", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId).DbType = DbType.Int32;

                var userModel = new UsersModel();

                using (var reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userModel.UserId = Convert.ToInt32(reader["UserId"]);
                        userModel.Name = reader["Name"].ToString();
                        userModel.Surname = reader["Surname"].ToString();
                        userModel.Email = reader["Email"].ToString();
                        userModel.Password = reader["Password"].ToString();
                        userModel.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                        userModel.Gender = Convert.ToBoolean(reader["Gender"]);
                        userModel.Address = reader["Address"].ToString();
                    }
                }

                return userModel;
            }
        }

        public bool UpdateUsers(UpdateUserModel updateUserModel)
        {
            using (var con = CreateConnection())
            using (var com = new SqlCommand("dbo.SpUpdateUsers", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", updateUserModel.UserId).DbType = DbType.Int32;
                com.Parameters.AddWithValue("@Email", updateUserModel.Email).DbType = DbType.String;
                com.Parameters.AddWithValue("@Password", updateUserModel.Password).DbType = DbType.String;
                com.Parameters.AddWithValue("@Address", updateUserModel.Address).DbType = DbType.String;

                var sp = com.ExecuteScalar();
                return sp != null && sp.GetHashCode() >= 1;
            }
        }

        public List<UsersModel> GetAllUsersWithPagenation(int pageNumber, int pageSize)
        {
            List<UsersModel> userList = new List<UsersModel>();

            using (var con = CreateConnection())
            {
                //int pageNumber = 1; // Sayfa numarasını belirleyin
                //int pageSize = 10; // Sayfa boyutunu belirleyin

                using (var com = new SqlCommand("dbo.SpGetAllUsersWithPagination", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@PageNumber", pageNumber);
                    com.Parameters.AddWithValue("@PageSize", pageSize);

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UsersModel user = new UsersModel
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Surname = reader.GetString(reader.GetOrdinal("Surname")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                Gender = reader.GetBoolean(reader.GetOrdinal("Gender")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                GenderTypeText = reader.IsDBNull(reader.GetOrdinal("GenderTypeText")) ? null : reader.GetString(reader.GetOrdinal("GenderTypeText"))

                            };

                            userList.Add(user);
                        }
                    }
                }
            }
            return userList;
        }
    }
}
