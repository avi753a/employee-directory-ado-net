using MyApplication.Services;
using MyApplication.Infrastructure;
using MyApplication.Models;
using Infrastructure;

namespace MyApplication.Presentation
{
    public class RoleManager:IRoleManager
    {
        private IValidation validate;
        private IEmployeeServices empService;
        private IRoleServices roleService;
        public RoleManager(IValidation _validation, IEmployeeServices _employeeServices, IRoleServices _roleServices)
        {
            validate = _validation;
            empService = _employeeServices;
            roleService = _roleServices;
        }
        public  void CreateRoleAsync()
        {
            Console.WriteLine("Creating new RoleView");
            Console.WriteLine("Enter RoleView ID(RL00)");
            string roleId = Console.ReadLine() ?? "";
            if (!validate.ValidateRoleId(roleId))
            {
                Console.WriteLine("RoleName Invalid\n Operation Terminated");
                return;

            }
            else if ( roleService.checkId(roleId))
            {
                Console.WriteLine("RoleID cannot be repeated\t Operation Terminated");
                return;
            }
            Console.WriteLine("Enter RoleView Name");
            string roleName = Console.ReadLine() ?? "";
            if (!validate.ValidateName(roleName))
            {
                roleName = ReTakeData(1);
                if (!validate.ValidateName(roleName))
                {
                    return;
                }
            }
            Console.WriteLine("Enter RoleView Department");
            string roleDepartment = Console.ReadLine() ?? "";
            if (!validate.ValidateName(roleDepartment))
            {
                roleDepartment = ReTakeData(1);
                if (!validate.ValidateName(roleDepartment))
                {
                    return;
                }
            }
            Console.WriteLine("Enter RoleView Description");
            string roleDescription = Console.ReadLine() ?? "";
            Console.WriteLine("Enter RoleView  Location");
            string roleLocation = Console.ReadLine() ?? "";
            if (!validate.ValidateName(roleLocation))
            {
                roleLocation = ReTakeData(1);
                if (!validate.ValidateName(roleLocation))
                {
                    return;
                }
            }
            RoleView newRole=new();
            newRole.Id=roleId;
            newRole.Name=roleName;
            newRole.Department=roleDepartment;
            newRole.Location=roleLocation;
            newRole.Description=roleDescription;
             roleService.Insert(newRole);
            Console.WriteLine("New RoleView Added");
             AssignEmployeesAsync(roleId);
        }
        public  void DeleteRoleAsync()
        {
            Console.WriteLine("Enter RoleID to Delete");
            string roleToDelete = Console.ReadLine() ?? "";
            if ( roleService.checkId(roleToDelete))
            {
                 roleService.Delete(roleToDelete);
                Console.WriteLine($"RoleView Deleted");
            }
            else
            {
                Console.WriteLine("RoleView Not found");
            }

        }
        public  void ViewRoles()
        {
            const int idLength = 5;
            const int nameLength = 30;
            const int descriptionLength = 30;
            const int departmentLength = 20;
            const int locationLength = 15;

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| {"Id ",-idLength}| {"Name",-nameLength} | {"Description",-descriptionLength} | {"Role Department",-departmentLength} | {"Location",-locationLength} |");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");

