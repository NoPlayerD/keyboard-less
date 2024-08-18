using variables;
using types;
using menuMethods;
using System.Security.Cryptography;

public class creationMethods
{

    // creates the root menu..
    public static void createRoot()
    {
        
        List<string> stage = sharedMethods.returnRootExcludings(json.showSeparator);

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
    }


    // creates the branch menu..
    public static void createBranch()
    {
        // define the menu for creation
        myMenu brach = new myMenu
        {
            title = null,
            enableSearch = true,
            choices = branchMethods.getNamesOfFiles(sharedMethods.getFiles(root.selectedFolder.path)),
            pageSize = 25
        };

        branch.selectedFile = branchMethods.createMenu(brach);
    }


    public static void createSIA(string[] Bchoices)
    {

        List<string> choices = new List<string>{siaExcludings.first_goBack, siaExcludings.second_Separator};
        Bchoices.ToList<string>().ForEach(x=>choices.Add(x));
        
        var menu = new myMenu()
        {
            title = null,
            enableSearch = json.enableSearch,
            pageSize = json.pageSize,
            choices = choices.ToArray()
        };
        var x = sharedMethods.createMenu(menu);
    }

}