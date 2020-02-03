namespace GitMoreOperations.VS2015.UI.ViewModels
{
    public class SelectionItemListBox : ViewModelBase
    {
        private string _item;
        private bool _estSelectionne;

        public string Item
        {
            get { return _item; }
            set
            {
                _item = value;
                this.RaisePropertyChanged(nameof(Item));
            }
        }

        public bool IsSelected
        {
            get { return _estSelectionne; }
            set
            {
                _estSelectionne = value;
                this.RaisePropertyChanged(nameof(IsSelected));
            }
        }
    }
}
