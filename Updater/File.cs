using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater;
public enum FileType
{
    Directory,
    File
}
public class File
{
    public string Name { get; set; }
    public FileType Type { get; set; }
    public File(string name, FileType type)
    {
        Name = name;
        Type = type;
    }
    internal File() { }
}
