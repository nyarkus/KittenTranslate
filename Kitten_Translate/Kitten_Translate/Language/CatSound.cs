using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitten_Translate;

public class CatSound
{
    public string OriginalText { get; }
    public int[] IndicesPossibleDuplications { get; }
    public char[] CharsPossibleChangeRegister { get; private set; }
    public List<string> Duplications { get; private set; } = new List<string>();
    public string CreateNewText()
    {
        var s = Duplications.First();
        Duplications.RemoveAt(0);
        return s;
    }
    public void Generate(int MaxLength)
    {
        Duplications = GenerateVariations(OriginalText, IndicesPossibleDuplications, MaxLength, CharsPossibleChangeRegister);
    }
    private List<string> GenerateVariations(string originalText, int[] indicesPossibleDuplications, int maxLength, char[] charsPossibleChangeRegister)
    {
        List<string> results = new List<string>();
        for (int length = originalText.Length; length <= maxLength; length++)
        {
            GenerateVariationsRecursive(originalText, indicesPossibleDuplications, length, originalText, results, charsPossibleChangeRegister);
        }
        return results;
    }
    static void GenerateVariationsRecursive(string originalText, int[] indicesPossibleDuplications, int targetLength, string current, List<string> results, char[] charsPossibleChangeRegister)
    {
        if (current.Length == targetLength)
        {
            if (charsPossibleChangeRegister != null)
            {
                GenerateCasePermutations(current, results, charsPossibleChangeRegister);
            }
            else
            {
                results.Add(current);
            }
            return;
        }

        foreach (int index in indicesPossibleDuplications)
        {
            if (index < originalText.Length)
            {
                string next = current.Insert(current.IndexOf(originalText[index]), originalText[index].ToString());
                GenerateVariationsRecursive(originalText, indicesPossibleDuplications, targetLength, next, results, charsPossibleChangeRegister);

            }
        }
    }

    static void GenerateCasePermutations(string text, List<string> results, char[] charsPossibleChangeRegister)
    {
        int count = (int)Math.Pow(2, text.Length);
        for (int i = 0; i < count; i++)
        {
            char[] combination = text.ToCharArray();
            for (int j = 0; j < text.Length; j++)
            {
                if (((i >> j) & 1) == 1 && Array.IndexOf(charsPossibleChangeRegister, combination[j]) != -1)
                {
                    combination[j] = char.ToUpper(combination[j]);
                }
            }
            string result = new string(combination);
            if (!results.Contains(result))
            {
                results.Add(result);
            }
        }
    }
    public CatSound AddCharsPossibleChangeRegister(params char[] chars)
    {
        CharsPossibleChangeRegister = chars;
        return this;
    }
    public CatSound(string originalText, params int[] indicesPossibleDuplications)
    {
        OriginalText = originalText;
        IndicesPossibleDuplications = indicesPossibleDuplications;
    }
}
