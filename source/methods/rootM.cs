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