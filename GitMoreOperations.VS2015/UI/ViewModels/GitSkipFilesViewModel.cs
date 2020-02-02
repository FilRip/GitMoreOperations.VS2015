using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;
using System.Collections.ObjectModel;

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

        private ObservableCollection<string> _listeModif;
        private ObservableCollection<string> _listeSkip;
        private string[] _listeModifSelect;
        private string[] _listeSkipSelect;

        public ObservableCollection<string> ListeModif
        {
            get { return _listeModif; }
            set
            {
                _listeModif = value;
                RaisePropertyChanged(nameof(ListeModif));
            }
        }

        public ObservableCollection<string> ListeSkip
        {
            get { return _listeSkip; }
            set
            {
                _listeSkip = value;
                RaisePropertyChanged(nameof(ListeSkip));
            }
        }

        public string[] ListeSkipSelected
        {
            get { return _listeSkipSelect; }
            set
            {
                _listeSkipSelect = value;
                RaisePropertyChanged(nameof(ListeSkipSelected));
            }
        }

        public string[] ListeModifSelected
        {
            get { return _listeModifSelect; }
            set
            {
                _listeModifSelect = value;
                RaisePropertyChanged(nameof(ListeModifSelected));
            }
        }

        public void SkipFile()
        {
            var wrapper = new VsGitWrapper(GitLogPage.ActiveRepoPath, GitLogPage.OutputWindow);
            foreach (string fichier in _listeModifSelect)
                wrapper.SkipFile(fichier);
            Update();
        }

        public void UnSkipFile()
        {
            var wrapper = new VsGitWrapper(GitLogPage.ActiveRepoPath, GitLogPage.OutputWindow);
            foreach (string fichier in _listeSkipSelect)
                wrapper.UnSkipFile(fichier);
            Update();
        }

        public void Update()
        {
            GitSkipFilesPage.MyHiddenGitOutput.Clear();
            var wrapper = new VsGitWrapper(GitSkipFilesPage.ActiveRepoPath, GitSkipFilesPage.MyHiddenGitOutput);
            wrapper.ListeFichiersModifies();
            Service.HiddenGitOutput output = GitSkipFilesPage.MyHiddenGitOutput as Service.HiddenGitOutput;
            if (!string.IsNullOrWhiteSpace(output.TouteLaSortie))
            {
                string[] lignes;
                lignes = output.TouteLaSortie.Trim().Split((char)13);
                foreach (string ligne in lignes)
                    _listeModif.Add(ligne);
            }

            GitSkipFilesPage.MyHiddenGitOutput.Clear();
            wrapper = new VsGitWrapper(GitSkipFilesPage.ActiveRepoPath, GitSkipFilesPage.MyHiddenGitOutput);
            output = GitSkipFilesPage.MyHiddenGitOutput as Service.HiddenGitOutput;
            wrapper.ListeFichiersAssumes();
            if (!string.IsNullOrWhiteSpace(output.TouteLaSortie))
            {
                string[] lignes;
                lignes = output.TouteLaSortie.Trim().Split((char)13);
                foreach (string ligne in lignes)
                    _listeModif.Add(ligne);
            }
        }
    }
}
