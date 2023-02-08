import java.io.File;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Collections;
import java.util.List;

public class Index {

    String pathToFile;
    File folder;
    File[] listOfFiles;

    public Index(String pathToFile) {
        this.pathToFile = pathToFile;
        this.folder = new File(pathToFile);
        this.listOfFiles = folder.listFiles();
    }

    void makeDataSet(WriteAble dataSet) {
        handleEmptyFolder(listOfFiles);
        for (int i = 0; i < listOfFiles.length; i++) {
            List<String> tempList = readFileInList(pathToFile + listOfFiles[i].getName());
            if (!tempList.isEmpty()){
                String[] listOfTermsInCurrentFile = tempList.get(0).split(" ");
                main.java.stemmer.PorterStemmer porterStemmer = new main.java.stemmer.PorterStemmer();
                stem(listOfTermsInCurrentFile, porterStemmer);
                toUpper(listOfTermsInCurrentFile);
                dataSet.write(listOfTermsInCurrentFile,i);
            }
        }
    }
    private void toUpper(String[] listOfTermsInCurrentFile) {
        for (int j = 0; j < listOfTermsInCurrentFile.length; j++) {
            listOfTermsInCurrentFile[j] = listOfTermsInCurrentFile[j].toUpperCase();
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
        for (int i = 0; i < lines.size(); i++) {
            lines.set(i,lines.get(i).toUpperCase());
        }
        return lines;
    }
    private void stem(String[] stringsToStem, main.java.stemmer.PorterStemmer porterStemmer) {
        for (int j = 0; j < stringsToStem.length; j++) {
            stringsToStem[j] = porterStemmer.stem(stringsToStem[j]);
        }
    }
    private static void handleEmptyFolder(File[] listOfFiles) {
        if(listOfFiles == null){
            System.out.println("Given directory is empty!!");
            System.exit(0);
        }
    }
    public int getNumberOfFiles(){
        return listOfFiles.length;
    }
}
