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
            DelStashCommand = new RelayCommand(x => DelStash());
            Update();
            _delAfterPop = true;
        }

        private string _currentSelected = "";
        private string _nom = "";
        private bool _delAfterPop;

        public ICommand StashCommand { get; private set; }
        public ICommand PopStashCommand { get; private set; }
        public ICommand DelStashCommand { get; private set; }

        public void Stash()
        {
            var wrapper = new VsGitWrapper(GitStashPage.ActiveRepoPath, GitStashPage.OutputWindow);
            wrapper.Stash(_nom);
            Update();
        }

        public void PopStash()
        {
            var wrapper = new VsGitWrapper(GitStashPage.ActiveRepoPath, GitStashPage.OutputWindow);
            wrapper.PopStash(_currentSelected);
            if (_delAfterPop)
            {
                DelStash();
                Update();
            }
        }

        public void DelStash()
        {
            if (string.IsNullOrWhiteSpace(_currentSelected)) return;
            var wrapper = new VsGitWrapper(GitStashPage.ActiveRepoPath, GitStashPage.OutputWindow);
            wrapper.DelStash(_currentSelected);
            Update();
        }

        private void listStash()
        {
            GitStashPage.MyHiddenGitOutput.Clear();
            var wrapper = new VsGitWrapper(GitStashPage.ActiveRepoPath, GitStashPage.MyHiddenGitOutput);
            wrapper.ListStash();
        }

        public bool DelAfterPop
        {
            get { return _delAfterPop; }
            set { _delAfterPop = value; base.RaisePropertyChanged(nameof(DelAfterPop)); }
        }

        public string NomStash
        {
            get { return _nom; }
            set { _nom = value; base.RaisePropertyChanged(nameof(NomStash)); }
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
