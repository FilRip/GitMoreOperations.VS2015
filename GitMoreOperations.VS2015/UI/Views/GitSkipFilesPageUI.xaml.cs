using GitMoreOperations.VS2015.UI.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GitMoreOperations.VS2015.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour GitSkipFilesPageUI.xaml
    /// </summary>
    public partial class GitSkipFilesPageUI : UserControl
    {
        private readonly GitSkipFilesViewModel model;
        private bool _firstTime;

        public GitSkipFilesPageUI()
        {
            InitializeComponent();
            model = new GitSkipFilesViewModel();

            _firstTime = true;
            DataContext = model;
        }

        public void Refresh()
        {
            model.Update();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_firstTime)
            {
                _firstTime = false;
                model.Update();
            }
        }
    }
}
