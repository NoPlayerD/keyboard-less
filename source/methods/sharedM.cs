using System.Runtime.InteropServices;
using Spectre.Console;

public static class sharedMethods
{
    // returns the name of the given directory without it's parent
    public static string directoryNameWithoutParent(string path)
    {
        string me;
        me = path.Remove(0,path.LastIndexOfAny(@"/\".ToCharArray())+1);

        return me;
    }


    // returns the parent directory of given path
    public static string getParentDir(string path)
    {
        var from = path.LastIndexOfAny(@"/\".ToCharArray());
        string me = path.Remove(from, (path.Length - from));
        return me;
    }
    

    // creates a menu with the given info and returns the selected choice
    public static string createMenu(myMenu choices)
    {
        string me;

        if (choices.enableSearch)
        {
            me = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(choices.title)
            .PageSize(choices.pageSize)
            .AddChoices(choices.choices)
            .EnableSearch());
        }
        else
        {
            me = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(choices.title)
            .PageSize(choices.pageSize)
            .AddChoices(choices.choices)
            .DisableSearch());
        }

        return me;
    }

}