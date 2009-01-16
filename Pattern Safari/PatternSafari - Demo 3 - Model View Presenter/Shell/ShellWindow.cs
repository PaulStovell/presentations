using System.Windows;
using Castle.Windsor;
using SearchApplication.Views.SearchInput;
using SearchApplication.Views.SearchResults;

namespace Shell
{
    public partial class ShellWindow : Window
    {
        public ShellWindow(IWindsorContainer container)
        {
            InitializeComponent();
            
            _inputContainer.Content = container.Resolve<ISearchInputView>();

            var presenter = container.Resolve<SearchResultsPresenter>();
            _resultsContainer.Content = presenter.View;
        }
    }
}