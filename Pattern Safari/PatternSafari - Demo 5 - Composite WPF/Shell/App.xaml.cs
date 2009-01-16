using System.Windows;
using System.Windows.Controls;
using AdventureWorksSearch;
using Advertising;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Infrastructure;
using Common.Threading;
using CompositeWPFContrib.Composite.WindsorExtensions;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Composite.Wpf.Regions;
using Search;

namespace Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize the IOC container and some of the default services
            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
            container.Register(Component.For<IWindsorContainer>().Instance(container));
            container.Register(Component.For<ILog>().ImplementedBy<DwightsLog>());
            container.Register(Component.For<IDispatcher>().Instance(new WpfDispatcher(Dispatcher)));
            container.Register(Component.For<ShellWindow>().LifeStyle.Transient);
            container.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifeStyle.Singleton);
            container.Register(Component.For<IRegionManager>().ImplementedBy<RegionManager>().LifeStyle.Singleton);
            container.Register(Component.For<IApplicationResourcesManager>().ImplementedBy<ApplicationResourcesManager>().LifeStyle.Singleton);

            // Setup the region adapter mappings
            var regionMappings = new RegionAdapterMappings();
            regionMappings.RegisterMapping(typeof(ContentControl), new ContentControlRegionAdapter());
            regionMappings.RegisterMapping(typeof(ItemsControl), new ItemsControlRegionAdapter());
            container.Register(Component.For<RegionAdapterMappings>().Instance(regionMappings));

            // Setup the main window
            MainWindow = container.Resolve<ShellWindow>();
            MainWindow.Show();

            // Define the modules to load
            var modules = new StaticModuleEnumerator()
                .AddModule(typeof(SearchModule))
                .AddModule(typeof(AdvertisingModule))
                .AddModule(typeof(AdventureWorksSearchModule));
            
            // Load the modules
            var moduleLoader = new ModuleLoader(new WindsorContainerAdapter(container), new TraceLogger());
            moduleLoader.Initialize(modules.GetModules());
        }
    }
}