using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;

namespace GitMoreOperations.VS2015.UI.ViewModels
{
    public class GitStashViewModel
    {
        public GitStashViewModel()
        {
            StashCommand = new RelayCommand(x => Stash());
            PopStashCommand = new RelayCommand(x => PopStash());
            ListStashCommand = new RelayCommand(x => listStash());
        }

        public ICommand StashCommand { get; private set; }
        public ICommand PopStashCommand { get; private set; }
        public ICommand ListStashCommand { get; private set; }

        public void Stash()
        {
            var wrapper = new VsGitWrapper(GitStashPage.ActiveRepoPath, GitStashPage.OutputWindow);
            wrapper.Stash();
        }

        public void PopStash()
        {
            var wrapper = new VsGitWrapper(GitStashPage.ActiveRepoPath, GitStashPage.OutputWindow);
            wrapper.PopStash();
        }

        public void listStash()
        {
            var wrapper = new VsGitWrapper(GitStashPage.ActiveRepoPath, GitStashPage.OutputWindow);
            wrapper.ListStash();
        }

        public void Update()
        {
        }
    }
}