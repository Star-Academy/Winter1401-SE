import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class Search {
    public ArrayList<Integer> searchKey(String key, ReadAble dataSet, int numberOfFiles) {
        key = key.toUpperCase();
        String[] keys = key.split(" ");

        ArrayList<Integer> resultFromPlusMethod = new ArrayList<>();
        boolean isPlusEverUsed = handlePlus(dataSet, keys, resultFromPlusMethod);

        ArrayList<Integer> resultFromBareMethod = new ArrayList<>();
        boolean isEverUsed = handleBare(dataSet, keys, resultFromBareMethod);

        ArrayList<Integer> keysToDelete = handleMinus(dataSet, keys);

        ArrayList<Integer> result = combineResults(isPlusEverUsed, isEverUsed, resultFromPlusMethod, resultFromBareMethod, numberOfFiles);

        deleteKeys(keysToDelete, result);
        return result;
    }

    private Boolean handlePlus(ReadAble dataSet, String[] keys, ArrayList<Integer> resultFromPlusMethod) {
        boolean flag1 = true;
        for (String key : keys) {
            if (!key.startsWith("+")) {
                continue;
            }
            flag1 = false;
            HashMap<Integer, Integer> temp = dataSet.read(key.replaceFirst("\\+", ""));
            if (temp == null)
                continue;
            traverseHashMap(resultFromPlusMethod, temp);
        }
        return !flag1;
    }

    private void traverseHashMap(ArrayList<Integer> result, HashMap<Integer, Integer> hashMapToTraverse) {
        for (Map.Entry<Integer, Integer> mapElement : hashMapToTraverse.entrySet()) {
            Integer k = mapElement.getKey();
            if (!result.contains(k))
                result.add(k);
        }
    }

    private boolean handleBare(ReadAble dataSet, String[] keys, ArrayList<Integer> resultFromBareMethod) {
        boolean isEverUsed = false;
        boolean isFirstTime = true;
        for (String key : keys) {
            if (!startsWithNothing(key)) {
                continue;
            }
            isEverUsed = true;
            isFirstTime = handleEachTerm(dataSet, resultFromBareMethod, isFirstTime, key);
        }
        return isEverUsed;
    }

    private boolean handleEachTerm(ReadAble dataSet, ArrayList<Integer> resultFromBareMethod, boolean isFirstTime, String key) {
        HashMap<Integer, Integer> currentTermFiles = dataSet.read(key);
        if (currentTermFiles == null) {
            handleEmptyList();
        }
        else {
            isFirstTime = handleNotEmpty(resultFromBareMethod, isFirstTime, currentTermFiles);
        }
        return isFirstTime;
    }

    private static void handleEmptyList() {
        System.out.println("key does not exist!");
        System.exit(0);
    }

    private boolean handleNotEmpty(ArrayList<Integer> resultFromBareMethod, boolean isFirstTime, HashMap<Integer, Integer> temp) {
        if (isFirstTime) {
            traverseHashMap(resultFromBareMethod, temp);
        } else {
            handleNextTimes(resultFromBareMethod, temp);
        }
        return false;
    }

    private static void handleNextTimes(ArrayList<Integer> resultFromBareMethod, HashMap<Integer, Integer> temp) {
        for (int j = 0; j < resultFromBareMethod.size(); j++) {
            if (!temp.containsKey(resultFromBareMethod.get(j)))
                resultFromBareMethod.remove(j);
        }
    }

    private ArrayList<Integer> handleMinus(ReadAble dataSet, String[] keys) {
        ArrayList<Integer> keysToDelete = new ArrayList<>();
        for (String key : keys) {
            startWithMinusHandler(dataSet, keysToDelete, key);
        }
        return keysToDelete;
    }

    private void startWithMinusHandler(ReadAble dataSet, ArrayList<Integer> keysToDelete, String key) {
        if (key.startsWith("-")) {
            HashMap<Integer, Integer> temp = dataSet.read(key.replaceFirst("-", ""));
            if (temp == null)
                return;
            traverseHashMap(keysToDelete, temp);
        }
    }

    private ArrayList<Integer> combineResults(boolean isPlusEverUsed, boolean isEverUsed, ArrayList<Integer> resultFromPlusMethod, ArrayList<Integer> resultFromBareMethod, int size) {
        ArrayList<Integer> result = new ArrayList<>();

        if ((!isEverUsed) && (!isPlusEverUsed))
            addAllToResults(result, size);

        else if (isEverUsed && !isPlusEverUsed)
            result.addAll(resultFromBareMethod);

        else if (!isEverUsed)
            result.addAll(resultFromPlusMethod);

        else
            for (Integer integer : resultFromBareMethod) {
                if (resultFromPlusMethod.contains(integer))
                    result.add(integer);
            }

        return result;
    }

    private void deleteKeys(ArrayList<Integer> keysToDelete, ArrayList<Integer> result) {
        for (Integer integer : keysToDelete) {
            result.remove(integer);
        }
    }

    private void addAllToResults(ArrayList<Integer> results, int size) {
        for (int i = 0; i < size; i++) {
            results.add(i);
        }
    }

    private boolean startsWithNothing(String key) {
        return (!(key.startsWith("+"))) && (!(key.startsWith("-")));
    }
}
