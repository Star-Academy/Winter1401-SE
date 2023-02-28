using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class FileHandler : IFileAble
{
    private string[] _listOfFiles;

    public void LoadFile(string pathToFile)
    {
        _listOfFiles = Directory.GetFiles(pathToFile);
    }

    public string GetFileName(int numberOfFile)
    {
        return _listOfFiles[numberOfFile];
    }

    public string[] GetListOfFiles()
    {
        return _listOfFiles;
    }
}