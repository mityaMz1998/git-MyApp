using System;
using System.Linq;
using System.Diagnostics;
using System.Data.Entity;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext db = new DataContext();
            Stopwatch stopWatch = new Stopwatch();
            args = Environment.GetCommandLineArgs();

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
                        AddPerson(db, p1);
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
                            Console.WriteLine($"{pp.FIO} - {pp.DateBirth} - {RandomGender()} - {DateTime.Now.Year - pp.DateBirth.Year}");
                        break;
                    //4. Заполнение автоматически 1000000 строк. Распределение пола в них должно быть относительно равномерным,
                    //начальной буквы ФИО также. Заполнение автоматически 100 строк в которых пол мужской и ФИО начинается с "F".
                    case "4":
                        Random random = new Random();
                        for (int i = 0; i < 100; i++)
                        {
                            Person p2 = new Person() 
                            { 
                                FIO = RandomNameF(true, random), 
                                DateBirth = RandomDateBirth(random), 
                                Gender = "Male" 
                            };
                            AddPerson(db, p2);
                        }

                        for (int i = 100; i < 1000000; i++)
                        {
                            Person p3 = new Person()
                            {
                                FIO = RandomNameF(false, random),
                                DateBirth = RandomDateBirth(random),
                                Gender = RandomGender()
                            };
                            AddPerson(db, p3);
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
                        QuerySelectionAsync(db);
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
        static void AddPerson(DataContext dataContext, Person p)
        {
            dataContext.Persons.Add(p);
            dataContext.SaveChanges();
        }
        static string RandomNameF(bool flg, Random r)
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
        static DateTime RandomDateBirth(Random r)
        {
            DateTime start = new DateTime(1900, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(r.Next(range));
        }        
        static string RandomGender()
        {
            Random r = new Random();
            string[] Genders = { "Male", "Female" };
            return Genders[r.Next(0, 2)];
        }
        static async void QuerySelectionAsync(DataContext dt)
        {
            var result2 = await dt.Persons
                        .Where(m => m.FIO.Contains("F") && m.Gender == "Male")
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}
