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
        public static bool CanBeMadeFromLetters(this string word, string letters)
        {

            var subLetterCounts = GetLetterCounts(letters);
            var wordLetterCounts = GetLetterCounts(word);
            return wordLetterCounts.All(letter =>
                subLetterCounts.ContainsKey(letter.Key)
                && subLetterCounts[letter.Key] >= letter.Value);
        }
        public static string Shuffle(this string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }

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
