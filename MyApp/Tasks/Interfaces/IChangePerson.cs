﻿using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tasks.Interfaces
{
    interface IChangePerson
    {
        void AddPerson(DataContext dataContext, string fio, DateTime dateBirth, string gender);
    }
}
