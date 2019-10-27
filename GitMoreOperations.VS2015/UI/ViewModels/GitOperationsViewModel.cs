using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;

namespace GitMoreOperations.VS2015.UI.ViewModels
{
    public class GitOperationsViewModel
    {
        public GitOperationsViewModel()
        {
            PullCommand = new RelayCommand(x => GitPull());
            PushCommand = new RelayCommand(x => GitPush());
            CleanCommand = new RelayCommand(x => GitClean());
        }

        public ICommand PullCommand { get; private set; }
        public ICommand PushCommand { get; private set; }
        public ICommand CleanCommand { get; private set; }

        public void GitPull()
        {
            var wrapper = new VsGitWrapper(GitOperationsPage.ActiveRepoPath, GitOperationsPage.OutputWindow);
            List<string> options = new List<string>()
            {
                {"all"}
            };

            wrapper.Pull(options);
        }

        public void GitPush()
        {
            var wrapper = new VsGitWrapper(GitOperationsPage.ActiveRepoPath, GitOperationsPage.OutputWindow);
            wrapper.Push();
        }

        public void GitClean()
        {
            var wrapper = new VsGitWrapper(GitOperationsPage.ActiveRepoPath, GitOperationsPage.OutputWindow);
            wrapper.Clean();
        }

        public void Update()
        {
        }
    }
}