using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitten_Translate;
public partial class MainPage : Page
{
    public async Task ShowNotification(string message)
    {
        PanelText.Text = message;
        ShowPanel.Begin();
        await Task.Delay(3500);
        HidePanel.Begin();
    }
}
