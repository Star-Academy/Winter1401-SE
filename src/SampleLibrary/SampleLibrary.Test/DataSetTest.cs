using System.Collections.Generic;

namespace SampleLibrary.Test;

public class DataSetTest
{
    [Fact]
    public void Write_NoPreviousRecord()
    {
        var dataSet = new DataSet();
        var expected = new List<int>() { { 12 } };

        dataSet.Write("Ali",12);
        
        Assert.Equal(expected, dataSet.Read("Ali"));
    }

    [Fact]
    public void Write_WithPreviousRecordOnOtherFile()
    {
        var dataSet = new DataSet();
        var expected = new List<int>() { { 0 }, { 1 } };
        
        dataSet.Write("Ali", 0);
        dataSet.Write("Ali", 1);
        
        Assert.Equal(expected,dataSet.Read("Ali"));
    }

    [Fact]
    public void Write_WithPreviousRecordOnSameFile()
    {
        var dataSet = new DataSet();
        var expected = new List<int>() { { 0 } };

        dataSet.Write("Ali", 0);
        dataSet.Write("Ali", 0);

        Assert.Equal(expected,dataSet.Read("Ali"));
    }
}