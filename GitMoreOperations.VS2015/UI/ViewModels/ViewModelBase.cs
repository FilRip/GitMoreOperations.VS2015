using System.ComponentModel;

namespace GitMoreOperations.VS2015.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedLocal = PropertyChanged;
            if (propertyChangedLocal != null)
            {
                propertyChangedLocal(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
