import java.io.File;

public class FileHandler implements FileAble{
    private final File folder;
    private final File[] listOfFiles;

    public FileHandler(String pathToFile) {
        this.folder = new File(pathToFile);
        this.listOfFiles = folder.listFiles();
    }

    @Override
    public File[] getListOfFiles() {
        return listOfFiles;
    }
}
