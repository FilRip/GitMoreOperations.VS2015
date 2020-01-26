using System.Windows.Controls;
using GitMoreOperations.VS2015.UI.ViewModels;

namespace GitMoreOperations.VS2015.UI
{
    /// <summary>
    ///     Interaction logic for GitStashPageUI.xaml
    /// </summary>
    public partial class GitSkipFilesPageUI : UserControl
    {
        private readonly GitSkipFilesViewModel model;

        public GitSkipFilesPageUI()
        {
            InitializeComponent();
            model = new GitSkipFilesViewModel();

            DataContext = model;
        }

        public void Refresh()
        {
            model.Update();
        }
    }
}