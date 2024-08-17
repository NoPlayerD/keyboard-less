using System.Collections;
using System.Net.Http.Headers;
using Spectre.Console;

public class analysisMethods
{

    public static void analysisRoot()
    {
        List<string> x = sharedMethods.returnRootExcludings(true);
        var y = root.selectedFolder.name;
        var z = new subMenusMethods(true);

        for (int i = 0; i<x.Count;i++)
        {
            if(y == x[i])
            {rootCaught(i,x,z);}
        }     
    }

    private static void rootCaught(int caughtIndex, List<string> excludings, subMenusMethods subMenu)
    {
        if (caughtIndex == 0)
        {subMenu.first_Exit();}
    }


}