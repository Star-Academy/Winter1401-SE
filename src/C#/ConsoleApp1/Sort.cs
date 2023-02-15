namespace ConsoleApp1;

public class Sort : IPreProcess
{
    public List<StudentAverage> Process(List<StudentAverage> studentAverages)
    {
        studentAverages = studentAverages.OrderByDescending(x => x.Average).ToList();
        return studentAverages;
    }
}