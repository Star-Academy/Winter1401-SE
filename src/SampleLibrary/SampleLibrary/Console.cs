using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Console : IInput, IOutPut
{
    public string? Read()
    {
        return System.Console.ReadLine();
    }  
    
    public void OutPut(string output)
    {
        System.Console.WriteLine(output);
    }
}