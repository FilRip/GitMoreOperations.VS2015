using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TeamFoundation.Git.Extensibility;
using TeamExplorer.Common;

namespace GitMoreOperations.VS2015.UI
{
    [TeamExplorerPage(GitOperationsGuids.GitStashPage, Undockable = true)]
    public class GitStashPage : TeamExplorerBasePage
    {
        private static IGitExt gitService;
        private static ITeamExplorer teamExplorer;
        private static IVsOutputWindowPane outputWindow;
        private readonly GitStashPageUI ui;

        [ImportingConstructor]
        public GitStashPage([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
        {
            Title = "Git Stash";
            gitService = (IGitExt)serviceProvider.GetService(typeof(IGitExt));
            teamExplorer = (ITeamExplorer)serviceProvider.GetService(typeof(ITeamExplorer));
            gitService.PropertyChanged += OnGitServicePropertyChanged;

            var outWindow = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            var customGuid = new Guid("B602D8ED-2A92-4EB6-AE2C-A04F19386BAF");
            outWindow.CreatePane(ref customGuid, "Git stash", 1, 1);
            outWindow.GetPane(ref customGuid, out outputWindow);
            
            ui = new GitStashPageUI();
            PageContent = ui;
        }

        public static IGitRepositoryInfo ActiveRepo
        {
            get { return gitService.ActiveRepositories.FirstOrDefault(); }
        }

        public static IVsOutputWindowPane OutputWindow
        {
            get { return outputWindow; }
        }

        public static string ActiveRepoPath
        {
            get { return ActiveRepo.RepositoryPath; }
        }

        public override void Refresh()
        {
            ui.Refresh();
        }

        private void OnGitServicePropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Refresh();
        }

        public static void ActiveOutputWindow()
        {
            OutputWindow.Activate();
        }

        public static void ShowPage(string page)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() =>
                    teamExplorer.NavigateToPage(new Guid(page), null)));
        }
    }
}
