using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace KelimeBul.API
{
    public static class WordExtension
    {


        //https://stackoverflow.com/questions/41076904/check-if-chars-of-a-string-contains-in-another-string-with-linq
        public static bool CanBeMadeFromLetters(this string word, string tileLetters)
        {

            var tileLetterCounts = GetLetterCounts(tileLetters);
            var wordLetterCounts = GetLetterCounts(word);

            return wordLetterCounts.All(letter =>
                tileLetterCounts.ContainsKey(letter.Key)
                && tileLetterCounts[letter.Key] >= letter.Value);
        }
        public static string Shuffle(this string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }
        //Gets dictionary of letter/# of letter in word
        public static Dictionary<char, int> GetLetterCounts(string word)
        {
            return word
                .GroupBy(c => c)
                .ToDictionary(
                    grp => grp.Key,
                    grp => grp.Count());
        }
    }
}
