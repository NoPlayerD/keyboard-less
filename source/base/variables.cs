namespace variables;

using types;
public static class rootExludings
{
    public static string first_Exit { get; }            =   "0|.."                  ;
    public static string second_OpenLocation { get; }   =   "1|OpenLocation"        ;
    public static string third_SearchInAll { get; }     =   "2|SearchInAll"         ;
    // public static string fourth_Preferences { get; }    =   "3|Preferences"         ;
    public static string fourth_Create { get; }          =   "3|Info"                ;
    public static string fifth_Separator { get; }       =   "===================="  ;
}


public static class branchExcludings
{
    public static string first_goBack {get;}            =   "0|..";
    public static string second_OpenLocation { get;}    =   "1|OpenLocation";
    public static string third_Separator { get; }       =   "====================";
}


public static class siaExcludings // search in all
{
    public static string first_goBack {get;}            =   "0|..";
    public static string second_Separator { get; }       =   "====================";
}


public static class json
{
    public static bool runLocal { get; set; }
    public static bool exitAfterSelection { get; set; }
    public static bool showHeader { get; set; }
    public static bool inspectWithSelection { get; set; }
    public static int pageSize { get; set; }
    public static bool showHeaderSeparator { get; set; }
    public static bool showSeparator { get; set; }
    public static bool includeNonCategoriesTo_SearchInAll { get; set; }
    public static bool enableSearch { get; set; }
}



public static class root
{
    public static List<myFolder> categories { get; set; } = new List<myFolder>();
    public static myFolder selectedFolder { get; set; }
}



public static class branch
{
    public static List<myFile> files { get; set; } = new List<myFile>();
    public static List<myFolder> folders { get; set; } = new List<myFolder>();
    public static string selectedPath { get; set; }
}


public static class sia // search in all
{
    public static List<myFile> files {get; set;} = new List<myFile>();
    public static List<myFolder> folders { get; set; } = new List<myFolder>();
    public static string selectedItem { get; set; }
}

public static class global
{
    public static string workingDir { get; set; }
    public static string appDomainPath { get; } =AppDomain.CurrentDomain.BaseDirectory;
    public static string AppDataPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string jsonPath { get; } = Path.Combine(global.appDomainPath,"preferences.json");
    public static string title {get;} = @"
 /$$   /$$                     /$$                                   
| $$  /$$/                    | $$                                   
| $$ /$$/   /$$$$$$  /$$   /$$| $$        /$$$$$$   /$$$$$$$ /$$$$$$$
| $$$$$/   /$$__  $$| $$  | $$| $$       /$$__  $$ /$$_____//$$_____/
| $$  $$  | $$$$$$$$| $$  | $$| $$      | $$$$$$$$|  $$$$$$|  $$$$$$ 
| $$\  $$ | $$_____/| $$  | $$| $$      | $$_____/ \____  $$\____  $$
| $$ \  $$|  $$$$$$$|  $$$$$$$| $$$$$$$$|  $$$$$$$ /$$$$$$$//$$$$$$$/
|__/  \__/ \_______/ \____  $$|________/ \_______/|_______/|_______/ 
                     /$$  | $$                                       
                    |  $$$$$$/                                       
                     \______/                                        
";
}


public static class inspectItems
{
    public static string minus_EXEUTE { get; }          =   "-1|Execute";
    public static string first_GoBack { get; }          =   "0|GoBack..";
    public static string second_OpenLocation { get; }   =   "1|OpenLocation";
}


public static class preferencesItems
{
    public static string MyProperty { get;}
}