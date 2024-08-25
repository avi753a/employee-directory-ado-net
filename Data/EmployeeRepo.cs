using System.Text.RegularExpressions;
using MyApplication.Data.Entites;
using MyApplication.Data.Models;
using System.Data.SqlClient;
namespace MyApplication.Data
{
    public class EmployeeRepo:EmployeeBaseRepo,IEmployeeRepo
    {
        private IRoleRepo roleRepo;
        SqlConnection connection;
        public EmployeeRepo(IRoleRepo _roleRepo)
        {
            roleRepo = _roleRepo;
            string connectionString = $"Data Source=sql-dev;Initial Catalog=AravindDB;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }
        public  void InsertAsync(Employee employee)
        {
            base.InsertAsync(employee);
        }
        public  void EditAsync(Employee employee)
        {
             base.EditAsync(employee);
        }
        public  void DeleteAsync(string id)
        {
             base.DeleteAsync(id);
        }
        public  List<Employee> ViewAllAsync()
        {
            return  base.ReadDataAsync();
        }

        public  Employee ViewOneAsync(string id)
        {
           Employee employee =  base.ReadSingleDataAsync(id);
            return employee;
        }
        public  List<Employee> SortAsync(int sortChoice)
        {
            try
            {
                List<Employee> EmployeeList =  base.ReadDataAsync();
                Dictionary<string, Role> RoleList = [];
                foreach (var r in  roleRepo.ViewAll())
                {
                    RoleList.Add(r.Id, r);
                }
                List<Employee> sortedData = [];
                switch (sortChoice)
                {
                    case 1:
                        sortedData = [.. EmployeeList.OrderBy(e => e.Id)];
                        break;
                    case 2:
                        sortedData = [.. EmployeeList.OrderBy(e => e.FirstName)];
                        break;
                    case 3:
                        sortedData = [.. EmployeeList.OrderBy(e => e.LastName)];
                        break;
                    case 4:
                        sortedData = [.. EmployeeList.OrderBy(e => e.DateOfBirth)];
                        break;
                    case 5:
                        sortedData = [.. EmployeeList.OrderBy(e => e.EmailId)];
                        break;
                    case 6:
                        sortedData = [.. EmployeeList.OrderBy(e => e.Location)];
                        break;
                    case 7:
                        sortedData = [.. EmployeeList.OrderBy(e => e.Department)];
                        break;
                    case 8:
                        sortedData = [.. EmployeeList.OrderBy(e => e.RoleId)];
                        break;
                    case 9:
                        sortedData = [.. EmployeeList.OrderBy(e => e.DateOfJoining)];
                        break;
                }
                return  sortedData;
            }
            catch (Exception e)
            {
                 File.WriteAllTextAsync("../Presentation/Exceptions.txt", e.Message);
                return [];
            }
        }
        public  List<Employee> SearchByLetterAsync(char letterToSearch)
        {
            List<Employee> EmployeeList =  base.ReadDataAsync();
            return  EmployeeList.Where(e => e.FirstName.StartsWith(letterToSearch.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public  bool checkIdAsync(string id)
        {
            List<Employee> EmployeeList =  base.ReadDataAsync();
            if (EmployeeList.Any(e=>e.Id==id))
            {
                return true;
            }
            return false;
        }
        public  List<Employee> filterAsync(int choice, string filterOption)
        {
            List<Employee> EmployeeList =  base.ReadDataAsync();
            switch (choice)
            {
                case 1:
                    return  EmployeeList.Where(e => e.RoleId == filterOption).ToList();
                case 2:
                    return  EmployeeList.Where(e => e.Department == filterOption).ToList();
                case 3:
                    return  EmployeeList.Where(e => e.Location == filterOption).ToList();
                default:
                    return [];
            }
        }
        
    }
}