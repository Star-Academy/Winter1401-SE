namespace ConsoleApp1;

public struct StudentAverage
{
    public int StudentId { get; set; } 
    public double Average { get; set; }

    public StudentAverage(int studentId, double average)
    {
        StudentId = studentId;
        Average = average;
    }
}