using Advertising.Views.Advert;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;

namespace Advertising
{
    public class AdvertisingModule : IModule
    {
        private readonly IWindsorContainer _container;

        public AdvertisingModule(IWindsorContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            var advertView = _container.Register(Component.For<AdvertView>().LifeStyle.Transient).Resolve<AdvertView>();
            
            _container.Resolve<IRegionManager>().Regions["SidebarRegion"].Add(advertView);
        }
    }
}
