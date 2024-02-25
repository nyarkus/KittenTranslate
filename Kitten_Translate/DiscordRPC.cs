using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordRPC;

namespace Kitten_Translate;
public partial class MainPage : Page
{
    DiscordRpcClient client;
    async void InitRPC()
    {
        var StartTime = DateTime.UtcNow;
        client = new DiscordRpcClient("1211342546450522182");
        client.Initialize();
        string[] ImageList = { "angry_cat", "cat", "frozen_cat", "goofy_cat", "happy_cat", "nyom_nyom_cat", "omg_cat", "party_cat", "sleepy_cat", "sob_cat", "sunglasses_cat", "yapiyi" };
        Random rnd = new Random();
        while (true)
        {
            client.SetPresence(new RichPresence()
            {
                //Details = "Kitten Translate",
                State = ComboBox2.SelectedIndex == 1 ? "мяЯЯяЯв" : "Translates",
                Assets = new Assets()
                {
                    LargeImageKey = ImageList[rnd.Next(0, ImageList.Length - 1)],
                    LargeImageText = "🐈",
                },
                Timestamps = new Timestamps(StartTime)
                
            });
            await Task.Delay(30000);
        }


    }
}
