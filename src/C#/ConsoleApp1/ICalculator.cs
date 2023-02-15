namespace ConsoleApp1;

public interface ICalculator
{
    public List<StudentAverage> Calculate(List<StudentPersonalInfo> personalInfos, List<StudentEduInfo> eduInfos);
}