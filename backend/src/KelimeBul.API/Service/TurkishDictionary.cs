﻿using KelimeBul.API.Service;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KelimeBul.API
{
    public class TurkishDictionary : ITurkishDictionary
    {
        private IWebHostEnvironment _env;
        public TurkishDictionary(IWebHostEnvironment env)
        {
            _env = env;
        }

        //mythes-tr turkish database used.
        //https://github.com/maidis/mythes-tr
        public static HashSet<string> Words = new HashSet<string>();
        public bool ReadTurkishWordsFile()
        {
            try
            {
                var file = Path.Combine(_env.ContentRootPath, "tr.txt");
                foreach (var line in File.ReadLines(file))
                {
                    Words.Add(line);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
