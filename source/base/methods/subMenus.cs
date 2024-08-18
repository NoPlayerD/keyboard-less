using menuMethods;
using types;
using variables;
public class subMenusMethods(bool isThisRoot)
{
    public void first_Exit()
    {
        if(isThisRoot)
        {Environment.Exit(0);}
    }


    public void second_OpenLocation()
    {
        if (isThisRoot)
        {sharedMethods.EXECUTE(global.workingDir);}
    }


    public void third_SearchInAll()
    {
        if (isThisRoot)
        {
            creationMethods.createSIA(getItemsOf_SearchInAll());
        }
    }


    private string[] getItemsOf_SearchInAll()
    {
        var me = new List<string>();

        foreach (string x in Directory.GetDirectories(global.workingDir))
        {
            var y = sharedMethods.getBothNames_setBothData(x, sia.files, sia.folders);
            y.ToList<string>().ForEach(x=>me.Add(x));
        }        

        return me.ToArray();
    }
    

}