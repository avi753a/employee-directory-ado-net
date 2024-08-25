using MyApplication.Models;

namespace MyApplication.Services
{
    public interface IEmployeeServices
    {
        public void InsertAsync(EmployeeRegistration employeeView);
        public void EditAsync(EmployeeView employeeView);
        public void DeleteAsync(string id);
        public List<EmployeeView> ViewAsync();
        public EmployeeView ViewEmployeeAsync(string id);
        public List<EmployeeView> SortAsync(int sortChoice);
        public List<EmployeeView> SearchByLetterAsync(char letterToSearch);
        public void ExportEmployeeToCsvAsync();
        public bool checkIdAsync(string id);
        public List<EmployeeView> filterAsync(int choice, string filterOption);
        public EmployeeView GetDataToEdit(string id);

    }
}