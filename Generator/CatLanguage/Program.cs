using System.Diagnostics.Tracing;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
namespace CatLanguage
{
    public class Generator
    {
        internal static Random rnd = new Random(DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Day + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Month);
        public static List<Translate> list = new List<Translate>();
        public delegate Task meow(Translate translate);
        public static event meow? OnGenerateEnd;
        static void Main(string[] args)
        {
            Translate();
        }
        public static void Translate()
        {
            var language = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Translate>>(File.ReadAllText("words\\english_cat.json"));
            Console.WriteLine("/switch - change translate");
            Console.WriteLine("/clear - clear");
            Console.WriteLine("/generate - generate language file");
            bool fromcat = true;
            Console.Title = "Cat to Eng";
            while (true)
            {
                string input = Console.ReadLine();
                if(input == "/switch")
                {
                    if(fromcat)
                    {
                        fromcat = false;
                        Console.Title = "Eng to Cat";
                    }
                    else
                    {
                        fromcat = true;
                        Console.Title = "Cat to Eng";
                    }
                    
                }
                else if (input == "/clear") Console.Clear();
                else if(input == "/generate")
                {
                    Generate();
                    language = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Translate>>(File.ReadAllText("words\\english_cat.json"));
                }
                else
                {
                    string[] words = input.Split(" ");
                    string translate = "";
                    foreach (var s in words)
                    {
                        if (fromcat)
                            translate += " " + TranslateToEng(language, s);
                        else translate += " " + TranslateToCat(language, s.ToLower());
                    }
                    Console.WriteLine(translate);
                }
            }
        }
        public static string TranslateToCat(List<Translate> translates, string word)
        {
            try
            {

                List<char> toadd = new();
                bool oper = true;
                while(oper)
                {
                    switch (word.Last())
                    {
                        case '!':
                        case '.':
                        case ',':
                        case ';':
                        case ':':
                        case '?':
                        case '-':
                        case '_':
                        case '(':
                        case ')':
                        case '[':
                        case ']':
                        case '{':
                        case '}':
                        case '\'':
                        case '\"':
                            toadd.Add(word.Replace(word.Remove(word.Length - 1), "").ToCharArray()[0]);
                            word = word.Remove(word.Length - 1);
                            break;
                        default:
                            oper = false;
                            break;
                    }

                }
                string translate = translates.First(t => t.Word == word).sound;
                toadd.Reverse();
                foreach (var s in toadd) translate += s;
                return translate;
            }
            catch
            {
                return word;
            }
        }
        public static string TranslateToEng(List<Translate> translates, string sound)
        {
            try
            {
                List<char> toadd = new();
                bool oper = true;
                while (oper)
                {
                    switch (sound.Last())
                    {
                        case '!':
                        case '.':
                        case ',':
                        case ';':
                        case ':':
                        case '?':
                        case '-':
                        case '_':
                        case '(':
                        case ')':
                        case '[':
                        case ']':
                        case '{':
                        case '}':
                        case '\'':
                        case '\"':
                            toadd.Add(sound.Replace(sound.Remove(sound.Length - 1), "").ToCharArray()[0]);
                            sound = sound.Remove(sound.Length - 1);
                            break;
                        default:
                            oper = false;
                            break;
                    }

                }
                string translate = translates.First(t => t.sound == sound).Word;
                toadd.Reverse();
                foreach (var s in toadd) translate += s;
                return translate;
            }
            catch
            {
                return sound;
            }
        }
        public static void Generate()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string encoding = "";
            while (true)
            {
                Console.WriteLine("Write encoding");
                encoding = Console.ReadLine();
                try
                {
                    Encoding.GetEncoding(encoding);
                }
                catch
                {
                    continue;
                }
                break;
            }
            var win1251 = Encoding.GetEncoding(encoding);
            string filename = "";
            while (true)
            {
                Console.WriteLine("Write file path");
                filename = Console.ReadLine();
                if (!File.Exists(filename)) continue;
                else break;
            }
            string[] words = Encoding.UTF8.GetString(Encoding.Convert(win1251, Encoding.UTF8, win1251.GetBytes(File.ReadAllText(filename, win1251)))).Replace(" ", "").ToLower().Split(",");
            
