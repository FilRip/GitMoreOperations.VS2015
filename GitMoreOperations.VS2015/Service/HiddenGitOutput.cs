using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace GitMoreOperations.VS2015.Service
{
    internal class HiddenGitOutput : IVsOutputWindowPane
    {
        private string _touteLaSortie;

        public string TouteLaSortie
        {
            get
            {
                return _touteLaSortie;
            }

            set
            {
                _touteLaSortie = value;
            }
        }

        public int Activate()
        {
            throw new NotImplementedException();
        }

        public int Clear()
        {
            _touteLaSortie = "";
            return 0;
        }

        public int FlushToTaskList()
        {
            throw new NotImplementedException();
        }

        public int GetName(ref string pbstrPaneName)
        {
            throw new NotImplementedException();
        }

        public int Hide()
        {
            throw new NotImplementedException();
        }

        public int OutputString(string pszOutputString)
        {
            _touteLaSortie += pszOutputString;
            return 0;
        }

        public int OutputStringThreadSafe(string pszOutputString)
        {
            _touteLaSortie += pszOutputString;
            return 0;
        }

        public int OutputTaskItemString(string pszOutputString, VSTASKPRIORITY nPriority, VSTASKCATEGORY nCategory, string pszSubcategory, int nBitmap, string pszFilename, uint nLineNum, string pszTaskItemText)
        {
            throw new NotImplementedException();
        }

        public int OutputTaskItemStringEx(string pszOutputString, VSTASKPRIORITY nPriority, VSTASKCATEGORY nCategory, string pszSubcategory, int nBitmap, string pszFilename, uint nLineNum, string pszTaskItemText, string pszLookupKwd)
        {
            throw new NotImplementedException();
        }

        public int SetName(string pszPaneName)
        {
            throw new NotImplementedException();
        }
    }
}
