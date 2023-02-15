using System.Text.Json;

namespace ConsoleApp1;

public class FileDeserializer : IReadable
{
    public List<T> Read<T>(string whereToReadFrom)
    {
        return JsonSerializer.Deserialize<List<T>>(
            File.ReadAllText(whereToReadFrom))!;
    }
}