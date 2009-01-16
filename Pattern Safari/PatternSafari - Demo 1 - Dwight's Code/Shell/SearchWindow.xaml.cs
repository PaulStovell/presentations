using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Common;
using SearchApplication.Data;

namespace SearchApplication
{
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        
        }




        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            _list.Items.Clear();


            var text = _searchTextBox.Text;

            ThreadPool.QueueUserWorkItem(
                delegate
                {
                    // PERFORM THE SEARCH
                    Log.Write(string.Format("Performing the search for '{0}'", text));
                    var results = null as IEnumerable<Contact>;
                    using (var dataContext = new AdventureWorksDataContext())
                    {
                        var query = (from contact in dataContext.Contacts
                                       where contact.FirstName.StartsWith(text)
                                       select contact).Take(5);
                        results = query.ToList();
                    }
                    // TODO: PUT OTHER SEARCH PROVIDERS HERE



                    // Add the results to the results list
                    Dispatcher.Invoke(
                        DispatcherPriority.Normal,
                        new Action(
                            delegate
                            {
                                foreach (var result in results)
                                {
                                    _list.Items.Add(result);
                                }
                            }));
                }); 

            // TODO: CHANGE ADVERT HRE!!
        }
    }
}