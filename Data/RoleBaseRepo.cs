using System.Data.SqlClient;
using MyApplication.Data.Entites;
namespace MyApplication.Data
{
    public class RoleBaseRepo:IRepo<Role>
    {
        string connectionString;
        public RoleBaseRepo()
        {
            connectionString = $"Data Source=sql-dev;Initial Catalog=AravindDB;Integrated Security=True";
        }

        public  void InsertAsync(Role role)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "INSERT INTO EmployeeApp.Roles (Id, Name, Department, Description, Location) " + "VALUES (@Id, @Name, @Department, @Description, @Location)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", role.Id);
                command.Parameters.AddWithValue("@Name", role.Name);
                command.Parameters.AddWithValue("@Department", role.Department);
                command.Parameters.AddWithValue("@Description", role.Description);
                command.Parameters.AddWithValue("@Location", role.Location);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Here in baserolerepo");
                Console.WriteLine("Error inserting data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public  void EditAsync(Role role)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "UPDATE EmployeeApp.Roles " +
               "SET Name = @Name, " +
               "    Department = @Department, " +
               "    Description = @Description, " +
               "    Location = @Location " +
               "WHERE Id = @Id;";

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", role.Id);
                command.Parameters.AddWithValue("@Name", role.Name);
                command.Parameters.AddWithValue("@Department", role.Department);
                command.Parameters.AddWithValue("@Description", role.Description);
                command.Parameters.AddWithValue("@Location", role.Location); 
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

                string query = "DELETE FROM EmployeeApp.Roles WHERE Id = @Id;";
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
        public  List<Role> ReadDataAsync()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            List<Role> roles = [];
            string query = "SELECT * FROM EmployeeApp.Roles"; 
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Role role = new Role();
                role.Id = reader.GetString(reader.GetOrdinal("Id"));
                role.Name = reader.GetString(reader.GetOrdinal("Name"));
                role.Location = reader.GetString(reader.GetOrdinal("Location"));
                role.Department = reader.GetString(reader.GetOrdinal("Department"));
                role.Description=reader.GetString(reader.GetOrdinal("Description"));
                roles.Add(role);
            }

            return roles;
        }
        public  Role ReadSingleDataAsync(String id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = $"SELECT * FROM EmployeeApp.Roles where Id='{id}'"; 
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            Role role = new Role();
            reader.Read();
            role.Id = reader.GetString(reader.GetOrdinal("Id"));
            role.Name = reader.GetString(reader.GetOrdinal("Name"));
            role.Location = reader.GetString(reader.GetOrdinal("Location"));
            role.Department = reader.GetString(reader.GetOrdinal("Department"));
            role.Description=reader.GetString(reader.GetOrdinal("Description"));
            return role;
        }
    }
}