namespace ConsoleApp1.Interfaces;

public interface IReadable
{ 
    List<T> Read<T>(string whereToReadFrom);
}