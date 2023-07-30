using System;
using System.Linq;
using System.Diagnostics;
using System.Data.Entity;
using MyApp.Models;
using MyApp.Tasks;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext db = new DataContext();
            Stopwatch stopWatch = new Stopwatch();
            args = Environment.GetCommandLineArgs();
            AddPerson cp = new AddPerson();
            GenerateRandomDateBirth generateRandomDateBirth = new GenerateRandomDateBirth();
            GenerateRandomGender generateRandomGender = new GenerateRandomGender();
            GenerateRandomNameF generateRandomNameF = new GenerateRandomNameF();
            FormQuerySelectionAsync formQuerySelectionAsync = new FormQuerySelectionAsync();

            foreach (var arg in args)
            {
                switch (arg)
                {
                    // 1. Creating a table with fields representing full name, date of birth, gender.
                    case "1":
                        db.Database.EnsureDeleted(); 
                        db.Database.EnsureCreated();
                        break;
                    // 2. Creating a record. Use the following format:
                    case "2":
                        cp.AddNewPerson(db, args[2] + " " + args[3] + " " + args[4], DateTime.Parse(args[5]), args[6]);
                        break;
                    //3. Output of all lines with a unique value of full name + date, sorted by full name,
                    //output full name, date of birth, gender, number of full years.
                    case "3":
                        var result = db.Persons.AsEnumerable()
                            .GroupBy(d => new { d.FIO, d.DateBirth })
                            .Select(m => new { m.Key.FIO, m.Key.DateBirth})
                            .OrderBy(m => m.FIO)
                            .ToList();
                        foreach (var pp in result)
                            Console.WriteLine($"{pp.FIO} - {pp.DateBirth} - {generateRandomGender.RandomGender()} - {DateTime.Now.Year - pp.DateBirth.Year}");
                        break;
                    //4. Filling in 1,000,000 rows automatically. The distribution of gender in them should be relatively uniform,
                    //the initial letter of the full name as well. Filling in automatically 100 lines in which the gender is male and the full name begins with "F".
                    case "4":
                        Random random = new Random();
                        for (int i = 0; i < 100; i++)
                            cp.AddNewPerson(db, generateRandomNameF.RandomNameF(true, random), generateRandomDateBirth.RandomDateBirth(random), "Male");

                        for (int i = 100; i < 1000000; i++)
                            cp.AddNewPerson(db, generateRandomNameF.RandomNameF(false, random), generateRandomDateBirth.RandomDateBirth(random), generateRandomGender.RandomGender());
                        Console.WriteLine("Готово!");
                        break;
                    //5.  The result of the selection from the table according to the criterion: male gender, full name begins with "F".
                    //Make a measurement of the execution time.
                    //6. Perform certain manipulations on the database to speed up the query from point 5.
                    // Make sure that the execution time has decreased. Explain the meaning of the actions performed.
                    // Provide the measurement results before and after.
                    case "5":
                        //5
                        stopWatch.Start();
                        var result1 = db.Persons
                                    .Where(m => m.FIO.StartsWith("F") && m.Gender == "Male")
                                    .ToList();
                        stopWatch.Stop();
                        Console.WriteLine("До ускоренной выборки запроса в {0} мс", stopWatch.ElapsedMilliseconds);
                        stopWatch.Reset();

                        //6
                        stopWatch.Start();
                        formQuerySelectionAsync.QuerySelectionAsync(db);
                        stopWatch.Stop();
                        Console.WriteLine("После ускоренной выборки запроса в {0} мс", stopWatch.ElapsedMilliseconds);
                        break;
                        // At this point, to speed up the request, I applied specifically
                        // the asynchronous method QuerySelectionAsync() written by me
                        // (in this method the same query passes as in the 5th paragraph), also for the complement
                        // I applied the AsNoTracking() method, which allows not to track
                        // or change the data, and output them for viewing.
                }
            }
        }       
    }
}
