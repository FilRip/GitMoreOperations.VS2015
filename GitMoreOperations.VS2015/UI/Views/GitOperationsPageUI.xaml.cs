using System.Windows.Controls;
using GitMoreOperations.VS2015.UI.ViewModels;

namespace GitMoreOperations.VS2015.UI.Views
{
    /// <summary>
    ///     Interaction logic for GitOperationsPageUI.xaml
    /// </summary>
    public partial class GitOperationsPageUI : UserControl
    {
        private readonly GitOperationsViewModel model;

        public GitOperationsPageUI()
        {
            InitializeComponent();
            model = new GitOperationsViewModel();

            DataContext = model;
        }

        public void Refresh()
        {
            model.Update();
        }
    }
}