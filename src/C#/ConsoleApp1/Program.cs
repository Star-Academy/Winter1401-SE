namespace ConsoleApp1;

public static class Program
    {
        public static void Main(string[] args)
        {
            var pathToScores = ReadPathToScores();
            
            var pathToInfo = ReadPathToInfo();

            Manager manager = new Manager(new FileDeserializer(), new AverageCalculator(), new Sort(), new ConsoleOutput());
            manager.Run(pathToScores!, pathToInfo!);
            
        }

        private static string? ReadPathToInfo()
        {
            Console.WriteLine("Enter path to students personal information file:");
            // Default path to students file C:\\Users\\mahdi.mohammadi\\Desktop\\Students.txt
            string? pathToInfo = Console.ReadLine();
            return pathToInfo;
        }

        private static string? ReadPathToScores()
        {
            Console.WriteLine("Enter path to Scores file:");
            // Default path to scores file C:\\Users\\mahdi.mohammadi\\Desktop\\Scores.txt   
            string? pathToScores = Console.ReadLine();
            return pathToScores;
        }
    }