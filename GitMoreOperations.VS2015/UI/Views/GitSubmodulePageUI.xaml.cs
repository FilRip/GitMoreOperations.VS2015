using System.Windows.Controls;
using GitMoreOperations.VS2015.UI.ViewModels;

namespace GitMoreOperations.VS2015.UI.Views
{
    /// <summary>
    ///     Interaction logic for GitSubmodulePageUI.xaml
    /// </summary>
    public partial class GitSubmodulePageUI : UserControl
    {
        private readonly GitSubmodulesViewModel model;

        public GitSubmodulePageUI()
        {
            InitializeComponent();
            model = new GitSubmodulesViewModel();

            DataContext = model;
        }

        public void Refresh()
        {
            model.Update();
        }
    }
}