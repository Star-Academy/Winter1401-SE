using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class DataSet : IReadAble, IWriteAble
{
    public Dictionary<string, Dictionary<int, int>> _dataset
    {
        set;
        get;
    }
    public int _numberOfFiles { get; set; }

    public DataSet()
    {
        _dataset = new Dictionary<string, Dictionary<int, int>>();
    }

    public void SetNumberOfFiles(int size)
    {
        _numberOfFiles = size;
    }

    public int GetNumberOfFiles()
    {
        return _numberOfFiles;
    }
    
    public void Write(string key, int numberOfCurrentFile)
    {
        if (!_dataset.ContainsKey(key))
        {
            var temporary = new Dictionary<int, int>();
            _dataset.Add(key, temporary);
        }
        var countByFiles = _dataset[key];
        if(!countByFiles.ContainsKey(numberOfCurrentFile)){
            countByFiles.Add(numberOfCurrentFile, 0);
        }
        countByFiles[numberOfCurrentFile] += 1;
    }

    public Dictionary<int, int> Read(string key)
    {
        if (!_dataset.ContainsKey(key))
            return null;
        Dictionary<int, int> result = new Dictionary<int, int>();
        _dataset.TryGetValue(key, out result);
        return result;
    }

    public bool Equals(DataSet dataSet)
    {
        if (_dataset.Count != dataSet._dataset.Count)
            return false;
        foreach (var item in dataSet._dataset)
        {
            if(!_dataset.ContainsKey(item.Key))
                return false;
            bool result = AreEqual(item.Value, _dataset[item.Key]);
            if(result)
               continue;
            return false;
        }

        return true;
    }

    private bool AreEqual(Dictionary<int, int> first, Dictionary<int, int> second)
    {
        if (first.Count != second.Count)
            return false;
        foreach (var item in first)
        {
            if (!(second.ContainsKey(item.Key) && second[item.Key] == item.Value))
                return false;
        }

        return true;
    }
}