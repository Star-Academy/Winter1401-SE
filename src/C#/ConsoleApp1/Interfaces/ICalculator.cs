namespace ConsoleApp1.Interfaces;

public interface ICalculator
{
    public List<StudentFullInfo> Calculate(List<StudentPersonalInfo> personalInfos, List<StudentEduInfo> eduInfos);
}