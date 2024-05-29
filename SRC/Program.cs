using System;
using Spectre.Console;


string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string __path = _path + @"/.KEYBOARDLESS";

check();

Dictionary<string,string> root = new Dictionary<string, string>();

//Folder root = new Folder();
//root.name = new List<string>();
//root.path = new List<string>();

Folder branch = new Folder();
branch.name = new List<string>();
branch.path = new List<string>();

foreach (string f in Directory.GetDirectories(__path))
{
    root.Add(f.Remove(0,f.LastIndexOf(@"\")+1), f);
}

List<string> names = new List<string>();

foreach (KeyValuePair<string,string> n in root)
{
    names.Add(n.Key);
}

var selected = AnsiConsole.Prompt(new SelectionPrompt<string>()
    .EnableSearch()
    .AddChoices(names)
    .AddChoices("/Exit"));



string sPath = root[selected.ToString()];

Console.WriteLine(sPath);
void check()
{
    if (!Directory.Exists(__path))
    {
        Directory.CreateDirectory(__path);
    }
}

