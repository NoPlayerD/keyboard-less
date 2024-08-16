public class creation
{

    // creates the root menu..
    public static void createRoot()
    {
        // define the menu for creation
        myMenu rot = new myMenu
        {
            title = "KeyLess",
            enableSearch = true,
            choices = rootMethods.getNamesOfCategories(rootMethods.getCategories(global.workingDir)),
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