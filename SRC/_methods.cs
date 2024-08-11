using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;
using System.Text.Json;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Collections;
using System.ComponentModel;


class WAVES
{

public static void START()
// uygulamayı başlatma
{

    if (glob.root != null || glob.branch != null || glob.keyLess != null || glob.title != null)
        {
                Methods.checkAndDefineJson();// json dosyasını kontrol et ve ayarla.
                Methods.checkDataLocationAndCreate();// kullanılacak uygulama konumunu kontrol et, yok ise oluştur.
                RunRoot();
        }
}

public static void siaMenuCreator(string getFrom)
// search in all - siaStartLine'ın geldiği yer.
{
    Console.Clear();
    // temiz bir sayfa :)

    var choices = glob.branch.dic.Keys.ToArray();
    // choices isimli dizimizi, branch environment'imizdeki dictionary'daki itemlerin isimleri olarak ayarla.

    MENU.CreateMenu(choices, glob.branch, true);
    // branch için menümüzü oluşturalım.

    if (glob.branch.selectedName == glob.excludeOfBranch[0])
        {RunRoot();return;}
        // geri dönmek isteyen dönebilir (root'a).
    else if (glob.branch.selectedName == glob.excludeOfBranch[2])
    {
        Methods.siaStartLine();
    }
    else
    {
        string file;
        glob.branch.dic.TryGetValue(key: glob.branch.selectedName,out file);
        // seçili itemin konumunu 'file' değişkenine ata.

        MENU.InspectItem(file, glob.branch, true);
        // seçili itemi incele, kullanıcının seçtiği işlemi uygula.

        Methods.siaStartLine();
        // eski yerine geri dön.
    }

}

static void RunRoot()
// root aşamasında yapılacaklar..
{
    Console.Clear();
    // temiz bir sayfa :)
    
    glob.root.dic = MENU.getDirectories(glob.keyLess);
    // root isimli environment'in dictionary'sini, 'getFrom'daki klasörlerin isimleri ve konumları olarak ayarla. 

    var choices = glob.root.dic.Keys.ToArray();
    // choices isimli dizimizi, root environment'imizdeki dictionary'daki klasör isimleri olarak ayarla.

    MENU.CreateMenu(choices, glob.root, false);
    // root için menümüzü oluşturalım.

    if (glob.root.selectedName == glob.excludeOfRoot[0])
        {Environment.Exit(0);}
        // çıkmak isteyen çıkabilir.
    else if (glob.root.selectedName == glob.excludeOfRoot[1])
    // konum açma seçeneği.
    {
        MENU.ExecuteItem(glob.keyLess);
        RunRoot();
    }
    else if(glob.root.selectedName == glob.excludeOfRoot[2])
    // tüm kategorilerde mi arican la.
    {
        Methods.siaStartLine();
    }
    else if(glob.root.selectedName == glob.excludeOfRoot[3])
    // ayarlar (preferences) seçeneği.
    {
        MENU.createPrefsMenu(Methods.readLocalJson());
    }
    else if (glob.root.selectedName == glob.excludeOfRoot[4])
    {
        Console.WriteLine("- Folders are categories\n- Folders that ending with .nc are nonCategory items, you can open them like an item and make them seen in the root ");
        Console.ReadKey();
        RunRoot();
        // 'Create' seçeneği
    }
    else if (glob.root.selectedName == glob.excludeOfRoot[5])
    {
        // separator
        RunRoot();
    }
    else
        {RunBranch(glob.root.selectedPath);}
        // çıkmak istemeyen, seçtiği şık ile devam eder.
}

static void RunBranch(string getFrom)
// branch/yan aşamalarda yapılacaklar..
{
    Console.Clear();
    // temiz bir sayfa :)

    glob.branch.dic.Clear();
    // eski itemleri sil.

    glob.branch.dic = MENU.getBoth(getFrom);
    // branch isimli environment'in dictionary'sini, 'getFrom'daki dosya ve klasörlerin isimleri ve konumları olarak ayarla.

    var choices = glob.branch.dic.Keys.ToArray();
    // choices isimli dizimizi, branch environment'imizdeki dictionary'daki itemlerin isimleri olarak ayarla.

    if (MENU.GetName(getFrom).EndsWith(".nc"))
    {
        MENU.ExecuteItem(getFrom);
        RunRoot();
        return;
    }
    MENU.CreateMenu(choices, glob.branch,false);
    // branch için menümüzü oluşturalım.

    if (glob.branch.selectedName == glob.excludeOfBranch[0])
        {RunRoot();return;}
        // geri dönmek isteyen dönebilir (root'a).
    else if(glob.branch.selectedName == glob.excludeOfBranch[1])
    {MENU.ExecuteItem(glob.root.selectedPath);}
        // seçili kategorinin konumuna git.
    else if (glob.branch.selectedName == glob.excludeOfBranch[2])
    {
        RunBranch(getFrom);
    }
    else
    {
        string file = glob.branch.selectedPath;
        // seçili itemin konumunu 'file' değişkenine ata.
        
        MENU.InspectItem(file, glob.branch,false);
        // seçili itemi incele, kullanıcının seçtiği işlemi uygula.

        RunBranch(glob.branch.selectedPath);
        // eski yerine geri dön.
    }

}
}