            foreach (RoleView roleItem in  roleService.View())
            {
                string idString = (roleItem.Id??"").ToString().Substring(0, Math.Min((roleItem.Id??"").ToString().Length, idLength));
                string nameSubstring = (roleItem.Name??"").Substring(0, Math.Min((roleItem.Name??"").Length, nameLength));
                string descriptionSubstring = (roleItem.Description??"").Substring(0, Math.Min((roleItem.Description??"").Length, descriptionLength));
                string departmentSubstring = (roleItem.Department??"").Substring(0, Math.Min((roleItem.Department??"").Length, departmentLength));
                string locationSubstring = (roleItem.Location??"").Substring(0, Math.Min((roleItem.Location??"").Length, locationLength));

                string idPadding = new string(' ', idLength - idString.Length);
                string namePadding = new string(' ', nameLength - nameSubstring.Length);
                string descriptionPadding = new string(' ', descriptionLength - descriptionSubstring.Length);
                string departmentPadding = new string(' ', departmentLength - departmentSubstring.Length);
                string locationPadding = new string(' ', locationLength - locationSubstring.Length);

                Console.WriteLine($"| {idString}{idPadding} | {nameSubstring}{namePadding} | {descriptionSubstring}{descriptionPadding} | {departmentSubstring}{departmentPadding} | {locationSubstring}{locationPadding} |");
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");

        }
        public  void AssignEmployeesAsync(string roleID)
        {
            RoleView role =  roleService.ViewRole(roleID);
            Console.WriteLine("Here is the list of Employees");
            foreach (EmployeeView emp in  empService.ViewAsync())
            {
                Console.WriteLine($"{emp.Id} : {emp.FirstName} {emp.LastName}");
            }
            Console.WriteLine("Enter number of Employees need to assign");
            int employeeCount = int.Parse(Console.ReadLine() ?? "0");
            if (employeeCount > 0)
            {
                for (int i = 0; i < employeeCount; i++)
                {
                    Console.WriteLine($"Enter Employee ID of Employee {i + 1}");
                    string? empid = Console.ReadLine();
                    if (empid != null &&  empService.checkIdAsync(empid))
                    {
                        EmployeeView updateEmp =  empService.ViewEmployeeAsync(empid);
                        updateEmp.RoleName = roleID;
                         empService.EditAsync(updateEmp);
                    }
                    else
                    {
                        Console.WriteLine("Invalid EmployeeID");
                        i--;
                    }
                }
            }
        }
        public  void ShowRoleEmployees()
        {
            Console.WriteLine("Enter RoleView Id");
            string roleId = Console.ReadLine() ?? "";
            if ( roleService.checkId(roleId))
            {
                List<EmployeeView> roleEmpList =  roleService.ShowEmployees(roleId);
                foreach (EmployeeView e in roleEmpList)
                {
                    Console.WriteLine($"{e.Id} : {e.FirstName} {e.LastName}");
                }
                if (roleEmpList.Count == 0)
                {
                    Console.WriteLine("No Employees Found");
                }
            }
            else
            {
                Console.WriteLine("Invalid RoleView ID");
            }
        }
        public  void EditRoleAsync()
        {
            Console.WriteLine("EDITING ROLE");
            Console.WriteLine("Enter RoleView ID");
            string searchId = Console.ReadLine() ?? "";
            if ( roleService.checkId(searchId))
            {
                RoleView foundRole =  roleService.ViewRole(searchId);
                Console.WriteLine("Select your choice to edit");
                int editChoice = 1;
                while (editChoice > 0)
                {
                    Console.WriteLine("1.Name\t2.Department\t3.Location\t0:Submit");
                    editChoice = int.Parse(Console.ReadLine() ?? "0");
                    switch (editChoice)
                    {
                        case 1:
                            Console.WriteLine("Enter new Name");
                            string? nameInput = Console.ReadLine() ?? "";
                            if (!validate.ValidateName(nameInput))
                            {
                                nameInput = ReTakeData(1);
                                if (!validate.ValidateName(nameInput))
                                {
                                    return;
                                }
                            }
                            foundRole.Name = nameInput;
                            break;
                        case 2:
                            Console.WriteLine("Enter new Department");
                            string? departmentInput = Console.ReadLine() ?? "";
                            if (!validate.ValidateName(departmentInput))
                            {
                                departmentInput = ReTakeData(1);
                                if (!validate.ValidateName(departmentInput))
                                {
                                    return;
                                }
                            }
                            foundRole.Department = departmentInput;
                            break;
                        case 3:
                            Console.WriteLine("Enter new Location");
                            string? locationInput = Console.ReadLine() ?? "";
                            if (!validate.ValidateName(locationInput))
                            {
                                locationInput = ReTakeData(1);
                                if (!validate.ValidateName(locationInput))
                                {
                                    return;
                                }
                            }
                            foundRole.Location = locationInput;
                            break;
                        case 0:
                            List<RoleView> editedRole = [foundRole];
                             roleService.Edit(foundRole);
                            Console.WriteLine("Operation Completed");
                             ViewRoles();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid role Id");
            }
        }
        public string ReTakeData(int choice)
        {
            int trail = 0;
            while (trail < 3)
            {
                Console.WriteLine("Try Entering Data Again");
                string? data = Console.ReadLine();
                bool ans;
                switch (choice)
                {
                    case 1:
                        ans = validate.ValidateName(data);
                        break;
                    case 2:
                        ans = validate.ValidateEmployeeId(data);
                        break;
                    case 3:
                        ans = validate.ValidateRoleId(data);
                        break;
                    case 4:
                        ans = validate.ValidateEmail(data);
                        break;
                    case 5:
                        ans = validate.ValidateMobileNo(data);
                        break;
                    default:
                        ans = false;

                        break;
                }
                if (ans)
                {
                    return data ?? "";
                }
                else
                {
                    trail++;
                }
            }
            Console.WriteLine("You have entered 3 times wrong! Operation Cancelled");
            return "";

        }
        public void InsertFakeData(){
            TestData testData=new TestData();
            foreach(var r in testData.GenarateRoles()){
            RoleView newRole=new();
            newRole.Id=r.Id;
            newRole.Name=r.Name;
            newRole.Department=r.Department;
            newRole.Location=r.Location;
            newRole.Description=r.Description;
             roleService.Insert(newRole);
            Console.WriteLine("New RoleView Added");
            }
        }
    }
}
