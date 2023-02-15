namespace ConsoleApp1;

public class StudentFullInfo
{
    public StudentPersonalInfo? StudentPersonalInfo { get; init; }
    public double Average { get; init; }

    public StudentFullInfo(StudentPersonalInfo? studentPersonalInfo, double average)
    {
        StudentPersonalInfo = studentPersonalInfo;
        Average = average;
    }
}