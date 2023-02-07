import java.util.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.*;
import java.io.*;

public class Main{

    public static void main(String[] args)
    {
        Scanner scanner = new Scanner(System.in);
        String pathToFile;
        System.out.println("Enter full directory path:");   //Default directory C:\\Users\\mahdi.mohammadi\\Test\\
        pathToFile = scanner.nextLine();
        File folder = new File(pathToFile);
        File[] listOfFiles = folder.listFiles();
        HashMap<String, HashMap<Integer, Integer>> dataSet = new HashMap<>();
        if(listOfFiles == null){
            System.out.println("Given directory is empty!!");
            System.exit(0);
        }
        makeDataSet(pathToFile, listOfFiles, dataSet);
        System.out.println("Enter input word:");
        String key = scanner.nextLine();
        key = key.toUpperCase();
        String[] keys = key.split(" ");
        ArrayList<Integer> R1 = new ArrayList<>();
        boolean isPlusEverUsed = handlePlus(dataSet, keys, R1);
        ArrayList<Integer> R2 = new ArrayList<>();
        boolean isEverUsed = handleBare(dataSet, keys, R2);
        ArrayList<Integer> keysToDelete = handleMinus(dataSet, keys);
        ArrayList<Integer> result = combineResults(isPlusEverUsed, isEverUsed, R1, R2, listOfFiles.length);
        deleteKeys(keysToDelete, result);
        printResult(result);
    }

    public static List<String> readFileInList(String fileName)
    {

        List<String> lines = Collections.emptyList();
        try
        {
            lines =
                    Files.readAllLines(Paths.get(fileName), StandardCharsets.UTF_8);
        }

        catch (IOException e)
        {
            e.printStackTrace();
        }
        for (int i = 0; i < lines.size(); i++) {
            lines.set(i,lines.get(i).toUpperCase());
        }
        return lines;
    }

    private static Boolean handlePlus(HashMap<String, HashMap<Integer, Integer>> dataSet, String[] keys, ArrayList<Integer> R1) {
        boolean flag1 = true;
        for (String key : keys) {
            if (key.startsWith("+")) {
                flag1 = false;
                HashMap<Integer, Integer> temp = dataSet.get(key.replaceFirst("\\+", ""));
                if (temp == null)
                    continue;
                traverseHashMap(R1, temp);
            }
        }
        return !flag1;
    }

    private static void traverseHashMap(ArrayList<Integer> R1, HashMap<Integer, Integer> temp) {
        for (Map.Entry<Integer, Integer> mapElement : temp.entrySet()) {
            Integer k = mapElement.getKey();
            if (!R1.contains(k))
                R1.add(k);
        }
    }

    private static boolean handleBare(HashMap<String, HashMap<Integer, Integer>> dataSet, String[] keys, ArrayList<Integer> R2) {
        boolean isEverUsed = false;
        boolean flag2 = true;
        for (String key : keys) {
            if ((!(key.startsWith("+"))) && (!(key.startsWith("-")))) {
                isEverUsed = true;
                HashMap<Integer, Integer> temp = dataSet.get(key);
                if (temp == null){
                    System.out.println("key does not exist!");
                    System.exit(0);}
                else {
                    if (flag2) {
                        traverseHashMap(R2, temp);
                        flag2 = false;
                    } else {
                        for (int j = 0; j < R2.size(); j++) {
                            if (!temp.containsKey(R2.get(j)))
                                R2.remove(j);
                        }
                    }
                }
            }
        }
        return isEverUsed;
    }

    private static ArrayList<Integer> handleMinus(HashMap<String, HashMap<Integer, Integer>> dataSet, String[] keys) {
        ArrayList<Integer> keysToDelete = new ArrayList<>();
        for (String key : keys) {
            if (key.startsWith("-")) {
                HashMap<Integer, Integer> temp = dataSet.get(key.replaceFirst("-", ""));
                if (temp == null)
                    continue;
                traverseHashMap(keysToDelete, temp);
            }
        }
        return keysToDelete;
    }

    private static ArrayList<Integer> combineResults(boolean isPlusEverUsed,boolean isEverUsed, ArrayList<Integer> R1, ArrayList<Integer> R2, int size) {
        ArrayList<Integer> result = new ArrayList<>();
        if((!isEverUsed)&&(!isPlusEverUsed))
                addAllToResults(result, size);
        else if(isEverUsed&&!isPlusEverUsed)
            result.addAll(R2);
        else if(!isEverUsed)
            result.addAll(R1);
        else
        for (Integer integer : R2) {
            if (R1.contains(integer))
                result.add(integer);
        }
        return result;
    }

    private static void printResult(ArrayList<Integer> R2) {
        for (Integer integer : R2) {
            System.out.println(integer);
        }
    }

    private static void deleteKeys(ArrayList<Integer> keysToDelete, ArrayList<Integer> result) {
        for (Integer integer : keysToDelete) {
            result.remove(integer);
        }
    }

    private static void makeDataSet(String pathToFile, File[] listOfFiles, HashMap<String, HashMap<Integer, Integer>> dataSet) {
        for (int i = 0; i < listOfFiles.length; i++) {
            List<String> tempList = readFileInList(pathToFile + listOfFiles[i].getName());
            if (!tempList.isEmpty()){
                String[] temp = tempList.get(0).split(" ");
                main.java.stemmer.PorterStemmer porterStemmer = new main.java.stemmer.PorterStemmer();
                stem(temp, porterStemmer);
                toUpper(temp);
                for (String s : temp) {
                    if (dataSet.containsKey(s)) {
                        if (dataSet.get(s).containsKey(i))
                            dataSet.get(s).put(i, dataSet.get(s).get(i) + 1);
                        else
                            dataSet.get(s).put(i, 1);
                    } else {
                        HashMap<Integer, Integer> temporary = new HashMap<>();
                        temporary.put(i, 1);
                        dataSet.put(s, temporary);
                    }
                }
            }
        }
    }
    private static void stem(String[] temp, main.java.stemmer.PorterStemmer porterStemmer) {
        for (int j = 0; j < temp.length; j++) {
            temp[j] = porterStemmer.stem(temp[j]);
        }
    }

    private static void toUpper(String[] temp) {
        for (int j = 0; j < temp.length; j++) {
            temp[j] = temp[j].toUpperCase();
        }
    }
    private static void addAllToResults(ArrayList<Integer> results, int size){
        for (int i = 0; i < size; i++) {
            results.add(i);
        }
    }
}
