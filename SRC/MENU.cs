using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;

class MENU
{

public string selectedName { get; set; }
public string selectedPath { get; set; }
public string workingDir { get; set; }
//====================================================================================================

public void CreateMenu(bool stage,string title, string[] choices, bool doClean)
{
if (doClean){Console.Clear();}

string[] exclude;// things that are not 'file, folder or category'
if (stage == false) {exclude = ["/..", "/PREFS"];} else {exclude = ["/.."];}// define the 'exclude'

var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title(title)
        .EnableSearch()
        .AddChoices(exclude)
        .AddChoices(choices));

selectedName = menu;
if (stage == false)
{
        workingDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.KEYBOARDLESS/"+selectedName;
        selectedPath = workingDir;
}
else
{
        selectedPath= workingDir+"/"+selectedName;
}


}

public void ExecuteItem(string file)
{
        ProcessStartInfo pi = new ProcessStartInfo(file);
        pi.Arguments = Path.GetFileName(file);
        pi.UseShellExecute = true;
        pi.WorkingDirectory = Path.GetDirectoryName(file);
        pi.FileName = file;
        pi.Verb = "OPEN";
        Process.Start(pi);
}

//====================================================================================================

public Dictionary<string,string> getFiles(string path)
{
Dictionary<string,string> myDic = new Dictionary<string, string>();
foreach (string f in FileSystem.GetFiles(path)){myDic.Add(key: f.Remove(0,f.LastIndexOf(@"\")+1),value: f);}
return myDic;
}

public Dictionary<string,string> getDirectories(string path)
{
Dictionary<string,string> myDic = new Dictionary<string, string>();
foreach (string f in Directory.GetDirectories(path)){myDic.Add(key: f.Remove(0,f.LastIndexOf(@"\")+1),value: f);}
return myDic;
}

public Dictionary<string,string> getBoth(string path)
{
Dictionary<string,string> myDic = new Dictionary<string, string>();
var x1 = getFiles(path);
var x2 = getDirectories(path);

x1.ToList().ForEach(x=>x2.Add(x.Key,x.Value));
myDic = x2;

return myDic;
}

}