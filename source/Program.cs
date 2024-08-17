using Spectre.Console;

// json file exists? if not: create
if(!jsonMethods.checkIfJsonExists()){jsonMethods.writeMyJson();}


// read json file
jsonMethods.readMyJson();


// set working directory according to json
global.workingDir = json.runLocal ? Path.Combine(global.appDomainPath, "local") : Path.Combine(global.AppDataPath, ".KeyLess");


// working dir & working dir's parent dir exist? if not: create
if(!Directory.Exists(sharedMethods.getParentDir(global.workingDir))){Directory.CreateDirectory(sharedMethods.getParentDir(global.workingDir));}
if(!Directory.Exists(global.workingDir)){Directory.CreateDirectory(global.workingDir);}

creationMethods.createRoot();
    analysisMethods.analysisRoot();

Console.ReadKey();