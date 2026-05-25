using Assignment.Commands;
using Assignment.Models;
using Assignment.Strategies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Assignment.ViewModels
{
    public class ToDoSubmitViewModel : ViewModelBase
    {
        private string _itemName { get; set; }
        private int _selectedPriority { get; set; }
        private ISortStrategy _sortStrategy { get; set; }
        public ObservableCollection<ToDoItem> Items { get; set; } = new ObservableCollection<ToDoItem>();
        
        public string ItemName
        {
            get => _itemName;
            set
            {
                _itemName = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }
        public int SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                _selectedPriority = value;
                OnPropertyChanged(nameof(SelectedPriority));
            }
        }

        public List<int> Priorities { get; private set; }

        public ICommand SubmitCommand { get; private set; }

        public ToDoSubmitViewModel(ISortStrategy sortStrategy)
        {
            Initialize();
            _sortStrategy = sortStrategy;
        }

        private void Initialize()
        {
            SubmitCommand = new RelayCommand(SubmitItem, CanSubmit);
            Priorities = new List<int> { 1, 2, 3};
        }
        private void ResetFormFiledsAfterSubmit()
        {
            ItemName = string.Empty;
            SelectedPriority = 0;
        }

        private void SubmitItem(object _)
        {
            var item = new ToDoItem
            {
                ItemName = ItemName,
                Priority = SelectedPriority
            };

            _sortStrategy.Insert(Items, item);

            ResetFormFiledsAfterSubmit();
        }

        private bool CanSubmit(object _)
        {
            return !string.IsNullOrWhiteSpace(ItemName) && SelectedPriority != 0;
        }
    }
}
