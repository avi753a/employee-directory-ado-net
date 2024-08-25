using AutoMapper;
using MyApplication.Data;
using MyApplication.Data.Entites;
using MyApplication.Data.Models;
using MyApplication.Models;

namespace MyApplication.Services{
    public class RoleServices:IRoleServices
    {
        private IEmployeeRepo employeeAccess;
        private IRoleRepo roleAccess;
        private readonly IMapper _mapper;

        public RoleServices(IEmployeeRepo _employeeRepo,IRoleRepo _roleRepo,IMapper mapper){
                employeeAccess=_employeeRepo;
                roleAccess=_roleRepo;
                _mapper=mapper;
        }
        
       
        public  List<RoleView> View(){
            List<RoleView> roleViewList=[];
            foreach(var a in  roleAccess.ViewAll()){
                roleViewList.Add(_mapper.Map<Role,RoleView>(a));
            }
            return roleViewList;
        }
        public  void Insert(RoleView roleView){
            Role role = _mapper.Map<RoleView, Role>(roleView);
              roleAccess.Insert(role);
        }
        public  void Edit(RoleView roleView){
            Role role = _mapper.Map<RoleView, Role>(roleView);
            roleAccess.Edit(role);
        }
        public  void Delete(string id){
            roleAccess.Delete(id);
        }
        public  List<EmployeeView> ShowEmployees(string roleId)
        {
            RoleView r=_mapper.Map<Role,RoleView>(roleAccess.ViewOne(roleId));
            Console.WriteLine($"{roleId}-{r.Name}");
        return ( employeeAccess.ViewAllAsync()).Select(employee => _mapper.Map<Employee, EmployeeView>(employee)).Where(view => view.RoleName == r.Name).ToList();
        }
        public  RoleView ViewRole(string id){
            return _mapper.Map<Role,RoleView>( roleAccess.ViewOne(id));
        }
        public  bool checkId(string id){
            return   roleAccess.checkId(id);
        }

    }
}