using System.ComponentModel.Design.Serialization;
using System.Runtime.ConstrainedExecution;
using Microsoft.VisualBasic;

class WAVES
{

#region GLOBAL
    MENU menu = new MENU();
    private ENV root { get; set; }
    private ENV branch { get; set; }
    private string keyLess { get; set; }
    private string title { get; set; }
#endregion

public void DEFINE(ENV roOt, ENV branCh, string keyless, string titLE)
// root ve branch environment'lerini tanımla. 
{
    root = roOt;
    branch = branCh;
    keyLess = keyless;
    title = titLE;
}

public void START()
// uygulamayı başlatma
{
    if (root != null || branch != null || keyLess != null || title != null)
        {RunRoot();}
}

void RunRoot()
// root aşamasında yapılacaklar..
{
    Console.Clear();
    // temiz bir sayfa :)

    root.dic = menu.getDirectories(keyLess);
    // root isimli environment'in dictionary'sini, 'getFrom'daki klasörlerin isimleri ve konumları olarak ayarla. 

    var choices = root.dic.Keys.ToArray();
    // choices isimli dizimizi, root environment'imizdeki dictionary'daki klasör isimleri olarak ayarla.

    menu.CreateMenu(false,title,choices,true, root);
    // root için menümüzü oluşturalım.

    if (root.selectedName == "/..")
        {Environment.Exit(0);}
        // çıkmak isteyen çıkabilir.
    else
        {RunBranch(root.selectedPath);}
        // çıkmak istemeyen, seçtiği şık ile devam eder.
}

void RunBranch(string getFrom)
// branch/yan aşamalarda yapılacaklar..
{
    Console.Clear();
    // temiz bir sayfa :)

    branch.dic.Clear();
    // eski itemleri sil.

    branch.dic = menu.getBoth(getFrom);
    // branch isimli environment'in dictionary'sini, 'getFrom'daki dosya ve klasörlerin isimleri ve konumları olarak ayarla.

    var choices = branch.dic.Keys.ToArray();
    // choices isimli dizimizi, branch environment'imizdeki dictionary'daki itemlerin isimleri olarak ayarla.

    menu.CreateMenu(true,null,choices,true, branch);
    // branch için menümüzü oluşturalım.

    if (branch.selectedName == "/..")
        {RunRoot();return;}
        // geri dönmek isteyen dönebilir (root'a).
    else
    {
        string file = branch.selectedPath;
        // seçili itemin konumunu 'file' değişkenine ata.

        menu.InspectItem(file, branch);
        // seçili itemi incele, kullanıcının seçtiği işlemi uygula.

        RunBranch(branch.selectedPath);
        // eski yerine geri dön.
    }

}

}