using System;

namespace GitMoreOperations.VS2015.Service
{
    public class CommandOutputEventArgs : EventArgs
    {
        public CommandOutputEventArgs(string output)
        {
            Output = output;
        }

        public string Output { get; set; }
    }
}