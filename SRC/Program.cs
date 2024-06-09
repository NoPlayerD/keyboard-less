using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;

//=====GLOBAL=========================================================================================
#region GLOBAL
string _path_appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string _path_keyless = _path_appData + @"/.KEYBOARDLESS";

bool _stage = false;// 'false' = root, 'true' = branch

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

ENV root = new ENV();
ENV branch = new ENV();

#endregion
//=====WAVES==========================================================================================

void WAVES(bool STAGE){

if (STAGE == false) // root
{
    root.dic = MENU.getDirectories(_path_keyless);
    MENU.CreateMenu(STAGE,_title,root.dic.Keys.ToArray(),true, root, root, branch);
    if (root.selectedName == "/.."){Environment.Exit(0);}
    else{WAVES(true);}
}
else // branch
{
    Console.Clear();
    branch.dic.Clear();
    branch.dic = MENU.getBoth(root.selectedPath);
    MENU.CreateMenu(STAGE,null,branch.dic.Keys.ToArray(),true, branch, root,branch);
    if (branch.selectedName == "/.."){_stage = false; WAVES(_stage);return; }
    else
    {
        string file = branch.selectedPath;
        MENU.InspectItem(file, branch);
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