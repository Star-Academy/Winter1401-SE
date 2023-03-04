using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Search
{
    private readonly IReadAble _dataSet;

    public Search(IReadAble dataSet)
    {
        _dataSet = dataSet;
    }
    public List<int> SearchKey(string key) {
        key = key.ToUpper();
        var keys = key.Split(" ");

        var resultFromPlusMethod = HandlePlus(keys);
        
        var resultFromBareMethod = HandleBare(keys);

        var keysToDelete = HandleMinus(keys);

        var result = CombineResults(resultFromPlusMethod, resultFromBareMethod, _dataSet.GetNumberOfFiles());

        result.RemoveAll(x => keysToDelete.Contains(x));

        return result;
    }

    private HashSet<int> HandlePlus(string[] keys)
    {
        var result = new HashSet<int>();
        foreach (var key in keys) {
            if (!key.StartsWith("+")) {
                continue;
            }
            var modifiedKey = key.Substring(1);
            var temp = _dataSet.Read(modifiedKey);
            if (temp is null)
                continue;
            foreach (var item in temp.Keys)
            {
                result.Add(item);
            }
        }
        return result;
    }

    private HashSet<int> HandleBare(string[] keys)
    {
        var result = new HashSet<int>();
        var isFirstTime = true;
        if (keys.Length == 1 && keys[0] == "")
            return result;
        foreach (var key in keys) {
            if (!StartsWithNothing(key)) {
                continue;
            }
            isFirstTime = HandleEachTerm(result, isFirstTime, key);
        }
        return result;
    }

    private bool HandleEachTerm(HashSet<int> resultFromBareMethod, bool isFirstTime, string key) {
        var currentTermFiles = _dataSet.Read(key);
        if (currentTermFiles == null) {
            HandleEmptyList(); 
        }
        else {
            isFirstTime = HandleNotEmpty(resultFromBareMethod, isFirstTime, currentTermFiles);
        }
        return isFirstTime;
    }

    private static void HandleEmptyList()
    {
        var output = new ConsoleInput();
        output.OutPut("key does not exist!");
        throw new KeyNotFoundException("key does not exist!");
    }

    private bool HandleNotEmpty(HashSet<int> resultFromBareMethod, bool isFirstTime, Dictionary<int, int> temp) {
        if (isFirstTime)
        {
            foreach (var key in temp.Keys)
            {
                resultFromBareMethod.Add(key);
            }

        } else
        {
            resultFromBareMethod.RemoveWhere(x => !temp.ContainsKey(x));
        }
        return false;
    }

    private HashSet<int> HandleMinus(string[] keys) {
       var keysToDelete = new HashSet<int>();
        foreach (var key in keys) {
            keysToDelete = StartWithMinusHandler(keysToDelete, key);
        }
        return keysToDelete;
    }

    private HashSet<int> StartWithMinusHandler(HashSet<int> keysToDelete, string key) {
        if (key.StartsWith("-"))
        {
            var modifiedKey = key.Substring(1);
            var temp = _dataSet.Read(modifiedKey);
            if (temp == null)
                return keysToDelete;
            foreach (var item in temp.Keys)
            {
                keysToDelete.Add(item);
            }
        }
        return keysToDelete;
    }

    private List<int> CombineResults(HashSet<int> resultFromPlusMethod, HashSet<int> resultFromBareMethod, int size) {
        var result = new List<int>();

        if ((resultFromPlusMethod.Count is 0) && (resultFromBareMethod.Count is 0))
            result = AddAllToResults(size);

        else if (resultFromBareMethod.Count is not 0 && resultFromPlusMethod.Count is 0)
            result.AddRange(resultFromBareMethod);

        else if (resultFromBareMethod.Count is 0)
            result.AddRange(resultFromPlusMethod);
        
        else
            foreach (var item in resultFromBareMethod) {
                if (resultFromPlusMethod.Contains(item))
                    result.Add(item);
            }
        return result;
    }

    private List<int> AddAllToResults(int size)
    {
        var results = new List<int>();
        for (var i = 0; i < size; i++) {
            results.Add(i);
        }
        return results;
    }

    private bool StartsWithNothing(string key) {
        return !key.StartsWith("+") && !key.StartsWith("-");
    }
}