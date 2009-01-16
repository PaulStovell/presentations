using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace Example1.WindowsFormsSearchQuery
{
    public abstract class SearchQuery<T> : INotifyPropertyChanged
    {
        private BindingList<T> _searchResults;
        private BackgroundWorker _backgroundWorker;
        private bool _isSearching;

        public SearchQuery()
        {
            _searchResults = new BindingList<T>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSearching
        {
            get { return _isSearching; }
            set
            {
                _isSearching = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("IsSearching"));
            }
        }

        public BindingList<T> SearchResults
        {
            get { return _searchResults; }
        }

        protected bool CancellationPending
        {
            get
            {
                if (_backgroundWorker == null)
                {
                    return false;
                }
                else
                {
                    return _backgroundWorker.CancellationPending;
                }
            }
        }

        protected abstract IEnumerable<T> SearchOnBackgroundThread();

        public void Execute()
        {
            if (_backgroundWorker != null && _backgroundWorker.IsBusy)
            {
                Cancel();
            }

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            _backgroundWorker.RunWorkerAsync();
            this.IsSearching = true;
        }

        public void Cancel()
        {
            if (_backgroundWorker != null && _backgroundWorker.IsBusy)
            {
                _backgroundWorker.CancelAsync();
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            IEnumerable<T> results = SearchOnBackgroundThread();
            if (!_backgroundWorker.CancellationPending)
            {
                e.Result = results;
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                this.IsSearching = false;
                if (e.Result != null && e.Result is IEnumerable)
                {
                    _searchResults.Clear();
                    foreach (T item in (IEnumerable)e.Result)
                    {
                        _searchResults.Add(item);
                    }
                }
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
    }
}
