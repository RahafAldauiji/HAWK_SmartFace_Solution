using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartfaceSolution.Entities;
using SmartfaceSolution.SubEntities;

namespace SmartfaceSolution.Helpers
{
    public interface IDBConnection
    {
        public string getMemberId(int id);
        public void setMemberId(int empId, string memberId);
        public void deleteMemberById(int id);
    }

    public class DBConnection : IDBConnection
    {
        private Members allMembers = new SubWatchlistMember().retrievesAllWatchlistMembers();
        private readonly ServerConfig _serverName;

        public DBConnection(IOptions<ServerConfig> serverConfig)
        {
            _serverName = serverConfig.Value;
        }

        public string getMemberId(int id)
        {
            string sqlCommand;
            SqlCommand cmd;
            SqlDataReader dr = null;
            SqlConnection cnn;
            try
            {
                //open the connection with the database
                cnn = new SqlConnection(_serverName.DefaultConnection);
                cnn.Open();
                sqlCommand =
                    "SELECT * FROM [dbo].[SmartfaceLink] WHERE EmpId = @EmpId"; // sql select command to be executed
                cmd = new SqlCommand(sqlCommand, cnn);
                cmd.Parameters.Add("@EmpId", System.Data.SqlDbType.Int, 4).Value = id;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        return (string) dr["MemberID"];
                    }
                }
            }
            finally
            {
                dr.Close();
            }

            return null;
        }

        public void setMemberId(int empId, string memberId)
        {
            string sqlCommand;
            SqlCommand cmd;
            SqlConnection cnn;

            //open the connection with the database
            cnn = new SqlConnection(_serverName.DefaultConnection);
            cnn.Open();
            sqlCommand =
                "INSERT INTO [dbo].[SmartfaceLink] ([EmpId], [MemberId] ) VALUES (@EmpId ,@MemberId)";
            cmd = new SqlCommand(sqlCommand, cnn);
            cmd.Parameters.Add("@EmpId", System.Data.SqlDbType.Int, 4).Value = empId;
            cmd.Parameters.Add("@MemberId", System.Data.SqlDbType.VarChar, -1).Value = memberId.Trim();
            cmd.ExecuteNonQuery();
        }

        public void deleteMemberById(int id)
        {
            string sqlCommand;
            SqlCommand cmd;
            SqlConnection cnn;

            //open the connection with the database
            cnn = new SqlConnection(_serverName.DefaultConnection);
            cnn.Open();
            sqlCommand = "DELETE FROM [dbo].[SmartfaceLink] WHERE [EmpId]=@EmpId";
            cmd = new SqlCommand(sqlCommand, cnn);
            cmd.Parameters.Add("@EmpId", System.Data.SqlDbType.Int, 4).Value = id;
            cmd.ExecuteNonQuery();
        }
    }
}