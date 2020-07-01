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
        [Route("derive/{word}")]
        [HttpGet]
        public IEnumerable<string> Derive(string word,int minLength=4,int maxLength=0)
        {
            var result = new List<string>();
            if (word.Length > 10)
                return result;
            maxLength = (maxLength == 0 ? word.Length : maxLength);
            var turkishWords = TurkishDictionary.Words.Where(x => (x.Length <= maxLength) && x.Length >= minLength);
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
            if (word.Length > 0)
            {
                return new OkObjectResult(new RandomWordResponseModel { Word = word.Shuffle() });
            }
            return NotFound();

        }

        [HttpHead]
        [Route("{word}")]
        public IActionResult Exist([FromRoute] string word)
        {
            var exist = TurkishDictionary.Words.Contains(word.ToLower());
            if (exist)
            {
                return Ok();
            }
            return NotFound();

        }

        [HttpGet("wordlengthcount")]
        public List<WordLengthCountModel> Count()
        {
            var groups = TurkishDictionary.Words
            .GroupBy(n => n.Length)
            .Select(n => new WordLengthCountModel
            {
                WordLength = n.Key,
                Count = n.Count()
            })
            .OrderBy(n => n.WordLength);
            return groups.ToList();
        }

    }
}
