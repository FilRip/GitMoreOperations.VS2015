using System.Windows.Controls;
using GitMoreOperations.VS2015.UI.ViewModels;

namespace GitMoreOperations.VS2015.UI.Views
{
    /// <summary>
    ///     Interaction logic for GitStashPageUI.xaml
    /// </summary>
    public partial class GitStashPageUI : UserControl
    {
        private readonly GitStashViewModel model;
        private bool _firstTime;

        public GitStashPageUI()
        {
            _firstTime = true;
            InitializeComponent();
            model = new GitStashViewModel();

            DataContext = model;
        }

        public void Refresh()
        {
            model.Update();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_firstTime)
            {
                _firstTime = false;
                model.Update();
            }
        }
    }
}