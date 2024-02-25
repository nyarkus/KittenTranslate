using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        foreach(var s in Directory.GetDirectories(Directory.GetCurrentDirectory())) Directory.Delete(s, true);
        foreach(var s in Directory.GetFiles(Directory.GetCurrentDirectory()))
        {
            if(!s.EndsWith("Updater.exe") && !s.EndsWith("Updater.dll") && !s.EndsWith("D3DCompiler_47_cor3.dll") && !s.EndsWith("PenImc_cor3.dll")
                && !s.EndsWith("PresentationNative_cor3.dll") && !s.EndsWith("vcruntime140_cor3.dll") && !s.EndsWith("wpfgfx_cor3.dll")) System.IO.File.Delete(s);
        }
        using(var archive = SevenZipArchive.Open($"Update.v{update.Version}{update.Tag}.7z"))
        {
            archive.ExtractToDirectory(Directory.GetCurrentDirectory());
        }
        System.IO.File.Delete($"Update.v{update.Version}{update.Tag}.7z");
    }
    public static void Install(Update update, Action<double> Progress)
    {
        foreach (var s in Directory.GetDirectories(Directory.GetCurrentDirectory())) Directory.Delete(s, true);
        foreach (var s in Directory.GetFiles(Directory.GetCurrentDirectory()))
        {
            if (!s.EndsWith("Updater.exe") && !s.EndsWith("Updater.dll") && !s.EndsWith("D3DCompiler_47_cor3.dll") && !s.EndsWith("PenImc_cor3.dll")
                && !s.EndsWith("PresentationNative_cor3.dll") && !s.EndsWith("vcruntime140_cor3.dll") && !s.EndsWith("wpfgfx_cor3.dll") && !s.EndsWith(".7z")) System.IO.File.Delete(s);
        }
        using (var archive = SevenZipArchive.Open($"Update.v{update.Version}{update.Tag}.7z"))
        {
            archive.ExtractToDirectory(Directory.GetCurrentDirectory(), Progress);
        }
        System.IO.File.Delete($"Update.v{update.Version}{update.Tag}.7z");
    }
    public static void InstallUpdater(Update update, Action<double> Progress)
    {
        if (!System.IO.File.Exists($"Updater.forv{update.Version}{update.Tag}.7z"))
        {
            UpdateDownloader.DownloadUpdater(update).Wait();
        }
        Directory.CreateDirectory("@updatertemp");
        using (var archive = SevenZipArchive.Open($"Updater.forv{update.Version}{update.Tag}.7z"))
        {
            archive.ExtractToDirectory("@updatertemp", Progress);
        }
        string cmd = $"@echo off\ntaskkill /f /pid {Process.GetCurrentProcess().Id}\nmove /y \"%cd%\\@updatertemp\\*\" \"{Directory.GetCurrentDirectory()}\"\nstart Updater.exe\nexit";
        System.IO.File.WriteAllText("update.bat", cmd);
        Process.Start(new ProcessStartInfo("update.bat") { UseShellExecute = true });
    }
}
