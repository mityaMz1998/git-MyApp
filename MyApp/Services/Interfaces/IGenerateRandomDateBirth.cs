using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tasks.Interfaces
{
    interface IGenerateRandomDateBirth
    {
        DateTime RandomDateBirth(Random r);
    }
}
