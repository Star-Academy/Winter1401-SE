namespace ConsoleApp1;

public interface IPreProcess
{
    public Dictionary<TKey, TK> Process<TKey, TK>(Dictionary<TKey, TK> dictionary) where TKey : notnull;
}