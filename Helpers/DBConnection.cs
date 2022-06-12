using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartfaceSolution.Entities;
using SmartfaceSolution.SubEntities;

namespace SmartfaceSolution.Helpers
{
    /// <summary>
    /// <c>IDBConnection</c> it is an interface class that specify the database connection
    /// and contain the methods that will be used to connect to the database 
    /// </summary>
    public interface IDBConnection
    {
        public string getMemberId(int id);
        public void setMemberId(int empId, string memberId);
        public void deleteMemberById(int id);
    }

    /// <summary>
    /// <c>DBConnection</c> is a concrete class that implement IDBConnection interface 
    /// </summary>
    public class DBConnection : IDBConnection
    {
        private readonly ServerConfig _serverName;// server name that is stored in appsettings.json

        public DBConnection(IOptions<ServerConfig> serverConfig)
        {
            _serverName = serverConfig.Value;
        }
        /// <summary>
        /// Method <c>getMemberId</c> help to get the member id from the SmartfaceLink table using the empId
        /// </summary>
        /// <param name="id">Emp id</param>
        /// <returns>the member id</returns>
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
                        return (string) dr["MemberID"];// get the Member id 
                    }
                }
            }
            finally
            {
                dr.Close();
            }

            return null;
        }
        /// <summary>
        /// Method <c>setMemberId</c> help to set the emp id and member id in the SmartfaceLink table 
        /// </summary>
        /// <param name="empId">employee id</param>
        /// <param name="memberId">member id </param>
        public void setMemberId(int empId, string memberId)
        {
            string sqlCommand;
            SqlCommand cmd;
            SqlConnection cnn;

            //open the connection with the database
            cnn = new SqlConnection(_serverName.DefaultConnection);
            cnn.Open();
            sqlCommand =
                "INSERT INTO [dbo].[SmartfaceLink] ([EmpId], [MemberId] ) VALUES (@EmpId ,@MemberId)"; // sql insert command to be executed
            cmd = new SqlCommand(sqlCommand, cnn);
            cmd.Parameters.Add("@EmpId", System.Data.SqlDbType.Int, 4).Value = empId;
            cmd.Parameters.Add("@MemberId", System.Data.SqlDbType.VarChar, -1).Value = memberId.Trim();
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Method <c>deleteMemberById</c> help to delete the member from the SmartfaceLink table using the empId
        /// </summary>
        /// <param name="id">Emp id</param>
        public void deleteMemberById(int id)
        {
            string sqlCommand;
            SqlCommand cmd;
            SqlConnection cnn;

            //open the connection with the database
            cnn = new SqlConnection(_serverName.DefaultConnection);
            cnn.Open();
            sqlCommand = "DELETE FROM [dbo].[SmartfaceLink] WHERE [EmpId]=@EmpId";// sql delete command to be executed
            cmd = new SqlCommand(sqlCommand, cnn);
            cmd.Parameters.Add("@EmpId", System.Data.SqlDbType.Int, 4).Value = id;
            cmd.ExecuteNonQuery();
        }
    }
}