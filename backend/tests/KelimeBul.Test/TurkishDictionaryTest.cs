using KelimeBul.API;
using KelimeBul.API.Service;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KelimeBul.Test
{
    public class TurkishDictionaryTest
    {
        private Mock<ITurkishWordsService> _turkishDictionary;
        public TurkishDictionaryTest()
        {
            _turkishDictionary =  new Mock<ITurkishWordsService>();
        }
        //[Fact]
        //public void RandomWord_ShouldCorrectWordLength()
        //{
              //_turkishDictionary.Object.ReadFromFile("tr.txt");
        //}

    }
}
