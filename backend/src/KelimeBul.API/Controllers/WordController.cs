using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KelimeBul.API.Models;
using KelimeBul.API.Service;
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
        public static ITurkishWordsService _turkishWordService;
        public WordController(ITurkishWordsService turkishWordsService)
        {
            _turkishWordService = turkishWordsService;
        }


        [Route("derive/{word}")]
        [HttpGet]
        public IActionResult Derive(string word,int minLength=4,int maxLength=0)
        {
            return Ok(_turkishWordService.DeriveWord(word, minLength, maxLength));
        }

        [HttpGet]
        [Route("random")]
        public ApiResponse<string> Random([FromRoute]int length)
        {
            return new ApiResponse<string>(_turkishWordService.CreateRandomWord(length),true,"Success");

        }

        [HttpHead]
        [Route("{word}")]
        public IActionResult Exist([FromRoute] string word)
        {
            var exist = TurkishWordsService.Words.Contains(word.ToLower());
            if (exist)
            {
                return Ok();
            }
            return NotFound();

        }

        [HttpGet("wordlengthcount")]
        public ApiResponse<List<WordLength>> Count()
        {
            var wordLengths = _turkishWordService.WordLengths();
            return new ApiResponse<List<WordLength>>(wordLengths, true, "successfull");
        }

    }
}
