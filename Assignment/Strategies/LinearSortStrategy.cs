using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Strategies
{
    public class LinearSortStrategy : ISortStrategy
    {
        public void Insert(ObservableCollection<ToDoItem> items, ToDoItem newItem)
        {
            int index = items.Count(i => i.Priority <= newItem.Priority);
            items.Insert(index, newItem);
        }
    }
}
