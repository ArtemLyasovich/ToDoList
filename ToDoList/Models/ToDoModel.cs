using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDoList.Annotations;

namespace ToDoList.Models;

public class ToDoModel:INotifyPropertyChanged
{
    private bool _isDone;
    private string _task;

    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime CompletionDate { get; set; } 

    public bool IsDone
    {
        get => _isDone;
        set
        {
            if (_isDone == value)
                return;
            
            _isDone = value;
            OnPropertyChanged(nameof(IsDone));
        }
    }

    public string Task
    {
        get => _task;
        set
        {
            if (_task == value)
                return;
            
            _task = value;
            OnPropertyChanged(nameof(Task));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}