using MyApp.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tasks
{
    class GenerateRandomDateBirth : IGenerateRandomDateBirth
    {
        public DateTime RandomDateBirth(Random r)
        {
            DateTime start = new DateTime(1900, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(r.Next(range));
        }
    }
}
