using System.Diagnostics;

namespace Updater;

public partial class Form1 : Form
{
    public delegate Task UpdateProgrssBar(double d);
    public event UpdateProgrssBar? OnUpdateProgrssBar;
    public Form1()
    {
        InitializeComponent();
    }
    private Task Update(double value)
    {
        try
        {
            ProgressBar.Value = (int)value * 100;
        }
        catch
        {

        }
        return Task.CompletedTask;
    }
    private void StatusLabel_Click(object sender, EventArgs e)
    {

    }

    private async void Form1_Shown(object sender, EventArgs e)
    {
        OnUpdateProgrssBar += Update;
        var LastVersion = await UpdateChecker.GetLastVersion();
        /*
        Update(0.5);
        var DictonaryNeededUpdate = await UpdateChecker.CheckDictionaryUpdate();
        Update(1);
        if (!UpdateChecker.CheckUpdate(LastVersion))
        {
            MessageBox.Show("You already have the latest version installed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
        */
        LastVersion = new Update("1.0.1.0", "pre2");
        StatusLabel.Text = "Unpacking";
        await Task.Run(() =>
        {
            UpdateInstaller.Install(LastVersion, new Action<double>(d =>
            {
                OnUpdateProgrssBar.Invoke(d);
            }));
        });
        string[] directories = GetAllDirectories(Directory.GetCurrentDirectory());
        List<string> files = new List<string>();
        StatusLabel.Text = "Preparation";
        await OnUpdateProgrssBar.Invoke(0);
        await Task.Run(() =>
        {
            foreach (var directory in directories)
            {
                foreach (var s in Directory.GetFiles(directory)) files.Add(s);
            }
            OnUpdateProgrssBar.Invoke(1);
        });
        await OnUpdateProgrssBar.Invoke(0);
        StatusLabel.Text = "Install";
        var FileList = from f in Newtonsoft.Json.JsonConvert.DeserializeObject<List<File>>(System.IO.File.ReadAllText("updatelist.json")) where f.Type == FileType.File select f.Name;
        var DirectoryList = from f in Newtonsoft.Json.JsonConvert.DeserializeObject<List<File>>(System.IO.File.ReadAllText("updatelist.json")) where f.Type == FileType.Directory select f.Name;
        await Task.Run(() =>
        {
            int i = 0;
            foreach(var s in files)
            {
                if (!FileList.Contains(s.Substring(Directory.GetCurrentDirectory().Length)))
                {
                    if(System.IO.File.Exists(s))
                    System.IO.File.Delete(s);
                }
                i++;
                OnUpdateProgrssBar.Invoke(i / (directories.Length + files.Count));
            }
            foreach (var s in directories)
            {
                if (!DirectoryList.Contains(s.Substring(Directory.GetCurrentDirectory().Length)))
                {
                    if(Directory.Exists(s))
                    Directory.Delete(s, true);
                }
                i++;
                OnUpdateProgrssBar.Invoke(i / (directories.Length + files.Count));
            }
        });
        if (OpenAfterUpdate.Checked) Process.Start(new ProcessStartInfo("Kitten_Translate.Windows.exe") { UseShellExecute = true });
        Application.Exit();
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
