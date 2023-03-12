using ConsoleApp1.Interfaces;

namespace ConsoleApp1;

public class AverageCalculator : ICalculator
{
    public List<StudentFullInfo> Calculate(List<StudentPersonalInfo> personalInfos, List<StudentEduInfo> eduInfos)
    {
        var averages = new List<StudentFullInfo>();

        foreach (var t in personalInfos)
        {
            var currentStudentId = t.StudentNumber;
            var currentStudentScores = eduInfos.Where(x => x.StudentNumber == currentStudentId).Select(x => x.Score);
            var average = currentStudentScores.Average();

            averages.Add(new StudentFullInfo(personalInfos.Single(x=>x.StudentNumber==currentStudentId), average));
        }
        
        return averages;
    }
}