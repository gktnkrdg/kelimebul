using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KelimeBul.API.Controllers
{
    [ApiController]
    [Route("api/v1/words")]
    public class WordController : ControllerBase
    {

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
                if (turkishWord.CanBeMadeFromLetters(word))
                    result.Add(turkishWord);
            }
            return result;
        }
    
        [HttpGet("{word}")]
        public bool Get(string word)
        {
            return TurkishDictionary.Words.Contains(word);
        }


    }
}
