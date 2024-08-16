using Spectre.Console;

global.workingDir = AppDomain.CurrentDomain.BaseDirectory;
string from = Path.Combine(global.workingDir, "local");

myMenu my = new myMenu();

myMenu.title = "";
myMenu.enableSearch = true;
myMenu.choices = rootMethods.getNamesOfCategories(rootMethods.getCategories(from));
myMenu.pageSize = 25;

root.selectedFolder = rootMethods.createMenu(my);

Console.ReadKey();