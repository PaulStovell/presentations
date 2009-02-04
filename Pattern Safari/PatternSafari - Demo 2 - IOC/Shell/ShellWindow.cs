﻿using System.Windows;
using Castle.Windsor;
using SearchApplication.Views.SearchInput;
using SearchApplication.Views.SearchResults;

namespace SearchApplication
{
    public partial class ShellWindow : Window
    {
        public ShellWindow(IWindsorContainer container)
        {
            InitializeComponent();
            
            _inputContainer.Content = container.Resolve<ISearchInputView>();
            _resultsContainer.Content = container.Resolve<SearchResultsView>();
        }
    }
}