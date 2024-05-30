using System;
using System.Diagnostics;
using System.Net;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;

//=====GLOBAL=========================================================================================

string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string _keyboardless = _path + @"/.KEYBOARDLESS";
string _dynmc = null;

bool _stage = false;

string _title = @"
  _  __          _                   
 | |/ /         | |                  
 | ' / ___ _   _| |     ___  ___ ___ 
 |  < / _ \ | | | |    / _ \/ __/ __|
 | . \  __/ |_| | |___|  __/\__ \__ \
 |_|\_\___|\__, |______\___||___/___/
            __/ |                    
           |___/                     
";

//=====DICTIONARIES===================================================================================

Dictionary<string,string> root = new Dictionary<string, string>();
Dictionary<string,string> branch = new Dictionary<string, string>();

//=====WAVES==========================================================================================

void WAVES(bool STAGE){

if (STAGE == false) // 0
{
    check();
    addDirs(_keyboardless, root);
    menu(root);
}
else // 1
{
    addDirs(_dynmc, branch);
    addFiles(_dynmc, branch);
    menu(branch);
}

}

//=====RUNTIME========================================================================================

Console.Title = "KeyboardLess";
WAVES(_stage); // START THE APPLICATON

//=====VOIDS==========================================================================================

// checking application path
void check(){

    if (!Directory.Exists(_keyboardless))
    {
        Directory.CreateDirectory(_keyboardless);
    }
}

// adding directories to the Dictionary
void addDirs (string path, Dictionary<string,string> myDic){

myDic.Clear();
foreach (string f in Directory.GetDirectories(path))
{
        myDic.Add(key: f.Remove(0,f.LastIndexOf(@"\")+1),value: f);
}}

// adding files to the Dictionary
void addFiles (string path, Dictionary<string,string> myDic){

foreach (string f in FileSystem.GetFiles(path))
{
        myDic.Add(key: f.Remove(0,f.LastIndexOf(@"\")+1),value: f);
}}

// creating menu of the items
void menu(Dictionary<string,string> myDic){

#region creating

    List<string> names = new List<string>();
    foreach (KeyValuePair<string,string> n in myDic)
    {
        names.Add(n.Key);
    }
    string myTitle;
    if (_stage == false) {myTitle = _title;} else {myTitle = null;}
    var selected = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title(myTitle)
        .EnableSearch()
        .AddChoices("/..")
        .AddChoices(names));
#endregion

#region answering

if (selected.ToString() != "/..")// if not exiting
{

    if (_stage == false)// root
    {           
        _stage = true;
        _dynmc = _keyboardless + "/" + selected.ToString();
        WAVES(_stage);
    }
    else// branch
    {
        string file = _dynmc +"/" + selected.ToString();
        ProcessStartInfo pi = new ProcessStartInfo(file);
        pi.Arguments = Path.GetFileName(file);
        pi.UseShellExecute = true;
        pi.WorkingDirectory = Path.GetDirectoryName(file);
        pi.FileName = file;
        pi.Verb = "OPEN";
        Process.Start(pi);
}   
}
else// if exiting
{

    if (_stage == false)// root
    {
    Environment.Exit(0);
    }
    else// branch
    {
        _stage = false;
        WAVES(_stage);
    }
}
#endregion
}

