using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;

namespace PollBindingExample {
    public class BindingDispatcher : DispatcherObject {
        private static readonly BindingDispatcher _current = new BindingDispatcher();
        private Queue<WeakReference> _notificationObjects;
        private readonly object _notificationObjectsSyncRoot = new object();
        private DispatcherTimer _dispatcherTimer;

        private BindingDispatcher() {
            _notificationObjects = new Queue<WeakReference>();
        }

        public static BindingDispatcher Current {
            get { return _current; }
        }

        public bool NotifyChanged(object targetObject) {
            lock (_notificationObjectsSyncRoot) {
                _notificationObjects.Enqueue(new WeakReference(targetObject, false));
                return true;
            }
        }

        public void TriggerEventNotifications() {
            VerifyAccess();
            lock (_notificationObjectsSyncRoot) {
                WeakReference targetObjectReference = null;
                while (_notificationObjects.Count > 0) {
                    targetObjectReference = _notificationObjects.Dequeue();
                    if (targetObjectReference.IsAlive) {
                        object targetObject = targetObjectReference.Target;
                        if (targetObject != null) {
                            IRaiseQueuedEvents domainObject = targetObject as IRaiseQueuedEvents;
                            if (domainObject != null) {
                                domainObject.RaiseQueuedEvents();
                            }
                        }
                    }
                }
            }
        }

        public void EnablePropertyChangedNotifications() {
            VerifyAccess();
            if (_dispatcherTimer == null) {
                _dispatcherTimer = new DispatcherTimer();
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
                _dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            }
            if (!_dispatcherTimer.IsEnabled) {
                _dispatcherTimer.Start();
            }
        }

        public void DisableNotifications() {
            VerifyAccess();
            if (_dispatcherTimer != null && _dispatcherTimer.IsEnabled) {
                _dispatcherTimer.Stop();
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e) {
            VerifyAccess();
            TriggerEventNotifications();
        }
    }
}
