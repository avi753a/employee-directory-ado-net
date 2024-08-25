using MyApplication.Models;

namespace MyApplication.Presentation{
    public interface IEmployeeManager{
        public void CreateEmployeeAsync();
        public  void DeleteEmployeeAsync();
        public  void SearchByLetter();
        public  void ViewEmployees(List<EmployeeView> employees);
        public  void ViewEmployees();
        public void ViewSingleEmployee();
        public  void SortEmployees();
        public  void editEmployeeAsync();
        public  void ExportEmployeeToCsv();
        public  void FilterEmployees();
        public void InsertFakeData();
    }
    
}