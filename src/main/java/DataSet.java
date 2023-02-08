import java.util.HashMap;

public class DataSet implements ReadAble, WriteAble {

    private  HashMap<String, HashMap<Integer, Integer>> dataSet;

    public DataSet() {
        this.dataSet = new HashMap<>();
    }
    @Override
    public HashMap<Integer, Integer> read(String key){
        return this.dataSet.get(key);
    }
    @Override
    public void write(String[] temp, Integer i){
        for (String s : temp) {
            if (!dataSet.containsKey(s)) {
                HashMap<Integer, Integer> temporary = new HashMap<>();
                dataSet.put(s, temporary);
            }
            var countByFiles = dataSet.get(s);
            if(!countByFiles.containsKey(i)){
                countByFiles.put(i, 0);
            }
            countByFiles.put(i, countByFiles.get(i) + 1);
        }
    }
}
