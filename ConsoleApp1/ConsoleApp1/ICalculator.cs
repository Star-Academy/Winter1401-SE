namespace ConsoleApp1;

public interface ICalculator
{
    public void Calculate(List<StudentPersonalInfo> personalInfos, List<StudentEduInfo> eduInfos, Dictionary<int, double> result);
}