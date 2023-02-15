namespace ConsoleApp1;

public class Manager
{
    private readonly IReadable _readable;
    private readonly ICalculator _calculator;
    private readonly IPreProcess _process;
    private readonly IOutPut _outPut;

    public Manager(IReadable readable, ICalculator calculator, IPreProcess process, IOutPut outPut)
    {
        _readable = readable;
        _calculator = calculator;
        _process = process;
        _outPut = outPut;
    }

    public void Run(string pathToScores, string pathToInfo)
    {
        List<StudentPersonalInfo> studentPersonalInfos = _readable.Read<StudentPersonalInfo>(pathToInfo);
        
        List<StudentEduInfo> studentEduInfos = _readable.Read<StudentEduInfo>(pathToScores);

        List<StudentAverage> averages = _calculator.Calculate(studentPersonalInfos, studentEduInfos);

        averages = _process.Process(averages);

        List<string> bestStudents = ExtractBestStudents(studentPersonalInfos, averages);

        _outPut.OutPut(bestStudents);
    }

    private List<string> ExtractBestStudents(List<StudentPersonalInfo> studentPersonalInfo,
        List<StudentAverage> studentAverages)
    {
        List<String> bestStudents = new List<string>();
        int numberOfBestStudents = InputStNumber(studentPersonalInfo.Count);

        for (int i = 0; i < numberOfBestStudents; i++)
        {
            string currentStudentString = ExtractStudentInfo(studentPersonalInfo, studentAverages, i);
            bestStudents.Add(currentStudentString);
        }
        return bestStudents;
    }

    private int InputStNumber(int maxSize)
    {
        Console.WriteLine("How many Best Student do you want?");
        var stNum = Console.ReadLine();
        int n = Convert.ToInt32(stNum);
        if (maxSize < n)
        {
            Console.WriteLine("There is fewer number of students than you want!!");
            n = InputStNumber(maxSize);
        }
        return n;
    }

    private string ExtractStudentInfo(List<StudentPersonalInfo> studentPersonalInfo,
        List<StudentAverage> studentAverages, int currentId)
    {
        int currentStudentId = studentAverages.ElementAt(currentId).StudentId;
        StudentPersonalInfo currentStudentPersonalInfo =
            studentPersonalInfo.Single(x => x.StudentNumber == currentStudentId);
        string currentStudentString = currentStudentPersonalInfo.FirstName + " " +
                                      currentStudentPersonalInfo.LastName + "    " +
                                      studentAverages.ElementAt(currentId).Average;
        return currentStudentString;
    }
}