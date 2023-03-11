namespace SampleLibrary.Test;

public class DataSetGenerator
{
    public static DataSet MakeExampleDataSet()
    {
        var dataSet = new DataSet();
        dataSet.Write("HAJI",0);
        dataSet.Write("HAJI",1);
        dataSet.Write("ALI",0);
        dataSet.Write("J",0);
        dataSet.Write("HO",1);
        dataSet.Write("YO",1);

        return dataSet;
    }
}