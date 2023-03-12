using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Search
{
    private readonly IReadAble _dataSet;

    public Search(IReadAble dataSet)
    {
        _dataSet = dataSet;
    }

    public IEnumerable<int> SearchKey(string key)
    {

        var keys = key.ToUpper().Split(" ");

        var resultFromPlusMethod = HandlePlus(keys);

        var resultFromBareMethod = HandleBare(keys);

        var keysToDelete = HandleMinus(keys);

        var result = CombineResults(resultFromPlusMethod, resultFromBareMethod).ToList();

        result.RemoveAll(x => keysToDelete.Contains(x));

        return result;
    }

    private IEnumerable<int> HandlePlus(string[] keys)
    {
        if (!IsPlusEverUsed(keys))
            return _dataSet.GetFilesNames().ToHashSet();
        
        return keys.SelectMany(StartsWithPlusHandler);
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

    private IEnumerable<int> HandleBare(string[] keys)
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
                result = HandleFirstTime(key).ToHashSet();
                isFirstTime = false;
            }
            else
                result.RemoveWhere(x => !currentTermFiles.Contains(x));

        }
        return result;
    }

    private IEnumerable<int> HandleFirstTime(string key)
    {
        return _dataSet.Read(key);
    }

    private IEnumerable<int> HandleMinus(string[] keys) {
        return keys.SelectMany(StartWithMinusHandler);
    }
    private IEnumerable<int> StartWithMinusHandler(string key)
    {
        if (!key.StartsWith("-"))
        {
            return Enumerable.Empty<int>();
            
        }
        var modifiedKey = key.Substring(1); 
        var temp = _dataSet.Read(modifiedKey); 
        return temp;
    }

    private IEnumerable<int> CombineResults(IEnumerable<int> resultFromPlusMethod, IEnumerable<int> resultFromBareMethod) {

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