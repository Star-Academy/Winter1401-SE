package main.java;

import java.util.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.*;
import java.io.*;
import main.java.stemmer.*;

public class Main{

    public static void main(String[] args)
    {
        Scanner scanner = new Scanner(System.in);
        String pathToFile;
        System.out.println("Enter full directory path:");   //Default directory C:\\Users\\mahdi.mohammadi\\FullTextSearch\\Test\\
        pathToFile = scanner.nextLine();
        File folder = new File(pathToFile);
        File[] listOfFiles = folder.listFiles();
        HashMap<String, HashMap<Integer, Integer>> dataSet = new HashMap<>();
        makeDataSet(pathToFile, listOfFiles, dataSet);
        System.out.println("Enter input word:");
        String key = scanner.nextLine();
        key = key.toUpperCase();
        String[] keys = key.split(" ");
        ArrayList<Integer> R1 = new ArrayList<>();
        boolean flag1 = handlePlus(dataSet, keys, true, R1);
        ArrayList<Integer> R2 = new ArrayList<>();
        handleBare(dataSet, keys, R2);
        ArrayList<Integer> keysToDelete = handleMinus(dataSet, keys);
        ArrayList<Integer> result = combineResults(flag1, R1, R2);
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

    private static Boolean handlePlus(HashMap<String, HashMap<Integer, Integer>> dataSet, String[] keys, Boolean flag1, ArrayList<Integer> R1) {
        for (int i = 0; i < keys.length; i++) {
            if (keys[i].startsWith("+")) {
                flag1 = false;
                HashMap<Integer, Integer> temp = dataSet.get(keys[i].replaceFirst("\\+", ""));
                if(temp == null)
                    System.out.println("key does not exist!");
                traverseHashMap(R1, temp);
            }
        }
        return flag1;
    }

    private static void traverseHashMap(ArrayList<Integer> R1, HashMap<Integer, Integer> temp) {
        for (Map.Entry<Integer, Integer> mapElement : temp.entrySet()) {
            Integer k = mapElement.getKey();
            if (!R1.contains(k))
                R1.add(k);
        }
    }

    private static boolean handleBare(HashMap<String, HashMap<Integer, Integer>> dataSet, String[] keys, ArrayList<Integer> R2) {
        boolean flag2 = true;
        for (int i = 0; i < keys.length; i++) {
            if((!(keys[i].startsWith("+")))&&(!(keys[i].startsWith("-")))){
                HashMap<Integer, Integer> temp = dataSet.get(keys[i]);
                if(temp == null)
                    System.out.println("key does not exist!");
                else{
                if(flag2){
                        traverseHashMap(R2, temp);
                        flag2 = false;
                }
                else{
                    for (int j = 0; j < R2.size(); j++) {
                        if(!temp.containsKey(R2.get(j)))
                            R2.remove(j);
                    }}}
            }
        }
        return flag2;
    }

    private static ArrayList<Integer> handleMinus(HashMap<String, HashMap<Integer, Integer>> dataSet, String[] keys) {
        ArrayList<Integer> keysToDelete = new ArrayList<>();
        for (int i = 0; i < keys.length; i++) {
            if(keys[i].startsWith("-")){
                HashMap<Integer, Integer> temp = dataSet.get(keys[i].replaceFirst("\\-",""));
                if(temp == null)
                    System.out.println("key does not exist!");
                traverseHashMap(keysToDelete, temp);
            }}
        return keysToDelete;
    }

    private static ArrayList<Integer> combineResults(boolean flag, ArrayList<Integer> R1, ArrayList<Integer> R2) {
        ArrayList<Integer> result = new ArrayList<>();
        for (int i = 0; i < R2.size(); i++) {
            if(R1.contains(R2.get(i))||flag)
                result.add(R2.get(i));
        }
        return result;
    }

    private static void printResult(ArrayList<Integer> R2) {
        for (int i = 0; i < R2.size(); i++) {
            System.out.println(R2.get(i));
        }
    }

    private static void deleteKeys(ArrayList<Integer> keysToDelete, ArrayList<Integer> result) {
        for (int i = 0; i < keysToDelete.size(); i++) {
            if(result.contains(keysToDelete.get(i)))
                result.remove(keysToDelete.get(i));
        }
    }

    private static void makeDataSet(String pathToFile, File[] listOfFiles, HashMap<String, HashMap<Integer, Integer>> dataSet) {
        for (int i = 0; i < listOfFiles.length; i++) {
            List<String> tempList = readFileInList(pathToFile + listOfFiles[i].getName());
            String[] temp=tempList.get(0).split(" ");
            PorterStemmer porterStemmer = new PorterStemmer();
            stem(temp, porterStemmer);
            toUpper(temp);
            for (int j = 0; j < temp.length; j++) {
                if(dataSet.containsKey(temp[j])){
                    if(dataSet.get(temp[j]).containsKey(i))
                        dataSet.get(temp[j]).put(i, dataSet.get(temp[j]).get(i)+1);
                    else
                        dataSet.get(temp[j]).put(i,1);
                }
                else{
                    HashMap<Integer, Integer> temporary = new HashMap<>();
                    temporary.put(i,1);
                    dataSet.put(temp[j], temporary);
                }
            }
        }
    }

    private static void stem(String[] temp, PorterStemmer porterStemmer) {
        for (int j = 0; j < temp.length; j++) {
            temp[j] = porterStemmer.stem(temp[j]);
        }
    }

    private static void toUpper(String[] temp) {
        for (int j = 0; j < temp.length; j++) {
            temp[j] = temp[j].toUpperCase();
        }
    }

}
