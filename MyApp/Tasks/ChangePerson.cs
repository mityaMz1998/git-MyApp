using MyApp.Models;
using MyApp.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Tasks
{
    public class ChangePerson : IChangePerson
    {
        public void AddPerson(DataContext dataContext, Person p)
        {
            dataContext.Persons.Add(p);
            dataContext.SaveChanges();
        }
    }
}
