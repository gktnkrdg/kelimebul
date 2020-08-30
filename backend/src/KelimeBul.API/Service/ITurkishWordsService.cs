using KelimeBul.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace KelimeBul.API.Service
{
    public interface ITurkishWordsService
    {
        void ReadFromFile(string filename);
        List<string> DeriveWord(string word, int minLength, int maxLength);
        string CreateRandomWord(int length);
        List<WordLength> WordLengths();
    }
}
