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
using EnvDTE;

namespace GitMoreOperations.VS2015.UI
{
    [TeamExplorerPage(GitOperationsGuids.GitLogPage, Undockable = true)]
    public class GitLogPage : TeamExplorerBasePage
    {
        private static IGitExt gitService;
        private static ITeamExplorer teamExplorer;
        private static IVsOutputWindowPane outputWindow;
        private readonly GitLogPageUI ui;
        private static DTE ServiceDTE;

        [ImportingConstructor]
        public GitLogPage([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
        {
            Title = "Git Journal";

            ServiceDTE = (DTE)serviceProvider.GetService(typeof(DTE));

            gitService = (IGitExt)serviceProvider.GetService(typeof(IGitExt));
            teamExplorer = (ITeamExplorer)serviceProvider.GetService(typeof(ITeamExplorer));
            gitService.PropertyChanged += OnGitServicePropertyChanged;

            var outWindow = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            var customGuid = new Guid("9E717339-5502-4A0B-B04E-3D86AFE31F50");
            outWindow.CreatePane(ref customGuid, "Git journal", 1, 1);
            outWindow.GetPane(ref customGuid, out outputWindow);

            ui = new GitLogPageUI();
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

        public static string ActiveFileName
        {
            get { return ServiceDTE.ActiveDocument.FullName; }
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
