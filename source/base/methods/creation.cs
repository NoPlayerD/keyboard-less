using variables;
using types;
using menuMethods;

public class creationMethods
{

    // creates the root menu..
    public static void createRoot()
    {
        
        List<string> stage = sharedMethods.returnRootExcludings(json.showSeparator);

        foreach(string x in rootMethods.getNamesOfCategories(rootMethods.getCategories(global.workingDir)))
        {stage.Add(x);}

        // define the menu for creation
        myMenu rot = new myMenu
        {
            title = "KeyLess",
            enableSearch = true,
            choices = stage.ToArray(),
            pageSize = 25
        };


        // create the menu, get the selected folder
        root.selectedFolder = rootMethods.createMenu(rot);
    }


    // creates the branch menu..
    public static void createBranch()
    {
        // define the menu for creation
        myMenu brach = new myMenu
        {
            title = null,
            enableSearch = true,
            choices = branchMethods.getNamesOfFiles(branchMethods.getFiles(root.selectedFolder.path)),
            pageSize = 25
        };

        branch.selectedFile = branchMethods.createMenu(brach);
    }

}