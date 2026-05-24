using Assignment.Commands;
using Caliburn.Micro;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Assignment.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public ICommand LoadersCommand { get; set; }
        public ICommand ToDoListCommand { get; set; }

        private bool? _isLoadersActive;
        public bool? IsLoadersActive
        {
            get => _isLoadersActive;
            set
            {
                _isLoadersActive = value;
                NotifyOfPropertyChange(nameof(IsLoadersActive));
            }
        }

        private readonly ToDoListViewModel _toDoListViewModel = new ToDoListViewModel();
        private LoadersViewModel _loadersViewModel;
        public ShellViewModel() 
        {
            Initialize();
        }

        private void Initialize()
        {
            LoadersCommand = new RelayCommand(ActivateLoadersView);
            ToDoListCommand = new RelayCommand(ActivateToDoList);
        }

        public void ActivateLoadersView(object obj)
        {
            if (_loadersViewModel == null || _loadersViewModel.TotalProgress == 100 || _loadersViewModel.TotalProgress == 0) _loadersViewModel = new LoadersViewModel();
            ActiveItem = _loadersViewModel;
            IsLoadersActive = true;
        }

        public void ActivateToDoList(object obj)
        {
            ActiveItem = _toDoListViewModel;
            IsLoadersActive = false;
        }
    }
}
