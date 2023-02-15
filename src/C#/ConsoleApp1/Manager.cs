using ConsoleApp1.Interfaces;

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
        var studentPersonalInfos = _readable.Read<StudentPersonalInfo>(pathToInfo);
        
        var studentEduInfos = _readable.Read<StudentEduInfo>(pathToScores);

        var averages = _calculator.Calculate(studentPersonalInfos, studentEduInfos);

        averages = _process.Process(averages);

        var bestStudents = ExtractBestStudents(studentPersonalInfos, averages);

        _outPut.OutPut(bestStudents);
    }

    private List<StudentFullInfo> ExtractBestStudents(List<StudentPersonalInfo> studentPersonalInfo,
        List<StudentFullInfo> studentAverages)
    {
        var bestStudents = new List<StudentFullInfo>();
        var numberOfBestStudents = InputStNumber(studentPersonalInfo.Count);
        
        for (var i = 0; i < numberOfBestStudents; i++)
        {
            var currentStudentString = ExtractStudentInfo(studentPersonalInfo, studentAverages, i);
            bestStudents.Add(currentStudentString);
        }
        return bestStudents;
    }

    private int InputStNumber(int maxSize)
    {
        Console.WriteLine("How many Best Student do you want?");
        var stNum = Console.ReadLine();
        var n = Convert.ToInt32(stNum);
        if (maxSize >= n) return n;
        Console.WriteLine("There is fewer number of students than you want!!");
        n = InputStNumber(maxSize);
        return n;
    }

    private StudentFullInfo ExtractStudentInfo(List<StudentPersonalInfo> studentPersonalInfo,
        List<StudentFullInfo> studentAverages, int currentId)
    {
        var currentStudentId = studentAverages[currentId].StudentPersonalInfo!.StudentNumber;
        var currentStudentPersonalInfo =
            studentPersonalInfo.Single(x => x.StudentNumber == currentStudentId);
        var currentStudentFullInfo = new StudentFullInfo(currentStudentPersonalInfo, studentAverages[currentId].Average);
        return currentStudentFullInfo;
    }
}