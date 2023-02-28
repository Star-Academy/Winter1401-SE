using System.Collections.Generic;

namespace SampleLibrary.Test;

public class DataSetTest
{
    [Theory]
    [InlineData(3)]
    public void SetGetNumberOfFiles_Test(int size)
    {
        var dataSet = new DataSet();
        dataSet.SetNumberOfFiles(size);
        
        Assert.Equal(dataSet.GetNumberOfFiles(), size);
    }

        [Fact]
    public void Write_NoPreviousRecord()
    {
        var dataSet = new DataSet();
        dataSet.Write("Ali",12);
        var expected = new Dictionary<int, int>() { { 12, 1 } };
        
        Assert.Equal(expected, dataSet.Read("Ali"));
    }

    [Fact]
    public void Write_WithPreviousRecordOnOtherFile()
    {
        var dataSet = new DataSet();
        dataSet.Write("Ali", 0);
        dataSet.Write("Ali", 1);
        var expected = new Dictionary<int, int> { { 0, 1 }, { 1, 1 } };

        Assert.Equal(expected,dataSet.Read("Ali"));
    }

    [Fact]
    public void Write_WithPreviousRecordOnSameFile()
    {
        var dataSet = new DataSet();
        dataSet.Write("Ali", 0);
        dataSet.Write("Ali", 0);
        var expected = new Dictionary<int, int> { { 0, 2 } };

        Assert.Equal(expected,dataSet.Read("Ali"));
    }

    [Fact]
    public void Equals_Test()
    {
        var dataset1 = new DataSet();
        var dataset2 = new DataSet();
        var dataset3 = new DataSet();
        dataset1.Write("a",5);
        dataset2.Write("a",5);
        dataset3.Write("a",1);

        Assert.True(dataset1.Equals(dataset2));
        Assert.False(dataset1.Equals(dataset3));
    }
}