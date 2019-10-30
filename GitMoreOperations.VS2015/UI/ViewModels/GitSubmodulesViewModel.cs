using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;
using System.Collections.ObjectModel;

namespace GitMoreOperations.VS2015.UI.ViewModels
{
    public class GitSubmodulesViewModel : ViewModelBase
    {
        public GitSubmodulesViewModel()
        {
            InitialiseSubmodulesCommand = new RelayCommand(x => InitialiseSubmodules());
            UpdateSubmodulesCommand = new RelayCommand(x => UpdateSubmodules());
            Update();
        }

        public string[] _currentSelection;

        public ICommand InitialiseSubmodulesCommand { get; private set; }
        public ICommand UpdateSubmodulesCommand { get; private set; }

        public void InitialiseSubmodules()
        {
            var wrapper = new VsGitWrapper(GitSubmodulesPage.ActiveRepoPath, GitSubmodulesPage.OutputWindow);
            wrapper.InitialiseSubmodules();
        }

        public void UpdateSubmodules()
        {
            var wrapper = new VsGitWrapper(GitSubmodulesPage.ActiveRepoPath, GitSubmodulesPage.OutputWindow);
            wrapper.UpdateSubmodules();
        }

        private void ListeSousModules()
        {
            GitSubmodulesPage.MyHiddenGitOutput.Clear();
            var wrapper = new VsGitWrapper(GitSubmodulesPage.ActiveRepoPath, GitSubmodulesPage.MyHiddenGitOutput);
            wrapper.ListeSousModules();
        }

        public ObservableCollection<string> ListeSousModule
        {
            get
            {
                ObservableCollection<string> liste = new ObservableCollection<string>();
                Service.HiddenGitOutput output = GitSubmodulesPage.MyHiddenGitOutput as Service.HiddenGitOutput;
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
                            if (split.Length > 2)
                                liste.Add(split[1]);
                        }
                    }
                }
                return liste;
            }
        }

        public string[] SousModuleSelectiones
        {
            get { return _currentSelection; }
            set
            {
                _currentSelection = value;
                base.RaisePropertyChanged(nameof(SousModuleSelectiones));
            }
        }

        public void Update()
        {
            ListeSousModules();
        }
    }
}