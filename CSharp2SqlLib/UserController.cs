using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CSharp2SqlLib
{
    public class UserController
    {
        private SqlConnection sqlConn;

        private static Connection connection { get; set; }







        public bool Create(User user)
        {
            var sql = $"INSERT into Users " +
                " (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin) " +
                " VALUES " + $" (@username, @password, @firstname, @Lastname, " +
                $" @Phone, @Email, @isreviewer, @isadmin); ";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@username", user.Username);
            sqlcmd.Parameters.AddWithValue("@password", user.Password);
            sqlcmd.Parameters.AddWithValue("@firstname", user.Firstname);
            sqlcmd.Parameters.AddWithValue("@lastname", user.Lastname);
            sqlcmd.Parameters.AddWithValue("@phone", user.Phone);
            sqlcmd.Parameters.AddWithValue("@email", user.Email);
            sqlcmd.Parameters.AddWithValue("@isreviewer", user.IsReviewer);
            sqlcmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return (rowsAffected == 1);

        }


        public User GetByPk(int id)
        {

            var sql = $"SELECT * From Users where id = {id}";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            var sqldatareader = sqlcmd.ExecuteReader();
            if (!sqldatareader.HasRows)
            {
                return null;
            }
            sqldatareader.Read();
            var user = new User()
            {
                Id = Convert.ToInt32(sqldatareader["Id"]),
                Username = Convert.ToString(sqldatareader["Username"]),
                Password = Convert.ToString(sqldatareader["Password"]),
                Firstname = Convert.ToString(sqldatareader["Firstname"]),
                Lastname = Convert.ToString(sqldatareader["Lastname"]),
                Phone = Convert.ToString(sqldatareader["Phone"]),
                Email = Convert.ToString(sqldatareader["Email"]),
                IsReviewer = Convert.ToBoolean(sqldatareader["IsReviewer"]),
                IsAdmin = Convert.ToBoolean(sqldatareader["IsAdmin"])


            };
            sqldatareader.Close();
            return user;

        }


    public bool Change(User user)
    {

        var sql = $"UPDATE Users Set " +
            "Username = @username, " +
            "Password = @password, " +
            "Firstname = @firstname " +
            "Lastname = @lastname, " +
            "Phone = @phone, " +
            "Email = @email, " +
            "IsReviewer = @isreviewer, " +
            "IsAdmin = @isadmin " +
            "Where Id = @id;";
        var sqlcmd = new SqlCommand(sql, connection.SqlConn);
        sqlcmd.Parameters.AddWithValue("@id", user.Id);
        sqlcmd.Parameters.AddWithValue("@username", user.Username);
        sqlcmd.Parameters.AddWithValue("@password", user.Password);
        sqlcmd.Parameters.AddWithValue("@firstname", user.Firstname);
        sqlcmd.Parameters.AddWithValue("@lastname", user.Lastname);
        sqlcmd.Parameters.AddWithValue("@phone", user.Phone);
        sqlcmd.Parameters.AddWithValue("@email", user.Email);
        sqlcmd.Parameters.AddWithValue("@isreviewer", user.IsReviewer);
        sqlcmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
        var rowsAffected = sqlcmd.ExecuteNonQuery();

        return (rowsAffected == 1);
    }

        public List<User> GetAllUsers()
        {
            var sql = "SELECT * From Users";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            var sqldatareader = sqlcmd.ExecuteReader();
            var users = new List<User>();
            while (sqldatareader.Read())
            {
                var id = Convert.ToInt32(sqldatareader["Id"]);
                var username = Convert.ToString(sqldatareader["Username"]);
                var password = sqldatareader["Password"].ToString();
                var firstname = sqldatareader["Firstname"].ToString();
                var lastname = sqldatareader["Lastname"].ToString();
                var phone = sqldatareader["Phone"].ToString();
                var email = sqldatareader["Email"].ToString();
                var isreviewer = Convert.ToBoolean(sqldatareader["IsReviewer"]);
                var isadmin = Convert.ToBoolean(sqldatareader["IsAdmin"]);
                var user = new User()
                {
                    Id = id,
                    Username = username,
                    Password = password,
                    Firstname = firstname,
                    Lastname = lastname,
                    Phone = phone,
                    Email = email,
                    IsReviewer = isreviewer,
                    IsAdmin = isadmin
                };
                users.Add(user);
            }
            sqldatareader.Close();
            return users;


        }


        public bool Delete(User user)
    {
        var sql = $"Delete from Users " +
            "Where Id = @Id ";
        var sqlcmd = new SqlCommand(sql, connection.SqlConn);
        sqlcmd.Parameters.AddWithValue("@id", user.Id);
        var rowsAffected = sqlcmd.ExecuteNonQuery();
        return (rowsAffected == 1);

    }




    public void Connect()
    {
        var connStr = "server=localhost\\sqlexpress;" +
                    "database=PrsDb;" + "trusted_connection=true;";

        sqlConn = new SqlConnection(connStr);
        sqlConn.Open();

        if (sqlConn.State != System.Data.ConnectionState.Open)

        {
            throw new Exception("Connection is Broken!!! ");
        }
        Console.WriteLine("Open onnection");


    }

    public void Disconnect()
    {

        if (sqlConn == null)
        {
            return;
        }
        sqlConn.Close();
        sqlConn = null;

    }

    }
}
