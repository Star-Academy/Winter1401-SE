namespace ConsoleApp1;

public class AverageCalculator : ICalculator
{
    public void Calculate(List<StudentPersonalInfo> personalInfos, List<StudentEduInfo> eduInfos,
        Dictionary<int, double> result)
    {
        for (int i = 0; i < personalInfos.Count; i++)
        {
            int currentStudentId = personalInfos.ElementAt(i).StudentNumber;
            var currentStudentScores = eduInfos.Where(x => x.StudentNumber == currentStudentId).Select(x => x.Score);
            double average = currentStudentScores.Average();
            result.Add(currentStudentId, average);
        }
    }
}