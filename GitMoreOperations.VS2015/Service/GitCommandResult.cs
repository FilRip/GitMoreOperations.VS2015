namespace GitMoreOperations.VS2015.Service
{
    public class GitCommandResult
    {
        public GitCommandResult(bool success, string commandOutput)
        {
            Success = success;
            CommandOutput = commandOutput;
        }

        public bool Success { get; set; }
        public string CommandOutput { get; set; }
    }

    public class GitTimedOutCommandResult : GitCommandResult
    {
        public GitTimedOutCommandResult(string command)
            : base(
                false,
                "La commande '" + command +
                "' a pris plus de temps que requis. Vous devez peut être saisir des informations d'identification. Essayez la commande dans une console pour voir si des informations supplémentaires apparaissent."
                )
        {
        }
    }
}