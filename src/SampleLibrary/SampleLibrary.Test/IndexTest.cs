using System.Collections.Generic;
using NSubstitute;
using SampleLibrary.Interfaces;
using Xunit.Sdk;

namespace SampleLibrary.Test;

public class IndexTest
{
    [Theory]
    [MemberData(nameof(GenerateData))]
    public void MakeDataSetTest(DataSet expectedDataset)
    {
        var fileHandler = Substitute.For<IFileAble>();
        fileHandler.GetListOfFiles().Returns(new string[] { ".//TestFolder\\a.txt", ".//TestFolder\\b.txt" });
        fileHandler.GetFileName(Arg.Is(0)).Returns(".//TestFolder\\a.txt");
        fileHandler.GetFileName(Arg.Is(1)).Returns(".//TestFolder\\b.txt");
        var indexer = new Indexer(fileHandler);

        var dataSet = new DataSet();
        indexer.MakeDataSet(dataSet);
        var x = dataSet.Equals(expectedDataset);
        
        Assert.True(x);
    }
    
    [Fact]
    public void MakeDataSet_EmptyFolder_ThrowsException()
    {
        var fileHandler = Substitute.For<IFileAble>();
        fileHandler.GetListOfFiles().Returns(new string[] { });
        var indexer = new Indexer(fileHandler);

        var dataSet = new DataSet();
        var action = () => indexer.MakeDataSet(dataSet);
        Assert.Throws<EmptyException>(action);
    }

    public static TheoryData<DataSet> GenerateData => new()
    {
        {
            new DataSet()
            {
                Dataset =
                {
                    {"HAJI", new Dictionary<int, int>(){{0,1}, {1,1}}},
                    {"ALI", new Dictionary<int, int>(){{1, 1}}},
                    {"J", new Dictionary<int, int>(){{1, 1}}},
                    {"HO", new Dictionary<int, int>(){{0, 1}}},
                    {"YO", new Dictionary<int, int>(){{0, 1}}}
                }
            }
        }
    };
    
}