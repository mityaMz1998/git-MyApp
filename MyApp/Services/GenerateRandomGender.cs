using MyApp.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tasks
{
    class GenerateRandomGender : IGenerateRandomGender
    {
        public string RandomGender()
        {
            Random r = new Random();
            string[] Genders = { "Male", "Female" };
            return Genders[r.Next(0, 2)];
        }
    }
}
