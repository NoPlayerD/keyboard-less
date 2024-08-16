﻿using Spectre.Console;

// json file exists? if not: create
if(!jsonMethods.checkIfJsonExists()){jsonMethods.writeMyJson();}


// read json file
myJson prefs = jsonMethods.readMyJson();


// set working directory according to json
global.workingDir = prefs.runLocal ? Path.Combine(global.appDomainPath, "local") : Path.Combine(global.AppDataPath, ".KeyLess");


// working dir & working dir's parent dir exist? if not: create
if(!Directory.Exists(sharedMethods.getParentDir(global.workingDir))){Directory.CreateDirectory(sharedMethods.getParentDir(global.workingDir));}
if(!Directory.Exists(global.workingDir)){Directory.CreateDirectory(global.workingDir);}


// define the menu for creation
myMenu my = new myMenu
{
    title = "KeyLess",
    enableSearch = true,
    choices = rootMethods.getNamesOfCategories(rootMethods.getCategories(global.workingDir)),
    pageSize = 25
};


// create the menu, set the selected folder
root.selectedFolder = rootMethods.createMenu(my);

Console.ReadKey();