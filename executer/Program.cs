using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

foreach (string arg in args)
{
        string file = arg.Replace('?', ' ');
        using (Process myP = new Process())
        {
                myP.StartInfo.FileName = file;
                myP.StartInfo.UseShellExecute = true;
                //myP.StartInfo.WorkingDirectory = Path.GetDirectoryName(myP.StartInfo.FileName);
                myP.Start();
        }
}
Console.ReadKey();