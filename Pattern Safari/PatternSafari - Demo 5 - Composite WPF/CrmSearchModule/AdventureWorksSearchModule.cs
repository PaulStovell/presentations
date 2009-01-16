using AdventureWorksSearch.Searching;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Infrastructure;
using Microsoft.Practices.Composite.Modularity;
using Search.Public;

namespace AdventureWorksSearch
{
    public class AdventureWorksSearchModule : IModule
    {
        private readonly IWindsorContainer _container;

        public AdventureWorksSearchModule(IWindsorContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.Resolve<IApplicationResourcesManager>().LoadResources("/AdventureWorksSearch;Component/Resources/ModuleResources.xaml");

            _container.Register(Component.For<ISearchProvider>().ImplementedBy<AdventureWorksSearchProvider>());
        }
    }
}