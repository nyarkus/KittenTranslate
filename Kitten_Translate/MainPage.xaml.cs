using System.Text;

namespace Kitten_Translate;

public partial class MainPage : Page
{
    public static string WorkDirectory = "";
    public MainPage()
    {
        WorkDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(WorkDirectory, "english_cat.json");
        language = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Translate>>(File.ReadAllText(path));
        this.InitializeComponent();
    }
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        Panel.Visibility = Visibility.Collapsed;
        PanelText.Visibility = Visibility.Collapsed;
        HidePanel.Begin();
        await CheckAllUpdates();
        Panel.Visibility = Visibility.Visible;
        PanelText.Visibility = Visibility.Visible;
        InitRPC();
    }
    public static List<Translate> language = new List<Translate>();
    bool translateinprogress = false;
    bool WasEmpty = true;
    bool BeUpper = true;
    private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        await Task.Delay(100);
        if (translateinprogress) return;
        translateinprogress = true;
        string input = Input.Text;
        if (WasEmpty)
        {
            var lang = Translateor.GetLanguage(input);
            if (ComboBox2.SelectedIndex == 1 && lang == Kitten_Translate.Language.Cat)
            {
                ComboBox2.SelectedIndex = 0;
                ComboBox1_SelectionChanged(ComboBox1, null);
            }
            else if (ComboBox2.SelectedIndex == 0 && lang == Kitten_Translate.Language.English)
            {
                ComboBox2.SelectedIndex = 1;
                ComboBox1_SelectionChanged(ComboBox1, null);
            }
        }
        if (input is null || input is "")
        {
            WasEmpty = true;
            translateinprogress = false;
            return;
        }
        else WasEmpty = false;
        string[] words = input.Split(' ');
        if (ComboBox2.SelectedIndex == 1)
            words = Translateor.RemoveShortForms(words);
        string translate = "";
        foreach (var s in words)
        {
            if (ComboBox2.SelectedIndex == 1)
            {
                var trans = Translateor.TranslateToCat(language, s.ToLower());
                if(s.ToLower() != trans)
                translate += " " + trans;
                else translate += " " + s;
            }
            else if (ComboBox2.SelectedIndex == 0)
            {
                var trans = Translateor.TranslateToEng(language, s);
                if (BeUpper)
                {
                    trans = Translateor.CapitalizeFirstLetter(trans);
                    BeUpper = false;
                }
                if (trans.EndsWith('!') || trans.EndsWith('?') || trans.EndsWith('.')) BeUpper = true;
                translate += " " + trans;
            }
        }
        BeUpper = true;
        Output.Text = translate.Substring(1);
        translateinprogress = false;
    }
    private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            ComboBox comboBox = (ComboBox)sender;
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    ComboBox2.SelectedIndex = 1;
                    TextBox_TextChanged(null, null);
                    break;
                case 1:
                    ComboBox2.SelectedIndex = 0;
                    TextBox_TextChanged(null, null);
                    break;
            }
        }
        catch
        {

        }
    }
    private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            ComboBox comboBox = (ComboBox)sender;
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    ComboBox1.SelectedIndex = 1;
                    TextBox_TextChanged(null, null);
                    break;
                case 1:
                    ComboBox1.SelectedIndex = 0;
                    TextBox_TextChanged(null, null);
                    break;
            }
        }
        catch
        {

        }
    }
}
