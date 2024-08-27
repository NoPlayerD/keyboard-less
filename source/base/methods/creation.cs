using variables;
using types;
using menuMethods;

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

public class creationMethods
{

#region ROOT        (menu)
    
// creates the root menu..
    public static void createRoot()
    {
        
        List<string> stage = sharedMethods.returnRootExcludings(json.showHeaderSeparator);

        foreach(string x in rootMethods.getNamesOfCategories(sharedMethods.getFolders(global.workingDir)))
        {stage.Add(x);}

        // define the menu for creation
        myMenu rot = new myMenu
        {
            title = json.showHeader?global.title:null,
            enableSearch = json.enableSearch,
            choices = stage.ToArray(),
            pageSize = json.pageSize
        };


        // create the menu, get the selected folder
        root.selectedFolder = rootMethods.createMenu(rot);
        analysisRoot();
    }


// analyze the result of root and do whatever it needs
    private static void analysisRoot()
    {
        List<string> x = sharedMethods.returnRootExcludings(json.showHeaderSeparator);
        var y = root.selectedFolder.name;

        for (int i = 0; i<x.Count;i++)
        {
            if(y == x[i])
            {
                switch (i)
                {
                    case 0:
                        Environment.Exit(0);
                        // exit
                        break;
                    
                    case 1:
                        sharedMethods.EXECUTE(global.workingDir);
                        if (!json.exitAfterSelection){createRoot();}
                        // open location
                        break;

                    case 2:
                        createSIA();
                        break;

                    /*case 3: // preferences
                        break;*/

                    case 3:
                        showInfo();
                        break;

                    case 4:
                        createRoot();
                        break;                       
                }
                return;
            }
        }     


        if(root.selectedFolder.name.EndsWith(".nc"))
            {sharedMethods.EXECUTE(root.selectedFolder.path);}
        else
            {createBranch();}

    }
#endregion


#region BRANCH      (menu)

// creates the branch menu..
    public static void createBranch()
    {
        List<string> x1 = sharedMethods.returnBranchExcludings(json.showSeparator);
        var x2 = sharedMethods.getBothNamesWithoutParentName(root.selectedFolder.path).ToList<string>();
            x2.ForEach(x=>x1.Add(x));
        

        // define the menu for creation
        myMenu brach = new myMenu
        {
            title = null,
            enableSearch = json.enableSearch,
            choices = x1.ToArray(),
            pageSize = json.pageSize
        };
        
        branch.selectedPath = Path.Combine(root.selectedFolder.path, branchMethods.createMenu(brach).nameWithExt);

        analysisBranch();
    }


// analyze the result of branch and do whatever it needs
    private static void analysisBranch()
    {
        List<string> x = sharedMethods.returnBranchExcludings(json.showSeparator);
        var y = branch.selectedPath;
        var z = y.Remove(0,y.LastIndexOfAny(@"/\".ToCharArray())+1);
        for (int i = 0; i<x.Count; i++)
        {
            if (z == x[i])
            {
                switch (i)
                {
                    case 0:
                        createRoot();
                        break;
                    case 1:
                        sharedMethods.EXECUTE($"{root.selectedFolder.path}");
                        if(!json.exitAfterSelection){createBranch();}
                        break;
                    case 2:
                        createBranch();
                        break;
                }
                return;
            }
        }

        afterSelection(branch.selectedPath,false);
        if (!json.exitAfterSelection)
            {createBranch();}

    }
#endregion


#region SearchInAll (menu)

// creates the 'search in all' menu..
    public static void createSIA()
    {
        List<string> choices = new List<string>{siaExcludings.first_goBack};
            if (json.showSeparator) {choices.Add(siaExcludings.second_Separator);}
        
        choices.AddRange(sharedMethods.getItemsOf_SearchInAll().ToList());
        
        if (!json.includeNonCategoriesTo_SearchInAll)
        {
            // Filtering
            choices = choices.Where(x =>
            {
                var lastIndex = x.LastIndexOf(@"\");
                if (lastIndex >= 0)
                {
                    var xx = x.Remove(lastIndex, x.Length - lastIndex);
                    return !xx.EndsWith(".nc"); // if contains (true), return false
                }
                return true;
            }).ToList();
        }

        /* old version (not working) 
        if(json.includeNonCategoriesTo_SearchInAll == false)
        {
            for (int i = 0; i<choices.Count;i++)
            {
                try
                {
                    var lastIndex = choices[i].LastIndexOf(@"\");
                    var xx = choices[i].Remove(lastIndex, choices[i].Length - lastIndex);
                    
                    if (xx.EndsWith(".nc"))
                        {choices.Remove(choices[i]);}
                }
                catch(Exception ex)
                {}
            }
        }*/
        

        var menu = new myMenu()
        {
            title = null,
            enableSearch = json.enableSearch,
            pageSize = json.pageSize,
            choices = choices.ToArray()
        };
        var x = sharedMethods.createMenu(menu);

        sia.selectedItem = x;
        analysisSIA();
    }


// analyze the result of 'search in all' and do whatever it needs
    private static void analysisSIA()
    {
        var x = sharedMethods.returnSiaExcludings(json.showSeparator);
        var y = sia.selectedItem;

        for (int i = 0; i<x.Count;i++)
        {
            if(y==x[i])
            {
                switch(i)
                {
                    case 0:
                        createRoot();
                        break;

                    case 1:
                        createSIA();
                        break;

                }
                return;
            }
        }

        afterSelection(Path.Combine(global.workingDir, sia.selectedItem),true);
        if(!json.exitAfterSelection)
            {createSIA();}
    }
#endregion


#region Inspect     (menu)

// does whatever needed (inspect or execute) to do after selection (for branch and SIA only)
    private static void afterSelection(string path, bool isThisSIA)
    {
        if(json.inspectWithSelection)
        {
            var menu = new myMenu
            {
                title = sharedMethods.directoryNameWithoutParent(path),
                enableSearch = json.enableSearch,
                pageSize = json.pageSize,
                choices = sharedMethods.returnInspectItems().ToArray()
            };

            var result = sharedMethods.createMenu(menu);
            analysisInspect(result, path, isThisSIA);
        }
        else
        {
            sharedMethods.EXECUTE(path);
        }
    }


// if inspected after afterselection, then do whatever needs..
    private static void analysisInspect(string input, string path, bool isThisSIA)
    {
        var x = sharedMethods.returnInspectItems();

        for (int i = 0; i<x.Count;i++)
        {
            if(input == x[i])
            {
                switch(i)
                {
                    case 0:
                        sharedMethods.EXECUTE(path);
                        break;
                    case 1:
                        if (isThisSIA){createSIA();}
                        else {createBranch();}
                        break;
                    case 2:
                        sharedMethods.EXECUTE(Directory.GetParent(path).FullName);
                        break;
                }
            }
        }
        
    }
#endregion


#region Info        (page)
    
    public static void showInfo()
    {
        string info = "If 'runningLocal', the app uses 'assemblyLocation/local/' path as data location."
            + "\nIf not, it uses '%appData%/.keyLess'\n"
            + "\nIf name of a category ends with '.nc', the app will open it as a folder, not like a category.\n"
            + "\nYou can customize application with ur preferences by editing 'preferences.json' file."
            + "\nMinimum pageSize is 3 btw.\n"
            + "\nThere musn't be '[]' in the name of any item (for example: '[john] file.exe')\n\n";
    
        Console.WriteLine(info);
        Console.WriteLine("Press any key to go back..");
        Console.ReadKey();
        Console.Clear();
        createRoot();
    }
#endregion

}