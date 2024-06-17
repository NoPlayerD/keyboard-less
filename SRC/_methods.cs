using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.VisualBasic.FileIO;
using Spectre.Console;
using System.Text.Json;
using System.Globalization;
using System.Runtime.InteropServices;


class WAVES
{

public static void START()
// uygulamayı başlatma
{
    if (glob.root != null || glob.branch != null || glob.keyLess != null || glob.title != null)
        {RunRoot();}
}

public static void siaMenuCreator(string getFrom)
// search in all - siaStartLine'ın geldiği yer.
{
    Console.Clear();
    // temiz bir sayfa :)

    var choices = glob.branch.dic.Keys.ToArray();
    // choices isimli dizimizi, branch environment'imizdeki dictionary'daki itemlerin isimleri olarak ayarla.

    MENU.CreateMenu(choices, glob.branch);
    // branch için menümüzü oluşturalım.

    if (glob.branch.selectedName == glob.excludeOfBranch[0])
        {RunRoot();return;}
        // geri dönmek isteyen dönebilir (root'a).
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

    MENU.CreateMenu(choices, glob.root);
    // root için menümüzü oluşturalım.

    if (glob.root.selectedName == glob.excludeOfRoot[0])
        {Environment.Exit(0);}
        // çıkmak isteyen çıkabilir.
    else if (glob.root.selectedName == glob.excludeOfRoot[1])
    // tüm kategorilerde mi arican la.
    {
        Methods.siaStartLine();
    }
    else if(glob.root.selectedName == glob.excludeOfRoot[2])
    // konum açma seçeneği.
    {
        MENU.ExecuteItem(glob.keyLess);
        RunRoot();
    }
    else if(glob.root.selectedName == glob.excludeOfRoot[3])
    // ayarlar (preferences) seçeneği.
    {
        MENU.createPrefsMenu(Methods.readLocalJson());
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

    MENU.CreateMenu(choices, glob.branch);
    // branch için menümüzü oluşturalım.

    if (glob.branch.selectedName == glob.excludeOfBranch[0])
        {RunRoot();return;}
        // geri dönmek isteyen dönebilir (root'a).
    else if(glob.branch.selectedName == glob.excludeOfBranch[1])
    {MENU.ExecuteItem(glob.root.selectedPath);}
        // seçili kategorinin konumuna git.
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

public static void CreateMenu(string[] choices, ENV virtualEnv)
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
        {exclude = glob.excludeOfRoot;
        title = glob.title;} 
// root ise..
else 
        {exclude = glob.excludeOfBranch;
        title = null;}// define the 'exclude'
// branch ise


var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title(title)
        .EnableSearch()
        .PageSize(100)
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
        Process p = new Process();
        p.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,  @"exec\executer.exe");
        p.StartInfo.Arguments = Path.GetFileName(file.Replace(' ', '?'));
        p.StartInfo.UseShellExecute = false;
        //p.Verb = "OPEN";
        p.StartInfo.WorkingDirectory = Path.GetDirectoryName(file);
        p.Start();  
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


public static void createPrefsMenu(bool jsonState)
// ayarlar (preferences) menüsünün oluşturulması ve kontrolü..
{
        string[] choices = {"Run Locally? - FALSE", "Run Locally? - TRUE"};
        // 1 seçeneğimiz olacak var 2 farklı türde de olabilir..

        int state;
        if (jsonState == true) {state = 1;}
        else {state = 0;}
        // json dosyamızdaki değere göre hangi seçeneğin seçileceğine karar verme.

        Console.Clear();
        // temiz bir sayfa :)

        var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .AddChoices(glob.excludeOfBranch[0])
        .AddChoices(choices[state]));
        // menümüzü oluşturalım :)

        if (menu == choices[state])
        // eğer "o" seçeneği seçersek seçeneği ve dosyayı düzenleme..
        {
                bool old = Methods.readLocalJson();
                bool newer;
                if (old == false){newer = true;}
                else {newer = false;}
                
                File.Delete(Methods.myJson);
                Methods.createJson(Methods.myJson, newer);

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
                {createJson(myJson, true);
                glob.runLocal = readLocalJson();}
        else
                {glob.runLocal = readLocalJson();}

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
    public static void createJson(string json, bool state)
    // json dosyamızı oluşturur.
    {
        var options = new JsonWriterOptions { Indented = true };
        using (var stream = File.Create (json))
        using (var writer = new Utf8JsonWriter (stream, options))
        {
                writer.WriteStartObject();
                // Property name and value specified in one call
                /// writer.WritePropertyName("runLocal?");
                writer.WriteBoolean("runLocal?",state);
                // writer.WriteCommentValue ("Run application on binary(local) path? or default path?");
                writer.WriteEndObject();
        }

    }
    public static bool readLocalJson()
    // json dosyamızı okur ve bool değerimizi döndürür.
    {
        byte[] data = File.ReadAllBytes (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "prefs.json"));
        Utf8JsonReader reader = new Utf8JsonReader (data);

        bool me = false;

        while (reader.Read())
        {
        switch (reader.TokenType)
        {
        case JsonTokenType.False:
                me = reader.GetBoolean();
                break;
        case JsonTokenType.True:
                me = reader.GetBoolean();
                break;
        }
        }
        return me;
    }
}
