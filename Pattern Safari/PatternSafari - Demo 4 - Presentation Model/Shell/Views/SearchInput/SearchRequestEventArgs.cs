using System;

namespace SearchApplication.Views.SearchInput
{
    public delegate void SearchRequestEventHandler(object sender, SearchRequestEventArgs e); 

    public class SearchRequestEventArgs : EventArgs
    {
        public string SearchText { get; set; }
    }
}