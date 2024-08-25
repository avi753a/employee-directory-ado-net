using MyApplication.Data.Entites;
using MyApplication.Data;
using System.Globalization;
using System.Text;
using MyApplication.Models;
using CsvHelper;
using AutoMapper;
using MyApplication.Data.Models;

namespace MyApplication.Services
{
    public class EmployeeServices:IEmployeeServices
    {

        private readonly IEmployeeRepo _employeeAccess;
        private readonly IRoleRepo _roleAccess;
        private readonly IMapper _mapper;
        public EmployeeServices(IEmployeeRepo employeeAccess, IRoleRepo roleAccess,IMapper mapper)
        {
            _employeeAccess = employeeAccess;
            _roleAccess = roleAccess;
            _mapper=mapper;
        }
        
        public  void InsertAsync(EmployeeRegistration employeeView)
        {
            Employee employee = _mapper.Map<EmployeeRegistration, Employee>(employeeView);
            employee.Id= genrarateEmployeeId();
             _employeeAccess.InsertAsync(employee);
        }
        public  void EditAsync(EmployeeView employeeView)
        {
            Employee employee = _mapper.Map<EmployeeView, Employee>(employeeView);
             _employeeAccess.EditAsync(employee);
        }
        public  void DeleteAsync(string id)
        {
             _employeeAccess.DeleteAsync(id);
        }
        public  List<EmployeeView> ViewAsync()
        {
            List<Employee> employee=  _employeeAccess.ViewAllAsync();
            Console.WriteLine(employee.Count);
            List<EmployeeView> employeeViewList=[];
            foreach(var a in employee){
                employeeViewList.Add(_mapper.Map<Employee,EmployeeView>(a));
            }
            return employeeViewList;
        }
        public  EmployeeView ViewEmployeeAsync(string id)
        {
            Employee employee= _employeeAccess.ViewOneAsync(id);
            return _mapper.Map<Employee,EmployeeView>(employee);
        }
        public  List<EmployeeView> SortAsync(int sortChoice)
        {
            List<EmployeeView> employeeViewList=[];
            foreach(var a in  _employeeAccess.SortAsync(sortChoice)){
                employeeViewList.Add(_mapper.Map<Employee,EmployeeView>(a));
            }
            return employeeViewList;
        }
        public  List<EmployeeView> SearchByLetterAsync(char letterToSearch)
        {
            List<EmployeeView> employeeViewList=[];
            foreach(var a in  _employeeAccess.SearchByLetterAsync(letterToSearch)){
                employeeViewList.Add(_mapper.Map<Employee,EmployeeView>(a));
            }
            return employeeViewList;
        }
        public  void ExportEmployeeToCsvAsync()
        {
            string filePath = "C:\\Users\\aravind.a\\OneDrive - Technovert\\Documents\\DotnetApp\\Presentation\\EmployeeView.csv";
            // Use CultureInfo.InvariantCulture for consistent formatting
            List<Employee> employee =  _employeeAccess.ViewAllAsync();
            List<EmployeeView> employees = employee.Select(a => _mapper.Map<Employee, EmployeeView>(a)).ToList();
            using var writer = new StreamWriter(filePath, false, new UTF8Encoding(false)); // Replace 'false' with 'true' to append to existing file
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            // Write headers (optional)
            csv.WriteField("EmployeeView ID");
            csv.WriteField("First Name");
            csv.WriteField("Last Name");
            csv.WriteField("Email Id");
            csv.WriteField("Mobile No");
            csv.WriteField("Job Title");
            csv.WriteField("Location");
            csv.WriteField("Department");
            csv.WriteField("Project");
            csv.WriteField("Date of Birth"); // Assuming format yyyy-MM-dd
            csv.WriteField("Date of Joining"); // Assuming format yyyy-MM-dd
            csv.WriteField("Manager");
            csv.NextRecord(); // Move to the next line after headers
            foreach (var emp in employees)
            {
                csv.WriteField(emp.Id);
                csv.WriteField(emp.FirstName);
                csv.WriteField(emp.LastName);
                csv.WriteField(emp.EmailId);
                csv.WriteField(emp.MobileNo);
                csv.WriteField(emp.RoleName);
                csv.WriteField(emp.Location);
                csv.WriteField(emp.Department);
                csv.WriteField(emp.ProjectName);
                csv.WriteField(emp.DateOfBirth?.ToString("yyyy-MM-dd")); // Assuming format yyyy-MM-dd
                csv.WriteField(emp.DateOfJoining?.ToString("yyyy-MM-dd")); // Assuming format yyyy-MM-dd
                csv.WriteField(emp.ManagerName);
                csv.NextRecord();
            }
        }
        public  bool checkIdAsync(string id)
        {
            return  _employeeAccess.checkIdAsync(id);
        }
        public  List<EmployeeView> filterAsync(int choice, string filterOption)
        {
            
            List<EmployeeView> employeeViewList=[];
            foreach(var a in  _employeeAccess.filterAsync(choice, filterOption)){
                employeeViewList.Add(_mapper.Map<Employee,EmployeeView>(a));
            }
            return employeeViewList;
        }
        public EmployeeView GetDataToEdit(string id){
            Employee employee= _employeeAccess.ViewOneAsync(id);
            return _mapper.Map<Employee,EmployeeView>(employee);

        }
        public  String genrarateEmployeeId(){
            Employee? employee=( _employeeAccess.ViewAllAsync()).OrderByDescending(e=>e.Id).FirstOrDefault();
            int idNo=1;
            if(employee!=null){
                 idNo=int.Parse(employee.Id[2..6])+1;
            }
                return "TZ"+idNo.ToString("D4");

        }

    }
}