using Microsoft.VisualBasic.CompilerServices;

namespace SampleLibrary.Interfaces;

public interface IReadAble
{
    public Dictionary<int, int> Read(string key);

    public int GetNumberOfFiles();
}