import java.util.*;

//Single Responsibility Principle

//Open for extension/Closed for modification principle

//Liskov Substitution principle

//Interface segregation principle

//Dependency inversion principle

public class Main{

    public static void main(String[] args)
    {
        Scanner scanner = new Scanner(System.in);
        String pathToFile;
        System.out.println("Enter full directory path:");   //Default directory C:\\Users\\mahdi.mohammadi\\Test\\
        pathToFile = scanner.nextLine();

        DataSet dataSet = new DataSet();

        Index index = new Index(pathToFile);
        int numberOfFiles = index.getNumberOfFiles();
        index.makeDataSet(dataSet);

        System.out.println("Enter input word:");
        String key = scanner.nextLine();

        Search search = new Search();
        ArrayList<Integer> result = search.searchKey(key, dataSet, numberOfFiles);

        printResult(result);
    }
    private static void printResult(ArrayList<Integer> result) {
        for (Integer integer : result) {
            System.out.println(integer);
        }
    }
}
