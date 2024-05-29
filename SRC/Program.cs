using System;
using Spectre.Console;


string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string _keyboardless = _path + @"/.KEYBOARDLESS";

//=====DICTIONARIES===================================================================================

Dictionary<string,string> root = new Dictionary<string, string>();
Dictionary<string,string> branch = new Dictionary<string, string>();

//=====WAVES==========================================================================================

check();
addList(_keyboardless, root);
menu(root);

//====================================================================================================




//=====VOIDS==========================================================================================

// checking application path
void check()
{
    if (!Directory.Exists(_keyboardless))
    {
        Directory.CreateDirectory(_keyboardless);
    }
}

// adding items to the Dictionary
void addList (string path, Dictionary<string,string> myDic){

myDic.Clear();
foreach (string f in Directory.GetDirectories(path))
{
        myDic.Add(key: f.Remove(0,f.LastIndexOf(@"\")+1),value: f);
}}

// creating menu of the items
void menu(Dictionary<string,string> myDic){

    List<string> names = new List<string>();
    foreach (KeyValuePair<string,string> n in myDic)
    {
        names.Add(n.Key);
    }
    var selected = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .EnableSearch()
        .AddChoices(names)
        .AddChoices("/Exit"));
        string sPath = myDic[selected.ToString()];
        Console.WriteLine(sPath);
}

