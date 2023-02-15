using ConsoleApp1.Interfaces;

namespace ConsoleApp1;

public class ConsoleOutput : IOutPut
{
    public void OutPut(List<StudentFullInfo> studentsToPrint)
    {
        foreach (var t in studentsToPrint)
        {
            Console.WriteLine(t.StudentPersonalInfo!.FirstName + " " + t.StudentPersonalInfo.LastName + "    " + t.Average);
        }
    }
}