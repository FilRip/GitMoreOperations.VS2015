using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;

namespace GitMoreOperations.VS2015.UI.ViewModels
{
    public class GitLogViewModel
    {
        public GitLogViewModel()
        {
            LogCommand = new RelayCommand(x => Log());
            FileLogCommand = new RelayCommand(x => FileLog());
        }

        public ICommand LogCommand { get; private set; }
        public ICommand FileLogCommand { get; private set; }

        public void Log()
        {
            var wrapper = new VsGitWrapper(GitLogPage.ActiveRepoPath, GitLogPage.OutputWindow);
            wrapper.Log();
        }

        public void FileLog()
        {
            var wrapper = new VsGitWrapper(GitLogPage.ActiveRepoPath, GitLogPage.OutputWindow);
            wrapper.FileLog();
        }

        public void Update()
        {
        }
    }
}