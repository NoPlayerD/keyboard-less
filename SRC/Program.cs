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
  """|"..""", 
  """|"s  - SEARCH IN ALL""",
  """|"o  - OPEN LOCATION""",
  """|"p  - PREFERENCES"""
];
glob.excludeOfBranch = [
  """/"..""",
  """|"o - OPEN LOCATION"""
];
#endregion
//=====RUNTIME========================================================================================
#region RUNTIME
Console.Title = "KeyboardLess";// konsol penceresinin başlığını belirler.
Methods.checkAndDefineJson();// json dosyasını kontrol et ve ayarla.
Methods.checkDataLocationAndCreate();// kullanılacak uygulama konumunu kontrol et, yok ise oluştur.

WAVES.START();// başlat
#endregion