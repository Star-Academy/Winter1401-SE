import java.io.File;

public class FileHandler implements FileAble{
    private final String pathToFile;
    private File[] listOfFiles;

    public FileHandler(String pathToFile) {
        this.pathToFile = pathToFile;
        initialize();
    }

    private void initialize(){
        File folder = new File(pathToFile);
        this.listOfFiles = folder.listFiles();
    }

    @Override
    public File[] getListOfFiles() {
        return listOfFiles;
    }

    @Override
    public String getFileName(int i){
        return pathToFile+listOfFiles[i].getName();
    }
}
