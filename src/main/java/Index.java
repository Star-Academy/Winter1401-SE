import java.io.File;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Collections;
import java.util.List;

public class Index {
    private final FileAble fileHandler;
    private final String pathToFile;
    private final main.java.stemmer.PorterStemmer porterStemmer = new main.java.stemmer.PorterStemmer();

    public Index(String pathToFile, FileAble fileAble) {
        this.pathToFile = pathToFile;
        this.fileHandler = fileAble;
    }

    public void makeDataSet(WriteAble dataSet) {
        handleEmptyFolder(fileHandler.getListOfFiles());
        for (int i = 0; i < fileHandler.getListOfFiles().length; i++) {
            List<String> tempList = readFileInList(pathToFile + fileHandler.getListOfFiles()[i].getName());
            if (tempList.isEmpty()){
                continue;
            }
            String[] listOfTermsInCurrentFile = tempList.get(0).split(" ");
            for (int j = 0; j < listOfTermsInCurrentFile.length; j++) {
                listOfTermsInCurrentFile[j] = stem(listOfTermsInCurrentFile[j]);
                listOfTermsInCurrentFile[j] = listOfTermsInCurrentFile[j].toUpperCase();
                dataSet.write(listOfTermsInCurrentFile[j], i);
            }
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

    private static void handleEmptyFolder(File[] listOfFiles) {
        if(listOfFiles == null){
            System.out.println("Given directory is empty!!");
            System.exit(0);
        }
    }

    public int getNumberOfFiles(){
        return fileHandler.getListOfFiles().length;
    }
}
