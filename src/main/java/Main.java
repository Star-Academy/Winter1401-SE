import java.util.*;

public class Main{

    public static void main(String[] args)
    {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Enter full directory path:");   //Default directory C:\\Users\\mahdi.mohammadi\\Test\\
        String pathToFile = scanner.nextLine();

        DataSet dataSet = new DataSet();

        Index index = new Index(new FileHandler(pathToFile));
        index.makeDataSet(dataSet);

        System.out.println("Enter input word:");
        String key = scanner.nextLine();

        Search search = new Search();
        printResult(search.searchKey(key, dataSet, index.getNumberOfFiles()));
    }

    private static void printResult(ArrayList<Integer> result) {
        for (Integer integer : result) {
            System.out.println(integer);
        }
    }
}
