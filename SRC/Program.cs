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

WAVES wave = new WAVES();

ENV root = new ENV();// root environment
ENV branch = new ENV();// branch environment

#endregion
//=====RUNTIME========================================================================================

Console.Title = "KeyboardLess";
check();
wave.DEFINE(root, branch, _path_keyless, _title);
wave.START();

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
