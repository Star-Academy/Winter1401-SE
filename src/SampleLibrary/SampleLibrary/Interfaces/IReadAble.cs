using Microsoft.VisualBasic.CompilerServices;

namespace SampleLibrary.Interfaces;

public interface IReadAble
{
    public IEnumerable<int> Read(string key);

    public List<int> GetFilesNames();
}