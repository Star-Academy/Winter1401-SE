namespace ConsoleApp1.Interfaces;

public interface IPreProcess
{
    public List<StudentFullInfo> Process(IEnumerable<StudentFullInfo> studentAverages);
}