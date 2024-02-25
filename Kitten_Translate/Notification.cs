using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitten_Translate;
public partial class MainPage : Page
{
    public Task ShowNotification(string message)
    {
        PanelText.Text = message;
        ShowPanel.Begin();
        return Task.CompletedTask;
    }
    public async Task ShowNotificationLoading(string message)
    {
        PanelText.Text = message;
        ShowPanel.Begin();
        while (true)
        {
            await Task.Delay(350);
            message += ".";
            await Task.Delay(350);
            message += ".";
            await Task.Delay(350);
            message += ".";
            await Task.Delay(350);
            message = message.Replace(message.Substring(message.Length - 3), "");
        }
    }
    public async Task ShowNotification(string message, TimeSpan duration)
    {
        PanelText.Text = message;
        ShowPanel.Begin();
        await Task.Delay(duration);
        HidePanel.Begin();
    }
}
