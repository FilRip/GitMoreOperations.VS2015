using System.Windows.Input;
using Microsoft.TeamFoundation.MVVM;
using System.Collections.ObjectModel;
using System.Linq;

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

        private ObservableCollection<SelectionItemListBox> _listeModif;
        private ObservableCollection<SelectionItemListBox> _listeSkip;

        public ObservableCollection<SelectionItemListBox> ListeModif
        {
            get { return _listeModif; }
            set
            {
                _listeModif = value;
                RaisePropertyChanged(nameof(ListeModif));
            }
        }

        public ObservableCollection<SelectionItemListBox> ListeSkip
        {
            get { return _listeSkip; }
            set
            {
                _listeSkip = value;
                RaisePropertyChanged(nameof(ListeSkip));
            }
        }

        public void SkipFile()
        {
            var wrapper = new VsGitWrapper(GitSkipFilesPage.ActiveRepoPath, GitSkipFilesPage.OutputWindow);
            foreach (SelectionItemListBox selected in _listeModif.Where(i => i.IsSelected).ToList())
                wrapper.SkipFile(selected.Item);
            Update();
        }

        public void UnSkipFile()
        {
            var wrapper = new VsGitWrapper(GitSkipFilesPage.ActiveRepoPath, GitSkipFilesPage.OutputWindow);
            foreach (SelectionItemListBox selected in _listeSkip.Where(i => i.IsSelected).ToList())
                wrapper.UnSkipFile(selected.Item);
            Update();
        }

        public void Update()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            _listeModif = new ObservableCollection<SelectionItemListBox>();
            GitSkipFilesPage.MyHiddenGitOutput.Clear();
            var wrapper = new VsGitWrapper(GitSkipFilesPage.ActiveRepoPath, GitSkipFilesPage.MyHiddenGitOutput);
            wrapper.ListeFichiersModifies();
            Service.HiddenGitOutput output = GitSkipFilesPage.MyHiddenGitOutput as Service.HiddenGitOutput;
            if (!string.IsNullOrWhiteSpace(output.TouteLaSortie))
            {
                string[] lignes;
                lignes = output.TouteLaSortie.Trim().Split("\r\n".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string ligne in lignes)
                    _listeModif.Add(new SelectionItemListBox() { Item = ligne });
            }
            RaisePropertyChanged(nameof(ListeModif));

            _listeSkip = new ObservableCollection<SelectionItemListBox>();
            GitSkipFilesPage.MyHiddenGitOutput.Clear();
            wrapper = new VsGitWrapper(GitSkipFilesPage.ActiveRepoPath, GitSkipFilesPage.MyHiddenGitOutput);
            output = GitSkipFilesPage.MyHiddenGitOutput as Service.HiddenGitOutput;
            wrapper.ListeFichiersAssumes();
            if (!string.IsNullOrWhiteSpace(output.TouteLaSortie))
            {
                string[] lignes;
                lignes = output.TouteLaSortie.Trim().Split("\r\n".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string ligne in lignes)
                    if (ligne.StartsWith("S ")) _listeSkip.Add(new SelectionItemListBox() { Item = ligne.Substring(2) });
            }
            RaisePropertyChanged(nameof(ListeSkip));
            Mouse.OverrideCursor = null;
        }
    }
}
