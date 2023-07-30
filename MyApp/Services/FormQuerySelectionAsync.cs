using MyApp.Models;
using MyApp.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyApp.Tasks
{
    class FormQuerySelectionAsync : IFormQuerySelectionAsync
    {
        public async void QuerySelectionAsync(DataContext dt)
        {
            var result2 = await dt.Persons
                        .Where(m => m.FIO.Contains("F") && m.Gender == "Male")
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}
