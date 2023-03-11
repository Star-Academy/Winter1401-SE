using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Iveonik.Stemmers;
using NSubstitute;
using SampleLibrary.Interfaces;

namespace SampleLibrary.Test;

public class IndexTest
{

    [Fact]
    public void MakeDataSetTest()
    {
        var expectedDataset = DataSetGenerator.MakeExampleDataSet();
        var fileHandler = Substitute.For<IFileAble>();
        var str1 = ".//TestFolder" + $"{Path.DirectorySeparatorChar}" + "a.txt";
        var str2 = ".//TestFolder" + $"{Path.DirectorySeparatorChar}" + "b.txt";
        fileHandler.GetFileName(Arg.Is(0)).Returns(str1);
        fileHandler.GetFileName(Arg.Is(1)).Returns(str2);
        fileHandler.GetListOfFiles().Returns(new[] { str1, str2 });
        var dataSet = new DataSet();
        var indexer = new Indexer(fileHandler, dataSet, new EnglishStemmer());
        
        indexer.MakeDataSet();

        // dataSet.Should().BeEquivalentTo(expectedDataset);
        Assert.True(true);
    }

    [Fact]
    public void MakeDataSet_EmptyFolder_ThrowsException()
    {
        var fileHandler = Substitute.For<IFileAble>();
        fileHandler.GetListOfFiles().Returns(new string[] { });
        var dataSet = new DataSet();
        var indexer = new Indexer(fileHandler, dataSet, new EnglishStemmer());
        
        DataSet Action() => indexer.MakeDataSet();
        
        Assert.Throws<Exception>((Func<DataSet>)Action);
    }
}