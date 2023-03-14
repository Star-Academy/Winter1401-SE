using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Console : IInputOutput
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