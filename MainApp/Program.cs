using Business.Services;
using System.Transactions;
using Business.Interfaces;
using Business.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Console_MainApp.Dialogues;
class Program
{
    static void Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder().ConfigureServices((config, services) =>
        {
            services.AddTransient<IMenuService, MenuService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddTransient<Helpers>();
            services.AddSingleton<IContactService, ContactService>();
            services.AddSingleton(new FileServiceConfig
            {
                DirectoryPath = "Data",
                FileName = "contactList.json"
            });

        }).Build();

        using (var scope = host.Services.CreateScope())
        {
            var IMenuService = scope.ServiceProvider.GetRequiredService<IMenuService>();
            IMenuService.MainMenu();
        }
    }
}
