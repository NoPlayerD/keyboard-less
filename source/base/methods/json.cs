using System.Text.Json;

using variables;
using types;
public class jsonMethods
{
    // returns a bool ('preferences.json' exists?)
    public static bool checkIfJsonExists()
    {
        var me = File.Exists(global.jsonPath);
        return me;
    }


    // restores the 'prefences.json' file to the defaults
    public static void restoreDefaults()
    {
        File.Delete(global.jsonPath);
        writeMyJson();
        readMyJson();
    }


    // reads 'preferences.json' and writes it to 'json'
    public static void readMyJson()
    {
        string jsFile = File.ReadAllText(global.jsonPath);
        myJson me = JsonSerializer.Deserialize<myJson>(jsFile);

        json.runLocal = me.runLocal;
        json.exitAfterSelection = me.exitAfterSelection;
        json.showHeader = me.showHeader;
        json.inspectWithSelection = me.inspectWithSelection;
        json.pageSize = me.pageSize;
        json.showHeaderSeparator = me.showHeaderSeparator;
        json.showSeparator = me.showSeparator;
    }


    // writes the 'preferences.json' from zero
    public static void writeMyJson()
    {
        myJson json = new myJson
        {
            runLocal = true,
            exitAfterSelection = false,
            showHeader = true,
            inspectWithSelection = true,
            pageSize = 20,
            showHeaderSeparator = true,
            showSeparator = true
        };
    
        string jsonFile = JsonSerializer.Serialize(json);

        File.WriteAllText(global.jsonPath,jsonFile);
    
    }
}