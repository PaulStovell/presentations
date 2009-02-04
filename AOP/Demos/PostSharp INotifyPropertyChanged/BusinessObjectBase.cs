using System.ComponentModel;
using Demo.Aspects;

namespace Demo
{
    public class BusinessObjectBase : INotifyPropertyChanged, ICanRaisePropertyChangedEvents
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void ICanRaisePropertyChangedEvents.RaisePropertyChangedEvent(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
    }
}
