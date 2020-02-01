using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;

namespace GitMoreOperations.VS2015.UI.ViewModels
{
    public class GitSkipFilesViewModel : ViewModelBase
    {
        public GitSkipFilesViewModel()
        {
            SkipFileCommand = new RelayCommand(x => SkipFile());
            UnSkipFileCommand = new RelayCommand(x => UnSkipFile());
        }

        public ICommand SkipFileCommand { get; private set; }
        public ICommand UnSkipFileCommand { get; private set; }

        public void SkipFile()
        {
            var wrapper = new VsGitWrapper(GitLogPage.ActiveRepoPath, GitLogPage.OutputWindow);
            wrapper.Log();
        }

        public void UnSkipFile()
        {
            var wrapper = new VsGitWrapper(GitLogPage.ActiveRepoPath, GitLogPage.OutputWindow);
            wrapper.FileLog();
        }

        public void Update()
        {
        }
    }
}
