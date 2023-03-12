namespace SampleLibrary.Interfaces;

public interface IDataSetMaker
{
    public DataSet MakeDataSet();

    public void LoadDirectory(string pathToFile);

}