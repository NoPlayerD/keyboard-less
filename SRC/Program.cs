using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;

//=====GLOBAL=========================================================================================
#region GLOBAL
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
glob.excludeOfRoot = [
  "0|..", 
  "1|OPEN LOCATION",
  "2|SEARCH IN ALL",
  "3|PREFERENCES",
  "4|CREATE",
  "===================="
];
glob.excludeOfBranch = [
  "0|..",
  "1|OPEN LOCATION",
  "===================="
];
#endregion
//=====RUNTIME========================================================================================
#region RUNTIME
Console.Title = "KeyboardLess";// konsol penceresinin başlığını belirler.

WAVES.START();// başlat
#endregion