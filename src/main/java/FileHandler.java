import java.io.File;

public class FileHandler implements FileAble{
    private final String pathToFile;
    private final File folder;
    private final File[] listOfFiles;

    public FileHandler(String pathToFile) {
        this.pathToFile = pathToFile;
        this.folder = new File(pathToFile);
        this.listOfFiles = folder.listFiles();
    }

    @Override
    public File[] getListOfFiles() {
        return listOfFiles;
    }

    @Override
    public String getFileName(int i){
        return listOfFiles[i].getName()+pathToFile;
    }
}
