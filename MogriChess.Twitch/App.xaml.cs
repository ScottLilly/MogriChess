using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MogriChess.WPF.Windows;

namespace MogriChess.WPF
{
    public partial class App : Application
    {
        private IServiceProvider ServiceProvider { get; set; }
        private IConfiguration Configuration { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Startup window
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.ShowDialog();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(MainWindow));
            services.AddTransient(typeof(Help));
            services.AddTransient(typeof(About));
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO: Put logging here
        }
    }
}