namespace ConsoleApp1;

public class AverageCalculator : ICalculator
{
    private readonly List<StudentAverage> _averages = new List<StudentAverage>();
    public List<StudentAverage> Calculate(List<StudentPersonalInfo> personalInfos, List<StudentEduInfo> eduInfos)
    {
        for (int i = 0; i < personalInfos.Count; i++)
        {
            int currentStudentId = personalInfos.ElementAt(i).StudentNumber;
            var currentStudentScores = eduInfos.Where(x => x.StudentNumber == currentStudentId).Select(x => x.Score);
            double average = currentStudentScores.Average();
            StudentAverage currentStudentAverage = new StudentAverage(currentStudentId, average);
            _averages.Add(currentStudentAverage);
        }
        return _averages;
    }
}