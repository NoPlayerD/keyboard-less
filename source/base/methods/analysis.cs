using System.Collections;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Spectre.Console;

using variables;
public class analysisMethods
{

    // analyze the result of root and do whatever it needs
    public static void analysisRoot()
    {
        List<string> x = sharedMethods.returnRootExcludings(json.showSeparator);
        var y = root.selectedFolder.name;

        for (int i = 0; i<x.Count;i++)
        {
            if(y == x[i])
            {rootCaught(i,x);
            return;}
        }     

        creationMethods.createBranch();
    }


    // if selected item of root is an 'rootExcluding' then do whatever it needs
    private static void rootCaught(int caughtIndex, List<string> excludings)
    {
        switch (caughtIndex)
        {
            case 0:
                Environment.Exit(0);
                // exit
                break;
            
            case 1:
                sharedMethods.EXECUTE(global.workingDir);
                // open location
                break;

            case 2:
                creationMethods.createSIA();
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



    private static void siaCaught(int caughtIndex, List<string> excludings)
    {
        switch(caughtIndex)
        {
            case 0:
                creationMethods.createRoot();
                break;

            case 1:
                creationMethods.createSIA();
                break;

        }
    }

    // analyze the result of 'search in all' and do whatever it needs
    public static void analysisSIA()
    {
        var x = sharedMethods.returnSiaExcludings(json.showSeparator);
        var y = sia.selectedItem;

        for (int i = 0; i<x.Count;i++)
        {
            if(y==x[i])
            {siaCaught(i,x);
            return;}
        }

        sharedMethods.afterSelection(Path.Combine(global.workingDir, sia.selectedItem),true);
    }


    public static void analysisInspect(string input, string selectedItem, bool isThisSIA)
    {
        var x = sharedMethods.returnInspectItems();

        for (int i = 0; i<x.Count;i++)
        {
            if(input == x[i])
            {inpsectCaught(i, selectedItem, isThisSIA);}
        }
    }


    private static void inpsectCaught(int caughtIndex, string input, bool isThisSIA)
    {
        switch(caughtIndex)
        {
            case 0:
                sharedMethods.EXECUTE(input);
                break;
            case 1:
                if (isThisSIA){creationMethods.createSIA();}
                else {/* if a branch.. */}
                break;
        }
    }
}