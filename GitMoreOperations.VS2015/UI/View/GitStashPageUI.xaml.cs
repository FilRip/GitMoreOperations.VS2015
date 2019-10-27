using System.Windows.Controls;
using GitMoreOperations.VS2015.UI.ViewModels;

namespace GitMoreOperations.VS2015.UI
{
    /// <summary>
    ///     Interaction logic for GitStashPageUI.xaml
    /// </summary>
    public partial class GitStashPageUI : UserControl
    {
        private readonly GitStashViewModel model;

        public GitStashPageUI()
        {
            InitializeComponent();
            model = new GitStashViewModel();

            DataContext = model;
        }

        public void Refresh()
        {
            model.Update();
        }
    }
}