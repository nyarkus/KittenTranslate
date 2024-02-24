using CatLanguage;
using System.Collections.Generic;
using System.Text;

namespace TestGeneration
{
    [TestClass]
    public class Generation
    {
        public static List<CatSound> catsounds { get; private set; }
        public static List<Translate> list = new List<Translate>();
        [TestMethod]
        public void TestMethod1()
        {
            string[] words = File.ReadAllText(@"words\english.txt").Replace(" ", "").ToLower().Split(",");

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
            Assert.AreEqual(File.ReadAllText(@"words\english_cat.json") ,Newtonsoft.Json.JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented));
        }
        public static Translate GenerateTranslate(string Word)
        {
            var s = catsounds.First();
            try
            {
                var t = new Translate(s.CreateNewText(), Word);
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