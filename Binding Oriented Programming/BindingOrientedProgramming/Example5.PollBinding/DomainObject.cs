using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Threading;

namespace PollBindingExample {
    public abstract class DomainObject : INotifyPropertyChanged, IRaiseQueuedEvents {
        public event PropertyChangedEventHandler PropertyChanged;
        private Queue<PropertyChangedEventArgs> _queuedPropertyChangeNotifications;
        private readonly object _queuedPropertyChangeNotificationsSyncRoot = new object();

        protected DomainObject() {
            _queuedPropertyChangeNotifications = new Queue<PropertyChangedEventArgs>();
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) {
            if (e != null)
            {
                lock (_queuedPropertyChangeNotificationsSyncRoot)
                {
                    _queuedPropertyChangeNotifications.Enqueue(e);
                }
                BindingDispatcher.Current.NotifyChanged(this);
            }

            //if (this.PropertyChanged != null) {
            //    this.PropertyChanged(this, e);
            //}
        }

        public void RaiseQueuedEvents() {
            lock (_queuedPropertyChangeNotificationsSyncRoot) {
                while (_queuedPropertyChangeNotifications.Count > 0) {
                    PropertyChangedEventArgs queuedEvent = _queuedPropertyChangeNotifications.Dequeue();
                    if (this.PropertyChanged != null) {
                        this.PropertyChanged(this, queuedEvent);
                    }
                }
            }
        }
    }
}
