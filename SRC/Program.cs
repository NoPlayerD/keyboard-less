using System;
using System.Diagnostics;
using System.Net;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;

//=====GLOBAL=========================================================================================
#region GLOBAL
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
#endregion
//=====DICTIONARIES===================================================================================
#region DICTIONARIES
Dictionary<string,string> root = new Dictionary<string, string>();
Dictionary<string,string> branch = new Dictionary<string, string>();
#endregion
//=====WAVES==========================================================================================

void WAVES(bool STAGE){

if (STAGE == false) // root
{
    root = menum.getDirectories(_path_keyless);
    menum.CreateMenu(STAGE,_title,root.Keys.ToArray(),true);
    _selected_path = menum.selectedPath;
    _selected_name = menum.selectedName;
    if (_selected_name == "/.."){Environment.Exit(0);}
    else{WAVES(true);}
}
else // branch
{
    branch = menum.getBoth(_selected_path);
    menum.CreateMenu(STAGE,null,branch.Keys.ToArray(),true);
    _selected_name = menum.selectedName;
    _selected_path = menum.selectedPath;
    if (_selected_name == "/.."){_stage = false; WAVES(_stage);return; }
    if (_selected_name != "/PREFS" || _selected_name != "/..")
    {
    string file = _selected_path;
    menum.ExecuteItem(file);
    }
    
    
}

}

//=====RUNTIME========================================================================================

Console.Title = "KeyboardLess";
check();
WAVES(_stage); // START THE APPLICATON

//=====VOIDS==========================================================================================

// checking application path
void check(){

    if (!Directory.Exists(_path_keyless))
    {
        Directory.CreateDirectory(_path_keyless);
    }
}
