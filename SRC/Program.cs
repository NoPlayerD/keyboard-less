using System;
using Spectre.Console;


string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string __path = _path + @"/.KEYBOARDLESS";

check();

Folder root = new Folder();
root.name = new List<string>();
root.path = new List<string>();

foreach (string f in Directory.GetDirectories(__path))
{
    root.name.Add(f.Remove(0,f.LastIndexOf(@"\")+1));
    root.path.Add(f);
}

Console.WriteLine(root.name[0]);

void check()
{
    if (!Directory.Exists(__path))
    {
        Directory.CreateDirectory(__path);
    }
}