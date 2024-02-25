using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslateTools.Tools;
public class Spliter
{
    public static string[] Split(string text)
    {
        char[] chars = "()[]{}!.,\"/\\?-@#$%^&*".ToCharArray(); 
        List<string> temp = [.. text.Split(' ', StringSplitOptions.RemoveEmptyEntries)];
        List<string> result = new List<string>();
        foreach (string s in temp)
        {
            
            foreach(var c in chars)
            {
                int index = s.IndexOf(c);
                if (index > -1)
                {
                    if(index == 0)
                    {
                        result.Add(s.Replace(s.Substring(1), ""));
                        result.Add(s.Substring(1));
                    }
                }
            }
        }
    }
}
