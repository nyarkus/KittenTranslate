using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Kitten_Translate.Updater;

namespace Kitten_Translate;
public sealed partial class MainPage : Page
{
    public delegate Task Updates();
    public static event Updates? Updated;
    public static event Updates? UpdateDownloadStart;
    public async void AskAboutUpdate(Update update)
    {
        string cont = $"Найдена новая версия приложения {update.Version}\nСкачать и установить её?";
        var c = new ContentDialog()
        {
            Title = "Обновление",
            Content = cont,
            PrimaryButtonText = "Да",
            DefaultButton = ContentDialogButton.Primary,
            XamlRoot = XamlRoot,
            SecondaryButtonText = "Нет",
            PrimaryButtonCommand = new Download(),
            PrimaryButtonCommandParameter = update,
        };
        while (Showed) await Task.Delay(100);
        Showed = true;
        await c.ShowAsync();
        
    }
    public static bool Showed = false;
    public async void AskAboutUpdateDictonary(Update update)
    {
        string cont = $"Найдена новая версия словаря\nСкачать и установить её?";
        var c = new ContentDialog()
        {
            Title = "Обновление",
            Content = cont,
            PrimaryButtonText = "Да",
            DefaultButton = ContentDialogButton.Primary,
            XamlRoot = XamlRoot,
            SecondaryButtonText = "Нет",
            PrimaryButtonCommand = new DownloadDictonary(),
            PrimaryButtonCommandParameter = update,
        };
        while (Showed) await Task.Delay(100);
        Showed = true;
        await c.ShowAsync();
    }
    class Download : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            UpdateDownloadStart?.Invoke();
            await UpdateDownloader.DownloadUpdate((Update)parameter);
            Process.Start(new ProcessStartInfo(Path.Combine(MainPage.WorkDirectory, "Updater.exe")) { UseShellExecute = true, WorkingDirectory = MainPage.WorkDirectory });
            Process.GetCurrentProcess().Kill();
        }
    }
    class DownloadDictonary : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            await UpdateDownloader.DownloadDictionary((Update)parameter);
            string path = Path.Combine(MainPage.WorkDirectory, "english_cat.json");
            language = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Translate>>(File.ReadAllText(path));
            Updated?.Invoke();
            Showed = false;
        }
    }
}
