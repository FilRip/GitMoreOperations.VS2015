using GitMoreOperations.VS2015.Service;
using Microsoft.VisualStudio.Shell.Interop;

namespace GitMoreOperations.VS2015
{
    public class VsGitWrapper : GitWrapper
    {
        public VsGitWrapper(string repoPath, IVsOutputWindowPane outputWindow) : base(repoPath)
        {
            CommandOutputDataReceived += (o, args) => outputWindow.OutputStringThreadSafe(args.Output);
            CommandErrorDataReceived += (o, args) => outputWindow.OutputStringThreadSafe(args.Output);
        }
    }
}