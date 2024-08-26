using variables;
using types;
using menuMethods;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class creationMethods
{

#region ROOT
    
// creates the root menu..
    public static void createRoot()
    {
        
        List<string> stage = sharedMethods.returnRootExcludings(json.showHeaderSeparator);

        foreach(string x in rootMethods.getNamesOfCategories(sharedMethods.getFolders(global.workingDir)))
        {stage.Add(x);}

        // define the menu for creation
        myMenu rot = new myMenu
        {
            title = "KeyLess",
            enableSearch = true,
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
                        // open location
                        break;

                    case 2:
                        creationMethods.createSIA();
                        break;

                    case 3:
                        // preferences
                        break;

                    case 4:
                        // create
                        break;

                    case 5:
                        creationMethods.createRoot();
                        break;
                }
            return;
            }
        }     

        creationMethods.createBranch();
    }
#endregion


#region BRANCH

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
            enableSearch = true,
            choices = x1.ToArray(),
            pageSize = 25
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
                        creationMethods.createRoot();
                        break;
                    case 1:
                        sharedMethods.EXECUTE($"{root.selectedFolder.path}");
                        break;
                    case 2:
                        creationMethods.createBranch();
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


#region SearchInAll

// creates the 'search in all' menu..
    public static void createSIA()
    {
        List<string> choices = new List<string>{siaExcludings.first_goBack, siaExcludings.second_Separator};
        sharedMethods.getItemsOf_SearchInAll().ToList<string>().ForEach(x=>choices.Add(x));
        
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
                        creationMethods.createRoot();
                        break;

                    case 1:
                        creationMethods.createSIA();
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


#region Inspect

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
}