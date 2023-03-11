using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Search
{
    private readonly IReadAble _dataSet;

    public Search(IReadAble dataSet)
    {
        _dataSet = dataSet;
    }

    public List<int> SearchKey(string key)
    {

        var keys = key.ToUpper().Split(" ");

        var resultFromPlusMethod = HandlePlus(keys);

        var resultFromBareMethod = HandleBare(keys);

        var keysToDelete = HandleMinus(keys);

        var result = CombineResults(resultFromPlusMethod, resultFromBareMethod);

        result.RemoveAll(x => keysToDelete.Contains(x));

        return result;
    }

    private HashSet<int> HandlePlus(string[] keys)
    {
        if (!IsPlusEverUsed(keys))
            return _dataSet.GetFilesNames().ToHashSet();
        var result = new HashSet<int>();
        keys.ToList().ForEach(x=>result.UnionWith(StartsWithPlusHandler(x)));
        return result;
    }

    private IEnumerable<int> StartsWithPlusHandler(string key)
    {
        if (!key.StartsWith("+"))
        {
            return Enumerable.Empty<int>();
        }
        
        var modifiedKey = key.Substring(1);
        return _dataSet.Read(modifiedKey);
    }

    private HashSet<int> HandleBare(string[] keys)
    {
        if (!IsBareEverUsed(keys))
            return _dataSet.GetFilesNames().ToHashSet();
        var result = new HashSet<int>();
        var isFirstTime = true;

        foreach (var key in keys)
        {
            if(!DoseNotStartWithPlusOrMinus(key))
                continue;
            var currentTermFiles = _dataSet.Read(key);
            if (isFirstTime)
            {
                result = HandleFirstTime(key);
                isFirstTime = false;
            }
            else
                result.RemoveWhere(x => !currentTermFiles!.Contains(x));

        }
        return result;
    }

    private HashSet<int> HandleFirstTime(string key)
    {
        var currentTermFiles = _dataSet.Read(key);
        return currentTermFiles.ToHashSet();
    }

    private HashSet<int> HandleMinus(string[] keys) {
       var keysToDelete = new HashSet<int>();
       keys.ToList().ForEach(x=>keysToDelete.UnionWith(StartWithMinusHandler(x)));
       return keysToDelete;
    }
    private IEnumerable<int> StartWithMinusHandler(string key)
    {
        if (!key.StartsWith("-"))
        {
            return Enumerable.Empty<int>();
            
        }
        var modifiedKey = key.Substring(1); 
        var temp = _dataSet.Read(modifiedKey); 
        return temp.ToHashSet();
    }

    private List<int> CombineResults(HashSet<int> resultFromPlusMethod, HashSet<int> resultFromBareMethod) {

        return resultFromBareMethod
            .Where(resultFromPlusMethod.Contains).ToList();
    }
    
    private bool DoseNotStartWithPlusOrMinus(string key) {
        return !key.StartsWith("+") && !key.StartsWith("-");
    }

    private bool IsBareEverUsed(string[] keys)
    {
        return keys.Any(DoseNotStartWithPlusOrMinus);
    }

    private bool IsPlusEverUsed(string[] keys)
    {
        return keys.Any(x=>x.StartsWith("+"));
    }
}