using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater;

public class UpdateInstaller
{
    public static void Install(Update update)
    {
        if (!System.IO.File.Exists($"Update.v{update.Version}{update.Tag}.7z"))
        {
            UpdateDownloader.DownloadUpdate(update).Wait();
        }
        using(var archive = SevenZipArchive.Open($"Update.v{update.Version}{update.Tag}.7z"))
        {
            archive.ExtractToDirectory(Directory.GetCurrentDirectory());
        }
        System.IO.File.Delete($"Update.v{update.Version}{update.Tag}.7z");
    }
    public static void Install(Update update, Action<double> Progress)
    {
        if (!System.IO.File.Exists($"Update.v{update.Version}{update.Tag}.7z"))
        {
            UpdateDownloader.DownloadUpdate(update).Wait();
        }
        using (var archive = SevenZipArchive.Open($"Update.v{update.Version}{update.Tag}.7z"))
        {
            archive.ExtractToDirectory(Directory.GetCurrentDirectory(), Progress);
        }
        System.IO.File.Delete($"Update.v{update.Version}{update.Tag}.7z");
    }
}
