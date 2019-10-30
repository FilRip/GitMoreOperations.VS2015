using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;
using System.Collections.ObjectModel;

namespace GitMoreOperations.VS2015.UI.ViewModels
{
    public class GitStashViewModel : ViewModelBase
    {
        public GitStashViewModel()
        {
            StashCommand = new RelayCommand(x => Stash());
            PopStashCommand = new RelayCommand(x => PopStash());
            Update();
        }

        private string _currentSelected;

        public ICommand StashCommand { get; private set; }
        public ICommand PopStashCommand { get; private set; }

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

        private void listStash()
        {
            GitStashPage.MyHiddenGitOutput.Clear();
            var wrapper = new VsGitWrapper(GitStashPage.ActiveRepoPath, GitStashPage.MyHiddenGitOutput);
            wrapper.ListStash();
        }

        public ObservableCollection<string> ListeStash
        {
            get
            {
                ObservableCollection<string> liste = new ObservableCollection<string>();
                Service.HiddenGitOutput output = GitStashPage.MyHiddenGitOutput as Service.HiddenGitOutput;
                if (!string.IsNullOrWhiteSpace(output.TouteLaSortie))
                {
                    string[] lignes;
                    lignes = output.TouteLaSortie.Trim().Split((char)13);
                    foreach (string ligne in lignes)
                    {
                        if (!string.IsNullOrWhiteSpace(ligne))
                        {
                            string[] split;
                            split = ligne.Split(' ');
                            if (split.Length > 1)
                                liste.Add(split[split.Length - 1]);
                        }
                    }
                }
                return liste;
            }
        }

        public string StashSelectiones
        {
            get { return _currentSelected; }
            set
            {
                _currentSelected = value;
                base.RaisePropertyChanged(nameof(StashSelectiones));
            }
        }

        public void Update()
        {
            listStash();
        }
    }
}
