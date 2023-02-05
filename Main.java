import java.util.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.*;
import java.io.*;

public class Main{

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
    public static void main(String[] args)
    {
        //Default directory C:\\Users\\mahdi.mohammadi\\FullTextSearch\\Test\\
        Scanner scanner = new Scanner(System.in);
        String pathToFile;
        System.out.println("Enter full directory path:");
        pathToFile = scanner.nextLine();
        File folder = new File(pathToFile);
        File[] listOfFiles = folder.listFiles();
        HashMap<String, HashMap<Integer, Integer>> dataSet = new HashMap<>();
        for (int i = 0; i < listOfFiles.length; i++) {
            List<String> tempList = readFileInList(pathToFile+listOfFiles[i].getName());
            String[] temp=tempList.get(0).split(" ");
            for (int j = 0; j < temp.length; j++) {
                if(dataSet.containsKey(temp[j])){
                    if(dataSet.get(temp[j]).containsKey(i))
                     dataSet.get(temp[j]).put(i,dataSet.get(temp[j]).get(i)+1);
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

        System.out.println("Enter input word:");
        String key = scanner.nextLine();
        String[] keys = key.split(" ");
        ArrayList<Integer> result = new ArrayList<>();
        boolean f = true;
        for (int i = 0; i < keys.length; i++) {
            if (keys[i].startsWith("+")) {
                f = false;
                HashMap<Integer, Integer> temp = dataSet.get(keys[i].replaceFirst("\\+", ""));
                for (Map.Entry<Integer, Integer> mapElement : temp.entrySet()) {
                    Integer k = mapElement.getKey();
                    if (!result.contains(k))
                        result.add(k);
                }
            }
        }
        ArrayList<Integer> R1 = new ArrayList<>();
        boolean flag = true;
        for (int i = 0; i < keys.length; i++) {
            if((!(keys[i].startsWith("+")))&&(!(keys[i].startsWith("-")))){
                HashMap<Integer, Integer> temp = dataSet.get(keys[i]);
                if(flag){
                    for (Map.Entry<Integer, Integer> mapElement : temp.entrySet()) {
                        Integer k = mapElement.getKey();
                            R1.add(k);
                    }
                    flag = false;
                }
                else{
                for (int j = 0; j < R1.size(); j++) {
                    if(!temp.containsKey(R1.get(j)))
                        R1.remove(j);
                }}
            }

        }
        ArrayList<Integer> keysToDelete = new ArrayList<>();
        for (int i = 0; i < keys.length; i++) {
            if(keys[i].startsWith("-")){
                HashMap<Integer, Integer> temp = dataSet.get(keys[i].replaceFirst("\\-",""));
                for (Map.Entry<Integer, Integer> mapElement : temp.entrySet()) {
                    Integer k = mapElement.getKey();
                    if(!keysToDelete.contains(k))
                        keysToDelete.add(k);
            }
        }}
        ArrayList<Integer> R2 = new ArrayList<>();
        for (int i = 0; i < R1.size(); i++) {
            if(result.contains(R1.get(i))||f)
                R2.add(R1.get(i));
        }
        for (int i = 0; i < keysToDelete.size(); i++) {
            if(R2.contains(keysToDelete.get(i)))
                R2.remove(keysToDelete.get(i));
        }
        for (int i = 0; i < R2.size(); i++) {
                System.out.println(R2.get(i));
        }
    }

}
