namespace ConsoleApp1;

public class Sort : IPreProcess
{
    public Dictionary<TKey, TK> Process<TKey, TK>(Dictionary<TKey, TK> dictionary) where TKey : notnull
    {
        dictionary = dictionary.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        return dictionary;
    }
}