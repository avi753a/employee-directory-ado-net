
using MyApplication.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MyApplication.Data.Entites;
using MyApplication.Presentation;
using MyApplication.Services;
using System.Data.SqlClient;
using MyApplication.Data;
namespace MyApplication.Hello
{
    class HelloWorld
    {
        private readonly Mainmenu _mainmenu;
        public HelloWorld(Mainmenu mainmenu)
        {
            _mainmenu = mainmenu;
        }
        public static  void Main()
        {

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IValidation, Validation>()
                .AddSingleton<IRepo<Employee>, EmployeeBaseRepo>()
                .AddSingleton<IRepo<Role>, RoleBaseRepo>()
                .AddSingleton<IRoleRepo, RoleRepo>()
                .AddSingleton<IEmployeeRepo, EmployeeRepo>()
                .AddAutoMapper(typeof(MapperService))
                .AddSingleton<IEmployeeServices, EmployeeServices>()
                .AddSingleton<IRoleServices, RoleServices>()
                .AddSingleton<IEmployeeManager, EmployeeManager>()
                .AddSingleton<IRoleManager, RoleManager>()
                .AddSingleton<IMainmenu, Mainmenu>()
                .BuildServiceProvider();
            var consolePresentation = serviceProvider.GetRequiredService<IMainmenu>();
             consolePresentation.EntryAsync();

        }
    }
}

