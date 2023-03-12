using ConsoleApp1.Interfaces;

namespace ConsoleApp1;

public class Sort : IPreProcess
{
    public List<StudentFullInfo> Process(IEnumerable<StudentFullInfo> studentAverages)
    {
       return studentAverages.OrderByDescending(x => x.Average).ToList();
    }
}