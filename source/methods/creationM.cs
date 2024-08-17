public class creationMethods
{

    // creates the root menu..
    public static void createRoot()
    {
        List<string> stage = new List<string>();
            stage.Add(rootExludings.first_Exit);
            stage.Add(rootExludings.second_OpenLocation);
            stage.Add(rootExludings.third_SearchInAll);
            stage.Add(rootExludings.fourth_Preferences);
            stage.Add(rootExludings.fifth_Create);
        if(json.showSeparator)
        {stage.Add(rootExludings.sixth_Separator);}

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