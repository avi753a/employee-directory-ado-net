namespace MyApplication.Presentation{
    public interface IRoleManager{
        public void CreateRoleAsync();
        public void DeleteRoleAsync();
        public void ViewRoles();
        public void AssignEmployeesAsync(string roleID);
        public void ShowRoleEmployees();
        public void EditRoleAsync();
         public string ReTakeData(int choice);
         public void InsertFakeData();
    }
}