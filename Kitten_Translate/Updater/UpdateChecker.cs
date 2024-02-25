using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Updater;

public class UpdateChecker
{
    public static async Task<Update> GetLastVersion()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://github.com/nyarkus/KittenTranslate/releases/latest");
        string? line = "";
        
        response = await httpClient.GetAsync(response.RequestMessage.RequestUri);
        string? version = null;
        using(var reader = new StreamReader(response.Content.ReadAsStream()))
        {
            while (null != (line = reader.ReadLine()))
            {
                if (line.Contains("<h1 data-view-component=\"true\" class=\"d-inline mr-3\">"))
                {
                    string tversion = line.Replace("<h1 data-view-component=\"true\" class=\"d-inline mr-3\">", "").Replace("</h1>", "").Replace(" ", "");
                    if (!tversion.StartsWith("Update.v")) continue;
                        tversion = tversion.Substring(8);
                    if(Regex.Match(tversion, "^\\d+\\.\\d+\\.\\d+\\.\\d+[a-zA-Z]*$").Success)
                    {
                        version = tversion;
                    }
                }
            }
        }
        string url = response.RequestMessage.RequestUri.ToString();
        return new Update(version, url.Substring(url.LastIndexOf('/') + 1));
    }
    public static async Task<bool> CheckDictionaryUpdate()
    {
        string hash = Kacianoki.HashGenerator.HashGenerator.ComputeSHA256("english_cat.json");
        var v = await GetLastVersion();
        var client = new HttpClient();
        var remotehash = await client.GetStringAsync($"https://github.com/nyarkus/KittenTranslate/releases/download/{v.Tag}/english_cat.json.sha256");
        remotehash = remotehash.Replace(remotehash.Substring(remotehash.IndexOf(' '), remotehash.Length - remotehash.IndexOf(' ')), "");
        if (hash != remotehash) return true;
        return false;
    }
    public static bool CheckUpdate(Update LastUpdate)
    {
        string curretupdatever = FileVersionInfo.GetVersionInfo("Kitten_Translate.Windows.exe").FileVersion;
        if(curretupdatever != LastUpdate.Version) return true;
        else return false;
    }
    

}
public class Update
{
    public string? Version { get; set; }
    public string Tag { get; set; }
    public Update(string? version, string tag) { Version = version; Tag = tag; }
}
