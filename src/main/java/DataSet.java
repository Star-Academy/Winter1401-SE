import java.util.HashMap;

public class DataSet implements ReadAble, WriteAble {

    private final HashMap<String, HashMap<Integer, Integer>> dataSet;

    public DataSet() {
        this.dataSet = new HashMap<>();
    }

    @Override
    public HashMap<Integer, Integer> read(String key){
        return this.dataSet.get(key);
    }

    @Override
    public void write(String stringToWrite, Integer numberOfCurrentFile){
            writeToDataSet(stringToWrite, numberOfCurrentFile);
    }
    private void writeToDataSet(String stringToWrite, Integer numberOfCurrentFile){
        if (!dataSet.containsKey(stringToWrite)) {
            HashMap<Integer, Integer> temporary = new HashMap<>();
            dataSet.put(stringToWrite, temporary);
        }
        var countByFiles = dataSet.get(stringToWrite);
        if(!countByFiles.containsKey(numberOfCurrentFile)){
            countByFiles.put(numberOfCurrentFile, 0);
        }
        countByFiles.put(numberOfCurrentFile, countByFiles.get(numberOfCurrentFile) + 1);
    }
}
