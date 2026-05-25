using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Strategies
{
    public interface ISortStrategy
    {
        void Insert(ObservableCollection<ToDoItem> items, ToDoItem newItem);
    }
}
