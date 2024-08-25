
namespace MyApplication.Presentation
{
    public class Mainmenu:IMainmenu
    {
        private IEmployeeManager employeeManager;
        private IRoleManager roleManager;
        public Mainmenu(IEmployeeManager _employeeManager,IRoleManager _roleManager){
            employeeManager=_employeeManager;
            roleManager=_roleManager;
        }
        public  void EntryAsync()
        {
            Console.WriteLine("Welcome to Employee Application\n");
            int operationChoice = 1;
            while (operationChoice > 0)
            {
                Console.WriteLine("Choose your Operations\n1.Employee Management\t2.Role Management\t0.Exit");
                operationChoice = int.Parse(Console.ReadLine() ?? "0");
                switch (operationChoice)
                {
                    case 1:
                         EmployeeOperationsAsync();
                        break;
                    case 2:
                         RoleOperationsAsync();
                        break;
                    case 0:
                        Console.WriteLine("Operations Completed... Closing");
                        break;
                }
            }

        }
        public   void EmployeeOperationsAsync()
        {
            int operationChoice = -1;
            while (operationChoice != 0)
            {
                Console.WriteLine("Enter your choice for EmployeeApplication");
                Console.WriteLine("1.Display All\t2.Add Employee\t3.Edit Employee\t4.SearchBy Letter\t5.Sort\t6.Delete Employee\t7.ExportToCSV\t8.Display One\t0.Back");
                operationChoice = int.Parse(Console.ReadLine() ?? "0");
                switch (operationChoice)
                {
                    case 1:
                         employeeManager.ViewEmployees();
                        break;
                    case 2:
                         employeeManager.CreateEmployeeAsync();
                        break;
                    case 3:
                         employeeManager.editEmployeeAsync();
                        break;
                    case 4:
                         employeeManager.SearchByLetter();
                        break;
                    case 5:
                         employeeManager.SortEmployees();
                        break;
                    case 6:
                         employeeManager.DeleteEmployeeAsync();
                        break;
                    case 7:
                         employeeManager.ExportEmployeeToCsv();
                        break;
                    case 8:
                         employeeManager.ViewSingleEmployee();
                        break;
                    case 0:
                        break;
                }
            }
        }
        public  void RoleOperationsAsync()
        {
            int operationChoice = -1;
            Console.WriteLine("Enter your choice for EmployeeApplication");
            while (operationChoice != 0)
            {
                Console.WriteLine("1.View\t2.Create\t3.Delete\t4.Show Employees\t5.Edit\t0.Back");
                operationChoice=int.Parse(Console.ReadLine()??"");
                switch (operationChoice)
                {
                    case 1:
                         roleManager.ViewRoles();
                        break;
                    case 2:
                         roleManager.CreateRoleAsync();
                        break;
                    case 3:
                         roleManager.DeleteRoleAsync();
                        break;
                    case 4:
                         roleManager.ShowRoleEmployees();
                        break;
                    case 5:
                         roleManager.EditRoleAsync();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Invalid Operation");
                        break;

                }
            }
        }

    }
}