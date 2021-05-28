using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CSharp2SqlLib
{
    class RequestController
    {
        private static Connection connection { get; set; }



        public bool Create(Request request)
        {
            var sql = $"INSERT into Requests " +
                " (Description, Justification, RejectionReason, DeliveryMode, Status, Total, UserId,) " +
                " VALUES " + $" (@description, @justification, @rejectionreason, @deliverymode, " +
                $" @status, @total, @userid); ";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@description", request.Description);
            sqlcmd.Parameters.AddWithValue("@justification", request.Justification);
            sqlcmd.Parameters.AddWithValue("@rejectionreason", request.RejectionReason);
            sqlcmd.Parameters.AddWithValue("@deliverymode", request.DeliveryMode);
            sqlcmd.Parameters.AddWithValue("@status", request.Status);
            sqlcmd.Parameters.AddWithValue("@total", request.Total);
            sqlcmd.Parameters.AddWithValue("@userid", request.Userid);
            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return (rowsAffected == 1);

        }


        public Request GetByPk(int id)
        {

            var sql = $"SELECT * From Request where id = {id}";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            var sqldatareader = sqlcmd.ExecuteReader();
            if (!sqldatareader.HasRows)
            {
                return null;
            }
            sqldatareader.Read();
            var user = new Request()
            {
                Id = Convert.ToInt32(sqldatareader["Id"]),
                Description = Convert.ToString(sqldatareader["Description"]),
                Justification = Convert.ToString(sqldatareader["Justification"]),
                RejectionReason = Convert.ToString(sqldatareader["RejectionReason"]),
                DeliveryMode = Convert.ToString(sqldatareader["DeliveryMode"]),
                Status = Convert.ToString(sqldatareader["Status"]),
                Total = Convert.ToDecimal(sqldatareader["Total"]),
                Userid = Convert.ToInt32(sqldatareader["Userid"]),
              
            };
            sqldatareader.Close();
            return user;

        }


        public bool Change(Request request)
        {

            var sql = $"UPDATE Requests Set " +
                "Description = @description, " +
                "Justification = @justification, " +
                "RejectionReason = @rejectionreason " +
                "DeliveryMode = @deliverymode, " +
                "Status = @status, " +
                "Total = @total, " +
                "Userid = @userid " +
                "Where Id = @id;";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@id", request.Id);
            sqlcmd.Parameters.AddWithValue("@description", request.Description);
            sqlcmd.Parameters.AddWithValue("@justification", request.Justification);
            sqlcmd.Parameters.AddWithValue("@rejectionreason", request.RejectionReason);
            sqlcmd.Parameters.AddWithValue("@deliverymode", request.DeliveryMode);
            sqlcmd.Parameters.AddWithValue("@status", request.Status);
            sqlcmd.Parameters.AddWithValue("@total", request.Total);
            sqlcmd.Parameters.AddWithValue("@userid", request.Userid);
            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        public List<Request> GetAllRequests()
        {
            var sql = "SELECT * From Request";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            var sqldatareader = sqlcmd.ExecuteReader();
            var users = new List<Request>();
            while (sqldatareader.Read())
            {
                var id = Convert.ToInt32(sqldatareader["Id"]);
                var description = Convert.ToString(sqldatareader["Description"]);
                var justification = sqldatareader["Justification"].ToString();
                var rejectionreason = sqldatareader["RejectionReason"].ToString();
                var deliverymode = sqldatareader["DeliveryMode"].ToString();
                var status = sqldatareader["Status"].ToString();
                var total = Convert.ToInt32(sqldatareader["Total"]);
                var userid = Convert.ToInt32(sqldatareader["Userid"]);
                var request = new Request()
                {
                    Id = id,
                    Description = description,
                    Justification = justification,
                    RejectionReason = rejectionreason,
                    DeliveryMode = deliverymode,
                    Status = status,
                    Total = total,
                    Userid = userid
                };
                users.Add(request);
            }
            sqldatareader.Close();
            return users;


        }


        public bool Delete(Request request)
        {
            var sql = $"Delete from Requests " +
                "Where Id = @Id ";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@id", request.Id);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return (rowsAffected == 1);

        }







































    }
}
