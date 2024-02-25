

using System.Diagnostics;

namespace UpdateBuilder;

internal class Program
{
    static void Main(string[] args)
    {
        Console.Write("Path: ");
        string path = Console.ReadLine();
        Console.WriteLine("");
        Console.WriteLine("Collection of information");
        string[] Directories = GetAllDirectories(path);
        List<string> files = new List<string>();
        foreach(var s in Directories)
        {
            foreach(var f in Directory.GetFiles(s)) files.Add(f);
        }
        List<Updater.File> UpdateFileList = new();
        foreach(var s in Directories) UpdateFileList.Add(new(s.Substring(path.Length), Updater.FileType.Directory));
        foreach(var s in files) UpdateFileList.Add(new(s.Substring(path.Length), Updater.FileType.File));
        File.WriteAllText(Path.Combine(path, "updatelist.json"), Newtonsoft.Json.JsonConvert.SerializeObject(UpdateFileList));
        Console.WriteLine($"Update name: Kitten_Translate.Windows.Update.v{FileVersionInfo.GetVersionInfo(Path.Combine(path, "Kitten_Translate.Windows.exe")).FileVersion}");
        
    }
    public static string[] GetAllDirectories(string path)
    {
        List<string> result = new List<string>();
        string[] dirs = Directory.GetDirectories(path);
        if (dirs.Length == 0) return result.ToArray();
        foreach (var dir in dirs)
        {
            result.Add(dir);
            var s = GetAllDirectories(dir);
            foreach (var dir2 in s) result.Add(dir2);
        }
        return result.ToArray();
    }
}