//==============================================================================================================

public class MENU
{
static int anan = 0;
public static void CreateMenu(string[] choices, ENV virtualEnv, bool SIA)
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


string[] exclude = stage ? glob.excludeOfBranch : glob.excludeOfRoot;
// dosya veya klasör olmayan itemler.

if (SIA) {string[] temp = [exclude[0], exclude[2]]; exclude = temp;}

string title = stage ? null : glob.title;
// başlık


var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title(title)
        .EnableSearch()
        .PageSize(50)
        .AddChoices(exclude)
        .AddChoices(choices));
// menümüzü oluşturalım :)

virtualEnv.selectedName = menu.ToString();
// seçimimizi, aktif environment'imizdeki değişkene ekleyelim.

if (stage == false)
{
        glob.root.workingDir = Path.Combine(glob.keyLess, glob.root.selectedName);
        glob.root.selectedPath = glob.root.workingDir;
}
// aktif env. root ise değişkenleri atayalım (menüdeki seçimimize göre)
else
{
        glob.branch.selectedPath= Path.Combine(glob.root.workingDir,glob.branch.selectedName);
        glob.branch.workingDir = glob.root.workingDir;
}
// aktif env. branch ise değişkenleri atayalım (menüdeki seçimimize göre)

}
public static void ExecuteItem(string file)
// seçili itemi çalıştırma
{
        switch(anan)
        {

        case 0: //new method (awesome)

               // string fileName = $"""\"{}\""";

                // CMD komutunu oluştur
                string command =$""""start "" "{Directory.GetParent(file)}/{Path.GetFileName(file)}" """";
                
                // ProcessStartInfo oluştur
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe"; // CMD'yi çalıştır
                psi.Arguments = "/c " + command; // /c argümanı ile komutu çalıştır ve ardından kapat

                // Yeni bir Process oluştur
                Process process = new Process();
                process.StartInfo = psi;

                // Process'i başlat
                process.Start();
                if (glob.exitAfter){Environment.Exit(0);}
                break;

        case 1: //old method (buggy)
                Process p = new Process();
                p.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,  @"exec\executer.exe");
                p.StartInfo.Arguments = Path.GetFileName(file.Replace(' ', '?'));
                p.StartInfo.UseShellExecute = false;
                //p.Verb = "OPEN";
                p.StartInfo.WorkingDirectory = Path.GetDirectoryName(file);
                p.Start();  
                break;
        }
}

public static void InspectItem (string path, ENV Venvironment,bool isSIA)
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
                ExecuteItem(Path.GetDirectoryName(path));
        }

        if (isSIA){Venvironment.selectedPath = glob.keyLess;}
        else {Venvironment.selectedPath = GetMainPath(path);}

        // seçilmiş olarak veya en son işlem olarak geriye, aynı kategoriye dönme.
}


public static void createPrefsMenu(List<bool> jsonState)
// ayarlar (preferences) menüsünün oluşturulması ve kontrolü..
{
        string[] choices1 = {"Run Locally? - FALSE", "Run Locally? - TRUE"};
        string[] choices2 = {"Exit After Selection? - FALSE", "Exit After Selection? - TRUE"};
        // 1 seçeneğimiz olacak var 2 farklı türde de olabilir..

        int state1;
        if (jsonState[0] == true) {state1 = 1;}
        else {state1 = 0;}

        int state2;
        if (jsonState[1] == true) {state2 = 1;}
        else {state2 = 0;}
        // json dosyamızdaki değere göre hangi seçeneğin seçileceğine karar verme.

        Console.Clear();
        // temiz bir sayfa :)

        var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .AddChoices(glob.excludeOfBranch[0])
        .AddChoices(choices1[state1])
        .AddChoices(choices2[state2]));
        // menümüzü oluşturalım :)

        if (menu != glob.excludeOfBranch[0])
        // eğer "o" seçeneği seçersek seçeneği ve dosyayı düzenleme..
        {
                bool old;
                bool newer;

                 // Read the JSON file
                string json = File.ReadAllText("prefs.json");

                // Deserialize the JSON into a Person object
                myJson person = JsonSerializer.Deserialize<myJson>(json);

                if (menu == choices1[state1])
                {
                        old= Methods.readLocalJson()[0];
                        newer = old ? false : true;
                        person.runLocal = newer;
                }
                else
                {
                        old  = Methods.readLocalJson()[1];
                        newer = old ? false : true;
                        person.exitAfter = newer;
                }
                
               
                // Serialize the Person object into JSON
                json = JsonSerializer.Serialize(person);

                // Write the updated JSON to the file
                File.WriteAllText("prefs.json", json);

                Methods.checkAndDefineJson();
        }
        else
        {WAVES.START();
        return;}

        createPrefsMenu(Methods.readLocalJson());
        // çıkış yapmadıysak tekrar başa döner.
}

