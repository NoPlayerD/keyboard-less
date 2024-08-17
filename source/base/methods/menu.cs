namespace menuMethods;

using types;
using variables;
public class branchMethods
{
    // returns the files(type: types.myFile) from selected category
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


    // returns the names of files from given list
    public static string[] getNamesOfFiles(List<myFile> from)
    {
        var x = new List<string>();

        for (int i = 0; i<from.Count; i++)
        {
            x.Add(from[i].nameWithExt);
        }

        return x.ToArray();
    }


    // creates the branch menu and returns the selected file(type: types.myFile)
    public static myFile createMenu(myMenu choices)
    {
        var me = new myFile();
        
        me.nameWithExt = sharedMethods.createMenu(choices);
        
        var shortcut = me.nameWithExt;

        me.pathWithNameWithExt = Path.Combine(sharedMethods.getParentDir(root.selectedFolder.path), shortcut);
        
        me.nameWithoutExt = Path.GetFileNameWithoutExtension(me.pathWithNameWithExt);

        me.parentPath = sharedMethods.getParentDir(me.pathWithNameWithExt);
        me.parentName = sharedMethods.directoryNameWithoutParent(me.parentPath);

        me.pathWithNameWithoutExt = Path.Combine(me.parentPath, me.nameWithoutExt);

        return me;
    }

}



public class rootMethods
{
    // returns the directories(type: myFolder) from workingDir*
    public static List<myFolder> getCategories(string from)
    {
        var me = new List<myFolder>();

        foreach (string dir in Directory.GetDirectories(from))
        {
            me.Add(new myFolder{name =sharedMethods.directoryNameWithoutParent(dir),path=dir});
        }
        
        return me;
    }


    // returns names of the given categories(type: myFolder)
    public static string[] getNamesOfCategories(List<myFolder> from)
    {
        List<string> x = new List<string>();
        string[] me;

        for (int i = 0; i < from.Count; i++)
        {
            x.Add(from[i].name);
        }

        me = x.ToArray();
        return me;
    }


    // creates the root menu and returns the selected category(type: myFolder)
    public static myFolder createMenu(myMenu choices)
    {
        myFolder me = new myFolder();

        me.name = sharedMethods.createMenu(choices);
        me.path = Path.Combine(global.workingDir,me.name);

        return me;
    }


}