using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyApp;
using MyApp.Models;
using MyApp.Tasks;
using System;

namespace MyApp.Tests
{
    [TestClass]
    public class TaskTests
    {
        //DataContext db = new DataContext();

        [TestMethod]
        public void ChangePersonTests()
        {
            DateTime dt = DateTime.Now;
            DataContext db = new DataContext();
            ChangePerson changePerson = new ChangePerson();

            Person expectedPerson = new Person() {FIO = "Asd bnd dfjk", DateBirth = dt, Gender = "Male"};
            //changePerson.AddPerson(db, expectedPerson);

            Assert.AreEqual(expectedPerson, changePerson.AddPerson(db, expectedPerson));
        }

        [TestMethod]
        public void FormQuerySelectionAsyncTest()
        {
            Assert.AreEqual();
        }

        [TestMethod]
        public void GenerateRandomDateBirthTests()
        {
            Assert.AreEqual();
        }

        [TestMethod]
        public void GenerateRandomGenderTests()
        {
            Assert.AreEqual();
        }

        [TestMethod]
        public void GenerateRandomNameFTests()
        {
            Assert.AreEqual();
        }
    }
}
