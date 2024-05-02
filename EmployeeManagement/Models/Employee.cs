namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmpNo { get; set; }
        public string Username { get; set; }
        public string Name{ get; set; }
        public string Password { get; set; }
        public string Department{ get; set; }
        public Qualification Qualification { get; set; }
    }
}
