using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CatLanguage;
public enum Language
{
    Cat,
    English
}
public class Translateor
{
    public static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        char firstChar = char.ToUpper(input[0]);
        string restOfString = input.Substring(1);

        return firstChar + restOfString;
    }

    public static Language GetLanguage(string text)
    {
        try
        {


            char[] CatSounds = "мяувкршх".ToCharArray();
            string pattern = "[^a-zA-Zа-яА-Я]";
            text = Regex.Replace(text, pattern, "");
            int CatSoundsInText = 0;
            text = text.ToLower();
            foreach (var s in text) if (CatSounds.Contains(s)) CatSoundsInText++;
            if (CatSoundsInText / text.Length * 100 > 50) return Language.Cat;
            else return Language.English;
        }
        catch
        {
            return Language.English;
        }
    }
    public static string TranslateToCat(List<Translate> translates, string word)
    {
        List<char> toadd = new();
        try
        {

            
            bool oper = true;
            while (oper)
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
            if (word.EndsWith("s"))
            {
                string s = TranslateToCat(translates, word.Remove(word.Length - 1));
                if (s != word.Remove(word.Length - 1)) 
                {
                    string translate = s;
                    toadd.Reverse();
                    foreach (var ss in toadd) translate += ss;
                    return translate;
                }
                else return s;
            }
            else return word;
        }
    }
    public static string[] RemoveShortForms(string[] words)
    {
        List<string> result = new List<string>();
        foreach (var word in words)
        {
            switch (word.ToLower())
            {
                case "i’m":
                    result.Add("i");
                    result.Add("am");
                break;
                case "we’re":
                    result.Add("we");
                    result.Add("are");
                    break;
                case "you’re":
                    result.Add("you");
                    result.Add("are");
                    break;
                case "they’re":
                    result.Add("they");
                    result.Add("are");
                    break;
                case "we've":
                    result.Add("we");
                    result.Add("have");
                    break;
                case "i've":
                    result.Add("i");
                    result.Add("have");
                    break;
                case "you've":
                    result.Add("you");
                    result.Add("have");
                    break;
                case "they've":
                    result.Add("they");
                    result.Add("have");
                    break;
                case "i'd":
                    result.Add("i");
                    result.Add("would");
                    break;
                case "he'd":
                    result.Add("he");
                    result.Add("would");
                    break;
                case "she'd":
                    result.Add("she");
                    result.Add("would");
                    break;
                case "it'd":
                    result.Add("it");
                    result.Add("would");
                    break;
                case "we'd":
                    result.Add("we");
                    result.Add("would");
                    break;
                case "you'd":
                    result.Add("you");
                    result.Add("would");
                    break;
                case "they'd":
                    result.Add("they");
                    result.Add("would");
                    break;
                case "i'll":
                    result.Add("i");
                    result.Add("will");
                    break;
                case "he'll":
                    result.Add("he");
                    result.Add("will");
                    break;
                case "she'll":
                    result.Add("she");
                    result.Add("will");
                    break;
                case "it'll":
                    result.Add("it");
                    result.Add("will");
                    break;
                case "we'll":
                    result.Add("we");
                    result.Add("will");
                    break;
                case "you'll":
                    result.Add("you");
                    result.Add("will");
                    break;
                case "they'll":
                    result.Add("they");
                    result.Add("will");
                    break;
                case "isn't":
                    result.Add("is");
                    result.Add("not");
                    break;
                case "hasn't":
                    result.Add("has");
                    result.Add("not");
                    break;
                case "don't":
                    result.Add("do");
                    result.Add("not");
                    break;
                case "can't":
                    result.Add("cannot");
                    break;
                case "aren't":
                    result.Add("are");
                    result.Add("not");
                    break;
                case "haven't":
                    result.Add("have");
                    result.Add("not");
                    break;
                case "doesn't":
                    result.Add("does");
                    result.Add("not");
                    break;
                case "couldn't":
                    result.Add("could");
                    result.Add("not");
                    break;
                case "wasn't":
                    result.Add("was");
                    result.Add("not");
                    break;
                case "hadn't":
                    result.Add("had");
                    result.Add("not");
                    break;
                case "didn't":
                    result.Add("did");
                    result.Add("not");
                    break;
                case "won't":
                    result.Add("will");
                    result.Add("not");
                    break;
                case "weren't":
                    result.Add("were");
                    result.Add("not");
                    break;
                case "wouldn't":
                    result.Add("would");
                    result.Add("not");
                    break;
                case "shouldn't":
                    result.Add("should");
                    result.Add("not");
                    break;
                case "mustn't":
                    result.Add("must");
                    result.Add("not");
                    break;
                case "needn't":
                    result.Add("need");
                    result.Add("not");
                    break;
                case "mightn't":
                    result.Add("might");
                    result.Add("not");
                    break;
                case "daren't":
                    result.Add("dare");
                    result.Add("not");
                    break;
                case "who's":
                    result.Add("who");
                    result.Add("is");
                    break;
                case "who'd":
                    result.Add("who");
                    result.Add("would");
                    break;
                case "who'll":
                    result.Add("who");
                    result.Add("will");
                    break;
                case "what's":
                    result.Add("what");
                    result.Add("is");
                    break;
                case "what'll":
                    result.Add("what");
                    result.Add("will");
                    break;
                case "how's":
                    result.Add("how");
                    result.Add("is");
                    break;
                case "where's":
                    result.Add("where");
                    result.Add("is");
                    break;
                case "when's":
                    result.Add("when");
                    result.Add("is");
                    break;
                case "here's":
                    result.Add("here");
                    result.Add("is");
                    break;
                case "there's":
                    result.Add("there");
                    result.Add("is");
                    break;
                case "that's":
                    result.Add("that");
                    result.Add("is");
                    break;
                case "there'd":
                    result.Add("there");
                    result.Add("would");
                    break;
                case "there'll":
                    result.Add("there");
                    result.Add("will");
                    break;
                default:
                    result.Add(word);
                    break;

            }
        }
        return result.ToArray();
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
}
