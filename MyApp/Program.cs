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
            ChangePerson cp = new ChangePerson();
            GenerateRandomDateBirth generateRandomDateBirth = new GenerateRandomDateBirth();
            GenerateRandomGender generateRandomGender = new GenerateRandomGender();
            GenerateRandomNameF generateRandomNameF = new GenerateRandomNameF();
            FormQuerySelectionAsync formQuerySelectionAsync = new FormQuerySelectionAsync();

            foreach (var arg in args)
            {
                switch (arg)
                {
                    // 1. Создание таблицы с полями представляющими ФИО, дату рождения, пол.
                    case "1":
                        db.Database.EnsureDeleted(); 
                        db.Database.EnsureCreated();
                        break;
                    // 2. Создание записи. Использовать следующий формат:
                    case "2":
                        Person p1 = new Person()
                        {
                            FIO = args[2] + " " + args[3] + " " + args[4],
                            DateBirth = DateTime.Parse(args[5]),
                            Gender = args[6]
                        };
                        cp.AddPerson(db, p1);
                        break;
                    //3. Вывод всех строк с уникальным значением ФИО+дата, отсортированным по ФИО,
                        //вывести ФИО, Дату рождения, пол, кол-во полных лет.
                    case "3":
                        var result = db.Persons.AsEnumerable()
                            .GroupBy(d => new { d.FIO, d.DateBirth })
                            .Select(m => new { m.Key.FIO, m.Key.DateBirth})
                            .OrderBy(m => m.FIO)
                            .ToList();
                        foreach (var pp in result)
                            Console.WriteLine($"{pp.FIO} - {pp.DateBirth} - {generateRandomGender.RandomGender()} - {DateTime.Now.Year - pp.DateBirth.Year}");
                        break;
                    //4. Заполнение автоматически 1000000 строк. Распределение пола в них должно быть относительно равномерным,
                    //начальной буквы ФИО также. Заполнение автоматически 100 строк в которых пол мужской и ФИО начинается с "F".
                    case "4":
                        Random random = new Random();
                        for (int i = 0; i < 100; i++)
                        {
                            Person p2 = new Person() 
                            { 
                                FIO = generateRandomNameF.RandomNameF(true, random), 
                                DateBirth = generateRandomDateBirth.RandomDateBirth(random), 
                                Gender = "Male" 
                            };
                            cp.AddPerson(db, p2);
                        }

                        for (int i = 100; i < 1000000; i++)
                        {
                            Person p3 = new Person()
                            {
                                FIO = generateRandomNameF.RandomNameF(false, random),
                                DateBirth = generateRandomDateBirth.RandomDateBirth(random),
                                Gender = generateRandomGender.RandomGender()
                            };
                            cp.AddPerson(db, p3);
                        }
                        Console.WriteLine("Готово!");
                        break;
                    //5.  Результат выборки из таблицы по критерию: пол мужской, ФИО  начинается с "F".
                    // Сделать замер времени выполнения.
                    //6. Произвести определенные манипуляции над базой данных для ускорения запроса из пункта 5.
                    // Убедиться, что время исполнения уменьшилось. Объяснить смысл произведенных действий.
                    // Предоставить результаты замера до и после.
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
                        // В этом пункте для ускорения запроса я применил специально
                        // написанный мной асинхронный метод QuerySelectionAsync()
                        // (в этом методе проходит тот же запрос, что в 5-ом пункте), также для дополнения
                        // я применил метод AsNoTracking(), который позволяет не отслеживать
                        // или изменять данные, а выводить их для просмотра.
                }
            }
        }       
    }
}
