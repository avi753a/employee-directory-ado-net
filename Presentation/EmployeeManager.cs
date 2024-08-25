using MyApplication.Infrastructure;
using MyApplication.Services;
using MyApplication.Models;
using Infrastructure;

namespace MyApplication.Presentation
{
    public class EmployeeManager:IEmployeeManager
    {
        private IValidation validate;
        private IEmployeeServices empService;
        private IRoleServices roleService;
        public EmployeeManager(IValidation _validate,IEmployeeServices _employeeServices,IRoleServices _roleServices)
        {
          validate=_validate;
          empService=_employeeServices;
          roleService=_roleServices;
        }

        public  void CreateEmployeeAsync()
        {
            Console.WriteLine("\n-------Creating new EmployeeView-----------\n");
            Console.WriteLine("Enter EmployeeView ID:");
            EmployeeRegistration newEmployee=new EmployeeRegistration();
            // string employeeId;
            // string? idValue = Console.ReadLine() ?? "";
            // if (!validate.ValidateEmployeeId(idValue))
            // {
            //     idValue = ReTakeData(2);
            //     if (!validate.ValidateEmployeeId(idValue))
            //     {
            //         return;
            //     }
            // }
            // else if ( empService.checkIdAsync(idValue))
            // {
            //     Console.WriteLine("Id Already Exists");
            // }
            // else
            // {
                // employeeId = idValue;
                Console.WriteLine("Enter First Name:");
                string firstName = Console.ReadLine() ?? "";
                if (!validate.ValidateName(firstName))
                {
                    firstName = ReTakeData(1);
                    if (!validate.ValidateName(firstName))
                    {
                        return;
                    }
                }
                Console.WriteLine("Enter Last Name:");
                string lastName = Console.ReadLine() ?? "";
                if (!validate.ValidateName(lastName))
                {
                    lastName = ReTakeData(1);
                    if (!validate.ValidateName(lastName))
                    {
                        return;
                    }
                }
                Console.WriteLine("Enter Date of Birth (YYYY-MM-DD) (optional):");
                DateTime? dateOfBirth = validate.ValidateDate(Console.ReadLine());
                Console.WriteLine("Enter Email ID:");
                string emailId = Console.ReadLine() ?? "__Empty";
                if (!validate.ValidateEmail(emailId))
                {
                    emailId = ReTakeData(4);
                    if (!validate.ValidateEmail(emailId))
                    {
                        return;
                    }
                }
                Console.WriteLine("Enter Mobile Number:");
                string mobileNo = Console.ReadLine() ?? "__Empty";
                if (!validate.ValidateMobileNo(mobileNo))
                {
                    mobileNo = "Invalid Change Later";
                }
                Console.WriteLine("Enter Department");
                 string department=Console.ReadLine()??"";
                if (!validate.ValidateName(department))
                {
                    department = ReTakeData(1);
                    if (!validate.ValidateName(department))
                    {
                        return;
                    }
                }
                Console.WriteLine("Enter Location");
                string location=Console.ReadLine()??"";
                if (!validate.ValidateName(location))
                {
                    location= ReTakeData(1);
                    if (!validate.ValidateName(location))
                    {
                        return;
                    }
                }
                Console.WriteLine("Assign Role\n Enter RoleID:");
                foreach (var temp in  roleService.View())
                {
                    Console.WriteLine($"{temp.Id} : {temp.Name}");
                }
                string role = Console.ReadLine() ?? "__Empty";
                if (!validate.ValidateRoleId(role))
                {
                    role= ReTakeData(3);
                    if (!validate.ValidateRoleId(role))
                    {
                        return;
                    }
                }
                Console.WriteLine("Assign Project\tEnter ProjectName:");
                string project = Console.ReadLine() ?? "__Empty";
                Console.WriteLine("Assign Project\tEnter ManagerName:");
                string managerName = Console.ReadLine() ?? "__Empty";
                if(!validate.ValidateName(managerName)){
                    managerName= ReTakeData(1);
                    if (!validate.ValidateName(managerName))
                    {
                        return;
                    }
                }
                // Create the employee object using the constructor
                // EmployeeView newEmployee = new Employee(employeeId, firstName, lastName, dateOfBirth, emailId, mobileNo, dateOfJoining, role, project,location,department,managerName);
                // newEmployee.Id=employeeId;
                newEmployee.FirstName=firstName;
                newEmployee.LastName=lastName;
                newEmployee.DateOfBirth=dateOfBirth;
                newEmployee.EmailId=emailId;
                newEmployee.MobileNo=mobileNo;
                newEmployee.RoleName=role;
                newEmployee.ProjectName=project;
                newEmployee.Location=location;
                newEmployee.Department=department;
                newEmployee.ManagerName=managerName;
                 empService.InsertAsync(newEmployee);
            }
        // }
        public  void DeleteEmployeeAsync()
        {
            Console.WriteLine("Enter ID to delete");
            string idToDelete = Console.ReadLine() ?? "";
            if (idToDelete.Length == 0)
            {
                Console.WriteLine("Invalid Input");
            }
            else if (!  empService.checkIdAsync(idToDelete))
            {
                Console.WriteLine("EmployeeView do not exist");
            }
            else{
                 empService.DeleteAsync(idToDelete);
            }
        }
        public  void SearchByLetter()
        {
            Console.WriteLine("Enter Letter to search");
            char letterToSearch = char.Parse(Console.ReadLine() ?? "-");
            if (char.IsLetter(letterToSearch))
            {
                var matchingEmployees = empService.SearchByLetterAsync(letterToSearch);
                 ViewEmployees(matchingEmployees);
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
        }
        public void ViewEmployees(List<EmployeeView> employees)
        {
            const int idLength = 12;  // EmployeeView ID
            const int nameLength = 17; // FirstName + LastName
            const int emailLength = 19;  // Email ID
            const int phoneLength = 15;  // Phone Number
            const int locationLength = 10; // Location
            const int departmentLength = 14; // Department
            const int roleLength = 14;   // Role
            const int projectLength = 10;  // Project
            const int managerNameLength = 12; // FirstName + LastName

            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| {"Employee ID",-idLength}| {" Name ",-nameLength} | {" Email ID ",-emailLength} | {" Phone Number ",-phoneLength} | {" Location ",-locationLength} | {" Department ",-departmentLength} | {" Role ",-roleLength} | {" Project ",-projectLength} | {" Manager ",-managerNameLength} |");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");

            foreach (var emp in employees)
            {
                string roleName = emp.RoleName??"";
                string fullName = $"{emp.FirstName} {emp.LastName}";
                string fullNameSubstring=fullName.Substring(0, Math.Min(fullName.Length, nameLength));
                string project = emp.ProjectName??"";
                string managerName = emp.ManagerName??"";
                string emailSubstring = (emp.EmailId??"").Substring(0, Math.Min((emp.EmailId??"").Length, emailLength));
                string phoneSubstring = (emp.MobileNo??"").Substring(0, Math.Min((emp.MobileNo??"").Length, phoneLength));
                string locationSubstring = (emp.Location??"").Substring(0, Math.Min((emp.Location??"").Length, locationLength));
                string departmentSubstring = (emp.Department??"").Substring(0, Math.Min((emp.Department??"").Length, departmentLength));
                string roleSubstring = roleName.Substring(0, Math.Min(roleName.Length, roleLength));
                string projectSubstring = project.Substring(0, Math.Min(project.Length, projectLength));
                string managerSubstring = managerName.Substring(0, Math.Min(managerName.Length, managerNameLength));


                Console.WriteLine($"| {emp.Id,-idLength}| {fullNameSubstring,-nameLength} | {emailSubstring,-emailLength} | {phoneSubstring,-phoneLength} | {locationSubstring,-locationLength} | {departmentSubstring,-departmentLength} | {roleSubstring,-roleLength} | {projectSubstring,-projectLength} | {managerSubstring,-managerNameLength} |");
            }

            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");
        }
        public  void ViewEmployees()
        {
             ViewEmployees( empService.ViewAsync());

        }
        public  void ViewSingleEmployee()
        {
            Console.WriteLine("Enter the employee ID");
            string getId = Console.ReadLine() ?? "";
            if ( empService.checkIdAsync(getId))
            {
                EmployeeView emp = empService.ViewEmployeeAsync(getId);
                Console.WriteLine("--------------------------------------");
                Console.WriteLine($"ID : {emp.Id}\nName : {emp.FirstName} {emp.LastName}\nEmailId : {emp.EmailId}\nMobileNo : {emp.MobileNo}");
                Console.WriteLine($"DateOfBirth : {emp.DateOfBirth}\nDateOfJoining : {emp.DateOfJoining}");
                Console.WriteLine($"Role : {emp.RoleName}\nDepartment : {emp.Department}\nLocation : {emp.Location}");
                Console.WriteLine($"Project : {emp.ProjectName}\nManager : {emp.ManagerName}");
                Console.WriteLine("--------------------------------------");

            }
        }
        public  void SortEmployees()
        {
            Console.WriteLine("Select your choice to sort");
            Console.WriteLine("1.EmployeeView Id\n2.FirstName\n3.LastName\n4.Dateof Birth\n5.Email id\n6.Location\n7.Department\n8.JobTItile\n9.Dateof Joining");
            int sortChoice = Int32.Parse(Console.ReadLine() ?? "0");
            List<EmployeeView> sortedData = [];
            if(sortChoice>0 && sortChoice<=9){
                sortedData= empService.SortAsync(sortChoice);
            }
            else{
                Console.WriteLine("Invalid choice");
                return;
            }
             ViewEmployees(sortedData);
        }
        public  void editEmployeeAsync()
        {
            Console.WriteLine("--Edit EmployeeView Id");
            Console.WriteLine("Enter EmployeeView ID");
            string searchId = Console.ReadLine() ?? "";
            if ( empService.checkIdAsync(searchId))
            {
                EmployeeView foundEmployee =  empService.GetDataToEdit(searchId);
                Console.WriteLine("Select your choice to edit");
                int editChoice = 1;
                while (editChoice > 0)
                {
                    Console.WriteLine("1.FirstName\t2.LastName\t3.Dateof Birth\t4.Email id\t5.Dateof Joining\t6.Role\t7.Project\t8.Location\t9.Department\t0:Submit");
                    editChoice = int.Parse(Console.ReadLine() ?? "0");
                    switch (editChoice)
                    {
                        case 1:
                            Console.WriteLine("Enter new FirstName");
                            string firstNameInput = Console.ReadLine() ?? "";
                            if (!validate.ValidateName(firstNameInput))
                            {
                                firstNameInput = ReTakeData(1);
                                if (!validate.ValidateName(firstNameInput))
                                {
                                    return;
                                }
                            }
                            foundEmployee.FirstName=firstNameInput;
                            break;
                        case 2:
                            Console.WriteLine("Enter new lastName");
                            string lastNameInput = Console.ReadLine() ?? "";
                            if (!validate.ValidateName(lastNameInput))
                            {
                                lastNameInput = ReTakeData(1);
                                if (!validate.ValidateName(lastNameInput))
                                {
                                    return;
                                }
                            }
                            foundEmployee.LastName=lastNameInput;
                            break;
                        case 3:
                            Console.WriteLine("Enter new DateOf Birth\n Follow format yyyy-mm-dd");
                            foundEmployee.DateOfBirth = validate.ValidateDate(Console.ReadLine());
                            break;
                        case 4:
                            Console.WriteLine("Enter new Email");
                            string emailInput = Console.ReadLine() ?? "";
                            if (!validate.ValidateEmail(emailInput))
                            {
                                emailInput= ReTakeData(4);
                                if (!validate.ValidateEmail(emailInput))
                                {
                                    return;
                                }
                            }
                            foundEmployee.EmailId=emailInput;
                            break;
                        case 5:
                            Console.WriteLine("Enter new Joining Date(yyyy-mm-dd)");
                            foundEmployee.DateOfJoining = validate.ValidateDate(Console.ReadLine());
                            break;
                        case 6:
                            Console.WriteLine("Enter new Role");
                            string roleIdInput = Console.ReadLine() ?? "";
                            if ( roleService.checkId(roleIdInput))
                            {
                                foundEmployee.RoleName = roleIdInput;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input");
                                return;
                            }
                            break;
                        case 7:
                            Console.WriteLine("Enter new Project Name");
                            string? projectIdInput = Console.ReadLine()??"";
                            if (validate.ValidateName(projectIdInput))
                            {
                                foundEmployee.ProjectName = projectIdInput;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input");
                            }
                            break;
                        case 8:
                            Console.WriteLine("Enter new location");
                            string locationInput = Console.ReadLine() ?? "";
                            if (!validate.ValidateName(locationInput))
                            {
                                locationInput = ReTakeData(1);
                                if (!validate.ValidateName(locationInput))
                                {
                                    return;
                                }
                            }
                            foundEmployee.Location=locationInput;
                            break;
                        case 9:
                            Console.WriteLine("Enter new Department");
                            string departmentInput = Console.ReadLine() ?? "";
                            if (!validate.ValidateName(departmentInput))
                            {
                                departmentInput = ReTakeData(1);
                                if (!validate.ValidateName(departmentInput))
                                {
                                    return;
                                }
                            }
                            foundEmployee.Department=departmentInput;
                            break;
                        case 0:
                            Console.WriteLine("Submitted");
                            List<EmployeeView> editedEmployeeList = [];
                            editedEmployeeList.Add(foundEmployee);
                             ViewEmployees(editedEmployeeList);
                             empService.EditAsync(foundEmployee);
                            break;
                        default:
                            Console.WriteLine("Wrong Input.. Action Not possible");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("EmployeeView Not Found");
                return;
            }
        }
        public  void ExportEmployeeToCsv()
        {
            empService.ExportEmployeeToCsvAsync();
            Console.WriteLine("Data exported to CSV");
        }
        public  void FilterEmployees()
        {
            int filterChoice=9; 
            string filterOption="";
            while(filterChoice>0){
                Console.WriteLine("Filter Options 1.Role\t 2.Department\t 3.Location");
                filterChoice=int.Parse(Console.ReadLine()??"0");
                switch(filterChoice){
                    case 1:
                        Console.WriteLine("Enter Role id");
                        filterOption=Console.ReadLine()??"";
                        if(!validate.ValidateRoleId(filterOption)){
                            Console.WriteLine("Invalid role id");
                            return;
                        }
                        break;
                    case 2:
                         Console.WriteLine("Enter Departent");
                        filterOption=Console.ReadLine()??"";
                        if(!validate.ValidateName(filterOption)){
                            Console.WriteLine("Invalid role id");
                            return;
                        }
                        break;
                    case 3:
                         Console.WriteLine("Enter Department");
                         filterOption=Console.ReadLine()??"";
                        if(!validate.ValidateName(filterOption)){
                            Console.WriteLine("Invalid role id");
                            return;
                        }
                        break;
                    case 0:
                        Console.WriteLine("Exiting");
                        return;
                    default:
                        Console.WriteLine("Invalid Input Exited");
                        return;
                }
                 ViewEmployees( empService.filterAsync(filterChoice,filterOption));

            }
        }
        public string ReTakeData(int choice)
        {
            int trail=0;
            while(trail<3){
                Console.WriteLine("Try Entering Data Again");
                string? data=Console.ReadLine();
                bool ans;
                switch(choice){
                    case 1:
                        ans=validate.ValidateName(data);
                        break;
                    case 2:
                        ans=validate.ValidateEmployeeId(data);
                        break;
                    case 3:
                        ans=validate.ValidateRoleId(data);
                        break;
                    case 4:
                        ans=validate.ValidateEmail(data);
                        break;
                    case 5:
                        ans=validate.ValidateMobileNo(data);
                        break;
                    default:
                        ans=false;
                    
                        break;
                }
                if(ans){
                    return data??"";
                }
                else{
                    trail++;
                }
            }
            Console.WriteLine("You have entered 3 times wrong! Operation Cancelled");
            return "";

        }
        public void InsertFakeData(){
            TestData testData=new TestData();
            foreach(var e in testData.GenerateEmployees()){
                EmployeeRegistration newEmployee=new EmployeeRegistration();
                newEmployee.FirstName=e.FirstName;
                newEmployee.LastName=e.LastName;
                newEmployee.DateOfBirth=e.DateOfBirth;
                newEmployee.EmailId=e.EmailId;
                newEmployee.MobileNo=e.MobileNo;
                newEmployee.RoleName=e.RoleName;
                newEmployee.ProjectName=e.ProjectName;
                newEmployee.Location=e.Location;
                newEmployee.Department=e.Department;
                newEmployee.ManagerName=e.ManagerName;
                 empService.InsertAsync(newEmployee);
            }
        }

    }
}