namespace menuMethods;

using types;
using variables;
public class branchMethods
{
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
    public static string createMenu(myMenu choices)
    {
        var me = new myFile();
        
        me.nameWithExt = sharedMethods.createMenu(choices);
        
        /* === useless for now ===
        var shortcut = me.nameWithExt;

        me.pathWithNameWithExt = Path.Combine(sharedMethods.getParentDir(root.selectedFolder.path), shortcut);
        
        me.nameWithoutExt = Path.GetFileNameWithoutExtension(me.pathWithNameWithExt);

        me.parentPath = sharedMethods.getParentDir(me.pathWithNameWithExt);
        me.parentName = sharedMethods.directoryNameWithoutParent(me.parentPath);

        me.pathWithNameWithoutExt = Path.Combine(me.parentPath, me.nameWithoutExt);*/

        return me.nameWithExt;
    }

}

// ====================================================================================================

public class rootMethods
{
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


    // returns the paths of the given list<myFolder>
    public static string[] getPathsOfCategories(List<myFolder> from)
    {
        List<string> x = new List<string>();
        string[] me;

        for (int i = 0; i < from.Count; i++)
        {
            x.Add(from[i].path);
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