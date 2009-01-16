using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BindingBasics.WindowsForms
{
    public class FilterableCollection<T> : INotifyPropertyChanged
    {
        private IEnumerable<T> _sourceCollection;
        private BindingList<T> _filteredCollection;
        private string _filterText;

        public FilterableCollection(IList<T> sourceCollection)
        {
            _sourceCollection = sourceCollection;
            _filteredCollection = new BindingList<T>();
            ApplyFilter();
        }

        public string FilterText
        {
            get { return _filterText ?? string.Empty; }
            set { 
                _filterText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FilterText"));
                ApplyFilter();
            }
        }

        public BindingList<T> FilteredCollection
        {
            get { return _filteredCollection; }
        }
        
        protected void ApplyFilter()
        {
            _filteredCollection.Clear();
            foreach (T item in _sourceCollection)
            {
                if (FilterItem(item)) 
                {
                    _filteredCollection.Add(item);
                }
            }
        }

        protected virtual bool FilterItem(T item)
        {
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }

        #endregion
    }
}
