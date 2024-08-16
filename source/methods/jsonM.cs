using System.Text.Json;

public class jsonMethods
{
    // returns a bool ('preferences.json' exists?)
    public static bool checkIfJsonExists()
    {
        var me = File.Exists(global.jsonPath);
        return me;
    }


    // reads the 'preferences.json' and repairs it if needed
    public static void repairIfNeeded()
    {
        var me = readMyJson;
    }


    // reads 'preferences.json' and returns the value of it
    public static myJson readMyJson()
    {
        var me = new myJson();
                
        string jsFile = File.ReadAllText(global.jsonPath);
        myJson tt = JsonSerializer.Deserialize<myJson>(jsFile);

        try
        {
            me.runLocal = tt.runLocal;
            me.exitAfterSelection = tt.exitAfterSelection;
            me.showHeader = tt.showHeader;
            me.inspectWithSelection = tt.inspectWithSelection;
            me.pageSize = tt.pageSize;
        }
        catch(Exception ex)
        {
            File.Delete(global.jsonPath);
            writeMyJson();
            var json = readMyJson();
        }

        return me;
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
            pageSize = 20
        };
    
        string jsonFile = JsonSerializer.Serialize(json);

        File.WriteAllText(global.jsonPath,jsonFile);
    
    }
}