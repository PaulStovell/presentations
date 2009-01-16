using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TimesheetEntrySystem.DomainModel {
    public abstract class Entity : INotifyPropertyChanged {
        public Entity() {

        }

        public abstract bool HasSameIdentity(Entity otherEntity);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged(this, e);
            }
        }
    }
}
