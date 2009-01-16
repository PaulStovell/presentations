using System.Windows;
using SearchApplication.Searching;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Infrastructure;
using Common.Threading;
using Search.Public;
using SearchApplication.Views.SearchInput;
using SearchApplication.Views.SearchResults;

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
            
            // Register the shell and other views
            container.Register(Component.For<ShellWindow>().LifeStyle.Transient);
            container.Register(Component.For<ISearchInputView>().ImplementedBy(typeof(SearchInputView)).LifeStyle.Singleton);
            container.Register(Component.For<ISearchResultsView>().ImplementedBy(typeof(SearchResultsView)).LifeStyle.Singleton);
            container.Register(Component.For<SearchResultsPresenter>().ImplementedBy(typeof(SearchResultsPresenter)).LifeStyle.Singleton);
            
            // Register all available search providers
            container.Register(Component.For<ISearchProvider>().ImplementedBy(typeof (AdventureWorksSearchProvider)));

            // Setup the main window
            MainWindow = container.Resolve<ShellWindow>();
            MainWindow.Show();
        }
    }
}