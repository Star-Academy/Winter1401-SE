using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Search
{
    public List<int> SearchKey(string key, IReadAble dataSet) {
        key = key.ToUpper();
        var keys = key.Split(" ");

        var resultFromPlusMethod = new List<int>();
        var isPlusEverUsed = HandlePlus(dataSet, keys, resultFromPlusMethod);

        var resultFromBareMethod = new List<int>();
        var isEverUsed = HandleBare(dataSet, keys, resultFromBareMethod);

        var keysToDelete = HandleMinus(dataSet, keys);

       var result = CombineResults(isPlusEverUsed, isEverUsed, resultFromPlusMethod, resultFromBareMethod, dataSet.GetNumberOfFiles());

        deleteKeys(keysToDelete, result);
        return result;
    }

    private bool HandlePlus(IReadAble dataSet, String[] keys, List<int> resultFromPlusMethod) {
        var flag1 = true;
        foreach (var key in keys) {
            if (!key.StartsWith("+")) {
                continue;
            }
            flag1 = false;
            var modifiedKey = key.Substring(1);
            var temp = dataSet.Read(modifiedKey);
            if (temp == null)
                continue;
            traverseDictionary(resultFromPlusMethod, temp);
        }
        return !flag1;
    }

    private void traverseDictionary(List<int> result, Dictionary<int, int> dictionaryToTraverse) {
        foreach (var item in dictionaryToTraverse)
        {
            if (!result.Contains(item.Key))
                result.Add(item.Key);
        }
    }

    private bool HandleBare(IReadAble dataSet, String[] keys, List<int> resultFromBareMethod) {
        var isEverUsed = false;
        var isFirstTime = true;
        if (keys.Length == 1 && keys[0] == "")
            return isEverUsed;
        foreach (String key in keys) {
            if (!startsWithNothing(key)) {
                continue;
            }
            isEverUsed = true;
            isFirstTime = HandleEachTerm(dataSet, resultFromBareMethod, isFirstTime, key);
        }
        return isEverUsed;
    }

    private bool HandleEachTerm(IReadAble dataSet, List<int> resultFromBareMethod, bool isFirstTime, String key) {
        var currentTermFiles = dataSet.Read(key);
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

    private bool HandleNotEmpty(List<int> resultFromBareMethod, bool isFirstTime, Dictionary<int, int> temp) {
        if (isFirstTime) {
            traverseDictionary(resultFromBareMethod, temp);
        } else {
            HandleNextTimes(resultFromBareMethod, temp);
        }
        return false;
    }

    private static void HandleNextTimes(List<int> resultFromBareMethod, Dictionary<int, int> temp) {
        for (var j = 0; j < resultFromBareMethod.Count; j++) {
            if (!temp.ContainsKey(resultFromBareMethod[j]))
                resultFromBareMethod.Remove(resultFromBareMethod[j]);
        }
    }

    private List<int> HandleMinus(IReadAble dataSet, String[] keys) {
       var keysToDelete = new List<int>();
        foreach (String key in keys) {
            StartWithMinusHandler(dataSet, keysToDelete, key);
        }
        return keysToDelete;
    }

    private void StartWithMinusHandler(IReadAble dataSet, List<int> keysToDelete, String key) {
        if (key.StartsWith("-"))
        {
            var modifiedKey = key.Substring(1);
            var temp = dataSet.Read(modifiedKey);
            if (temp == null)
                return;
            traverseDictionary(keysToDelete, temp);
        }
    }

    private List<int> CombineResults(bool isPlusEverUsed, bool isEverUsed, List<int> resultFromPlusMethod, List<int> resultFromBareMethod, int size) {
        var result = new List<int>();

        if ((!isEverUsed) && (!isPlusEverUsed))
            addAllToResults(result, size);

        else if (isEverUsed && !isPlusEverUsed)
            result.AddRange(resultFromBareMethod);

        else if (!isEverUsed)
            result.AddRange(resultFromPlusMethod);

        else
            foreach (int item in resultFromBareMethod) {
                if (resultFromPlusMethod.Contains(item))
                    result.Add(item);
            }

        return result;
    }

    private void deleteKeys(List<int> keysToDelete, List<int> result) {
        foreach (var item in keysToDelete) {
            result.Remove(item);
        }
    }

    private void addAllToResults(List<int> results, int size) {
        for (var i = 0; i < size; i++) {
            results.Add(i);
        }
    }

    private bool startsWithNothing(String key) {
        return (!(key.StartsWith("+"))) && (!(key.StartsWith("-")));
    }
}