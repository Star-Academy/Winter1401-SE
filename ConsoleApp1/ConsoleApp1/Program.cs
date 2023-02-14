namespace ConsoleApp1;

public static class Program
    {
        public static void Main(string[] args)
        {
            FileHandler fileHandler = new FileHandler();
            AverageCalculator averageCalculator = new AverageCalculator();
            Sort sort = new Sort();
            ConsoleOutput consoleOutput = new ConsoleOutput();
            
            Console.WriteLine("Enter path to Scores file:");
            // Default path to scores file C:\\Users\\mahdi.mohammadi\\Desktop\\Scores.txt   
            string? pathToScores = Console.ReadLine();
            
            List<StudentEduInfo> studentEduInfos = fileHandler.Read<StudentEduInfo>(pathToScores!);
            
            Console.WriteLine("Enter path to students personal information file:");
            // Default path to students file C:\\Users\\mahdi.mohammadi\\Desktop\\Students.txt
            string? pathToInfo = Console.ReadLine();
            
            List<StudentPersonalInfo> studentPersonalInfos = fileHandler.Read<StudentPersonalInfo>(pathToInfo!);

            Dictionary<int, double> averages = new Dictionary<int, double>();

            averageCalculator.Calculate(studentPersonalInfos, studentEduInfos, averages);
            
            averages = sort.Process(averages);
            
            consoleOutput.OutPut(averages, studentPersonalInfos);
        }
    }