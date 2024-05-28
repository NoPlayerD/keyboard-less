using System;
using Spectre.Console;


string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string __path = _path + @"/.KEYBOARDLESS";

check();

Folder root = new Folder();

foreach (string f in Directory.GetDirectories(__path))
{
    root.name = f.Remove(0,f.LastIndexOf(@"\")+1);
    root.path = f;
    Console.WriteLine(root.name);
}



void check()
{
    if (!Directory.Exists(__path))
    {
        Directory.CreateDirectory(__path);
    }
}