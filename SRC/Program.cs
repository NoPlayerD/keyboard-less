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

MENU MENU = new MENU();
ENV rootE = new ENV();
ENV branchE = new ENV();

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
    root = MENU.getDirectories(_path_keyless);
    MENU.CreateMenu(STAGE,_title,root.Keys.ToArray(),true, rootE);
    _selected_path = rootE.selectedPath;
    _selected_name = rootE.selectedName;
    if (_selected_name == "/.."){Environment.Exit(0);}
    else{WAVES(true);}
}
else // branch
{
    branch.Clear();
    branch = MENU.getBoth(_selected_path);
    MENU.CreateMenu(STAGE,null,branch.Keys.ToArray(),true, branchE);
    _selected_name = branchE.selectedName;
    _selected_path = branchE.selectedPath;
    if (_selected_name == "/.."){_stage = false; WAVES(_stage);return; }
    if (_selected_name != "/PREFS" || _selected_name != "/..")
    {
        string file = _selected_path;
        MENU.InspectItem(file, branchE);
        _selected_path = branchE.selectedPath;
        WAVES(true);
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

void _LOG (string LOG)
{
    Console.WriteLine(LOG);
}