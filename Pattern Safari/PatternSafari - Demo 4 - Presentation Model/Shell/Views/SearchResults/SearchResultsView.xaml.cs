using System.Windows.Controls;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace SearchApplication.Views.SearchResults
{
    public partial class SearchResultsView : UserControl
    {
        private readonly IWindsorContainer _container;

        public SearchResultsView(IWindsorContainer container)
        {
            _container = container;
            InitializeComponent();
        }

        public SearchResultsViewModel ViewModel
        {
            get { return (SearchResultsViewModel)DataContext; }
            set { DataContext = value; }
        }

        private void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel = _container.Register(Component.For<SearchResultsViewModel>().LifeStyle.Transient).Resolve<SearchResultsViewModel>();
            ViewModel.Initialize();
        }
    }
}
