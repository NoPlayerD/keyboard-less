using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;


class WAVES
{

MENU menu = new MENU();

public void START()
// uygulamayı başlatma
{
    if (glob.root != null || glob.branch != null || glob.keyLess != null || glob.title != null)
        {RunRoot();}
}

void RunRoot()
// root aşamasında yapılacaklar..
{
    Console.Clear();
    // temiz bir sayfa :)
    
    glob.root.dic = menu.getDirectories(glob.keyLess);
    // root isimli environment'in dictionary'sini, 'getFrom'daki klasörlerin isimleri ve konumları olarak ayarla. 

    var choices = glob.root.dic.Keys.ToArray();
    // choices isimli dizimizi, root environment'imizdeki dictionary'daki klasör isimleri olarak ayarla.

    menu.CreateMenu(choices, glob.root);
    // root için menümüzü oluşturalım.

    if (glob.root.selectedName == "/..")
        {Environment.Exit(0);}
        // çıkmak isteyen çıkabilir.
    else
        {RunBranch(glob.root.selectedPath);}
        // çıkmak istemeyen, seçtiği şık ile devam eder.
}

void RunBranch(string getFrom)
// branch/yan aşamalarda yapılacaklar..
{
    Console.Clear();
    // temiz bir sayfa :)

    glob.branch.dic.Clear();
    // eski itemleri sil.

    glob.branch.dic = menu.getBoth(getFrom);
    // branch isimli environment'in dictionary'sini, 'getFrom'daki dosya ve klasörlerin isimleri ve konumları olarak ayarla.

    var choices = glob.branch.dic.Keys.ToArray();
    // choices isimli dizimizi, branch environment'imizdeki dictionary'daki itemlerin isimleri olarak ayarla.

    menu.CreateMenu(choices, glob.branch);
    // branch için menümüzü oluşturalım.

    if (glob.branch.selectedName == "/..")
        {RunRoot();return;}
        // geri dönmek isteyen dönebilir (root'a).
    else
    {
        string file = glob.branch.selectedPath;
        // seçili itemin konumunu 'file' değişkenine ata.

        menu.InspectItem(file, glob.branch);
        // seçili itemi incele, kullanıcının seçtiği işlemi uygula.

        RunBranch(glob.branch.selectedPath);
        // eski yerine geri dön.
    }

}
}

//==============================================================================================================

class MENU
{

public void CreateMenu(string[] choices, ENV virtualEnv)
{

bool stage;
// root mu branch mi belirleyicisi.

Console.Clear();
// temizlen yaw


if (virtualEnv == glob.root)
        {stage = false;}
// root mu?
else
        {stage = true;}
// branch mi?


string[] exclude;
// dosya veya klasör olmayan itemler.

string title;
// başlık


if (stage == false)
        {exclude = ["/..", "/PREFS"];
        title = glob.title;} 
// root ise..
else 
        {exclude = ["/.."];
        title = null;}// define the 'exclude'
// branch ise


var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title(title)
        .EnableSearch()
        .AddChoices(exclude)
        .AddChoices(choices));
// menümüzü oluşturalım :)

virtualEnv.selectedName = menu.ToString();
// seçimimizi, aktif environment'imizdeki değişkene ekleyelim.

if (stage == false)
{
        glob.root.workingDir = glob.keyLess+glob.root.selectedName;
        glob.root.selectedPath = glob.root.workingDir;
}
// aktif env. root ise değişkenleri atayalım (menüdeki seçimimize göre)
else
{
        glob.branch.selectedPath= glob.root.workingDir+"/"+glob.branch.selectedName;
        glob.branch.workingDir = glob.root.workingDir;
}
// aktif env. branch ise değişkenleri atayalım (menüdeki seçimimize göre)

}

public void ExecuteItem(string file)
// seçili itemi çalıştırma
{
        ProcessStartInfo pi = new ProcessStartInfo(file);
        pi.Arguments = Path.GetFileName(file);
        pi.UseShellExecute = true;
        pi.WorkingDirectory = Path.GetDirectoryName(file);
        pi.FileName = file;
        pi.Verb = "OPEN";
        Process.Start(pi);
}

public void InspectItem (string path, ENV Venvironment)
// seçili itemi inceleme
{
        string toDo =  Inspecting(path, Venvironment);
        // inceleme menüsünü oluştur ve çıktıyı al.

        if (toDo == "Execute")
        // çıktıya göre çalıştır.
        {
                ExecuteItem(path);
        }
        else if (toDo == "Open File Location")
        // çıktıya göre dosya konumunu aç.
        {
                ExecuteItem(GetMainPath(path));
        }

        Venvironment.selectedPath = GetMainPath(path);
        // seçilmiş olarak veya en son işlem olarak geriye, aynı kategoriye dönme.
}

//==================================================

public Dictionary<string,string> getFiles(string path)
// dosyaları alan dictionary
{
        Dictionary<string,string> myDic = new Dictionary<string, string>();
        foreach (string f in FileSystem.GetFiles(path)){myDic.Add(key: f.Remove(0,f.LastIndexOf(@"\")+1),value: f);}
        return myDic;
}

public Dictionary<string,string> getDirectories(string path)
// klasörleri alan dictionary
{
        Dictionary<string,string> myDic = new Dictionary<string, string>();
        string chars = @"/\";
        foreach (string f in Directory.GetDirectories(path)){myDic.Add(key: f.Remove(0,f.LastIndexOfAny(chars.ToCharArray())+1),value: f);}
        return myDic;
}

public Dictionary<string,string> getBoth(string path)
// dosyaları ve klasörleri alıp birleştiren dictionary
{
        Dictionary<string,string> myDic = new Dictionary<string, string>();
        var x1 = getFiles(path);
        var x2 = getDirectories(path);

        x1.ToList().ForEach(x=>x2.Add(x.Key,x.Value));
        myDic = x2;

        return myDic;
}

private string Inspecting (string path, ENV branch)
// seçili iteme inceleme menüsü gösterme ve kullanıcının seçtiği işlemi döndürme
{
        string name = branch.selectedName; //path.Remove(0, path.LastIndexOf("/") + 1);
        string[] choices = {"Execute","GO BACK!","Open File Location"};

        var selection = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title(name)
        .EnableSearch()
        .AddChoices(choices));

        return selection;
}

private string GetMainPath(string path)
// gönderilen itemin konumunun ana(üst) konumunu gösterme.
{
        string me = path.Remove(path.LastIndexOf('/') +1, path.Length - (path.LastIndexOf('/') + 1));
        return me;
}
}

//==============================================================================================================

public class Methods
{
    public static void checknCreate(string path)
    {
    // belirlenen klasör var mı? yok ise oluştur.
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }}
}