using MyApplication.Models;

namespace MyApplication.Services
{
    public interface IRoleServices
    {
        public List<RoleView> View();
        public void Insert(RoleView roleView);
        public void Edit(RoleView roleView);
        public void Delete(string id);
        public List<EmployeeView> ShowEmployees(string roleId);
        public RoleView ViewRole(string id);
        public bool checkId(string id);
    }
}