using MyApplication.Data.Entites;

namespace MyApplication.Data{
    public interface IRoleRepo
    {
        public Role ViewOne(string id);
        public bool checkId(string id);
        public List<Role> ViewAll();
        public void Insert(Role role);
        public void Edit(Role role);
        public void Delete(string id);
    }
}