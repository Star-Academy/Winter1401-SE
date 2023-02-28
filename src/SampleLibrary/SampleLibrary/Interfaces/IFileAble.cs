namespace SampleLibrary.Interfaces;

public interface IFileAble
{
    public string[] GetListOfFiles();

    public string GetFileName(int numberOfCurrentFile);

    public void LoadFile(string pathToFile);

}