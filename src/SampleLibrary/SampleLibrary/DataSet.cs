using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class DataSet : IReadAble, IWriteAble
{
    public Dictionary<string, Dictionary<int, int>> Dataset
    {
        set;
        get;
    }
    public int NumberOfFiles { get; set; }

    public DataSet()
    {
        Dataset = new Dictionary<string, Dictionary<int, int>>();
    }

    public void SetNumberOfFiles(int size)
    {
        NumberOfFiles = size;
    }

    public int GetNumberOfFiles()
    {
        return NumberOfFiles;
    }
    
    public void Write(string key, int numberOfCurrentFile)
    {
        if (!Dataset.ContainsKey(key))
        {
            var temporary = new Dictionary<int, int>();
            Dataset.Add(key, temporary);
        }
        var countByFiles = Dataset[key];
        if(!countByFiles.ContainsKey(numberOfCurrentFile)){
            countByFiles.Add(numberOfCurrentFile, 0);
        }
        countByFiles[numberOfCurrentFile] += 1;
    }

    public Dictionary<int, int> Read(string key)
    {
        if (!Dataset.ContainsKey(key))
            return null;
        var result = new Dictionary<int, int>();
        Dataset.TryGetValue(key, out result);
        return result;
    }

    public bool Equals(DataSet dataSet)
    {
        if (Dataset.Count != dataSet.Dataset.Count)
            return false;
        foreach (var item in dataSet.Dataset)
        {
            if(!Dataset.ContainsKey(item.Key))
                return false;
            bool result = AreEqual(item.Value, Dataset[item.Key]);
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