//-------------------------

public static Dictionary<string,string> getFiles(string path)
// dosyaları alan dictionary
{
        Dictionary<string,string> myDic = new Dictionary<string, string>();
        foreach (string f in FileSystem.GetFiles(path)){myDic.Add(key: f.Remove(0,f.LastIndexOf(@"\")+1),value: f);}
        return myDic;
}

public static Dictionary<string,string> getDirectories(string path)
// klasörleri alan dictionary
{
        Dictionary<string,string> myDic = new Dictionary<string, string>();
        string chars = @"/\";
        foreach (string f in Directory.GetDirectories(path)){myDic.Add(key: f.Remove(0,f.LastIndexOfAny(chars.ToCharArray())+1),value: f);}
        return myDic;
}

public static Dictionary<string,string> getBoth(string path)
// dosyaları ve klasörleri alıp birleştiren dictionary
{
        Dictionary<string,string> myDic = new Dictionary<string, string>();
        var x1 = getFiles(path);
        var x2 = getDirectories(path);

        x1.ToList().ForEach(x=>x2.Add(x.Key,x.Value));
        myDic = x2;

        return myDic;
}

public static string GetName(string path)
{
        string ex = @"/\";
        string x = path.Remove(0,path.LastIndexOfAny(ex.ToCharArray()));

        return x;
}

private static string Inspecting (string path, ENV branch)
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

private static string GetMainPath(string path)
// gönderilen itemin konumunun ana(üst) konumunu gösterme.
{
        string ex = @"/\";
        string me = path.Remove(path.LastIndexOfAny(ex.ToCharArray()) +1, path.Length - (path.LastIndexOfAny(ex.ToCharArray()) + 1));
        return me;
}
}
//==============================================================================================================

public class myJson
{
        public bool runLocal { get; set; }
        public bool exitAfter { get; set; }
}

public class Methods
{
     public static string myJson {get;} = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "prefs.json");

    public static void checkDataLocationAndCreate()
    // uygulamanın çalışacağı klasör var mı? yok ise oluştur.
    {
        if (!Directory.Exists(glob.keyLess))
        {Directory.CreateDirectory(glob.keyLess);}
    }
    public static void checkAndDefineJson()
    // json dosyamız var mı diye kontrol eder, yok ise oluşturur, global değerleri atar/günceller
    {
        if (!File.Exists(myJson))
                {
                        myJson my = new myJson{runLocal = true, exitAfter = true};
                        string json = JsonSerializer.Serialize(my);
                        File.WriteAllText("prefs.json",json);
                
                }
        else
                {
                        List<bool> p = readLocalJson();
                        glob.runLocal = p[0];
                        glob.exitAfter = p[1];
                }

        if (glob.runLocal == true)
                {glob.keyLess = VARs._path_appStartup;}
        else
                {glob.keyLess = VARs._path_keyless;}
        checkDataLocationAndCreate();
    }
    public static void siaStartLine()
    // search in all - dictionary'lerin belirlendiği başlangıç alanı.
    {
        Dictionary<string,string> d1 = new Dictionary<string, string>();
        Dictionary<string,string> d2 = new Dictionary<string, string>();
        
        foreach (string x in MENU.getDirectories(glob.keyLess).Values)
        // d1'i belirle.
        {
                var x1 = MENU.getFiles(x);
                x1.ToList().ForEach(y=>d1.Add(y.Key,y.Value));
        }

        foreach (string x in MENU.getDirectories(glob.keyLess).Values)
        // d2'yi belirle - aşama 1.
        {
                var x1 = MENU.getDirectories(x);
                x1.ToList().ForEach(y=>d2.Add(y.Key,y.Value));
        }
        
        d1.ToList().ForEach(x =>d2.Add(x.Key, x.Value));
        // d2'yi belirle - aşama 2.

        glob.branch.dic.Clear();
        glob.branch.dic = d2;
        // artık işimizi branch'e taşımış olduk.

        WAVES.siaMenuCreator(glob.keyLess);
    }
    
    public static List<bool> readLocalJson()
    // json dosyamızı okur ve bool değerimizi döndürür.
    {
        byte[] data = File.ReadAllBytes (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "prefs.json"));
        Utf8JsonReader reader = new Utf8JsonReader (data);

        List<bool> me = new List<bool>();
        
        string myjsonfile = File.ReadAllText("prefs.json");
        myJson tt = JsonSerializer.Deserialize<myJson>(myjsonfile);

        me.Add(tt.runLocal);
        me.Add(tt.exitAfter);

        return me;
    }
}
