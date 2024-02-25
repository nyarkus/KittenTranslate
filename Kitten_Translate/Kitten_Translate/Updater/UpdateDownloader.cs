using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitten_Translate.Updater;

public class UpdateDownloader
{
    public static async Task DownloadUpdate(Update version)
    {
        var client = new HttpClient();
        string url = $"https://github.com/nyarkus/KittenTranslate/releases/download/{version.Tag}/Kitten_Translate.Windows.Update.v{version.Version}.7z";
            var stream = await client.GetStreamAsync(url);
        using (FileStream outputStream = File.Create(Path.Combine(MainPage.WorkDirectory, $"Update.v{version.Version}{version.Tag}.7z")))
        {
            await stream.CopyToAsync(outputStream);
        }
    }
    public static async Task DownloadDictionary(Update version)
    {
        var client = new HttpClient();
        string url = $"https://github.com/nyarkus/KittenTranslate/releases/download/{version.Tag}/english_cat.json";
        var stream = await client.GetStreamAsync(url);
        using (FileStream outputStream = File.Create(Path.Combine(MainPage.WorkDirectory, $"english_cat.json")))
        {
            await stream.CopyToAsync(outputStream);
        }
    }

}
