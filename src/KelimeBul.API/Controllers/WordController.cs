using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;

namespace KelimeBul.API.Controllers
{
    [ApiController]
    [Route("api/v1/words")]
    public class WordController : ControllerBase
    {
        private static readonly Random random = new Random();
        [Route("derive/{word}")]
        [HttpGet]
        public IEnumerable<string> Derive(string word)
        {
            var result = new List<string>();
            if (word.Length > 10)
                return result;
            var turkishWords = TurkishDictionary.Words.Where(x => (x.Length <= word.Length) && x.Length > 3);
            foreach (var turkishWord in turkishWords)
            {
                if (turkishWord.CanBeMadeFromLetters(word.ToLower()))
                    result.Add(turkishWord);
            }
            return result;
        }
        [HttpGet]
        [Route("random")]
        public string Random([FromQuery][Required()] int length)
        {
            return TurkishDictionary.Words.Where(x => x.Length == length).RandomElement<string>();
        }
        [HttpGet]
        [Route("{word}")]
        public bool Get([FromRoute]string word)
        {
            return TurkishDictionary.Words.Contains(word.ToLower());
        }
        public class CountModel
        {
            public int WordLength { get; set; }
            public int Count { get; set; }
        }
        [HttpGet("count")]
        public List<CountModel> Count()
        {
            var groups = TurkishDictionary.Words
            .GroupBy(n => n.Length)
            .Select(n => new CountModel
            {
                WordLength = n.Key,
                Count = n.Count()
            })
            .OrderBy(n => n.WordLength);
            return groups.ToList();
        }

    }
}
