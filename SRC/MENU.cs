using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Runtime.ConstrainedExecution;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;

class MENU
{
//LOG log = new LOG();

#region GLOBAL
    //MENU menu = new MENU();
    public ENV root { get; set; }
    public ENV branch { get; set; }
    public string title { get; set; }
#endregion

public void DEFINE(ENV roOt, ENV branCh, string keyless, string titLE)
// root ve branch environment'lerini tanımla. 
{
    root = roOt;
    branch = branCh;
    title = titLE;
}

//WAVES wave = new WAVES();

public void CreateMenu(string[] choices, ENV virtualEnv)
{

bool stage;
// root mu branch mi belirleyicisi.

Console.Clear();
// temizlen yaw


if (virtualEnv == root)
        {stage = false;}
// root mu?
else
        {stage = true;}
// branch mi?

string[] exclude;
// dosya veya klasör olmayan itemler.


if (stage == false)
        {exclude = ["/..", "/PREFS"];} 
// root ise..
else 
        {exclude = ["/.."];}// define the 'exclude'
// branch ise


var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title(title)
        .EnableSearch()
        .AddChoices(exclude)
        .AddChoices(choices));
// menümüzü oluşturalım :)

virtualEnv.selectedName = menu.ToString();
// seçimimizi, aktif environment'imizdeki değişkene ekleyelim.

if (stage == false)
{
        root.workingDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.KEYBOARDLESS/"+root.selectedName;
        root.selectedPath = root.workingDir;
}
// aktif env. root ise değişkenleri atayalım (menüdeki seçimimize göre)
else
{
        branch.selectedPath= root.workingDir+"/"+branch.selectedName;
        branch.workingDir = root.workingDir;
}
// aktif env. branch ise değişkenleri atayalım (menüdeki seçimimize göre)

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

public void InspectItem (string path, ENV Venvironment)
{
        string toDo =  Inspecting(path, Venvironment);

        if (toDo == "Execute")
        {
                ExecuteItem(path);
        }
        else if (toDo == "Open File Location")
        {
                ExecuteItem(GetMainPath(path));
        }

        Venvironment.selectedPath = GetMainPath(path);
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
        string chars = @"/\";
        foreach (string f in Directory.GetDirectories(path)){myDic.Add(key: f.Remove(0,f.LastIndexOfAny(chars.ToCharArray())+1),value: f);}
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

private string Inspecting (string path, ENV branch)
{
        string name = branch.selectedName; //path.Remove(0, path.LastIndexOf("/") + 1);
        string[] choices = {"Execute","GO BACK!","Open File Location"};

        var selection = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title(name)
        .EnableSearch()
        .AddChoices(choices));

        return selection;
}

private string GetMainPath(string path)
{
        string me = path.Remove(path.LastIndexOf('/') +1, path.Length - (path.LastIndexOf('/') + 1));
        return me;
}

}