using EmployeeManagement.Models.DTO;
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
                SqlParameter errorMessageParam =  cmd.Parameters.Add("@ErrorMessage", System.Data.SqlDbType.Char, 200);
                errorMessageParam.Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string msg = errorMessageParam.Value.ToString();
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

        public Status Login(EmployeeDto employee, SqlConnection conn)
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
            Console.WriteLine(status);
            return status;
        
        
        }

        public List<EmployeeData> GetEmployees(SqlConnection conn)
        {
            List<EmployeeData> employees = new List<EmployeeData>();

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_getEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) 
                {
                    EmployeeData emp = new EmployeeData
                    {
                        EmpId = Convert.ToInt32(reader["EmpId"]),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Department = reader["Department"].ToString(),
                        Qualification = reader["Qualification"].ToString()
                    };
                    employees.Add(emp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while retrieving employee data: " + e.Message);
            }
            return employees;
        }

        public Status InsertEmployee(EmployeeData employee, SqlConnection conn)
        {
            Status status = new Status();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insertion", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", employee.EmpId);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Qualification", employee.Qualification);

                SqlParameter errorMsgParam = cmd.Parameters.Add("@ErrorMessage", System.Data.SqlDbType.Char, 200);
                errorMsgParam.Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string msg = errorMsgParam.Value.ToString();
                if (i > 0)
                {
                    status.StatusCode = 200;
                    status.StatusMessage = msg;

                }
                else
                {
                    status.StatusCode = 200;
                    status.StatusMessage = msg;
                }
            }
            catch(Exception e)
            {
                status.StatusCode = 500;
                status.StatusMessage = e.Message;
            
            }

            return status;
        
        }

        public Status UpdateEmployee(EmployeeData employee, SqlConnection conn)
        {
            Status status = new Status();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_updateEmployee", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", employee.EmpId);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Qualification", employee.Qualification);

                SqlParameter errorMsgParam = cmd.Parameters.Add("@ErrorMessage", System.Data.SqlDbType.Char, 200);
                errorMsgParam.Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string msg = errorMsgParam.Value.ToString();
                if (i > 0)
                {
                    status.StatusCode = 200;
                    status.StatusMessage = msg;

                }
                else
                {
                    status.StatusCode = 200;
                    status.StatusMessage = msg;
                }
            }
            catch (Exception e)
            {
                status.StatusCode = 500;
                status.StatusMessage = e.Message;

            }

            return status;
        }

        public Status DeleteEmployee(int empId, SqlConnection conn)
        {
            Status status = new Status();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_deleteEmployee", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmpId", empId);

                SqlParameter errorMsgParam = cmd.Parameters.Add("@ErrorMessage", System.Data.SqlDbType.Char, 200);
                errorMsgParam.Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                string msg = errorMsgParam.Value.ToString();

                if (i > 0)
                {
                    status.StatusCode = 200;
                    status.StatusMessage = msg;

                }
                else
                {
                    status.StatusCode = 200;
                    status.StatusMessage = msg;
                }
            }
            catch (Exception e)
            {

                status.StatusCode = 500;
                status.StatusMessage = e.Message;
            }
            return status;
        }
    }
}
