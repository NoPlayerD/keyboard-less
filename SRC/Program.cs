using System;
using Spectre.Console;


string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string __path = _path + @"/.KEYBOARDLESS";

check();

Folder root = new Folder();
root.name = new List<string>();
root.path = new List<string>();

Folder branch = new Folder();
branch.name = new List<string>();
branch.path = new List<string>();

foreach (string f in Directory.GetDirectories(__path))
{
    root.name.Add(f.Remove(0,f.LastIndexOf(@"\")+1));
    root.path.Add(f);
}

var selected = AnsiConsole.Prompt(new SelectionPrompt<string>()
    .EnableSearch()
    .AddChoices(root.name)
    .AddChoices("/Exit"));

string sPath = null;
int counter = 0;
foreach (string ff in root.name)
{
    if (ff == selected.ToString())
    {
        sPath = root.path[counter];
    }
    counter++;
}

void check()
{
    if (!Directory.Exists(__path))
    {
        Directory.CreateDirectory(__path);
    }
}

