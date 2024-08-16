using System.Runtime.InteropServices;
using Spectre.Console;

public static class sharedMethods
{
    public static string directoryNameWithoutParent(string path)
    {
        string me;
        me = path.Remove(0,path.LastIndexOfAny(@"/\".ToCharArray())+1);

        return me;
    }

    public static string createMenu(myMenu choices)
    {
        string me;

        if (myMenu.enableSearch)
        {
            me = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(myMenu.title)
            .PageSize(myMenu.pageSize)
            .AddChoices(myMenu.choices)
            .EnableSearch());
        }
        else
        {
            me = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(myMenu.title)
            .PageSize(myMenu.pageSize)
            .AddChoices(myMenu.choices)
            .DisableSearch());
        }

        return me;
    }

}