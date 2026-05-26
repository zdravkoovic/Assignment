using Assignment.Models;
using Assignment.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Tests.ToDo
{
    // Ovi testovi proveravaju logiku insert metode kod LinearSortStrategy klase.

    [TestClass]
    public class LinearSortStrategyTests
    {
        private ISortStrategy _sort;

        [TestInitialize]
        public void Setup()
        {
            _sort = new LinearSortStrategy();
        }
        [TestMethod]
        public void Insert_WhenNewItemHasHighestPriority_InsertsAtBeginning()
        {
            //Arange
            var newItem = new ToDoItem { Priority = 1 };
            var items = new ObservableCollection<ToDoItem>
            {
                new ToDoItem { Priority = 2 },
                new ToDoItem { Priority = 3 }
            };

            //Act
            _sort.Insert(items, newItem);

            //Assert
            Assert.AreEqual(items[0], newItem);
        }

        [TestMethod]
        public void Insert_WhenNewItemHasLowestPriority_InsertsAtEnd()
        {
            //Arrange
            var newItem = new ToDoItem { Priority = 3 };
            var items = new ObservableCollection<ToDoItem>
            {
                new ToDoItem { Priority = 1 },
                new ToDoItem { Priority = 2 }
            };

            //Act
            _sort.Insert(items, newItem);

            //Assert
            Assert.AreEqual(items[2], newItem);
        }

        [TestMethod]
        public void Insert_WhenCollectionIsEmpty_InsertsItem()
        {
            //Arrange
            var newItem = new ToDoItem { Priority = 3 };
            var items = new ObservableCollection<ToDoItem>();

            //Act
            _sort.Insert(items, newItem);

            //Assert
            Assert.AreEqual(items[0], newItem);
        }

        [TestMethod]
        public void Insert_WhenItemHasSamePriorityAsExisting_InsertsAfterLastOfSamePriority()
        {
            //Arrange
            var newItem = new ToDoItem { Priority = 1 };
            var items = new ObservableCollection<ToDoItem>
            {
                new ToDoItem { Priority = 1 },
                new ToDoItem { Priority = 1 },
                new ToDoItem { Priority = 2 }
            };

            //Act
            _sort.Insert(items, newItem);

            //Assert
            Assert.AreEqual(items[2], newItem);
        }
    }
}
