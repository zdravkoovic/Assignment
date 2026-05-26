using Assignment.ViewModels;
using Caliburn.Micro;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Assignment.Strategies;

namespace Assignment
{
    public class Bootstrapper : BootstrapperBase
    {
        private ServiceProvider _provider;
        public Bootstrapper()
        {
            Initialize();
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync<ShellViewModel>();
            Application.Current.MainWindow.Title = "TBP Software";
        }

        protected override void Configure()
        {
            base.Configure();

            var services = new ServiceCollection();

            // Caliburn core services
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<IEventAggregator, EventAggregator>();

            services.AddSingleton<ShellViewModel>();
            services.AddSingleton<ToDoSubmitViewModel>();
            services.AddSingleton<ToDoListViewModel>();
            services.AddTransient<LoadersViewModel>();
            services.AddSingleton<Func<LoadersViewModel>>(sp =>
            {
                return () => sp.GetRequiredService<LoadersViewModel>();
            });

            services.AddSingleton<ISortStrategy, LinearSortStrategy>();

            _provider = services.BuildServiceProvider();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _provider.GetRequiredService(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _provider.GetServices(service);
        }

        protected override void BuildUp(object instance)
        {
        }
    }
}
