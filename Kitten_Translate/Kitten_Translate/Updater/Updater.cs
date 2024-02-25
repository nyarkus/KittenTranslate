using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitten_Translate.Updater;

namespace Kitten_Translate;
public sealed partial class MainPage : Page
{
    public async Task CheckAllUpdates()
    {
        Updated += OnDictonaryUpdated;
        var lastv = await UpdateChecker.GetLastVersion();
        if(UpdateChecker.CheckUpdate(lastv))
            AskAboutUpdate(lastv);
        if(await UpdateChecker.CheckDictionaryUpdate())
            AskAboutUpdateDictonary(lastv);
    }
    public async Task OnDictonaryUpdated()
    {
        await ShowNotification("Словарь обновлён");
    }
}
