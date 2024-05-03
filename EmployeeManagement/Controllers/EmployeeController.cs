using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection conn = null;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            conn = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString());
        }

        [HttpPost]
        [Route("RegisterUser")]
        public Status RegisterUser(Employee employee)
        {
            Status status = new Status();
            DAL dal = new DAL();
            status = dal.Registration(employee, conn);
            return status;
        }

        [HttpPost]
        [Route("LoginUser")]
        public Status LoginUser(EmployeeDto employeeDto)
        {
            Status status = new Status();
            DAL dal = new DAL();
            status = dal.Login(employeeDto, conn);
            return status;
        }

        [HttpPost]
        [Route("InsertUser")]
        public Status InsertUser(EmployeeData employee)
        {
            Status status = new Status();
            DAL dal = new DAL();
            status = dal.InsertEmployee(employee, conn);
            return status;
        }
    }
}