            catsounds = [
                new CatSound("мяу",0, 1, 2).AddCharsPossibleChangeRegister('я', 'у'),
                new CatSound("мур",0, 1, 2).AddCharsPossibleChangeRegister('у'),
                new CatSound("мявк", 0, 1).AddCharsPossibleChangeRegister('я'),
                new CatSound("мяв", 0, 1).AddCharsPossibleChangeRegister('я'),
                new CatSound("мряу", 0, 1, 2, 3).AddCharsPossibleChangeRegister('я', 'у'),
                new CatSound("кх", 1),
                new CatSound("мрп", 0, 1),
                new CatSound("мя", 1).AddCharsPossibleChangeRegister('я'),
                //
                new CatSound("мяу~",0, 1, 2).AddCharsPossibleChangeRegister('я', 'у'),
                new CatSound("мур~",0, 1, 2).AddCharsPossibleChangeRegister('у'),
                new CatSound("мявк~", 0, 1).AddCharsPossibleChangeRegister('я'),
                new CatSound("мяв~", 0, 1).AddCharsPossibleChangeRegister('я'),
                new CatSound("мряу~", 0, 1, 2, 3).AddCharsPossibleChangeRegister('я', 'у'),
                new CatSound("мрп~", 0, 1),
                new CatSound("мя~", 1).AddCharsPossibleChangeRegister('я'),
                //
                new CatSound("мяу!",0, 1, 2).AddCharsPossibleChangeRegister('я', 'у'),
                new CatSound("мур!",0, 1, 2).AddCharsPossibleChangeRegister('у'),
                new CatSound("мявк!", 0, 1).AddCharsPossibleChangeRegister('я'),
                new CatSound("мяв!", 0, 1).AddCharsPossibleChangeRegister('я'),
                new CatSound("мряу!", 0, 1, 2, 3).AddCharsPossibleChangeRegister('я', 'у'),
                new CatSound("мрп!", 0, 1),
                new CatSound("мя!", 1).AddCharsPossibleChangeRegister('я'),
                //
                new CatSound("мяу...",0, 1, 2).AddCharsPossibleChangeRegister('я', 'у'),
                new CatSound("мур...",0, 1, 2).AddCharsPossibleChangeRegister('у'),
                new CatSound("мявк...", 0, 1).AddCharsPossibleChangeRegister('я'),
                new CatSound("мяв...", 0, 1).AddCharsPossibleChangeRegister('я'),
                new CatSound("мряу...", 0, 1, 2, 3).AddCharsPossibleChangeRegister('я', 'у'),
                new CatSound("мрп...", 0, 1),
                new CatSound("мя...", 1).AddCharsPossibleChangeRegister('я'),
                //
                new CatSound("мяу?",0, 1, 2).AddCharsPossibleChangeRegister('я', 'у'),
                new CatSound("мур?",0, 1, 2).AddCharsPossibleChangeRegister('у'),
                new CatSound("мявк?", 0, 1).AddCharsPossibleChangeRegister('я'),
                new CatSound("мяв?", 0, 1).AddCharsPossibleChangeRegister('я'),
                new CatSound("мряу?", 0, 1, 2, 3).AddCharsPossibleChangeRegister('я', 'у'),
                new CatSound("мрп?", 0, 1),
                new CatSound("мя?", 1).AddCharsPossibleChangeRegister('я'),
            ];
            Console.WriteLine("Generate cat words...");
            List<Thread> GenerateThread = new List<Thread>();
            int i = 0;
            int endedthread = 0;

            foreach (var s in catsounds)
            {
                var t = new Thread(() =>
                {
                    s.Generate(8);
                    endedthread++;
                    Console.WriteLine($"{Thread.CurrentThread.Name}({s.OriginalText}) end generate! Total: {endedthread}/{GenerateThread.Count}");
                });
                t.IsBackground = true;
                t.Name = i.ToString();
                GenerateThread.Add(t);
                i++;
            }
            foreach (var s in GenerateThread) s.Start();
            while (true)
            {
                bool allend = true;
                foreach (var s in GenerateThread)
                {
                    if (s.IsAlive)
                    {
                        allend = false;
                    }
                }
                if (allend) break;
            }
            int catsoundwords = 0;
            foreach (var s in catsounds) catsoundwords += s.Duplications.Count;
            Console.WriteLine($"English words count: {words.Length}\nCat words count: {catsoundwords}");
            if (catsoundwords < words.Length)
            {
                Console.WriteLine("Press Enter to continue");
                Console.ReadLine();
            }
            Console.WriteLine("Generate translate list");
            OnGenerateEnd += OnGenerated;
            //i = 0;
            foreach (var s in words)
            {
                try
                {
                    list.Add(GenerateTranslate(s));
                    //i++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    break;
                }
                //if (i >= 1000) break;
            }
            Console.WriteLine("Save...");
            File.WriteAllText(@"words\english_cat.json", Newtonsoft.Json.JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented));
        }
        public static Task OnGenerated(Translate translate)
        {
            Console.WriteLine($"{translate.Word} - {translate.sound}");
            return Task.CompletedTask;
        }
        public static List<CatSound> catsounds { get; private set; }
        public static Translate GenerateTranslate(string Word)
        {
            var s = catsounds.First();
            try
            {
                var t = new Translate(s.CreateNewText(), Word);
                OnGenerateEnd?.Invoke(t);
                return t;
            }
            catch
            {
                catsounds.RemoveAt(0);
                return GenerateTranslate(Word);
            }
        }
    }
}
