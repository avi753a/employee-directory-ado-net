using System.Data.SqlClient;
using MyApplication.Data.Entites;
using MyApplication.Data.Models;
namespace MyApplication.Data
{
    public class EmployeeBaseRepo:IRepo<Employee>
    {
        string connectionString;
        
        public EmployeeBaseRepo(){
             connectionString = $"Data Source=sql-dev;Initial Catalog=AravindDB;Integrated Security=True;TrustServerCertificate=True";
        }

        public  void InsertAsync(Employee employee)
        {
             SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                string query = "INSERT INTO EmployeeApp.Employees (Id, FirstName, LastName, DateOfBirth, EmailId, MobileNo, DateOfJoining, RoleId, ProjectName, Location, Department, ManagerName) " +
                                "VALUES (@EmployeeId, @FirstName, @LastName, @DateOfBirth, @EmailId, @MobileNo, @DateOfJoining, @RoleId, @ProjectName, @Location, @Department, @ManagerName)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@EmployeeId", employee.Id);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DateOfBirth",(object)employee.DateOfBirth??DBNull.Value);
                command.Parameters.AddWithValue("@EmailId", employee.EmailId);
                command.Parameters.AddWithValue("@MobileNo", employee.MobileNo);
                command.Parameters.AddWithValue("@DateOfJoining", (object)employee.DateOfBirth??DBNull.Value);
                command.Parameters.AddWithValue("@RoleId", employee.RoleId);
                command.Parameters.AddWithValue("@ProjectName", employee.ProjectName);
                command.Parameters.AddWithValue("@Location", employee.Location);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@ManagerName", employee.ManagerName);
                Console.WriteLine($"{employee.DateOfBirth}-------{String.IsNullOrEmpty(employee.DateOfBirth.ToString())}");
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error inserting data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public  void EditAsync(Employee employee)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                string query = "UPDATE EmployeeApp.Employees " +
               "SET FirstName = @FirstName, " +
               "    LastName = @LastName, " +
               "    DateOfBirth = @DateOfBirth, " +
               "    EmailId = @EmailId, " +
               "    MobileNo = @MobileNo, " +
               "    DateOfJoining = @DateOfJoining, " +
               "    RoleId = @RoleId, " +
               "    ProjectName = @ProjectName, " +
               "    Location = @Location, " +
               "    Department = @Department, " +
               "    ManagerName = @ManagerName " +
               "WHERE Id = @Id;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", employee.Id);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", (object)employee.DateOfBirth??DBNull.Value);
                command.Parameters.AddWithValue("@EmailId", employee.EmailId);
                command.Parameters.AddWithValue("@MobileNo", employee.MobileNo);
                command.Parameters.AddWithValue("@DateOfJoining", (object)employee.DateOfBirth??DBNull.Value);
                command.Parameters.AddWithValue("@RoleId", employee.RoleId);
                command.Parameters.AddWithValue("@ProjectName", employee.ProjectName);
                command.Parameters.AddWithValue("@Location", employee.Location);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@ManagerName", employee.ManagerName);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error inserting data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public  void DeleteAsync(string id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "DELETE FROM EmployeeApp.Employees WHERE Id = @Id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error inserting data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public  List<Employee> ReadDataAsync()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            List<Employee> employees = [];
            string query = "SELECT E.Id, E.FirstName, E.LastName, E.DateOfBirth, E.EmailId, E.MobileNo, R.Name as 'RoleName', E.ProjectName, E.ManagerName, E.Location, E.Department, E.DateOfJoining FROM EmployeeApp.Employees E JOIN EmployeeApp.Roles R on E.RoleID=R.Id; "; 
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = reader.GetString(reader.GetOrdinal("Id"));
                employee.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                employee.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                if (!reader.IsDBNull(reader.GetOrdinal("DateOfBirth")))
                {
                    employee.DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                }
                else
                {
                    employee.DateOfBirth = null;
                }
                employee.EmailId = reader.GetString(reader.GetOrdinal("EmailId"));
                employee.MobileNo = reader.GetString(reader.GetOrdinal("MobileNo"));
                employee.RoleId = reader.GetString(reader.GetOrdinal("RoleName"));
                employee.ProjectName = reader.GetString(reader.GetOrdinal("ProjectName"));
                employee.ManagerName = reader.GetString(reader.GetOrdinal("ManagerName"));
                employee.Location = reader.GetString(reader.GetOrdinal("Location"));
                employee.Department = reader.GetString(reader.GetOrdinal("Department"));
                if (!reader.IsDBNull(reader.GetOrdinal("DateOfJoining")))
                {
                    employee.DateOfJoining = reader.GetDateTime(reader.GetOrdinal("DateOfJoining"));
                }
                else
                {
                    employee.DateOfJoining = null;
                }
                employees.Add(employee);
            }
            connection.Close();
            return employees;

        }
        public  Employee ReadSingleDataAsync(String id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = $"SELECT E.Id, E.FirstName, E.LastName, E.DateOfBirth, E.EmailId, E.MobileNo, E.RoleId, E.ProjectName, E.ManagerName, E.Location, E.Department, E.DateOfJoining FROM EmployeeApp.Employees E JOIN EmployeeApp.Roles R on E.RoleID=R.Id WHERE E.Id='{id}'" ; 
            // Replace "EmployeeApp.Employees" with your actual table name
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
                Employee employee = new Employee();
                reader.Read();
                employee.Id = reader.GetString(reader.GetOrdinal("Id"));
                employee.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                employee.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                if (!reader.IsDBNull(reader.GetOrdinal("DateOfBirth")))
                {
                    employee.DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                }
                else
                {
                    employee.DateOfBirth = null;
                }
                employee.EmailId = reader.GetString(reader.GetOrdinal("EmailId"));
                employee.MobileNo = reader.GetString(reader.GetOrdinal("MobileNo"));
                employee.RoleId = reader.GetString(reader.GetOrdinal("RoleId"));
                employee.ProjectName = reader.GetString(reader.GetOrdinal("ProjectName"));
                employee.ManagerName = reader.GetString(reader.GetOrdinal("ManagerName"));
                employee.Location = reader.GetString(reader.GetOrdinal("Location"));
                employee.Department = reader.GetString(reader.GetOrdinal("Department"));
                if (!reader.IsDBNull(reader.GetOrdinal("DateOfJoining")))
                {
                    employee.DateOfJoining = reader.GetDateTime(reader.GetOrdinal("DateOfJoining"));
                }
                else
                {
                    employee.DateOfJoining = null;
                }
                connection.Close();
                return employee;
        }
    }
}