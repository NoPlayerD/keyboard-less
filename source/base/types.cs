namespace types;

public class myFile
{
    public string nameWithExt { get; set; }
    public string nameWithoutExt { get; set; }


    public string pathWithNameWithExt { get; set; }
    public string pathWithNameWithoutExt { get; set; }

    public string parentName { get; set; }
    public string parentPath { get; set; }
}



public class myFolder
{
    public string name { get; set; }
    public string path { get; set; }
}



public class myJson
{
    public bool runLocal { get; set; }
    public bool exitAfterSelection { get; set; }
    public bool showHeader { get; set; }
    public bool inspectWithSelection { get; set; }
    public int pageSize { get; set; }
    public bool showHeaderSeparator { get; set; }
    public bool showSeparator { get; set; }
}



public class myMenu
{
    public string title { get; set; }
    public bool enableSearch { get; set; }
    public int pageSize { get; set; }
    public string[] choices { get; set; }
}