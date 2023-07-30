using MyApp.Models;
using MyApp.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tasks
{
    public class AddPerson : IAddPerson
    {
        public void AddNewPerson(DataContext dataContext, string fio, DateTime dateBirth, string gender)
        {
            Person p = new Person()
            {
                FIO = fio,
                DateBirth = dateBirth,
                Gender = gender
            };
            dataContext.Persons.Add(p);
            dataContext.SaveChanges();
        }
    }
}
