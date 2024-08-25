using MyApplication.Data.Entites;
using MyApplication.Data.Models;

namespace MyApplication.Data
{
    public interface IEmployeeRepo 
    {
        public void InsertAsync(Employee employee);
        public void EditAsync(Employee employee);
        public void DeleteAsync(string id);
        public List<Employee> ViewAllAsync();
        public Employee ViewOneAsync(string id);
        public List<Employee> SortAsync(int sortChoice);
        public List<Employee> SearchByLetterAsync(char letterToSearch);
        public bool checkIdAsync(string id);
        public List<Employee> filterAsync(int choice, string filterOption);


    }
}