using System.Windows.Controls;
using GitMoreOperations.VS2015.UI.ViewModels;

namespace GitMoreOperations.VS2015.UI.Views
{
    /// <summary>
    ///     Interaction logic for GitStashPageUI.xaml
    /// </summary>
    public partial class GitLogPageUI : UserControl
    {
        private readonly GitLogViewModel model;

        public GitLogPageUI()
        {
            InitializeComponent();
            model = new GitLogViewModel();

            DataContext = model;
        }

        public void Refresh()
        {
            model.Update();
        }
    }
}