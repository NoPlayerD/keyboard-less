public class ENV
{
public string selectedName { get; set; }
public string selectedPath { get; set; }
public string workingDir { get; set; }
public Dictionary<string,string> dic { get; set; } = new Dictionary<string, string>();
}

public static class glob
{
    public static ENV root { get; set;} = new ENV();
    public static ENV branch { get; set; }= new ENV();
    public static string keyLess { get; set; }
    public static string title { get; set; }
    public static bool runLocal {get;set;}
    public static string[] excludeOfRoot { get; set; }
    public static string[] excludeOfBranch { get; set; }
}

public static class VARs
{
   public static string _path_appData {get;} = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string _path_keyless {get;} = _path_appData + @"/.KEYBOARDLESS";
    public static string _path_appStartup {get;} = AppDomain.CurrentDomain.BaseDirectory;
}