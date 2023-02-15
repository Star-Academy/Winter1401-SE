namespace ConsoleApp1;

public class ConsoleOutput : IOutPut
{
    public void OutPut(List<string> stringsToPrint)
    {
        for (int i = 0; i < stringsToPrint.Count; i++)
        {
            Console.WriteLine(stringsToPrint.ElementAt(i));
        }
    }
}