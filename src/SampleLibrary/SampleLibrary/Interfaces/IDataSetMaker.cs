namespace SampleLibrary.Interfaces;

public interface IDataSetMaker
{
    public DataSet MakeDataSet(IWriteAble dataSet);
}