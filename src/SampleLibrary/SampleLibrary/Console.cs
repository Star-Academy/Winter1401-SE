using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class ConsoleInput : IInput, IOutPut
{
    public string? Read()
    {
        return Console.ReadLine();
    }  
    
    public void OutPut(string output)
    {
        Console.WriteLine(output);
    }
}