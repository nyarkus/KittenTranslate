using CatLanguage;

namespace Translate
{
    [TestClass]
    public class Translate
    {
        [TestMethod]
        public void TranslateToCat()
        {

            string input = "Hello! I am a cat!";
            string[] words = input.Split(" ");
            words = Translateor.RemoveShortForms(words);
            string translate = "";
            foreach(var s in words)
            {
                var trans = Translateor.TranslateToCat(Newtonsoft.Json.JsonConvert.DeserializeObject<List<CatLanguage.Translate>>(File.ReadAllText(@"words\english_cat.json")), s.ToLower());
                if (s.ToLower() != trans)
                    translate += " " + trans;
                else translate += " " + s;
            }
            translate = translate.Substring(1);
            Assert.AreEqual("ììÿÿÓóÓÓ! ììßóóóÓÓ ìßó ìÿó ììÿßßÓó!", translate);
        }
        [TestMethod]
        public void TranslateToEng()
        {
            string input = "ììÿÿÓóÓÓ! ììßóóóÓÓ ìßó ìÿó ììÿßßÓó!";
            string[] words = input.Split(" ");
            string translate = "";
            bool BeUpper = true;
            foreach (var s in words)
            {
                var trans = Translateor.TranslateToEng(Newtonsoft.Json.JsonConvert.DeserializeObject<List<CatLanguage.Translate>>(File.ReadAllText(@"words\english_cat.json")), s);
                if (BeUpper)
                {
                    trans = Translateor.CapitalizeFirstLetter(trans);
                    BeUpper = false;
                }
                if (trans.EndsWith('!') || trans.EndsWith('?') || trans.EndsWith('.')) BeUpper = true;
                translate += " " + trans;
            }
            translate = translate.Substring(1);
            Assert.AreEqual("Hello! I am a cat!", translate);
        }
    }
}