using System;

namespace GitMoreOperations.VS2015.Service
{
    public class BranchItem
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTimeOffset LastCommit { get; set; }
        public bool IsTracking { get; set; }
        public bool IsCurrentBranch { get; set; }
        public bool IsRemote { get; set; }

        public string LastCommitAsString
        {
            get
            {
                var timeSpan = DateTime.Now - LastCommit;
                var daysSince = timeSpan.Days;
                if (daysSince == 0)
                {
                    return "il y a " + timeSpan.Hours.ToString() + " heure(s)";
                }
                return "il y a " + daysSince.ToString() + " jour(s)";
            }
        }

        public string ToolTip
        {
            get
            {
                return "Commit: " + CommitId + "\nAuteur: " + Author + "\nDate: " + LastCommit.Date.ToShortDateString() +
                       "\n\n" + Message;
            }
        }

        public string CommitId { get; set; }
        public string Message { get; set; }
    }
}