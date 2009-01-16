using System.Windows;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Infrastructure;
using Common.Threading;
using Search.Public;
using SearchApplication.Searching;
using SearchApplication.Views.SearchInput;
using SearchApplication.Views.SearchResults;

namespace SearchApplication
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
            container.Register(Component.For<ILog>().ImplementedBy<DwightsLog>().LifeStyle.Singleton);
            container.Register(Component.For<IDispatcher>().Instance(new WpfDispatcher(Dispatcher)));
            
            // Register the shell and other views
            container.Register(Component.For<ShellWindow>().LifeStyle.Transient);
            container.Register(Component.For<ISearchInputView>().ImplementedBy(typeof(SearchInputView)).LifeStyle.Singleton);
            container.Register(Component.For<SearchResultsView>().ImplementedBy(typeof(SearchResultsView)).LifeStyle.Singleton);
            
            // Register all available search providers
            container.Register(Component.For<ISearchProvider>().ImplementedBy(typeof (AdventureWorksSearchProvider)));

            // Setup the main window
            MainWindow = container.Resolve<ShellWindow>();
            MainWindow.Show();
        }
    }
}