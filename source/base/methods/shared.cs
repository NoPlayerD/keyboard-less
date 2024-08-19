using System.Runtime.InteropServices;
using Spectre.Console;
using System.Diagnostics;

using types;
using variables;
public static class sharedMethods
{
    // returns the name of the given directory without it's parent
    public static string directoryNameWithoutParent(string path)
    {
        string me;
        me = path.Remove(0,path.LastIndexOfAny(@"/\".ToCharArray())+1);

        return me;
    }


    // returns the parent directory of given path
    public static string getParentDir(string path)
    {
        var from = path.LastIndexOfAny(@"/\".ToCharArray());
        string me = path.Remove(from, (path.Length - from));
        return me;
    }
    

    // creates a menu with the given info and returns the selected choice
    public static string createMenu(myMenu choices)
    {
        string me;

        if (choices.enableSearch)
        {
            me = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(choices.title)
            .PageSize(choices.pageSize)
            .AddChoices(choices.choices)
            .EnableSearch());
        }
        else
        {
            me = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(choices.title)
            .PageSize(choices.pageSize)
            .AddChoices(choices.choices)
            .DisableSearch());
        }

        return me;
    }


    // returns a list of rootExcludings
    public static List<string> returnRootExcludings(bool showSeparator)
    {
        List<string> stage = new List<string>();
            stage.Add(rootExludings.first_Exit);
            stage.Add(rootExludings.second_OpenLocation);
            stage.Add(rootExludings.third_SearchInAll);
            stage.Add(rootExludings.fourth_Preferences);
            stage.Add(rootExludings.fifth_Create);
        if(showSeparator)
        {stage.Add(rootExludings.sixth_Separator);}

        return stage;
    }
    
    
    // execute given file or folder
    public static void EXECUTE(string whatToExecute)
    {
                // CMD komutunu oluştur
                string command =$""""start "" "{getParentDir(whatToExecute)}/{Path.GetFileName(whatToExecute)}" """";
                
                // ProcessStartInfo oluştur
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe"; // CMD'yi çalıştır
                psi.Arguments = "/c " + command; // /c argümanı ile komutu çalıştır ve ardından kapat

                // Yeni bir Process oluştur
                Process process = new Process();
                process.StartInfo = psi;

                // Process'i başlat
                process.Start();
    }


    // returns the directories(type: myFolder)
    public static List<myFolder> getFolders(string from)
    {
        var me = new List<myFolder>();

        foreach (string dir in Directory.GetDirectories(from))
        {
            me.Add(new myFolder{name =directoryNameWithoutParent(dir),path=dir});
        }
        
        return me;
    }


    // returns the files(type: types.myFile)
    public static List<myFile> getFiles(string from)
    {
        var me = new List<myFile>();

        foreach (string mFile in Directory.GetFiles(from))
        {
            me.Add(new myFile
                {
                    nameWithExt = Path.GetFileName(mFile),
                    nameWithoutExt = Path.GetFileNameWithoutExtension(mFile),

                    pathWithNameWithExt = mFile,
                    pathWithNameWithoutExt = Path.Combine(sharedMethods.getParentDir(mFile), Path.GetFileNameWithoutExtension(mFile)),

                    parentPath = sharedMethods.getParentDir(mFile),
                    parentName = sharedMethods.directoryNameWithoutParent(sharedMethods.getParentDir(mFile))
                });
        }

        return me;
    }


    // returns a string array with the names of folders & files from given path and sets the given lists of 'myFile' and 'myFolder' according to the datas
    public static string[] getBothNames_setBothData(string from, List<myFile> filesList, List<myFolder> foldersList)
    {
        var x1 = getFolders(from);
        var x2 = getFiles(from);
        
        var y = new List<string>();

        for(int i = 0; i<x1.Count;i++)
        {
            y.Add(Path.Combine(directoryNameWithoutParent(getParentDir(x1[i].path)), x1[i].name));
        }
        for(int i = 0; i<x2.Count;i++)
        {
            y.Add(Path.Combine(x2[i].parentName, x2[i].nameWithExt));
        }

        foldersList = x1;
        filesList = x2;

        y.Sort();
        return y.ToArray();
    }


    public static List<string> returnSiaExcludings(bool showSeparator)
    {
        List<string> stage = new List<string>();
            stage.Add(siaExcludings.first_goBack);
        if(showSeparator)
        {stage.Add(rootExludings.sixth_Separator);}

        return stage;
    }


    public static string[] getItemsOf_SearchInAll()
    {
        var me = new List<string>();

        foreach (string x in Directory.GetDirectories(global.workingDir))
        {
            var y = getBothNames_setBothData(x, sia.files, sia.folders);
            y.ToList<string>().ForEach(x=>me.Add(x));
        }        

        return me.ToArray();
    }


    public static void afterSelection(string path, bool isThisSIA)
    {
        if(json.inspectWithSelection)
        {
            var menu = new myMenu
            {
                title = directoryNameWithoutParent(path),
                enableSearch = json.enableSearch,
                pageSize = json.pageSize,
                choices = returnInspectItems().ToArray()
            };

            var result = createMenu(menu);
            analysisMethods.analysisInspect(result, path, isThisSIA);
        }
        else
        {
            EXECUTE(path);
        }
    }

    public static List<string> returnInspectItems()
    {
        List<string> me = new List<string>();
            me.Add(inspectItems.first_EXEUTE);
            me.Add(inspectItems.second_Exit);
            me.Add(inspectItems.third_OpenLocation);

            return me;
    }

}