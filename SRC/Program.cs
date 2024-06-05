using System;
using System.Diagnostics;
using System.Net;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;

//=====GLOBAL=========================================================================================

string _path_appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string _path_keyless = _path_appData + @"/.KEYBOARDLESS";
string _path_dynamic = null;

string _selected_name = null;
string _selected_path = null;

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

MENU menum = new MENU();

//=====DICTIONARIES===================================================================================

Dictionary<string,string> root = new Dictionary<string, string>();
Dictionary<string,string> branch = new Dictionary<string, string>();

//=====WAVES==========================================================================================

void WAVES(bool STAGE){

if (STAGE == false) // root
{
    check();
    root = menum.getDirectories(_keyboardless);
    menum.CreateMenu(false,_title,root.Keys.ToArray(),true);
    _dynmc = menum.selectedPath;
    WAVES(true);
}
else // branch
{
    branch = menum.getBoth(_dynmc);
    menum.CreateMenu(true,null,branch.Keys.ToArray(),true);
    _selected = menum.selectedName;

    string file = _dynmc +"/" + _selected;
    menum.ExecuteItem(file);
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
