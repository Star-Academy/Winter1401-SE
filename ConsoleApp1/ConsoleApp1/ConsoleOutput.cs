namespace ConsoleApp1;

public class ConsoleOutput : IOutPut
{
    public void OutPut(Dictionary<int, double> averages, List<StudentPersonalInfo> studentPersonalInfos)
    {
        for (int i = 0; i < 3; i++)
        {
            int currentStudentId = averages.ElementAt(i).Key;
            StudentPersonalInfo currentStudentPersonalInfo =
                studentPersonalInfos.Single(x => x.StudentNumber == currentStudentId);
            Console.WriteLine(currentStudentPersonalInfo.FirstName + " " + currentStudentPersonalInfo.LastName + "    " +
                              averages.ElementAt(i).Value);
        }
    }
}