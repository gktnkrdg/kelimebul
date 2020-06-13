using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KelimeBul.API
{
    public static class TurkishDictionary
    {
        //zemberek turkish database used.
        //https://code.google.com/archive/p/zemberek/downloads
        public static HashSet<string> Words = new HashSet<string>();
        public static void ReadTurkishWordsFile()
        {
            foreach (var line in File.ReadLines("tr.txt")){
                Words.Add(line);
            }
        }
    }
}
