using MyApp.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tasks
{
    class GenerateRandomNameF : IGenerateRandomNameF
    {
        public string RandomNameF(bool flg, Random r)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            var stringChars = new char[30];
            Random random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            if (flg == true)
                return "F" + new String(stringChars);
            else
                return new String(stringChars);
        }
    }
}
