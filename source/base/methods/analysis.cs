using System.Collections;
using System.Data.SqlTypes;
using System.Net.Http.Headers;
using Spectre.Console;

using variables;
public class analysisMethods
{

    // define the selected item from root
    public static void analysisRoot()
    {
        List<string> x = sharedMethods.returnRootExcludings(true);
        var y = root.selectedFolder.name;
        var z = new subMenusMethods(true);

        for (int i = 0; i<x.Count;i++)
        {
            if(y == x[i])
            {rootCaught(i,x,z);
            return;}
        }     

        creationMethods.createBranch();
    }


    // if selected item is an 'rootExcluding' then do whatever it needs
    private static void rootCaught(int caughtIndex, List<string> excludings, subMenusMethods subMenu)
    {
        switch (caughtIndex)
        {
            case 0:
                subMenu.first_Exit();
                break;
            
            case 1:
                subMenu.second_OpenLocation();
                break;

            case 2:
                subMenu.third_SearchInAll();
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
    }


    public static void analysisSIA()
    {

    }

}