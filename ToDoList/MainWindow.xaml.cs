using System;
using System.ComponentModel;
using System.Windows;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        private readonly string _path = $"{Environment.CurrentDirectory}\\ToDoDataList.json";
        private BindingList<ToDoModel> _toDoDateList;
        private FileIoService _fileIoService;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIoService = new FileIoService(_path);
            try
            {
                _toDoDateList = _fileIoService.LoadData();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Close();
            }

            ToDoListGrid.ItemsSource = _toDoDateList;
            _toDoDateList.ListChanged += _toDoDataList_ListChanged;
        }

        private void _toDoDataList_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded ||
                e.ListChangedType == ListChangedType.ItemDeleted ||
                e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIoService.SaveData((BindingList<ToDoModel>) sender!);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    Close();
                }
            }
        }
        
    }
}