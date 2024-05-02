using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagement.Models
{
    public class DAL
    {
        public Status Registration(Employee employee, SqlConnection conn)
        { 
            Status status = new Status();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_register", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", employee.Username);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Password", employee.Password);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Qualification", employee.Qualification);
                cmd.Parameters.Add("@ErrorMessage", System.Data.SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string msg = (string)cmd.Parameters["ErrorMessage"].Value;
                if (i > 0)
                {
                    status.StatusCode = 200;
                    status.StatusMessage = msg;
                }
                else
                {
                    status.StatusCode = 100;
                    status.StatusMessage = msg;
                }

          
            }
            catch(Exception ex)
            { 
                status.StatusCode = 500;
                status.StatusMessage = ex.Message;
            }
           return status;
        }

        public Status Login(Employee employee, SqlConnection conn)
        {
            Status status = new Status();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_login", conn);
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Username", employee.Username);
                da.SelectCommand.Parameters.AddWithValue("@Password", employee.Password);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    status.StatusCode = 200;
                    status.StatusMessage = "User is valid";
                }
                else
                {
                    status.StatusCode = 100;
                    status.StatusMessage = "User is invalid";
                }
            }
            catch (Exception ex)
            {
                status.StatusCode = 500;
                status.StatusMessage = ex.Message;
            }
            return status;
        
        
        }
    }
}
