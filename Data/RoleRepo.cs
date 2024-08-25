using MyApplication.Data.Entites;
using Newtonsoft.Json;
namespace MyApplication.Data{
    public class RoleRepo:RoleBaseRepo,IRoleRepo
    {
        public  Role ViewOne(string id){
            Role role= base.ReadSingleDataAsync(id);
            return role;
        }
        public  bool checkId(string id){
            List<Role> RoleList= base.ReadDataAsync();
            return RoleList.Any(e=>e.Id==id);
        }
        public  List<Role> ViewAll(){
            List<Role> roleList= base.ReadDataAsync();
            return roleList;
        }
        public  void Insert(Role role){
            base.InsertAsync(role);
        }
        public  void Edit(Role role){
             base.EditAsync(role);
        }
        public  void Delete(string id){
             base.DeleteAsync(id);
        }
    }
}