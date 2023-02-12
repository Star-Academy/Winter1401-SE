import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Collections;
import java.util.List;

public class Index {
    private final FileAble fileHandler;
    private final main.java.stemmer.PorterStemmer porterStemmer = new main.java.stemmer.PorterStemmer();

    public Index(FileAble fileAble) {
        this.fileHandler = fileAble;
    }

    public void makeDataSet(WriteAble dataSet) {
        boolean isEmpty = true;
        for (int i = 0; i < fileHandler.getListOfFiles().length; i++) {
            isEmpty = false;
            List<String> tempList = readFileInList(fileHandler.getFileName(i));
            if (tempList.isEmpty()){
                continue;
            }
            addToDataSet(dataSet, i, tempList);
        }
        if(isEmpty)
            handleEmptyFolder();
    }

    private void addToDataSet(WriteAble dataSet, int i, List<String> tempList) {
        String[] listOfTermsInCurrentFile = tempList.get(0).split(" ");
        for (String currentTerm : listOfTermsInCurrentFile) {
            var item = currentTerm;
            item = stem(item);
            item = item.toUpperCase();
            dataSet.write(item, i);
        }
    }

    public List<String> readFileInList(String fileName)
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
        return lines;
    }

    private String stem(String stringToStem) {
            return porterStemmer.stem(stringToStem);
    }

    private static void handleEmptyFolder() {
            System.out.println("Given directory is empty!!");
            System.exit(0);
    }

    public int getNumberOfFiles(){
        return fileHandler.getListOfFiles().length;
    }
}
