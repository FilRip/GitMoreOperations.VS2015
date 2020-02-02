using GitMoreOperations.VS2015.UI.Views;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TeamFoundation.Git.Extensibility;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using TeamExplorer.Common;

namespace GitMoreOperations.VS2015.UI
{
    [TeamExplorerPage(GitOperationsGuids.GitSkipFilesPage, Undockable = true)]
    public class GitSkipFilesPage : TeamExplorerBasePage
    {
        private static IGitExt gitService;
        private static ITeamExplorer teamExplorer;
        private static IVsOutputWindowPane outputWindow;
        private readonly GitSkipFilesPageUI ui;
        private static IVsOutputWindowPane hiddenOutput;

        [ImportingConstructor]
        public GitSkipFilesPage([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
        {
            Title = "Git SkipFiles";
            gitService = (IGitExt)serviceProvider.GetService(typeof(IGitExt));
            teamExplorer = (ITeamExplorer)serviceProvider.GetService(typeof(ITeamExplorer));
            gitService.PropertyChanged += OnGitServicePropertyChanged;

            var outWindow = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            var customGuid = new Guid("BCAD885C-621F-4FFE-A746-E6AC35213F34");
            outWindow.CreatePane(ref customGuid, "Git SkipFiles", 1, 1);
            outWindow.GetPane(ref customGuid, out outputWindow);

            hiddenOutput = new Service.HiddenGitOutput();

            ui = new GitSkipFilesPageUI();
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

        public static IVsOutputWindowPane MyHiddenGitOutput
        {
            get { return hiddenOutput; }
        }

        public static void ShowPage(string page)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() =>
                    teamExplorer.NavigateToPage(new Guid(page), null)));
        }
    }
}
