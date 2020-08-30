using KelimeBul.API.Models;
using KelimeBul.API.Service;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KelimeBul.API
{
    public class TurkishWordsService : ITurkishWordsService
    {

        public static HashSet<string> Words = new HashSet<string>();
        private IWebHostEnvironment _env;

        public TurkishWordsService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string CreateRandomWord(int length)
        {

            var word = Words.Where(x => x.Length == length).RandomElement<string>();
            if (word.Length > 0)
            {
                return word.Shuffle();
            }
            return "";

        }

        public List<string> DeriveWord(string word, int minLength, int maxLength)
        {
            var result = new List<string>();
            if (word.Length > 10)
                return result;
            maxLength = (maxLength == 0 ? word.Length : maxLength);
            var turkishWords = Words.Where(x => (x.Length <= maxLength) && x.Length >= minLength);
            foreach (var turkishWord in turkishWords)
            {
                if (turkishWord.CanBeMadeFromLetters(word.ToLower()))
                    result.Add(turkishWord);
            }
            return result;
        }

        public void ReadFromFile(string filename)
        {

            try
            {
                var file = Path.Combine(_env.ContentRootPath, filename);
                foreach (var line in File.ReadLines(file))
                {
                    Words.Add(line);
                }

            }
            catch (Exception ex)
            {
                //todo
            }

        }

        public List<WordLength> WordLengths()
        {
            return Words.GroupBy(n => n.Length)
           .Select(n => new WordLength
           {
               Length = n.Key,
               Count = n.Count()
           })
           .OrderBy(n => n.Length).ToList();
        }
    }
}
