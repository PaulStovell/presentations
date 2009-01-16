using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Search.Views.SearchInput;
using Search.Views.SearchResults;

namespace Search
{
    public class SearchModule : IModule
    {
        private readonly IWindsorContainer _container;

        public SearchModule(IWindsorContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            var inputView = _container.Register(Component.For<SearchInputView>().LifeStyle.Transient).Resolve<SearchInputView>();
            var resultsView = _container.Register(Component.For<SearchResultsView>().LifeStyle.Transient).Resolve<SearchResultsView>();

            _container.Resolve<IRegionManager>().Regions["TopRegion"].Add(inputView);
            _container.Resolve<IRegionManager>().Regions["ContentRegion"].Add(resultsView);
        }
    }
}
