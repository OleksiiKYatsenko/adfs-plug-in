namespace ADFS_Plug_in.Interfaces
{
    internal interface ILogManager
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
