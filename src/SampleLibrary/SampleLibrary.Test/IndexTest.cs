using System.Collections.Generic;
using NSubstitute;
using SampleLibrary.Interfaces;

namespace SampleLibrary.Test;

public class IndexTest
{
    // [Theory]
    // [MemberData(nameof(GenerateData))]
    // public void MakeDataSetTest(DataSet expectedDataset)
    // {
    //     var fileHandler = Substitute.For<IFileAble>();
    //     fileHandler.GetListOfFiles().Returns(new string[] { ".//TestFolder\\.txt", ".//TestFolder\\a.txt" });
    //     fileHandler.GetFileName(Arg.Is(0)).Returns(".//TestFolder\\.txt");
    //     fileHandler.GetFileName(Arg.Is(1)).Returns(".//TestFolder\\a.txt");
    //     var indexer = new Indexer(fileHandler);
    //
    //     var dataSet = new DataSet();
    //     indexer.MakeDataSet(dataSet);
    //     var x = dataSet.Equals(expectedDataset);
    //     
    //     Assert.True(x);
    // }

    public static TheoryData<DataSet> GenerateData => new()
    {
        {
            new DataSet()
            {
                _dataset =
                {
                    {"HAJI", new Dictionary<int, int>(){{0,1}, {1,1}}},
                    {"ALI", new Dictionary<int, int>(){{0, 1}}},
                    {"J", new Dictionary<int, int>(){{0, 1}}},
                    {"HO", new Dictionary<int, int>(){{1, 1}}},
                    {"YO", new Dictionary<int, int>(){{1, 1}}}
                }
            }
        }
    };
    
}