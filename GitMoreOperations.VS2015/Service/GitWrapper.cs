﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using LibGit2Sharp;
using System.Threading.Tasks;

namespace GitMoreOperations.VS2015.Service
{
    public class GitWrapper
    {
        private readonly string repoDirectory;

        public GitWrapper(string repoDirectory)
        {
            this.repoDirectory = repoDirectory;
        }

        public string CurrentBranch
        {
            get
            {
                using (var repo = new Repository(repoDirectory))
                {
                    return repo.Head.Name;
                }
            }
        }

        #region MoreOperations

        public GitCommandResult Pull(IEnumerable<string> options = null)
        {
            if (options == null)
            {
                options = Enumerable.Empty<string>();
            }
            options = options.Select(x => " --" + x);
            var gitArguments = "pull" + string.Join(string.Empty, options);
            return RunGitCommand(gitArguments);
        }

        public GitCommandResult Push()
        {
            var gitArguments = "push";
            return RunGitCommand(gitArguments);
        }

        public GitCommandResult Clean()
        {
            var gitArguments = "gc --prune";
            return RunGitCommand(gitArguments);
        }

        public GitCommandResult PullRequest()
        {
            var gitArguments = "request-pull";
            return RunGitCommand(gitArguments);
        }

        #endregion

        #region SkipFiles

        public GitCommandResult SkipFile(string nom)
        {
            var gitArguments = "update-index --skip-worktree " + nom;
            return RunGitCommand(gitArguments, waitEndExecute: true);
        }

        public GitCommandResult UnSkipFile(string nom)
        {
            var gitArguments = "update-index --no-skip-worktree " + nom;
            return RunGitCommand(gitArguments, waitEndExecute: true);
        }
        
        public GitCommandResult ListeFichiersModifies()
        {
            var gitArguments = "ls-files -m";
            return RunGitCommand(gitArguments, true, true);
        }

        public GitCommandResult ListeFichiersAssumes()
        {
            var gitArguments = "ls-files -v";
            return RunGitCommand(gitArguments, true, true);
        }

        #endregion

        #region Sous-modules

        public GitCommandResult InitialiseSubmodules()
        {
            var gitArguments = "submodule init";
            return RunGitCommand(gitArguments);
        }

        public GitCommandResult UpdateSubmodules(string nom)
        {
            var gitArguments = "submodule update --progress --remote " + nom;
            return RunGitCommand(gitArguments);
        }

        public GitCommandResult ListeSousModules()
        {
            var gitArguments = "submodule status";
            return RunGitCommand(gitArguments, true, true);
        }

        #endregion

        #region Stash

        public GitCommandResult Stash(string nom)
        {
            var gitArguments = "stash push -a -u " + nom;
            return RunGitCommand(gitArguments);
        }

        public GitCommandResult PopStash(string nom)
        {
            var gitArguments = "stash pop " + nom;
            return RunGitCommand(gitArguments);
        }

        public GitCommandResult DelStash(string nom)
        {
            var gitArguments = "stash drop " + nom;
            return RunGitCommand(gitArguments, waitEndExecute: true);
        }

        public GitCommandResult ListStash()
        {
            var gitArguments = "stash list";
            return RunGitCommand(gitArguments, true, true);
        }

        #endregion

        #region Log

        public GitCommandResult Log()
        {
            var gitArguments = "log";
            return RunGitCommand(gitArguments);
        }

        public GitCommandResult FileLog()
        {
            var gitArguments = "log " + "\"" + UI.GitLogPage.ActiveFileName + "\"";
            return RunGitCommand(gitArguments);
        }

        #endregion

        #region Events

        public delegate void CommandErrorReceivedEventHandler(object sender, CommandOutputEventArgs args);
        public delegate void CommandOutputReceivedEventHandler(object sender, CommandOutputEventArgs args);
        public event CommandOutputReceivedEventHandler CommandOutputDataReceived;
        public event CommandErrorReceivedEventHandler CommandErrorDataReceived;

        protected virtual void OnCommandOutputDataReceived(CommandOutputEventArgs e)
        {
            if (CommandOutputDataReceived != null) CommandOutputDataReceived(this, e);
        }

        protected virtual void OnCommandErrorDataReceived(CommandOutputEventArgs e)
        {
            if (CommandErrorDataReceived != null) CommandErrorDataReceived(this, e);
        }

        private void P_Exited(object sender, EventArgs e)
        {
            OnCommandOutputDataReceived(new CommandOutputEventArgs("git terminé" + Environment.NewLine));
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            if (dataReceivedEventArgs.Data != null)
            {
                Debug.WriteLine(dataReceivedEventArgs.Data);
                OnCommandOutputDataReceived(new CommandOutputEventArgs(dataReceivedEventArgs.Data + Environment.NewLine));
            }
        }

        private void OnErrorReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            if (dataReceivedEventArgs.Data != null &&
                dataReceivedEventArgs.Data.StartsWith("fatal:", StringComparison.OrdinalIgnoreCase))
            {
                Debug.WriteLine(dataReceivedEventArgs.Data);
                OnCommandErrorDataReceived(new CommandOutputEventArgs(dataReceivedEventArgs.Data + Environment.NewLine));
            }
        }

        #endregion

        #region Processus Git.exe

        private Process CreateGitProcess(string arguments, string repoDirectory, bool silence)
        {
            var gitInstallationPath = GitHelper.GetGitInstallationPath();
            var pathToGit = Path.Combine(Path.Combine(gitInstallationPath, "git.exe"));
            Process newProcess = new Process();
            newProcess.StartInfo.CreateNoWindow = true;
            newProcess.StartInfo.UseShellExecute = false;
            newProcess.StartInfo.RedirectStandardOutput = true;
            newProcess.StartInfo.RedirectStandardError = true;
            newProcess.StartInfo.FileName = pathToGit;
            newProcess.StartInfo.Arguments = arguments;
            newProcess.StartInfo.WorkingDirectory = repoDirectory;
            newProcess.OutputDataReceived += OnOutputDataReceived;
            newProcess.ErrorDataReceived += OnErrorReceived;
            if (!silence)
            {
                newProcess.EnableRaisingEvents = true;
                newProcess.Exited += P_Exited;
            }
            return newProcess;
        }

        private GitCommandResult RunGitCommand(string gitArguments, bool silence = false, bool waitEndExecute = false)
        {
            Process p;
            p = CreateGitProcess(gitArguments, repoDirectory, silence);
            if (!silence)
                OnCommandOutputDataReceived(
                    new CommandOutputEventArgs("Lancement de git " + p.StartInfo.Arguments + Environment.NewLine));
            Task launchTask = new Task(new Action(() => executeGit(p)));
            launchTask.Start();
            if (waitEndExecute) launchTask.Wait();
            return new GitCommandResult(true, "");
        }

        private void executeGit(Process monProcessus)
        {
            monProcessus.Start();
            monProcessus.BeginOutputReadLine();
            monProcessus.BeginErrorReadLine();
            monProcessus.WaitForExit();
        }

        #endregion
    }
}