using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;

//=====GLOBAL=========================================================================================
#region GLOBAL
string _path_appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
string _path_keyless = _path_appData + @"/.KEYBOARDLESS";
string _path_appStartup = AppDomain.CurrentDomain.BaseDirectory;

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

glob.title = _title;
glob.keyLess = _path_appStartup;
glob.excludeOfRoot = [
  """/"..""", 
  """/"s - SEARCH IN ALL""",
  """/"o  - OPEN LOCATION""",
  """/"p  - PREFS"""
];
glob.excludeOfBranch = ["""/".."""];
#endregion
//=====RUNTIME========================================================================================

Console.Title = "KeyboardLess";
//Methods.checknCreate(_path_keyless);
WAVES.START();
