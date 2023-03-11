using SampleLibrary.Interfaces;
using FluentAssertions;

namespace SampleLibrary;

public class DataSet : IReadAble, IWriteAble
{
    private readonly HashSet<int> _namesOfFiles;
    private Dictionary<string, Dictionary<int, int>> _dataset;
    public DataSet()
    {
        _dataset = new Dictionary<string, Dictionary<int, int>>();
        _namesOfFiles = new HashSet<int>();
    }
    
    public List<int> GetFilesNames()
    {
        return _namesOfFiles.ToList();
    }
    
    public void Write(string key, int numberOfCurrentFile)
    {
        _namesOfFiles.Add(numberOfCurrentFile);
        
        if (!_dataset.TryGetValue(key, out var countByFiles))
        {
            countByFiles = new Dictionary<int, int>();
            _dataset.Add(key, countByFiles);
        }
        
        if(!countByFiles.ContainsKey(numberOfCurrentFile))
        {
            countByFiles.Add(numberOfCurrentFile, 0);
        }
        countByFiles[numberOfCurrentFile] += 1;
    }
    
    public IEnumerable<int> Read(string key)
    {
        if (!_dataset.TryGetValue(key, out var result))
            return Enumerable.Empty<int>();
        return result.Keys;
    }

    // public bool Equals(DataSet dataSet)
    // {
    //     if(!_dataset.Keys.ToList().SequenceEqual(dataSet._dataset.Keys.ToList()))
    //         return false;
    // }
}