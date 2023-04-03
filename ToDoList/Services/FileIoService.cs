using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using ToDoList.Models;

namespace ToDoList.Services;

public class FileIoService
{
    private readonly string _path;

    public FileIoService(string path)
    {
        _path = path;
    }

    public BindingList<ToDoModel> LoadData()
    {
        var fileExists = File.Exists(_path);
        if (!fileExists)
        {
            File.CreateText(_path).Dispose();
            return new BindingList<ToDoModel>();
        }

        using var reader = File.OpenText(_path);
        var fileText = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
    }

    public void SaveData(BindingList<ToDoModel> toDoDataList)
    {
        using var writer = File.CreateText(_path);
        var output = JsonConvert.SerializeObject(toDoDataList);
        writer.Write(output);
    }
}