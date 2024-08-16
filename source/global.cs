public static class global
{
    public static string workingDir { get; set; }
    public static string appDomainPath { get; } =AppDomain.CurrentDomain.BaseDirectory;
    public static string AppDataPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string jsonPath { get; set; } = Path.Combine(global.appDomainPath,"preferences.json");
}