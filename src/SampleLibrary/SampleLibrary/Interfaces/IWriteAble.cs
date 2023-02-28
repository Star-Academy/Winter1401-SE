namespace SampleLibrary.Interfaces;

public interface IWriteAble
{
    public void Write(string key, int numberOfCurrentFile);

    public void SetNumberOfFiles(int size);
}