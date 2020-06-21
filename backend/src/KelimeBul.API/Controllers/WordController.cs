using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KelimeBul.API.Models;
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
        public IActionResult Random([FromQuery][Required()] int length)
        {
            var word = TurkishDictionary.Words.Where(x => x.Length == length).RandomElement<string>();
            return new OkObjectResult(new { word = word .Shuffle()});
        }
        [HttpGet]
        [Route("{word}")]
        public IActionResult Get([FromRoute] string word)
        {
            return new OkObjectResult(new { exist = TurkishDictionary.Words.Contains(word.ToLower()) });

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
