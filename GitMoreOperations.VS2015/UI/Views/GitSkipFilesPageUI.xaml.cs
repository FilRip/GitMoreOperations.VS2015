using GitMoreOperations.VS2015.UI.ViewModels;
using System.Windows.Controls;

namespace GitMoreOperations.VS2015.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour GitSkipFilesPageUI.xaml
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
