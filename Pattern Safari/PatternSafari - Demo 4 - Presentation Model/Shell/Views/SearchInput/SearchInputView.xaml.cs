using System.Windows;
using System.Windows.Controls;

namespace SearchApplication.Views.SearchInput
{
    public partial class SearchInputView : UserControl, ISearchInputView
    {
        public SearchInputView()
        {
            InitializeComponent();
        }

        public event SearchRequestEventHandler SearchRequested;

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            var searchInformation = new SearchRequestEventArgs();
            searchInformation.SearchText = _searchTextBox.Text;

            // Raise the search event
            if (SearchRequested != null)
            {
                SearchRequested(this, searchInformation);
            }
        }
    }
}
