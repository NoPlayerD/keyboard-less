namespace variables;

public static class branch
{
    public static List<types.myFile> files { get; set; } = new List<types.myFile>();
    public static types.myFile selectedFile { get; set; }
}



public static class rootExludings
{
    public static string first_Exit { get; }            =   "0|.."                  ;
    public static string second_OpenLocation { get; }   =   "1|OpenLocation"        ;
    public static string third_SearchInAll { get; }     =   "2|SearchInAll"         ;
    public static string fourth_Preferences { get; }    =   "3|Preferences"         ;
    public static string fifth_Create { get; }          =   "4|Create"              ;
    public static string sixth_Separator { get; }       =   "===================="  ;
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
}



public static class root
{
    public static List<types.myFolder> categories { get; set; } = new List<types.myFolder>();
    public static types.myFolder selectedFolder { get; set; }
}