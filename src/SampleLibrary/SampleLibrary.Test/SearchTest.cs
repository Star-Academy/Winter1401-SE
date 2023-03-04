using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace SampleLibrary.Test;

public class SearchTest
{
    public static IEnumerable<object[]> GenerateBareData()
    {
          yield return new object[]
          {
              "ali j",
              2,
              new DataSet()
              {
                  Dataset =
                  {
                      { "ALI", new Dictionary<int, int>() { { 0, 1 } } },
                      { "J", new Dictionary<int, int>() { { 0, 1 } } },
                      { "HO", new Dictionary<int, int>() { { 1, 1 } } },
                      { "YO", new Dictionary<int, int>() { { 1, 1 } } }
                  }
              },
              new List<int> { 0 }
          };

          yield return new object[]
          {
              "ho yo",
              2,
              new DataSet()
              {
                  Dataset =
                  {
                      { "ALI", new Dictionary<int, int>() { { 0, 1 } } },
                      { "J", new Dictionary<int, int>() { { 0, 1 } } },
                      { "HO", new Dictionary<int, int>() { { 1, 1 } } },
                      { "YO", new Dictionary<int, int>() { { 1, 1 } } }
                  }
              },
              new List<int> { 1 }
          };
        
        yield return new object[]
        {
            "haji",
            2,
            new DataSet()
            {
                Dataset =
                {
                    {"HAJI", new Dictionary<int, int>(){{0,1},{1, 1}}},
                    { "ALI", new Dictionary<int, int>() { { 1, 0 } } },
                    { "J", new Dictionary<int, int>() { { 1, 0 } } },
                    { "HO", new Dictionary<int, int>() { { 1, 1 } } },
                    { "YO", new Dictionary<int, int>() { { 1, 1 } } }
                }
            },
            new List<int> { 0, 1 }
        };
        
    }
    
    [Theory]
    [MemberData(nameof(GenerateBareData))]
    public void Search_BareWords_Should_Return_True(string key, int numberOfFiles, DataSet dataSet, List<int> expectedResult)
    {
        var search = new Search(dataSet);
        var expected = search.SearchKey(key);
        
        var x = expected.SequenceEqual(expectedResult);
        x.Should().BeTrue();
    }

    public static IEnumerable<object[]> GenerateBadData()
    {
        yield return new object[]
        {
            "haji",
            2,
            new DataSet()
            {
                Dataset =
                {
                    { "ALI", new Dictionary<int, int>() { { 1, 0 } } },
                    { "J", new Dictionary<int, int>() { { 1, 0 } } },
                    { "HO", new Dictionary<int, int>() { { 1, 1 } } },
                    { "YO", new Dictionary<int, int>() { { 1, 1 } } }
                }
            },
            new List<int> { 0, 1 }
        };
        yield return new object[]
        {
            "haji hello",
            2,
            new DataSet()
            {
                Dataset =
                {
                    { "ALI", new Dictionary<int, int>() { { 1, 0 } } },
                    { "J", new Dictionary<int, int>() { { 1, 0 } } },
                    { "HO", new Dictionary<int, int>() { { 1, 1 } } },
                    { "YO", new Dictionary<int, int>() { { 1, 1 } } }
                }
            },
            new List<int> {  }
        };
    }

    [Theory]
    [MemberData(nameof(GenerateBadData))]
    public void Search_AbsentKeys_Should_Return_Exception(string key, int numberOfFiles, DataSet dataSet,
        List<int> expectedResult)
    {
        var search = new Search(dataSet);
        var action = () => search.SearchKey(key);
        
        Assert.Throws<KeyNotFoundException>(action);
    }
    
    public static IEnumerable<object[]> GeneratePlusData()
    {
        yield return new object[]
        {
            "+hello +yo +haji",
            2,
            new DataSet()
            {
                Dataset =
                {
                    {"HAJI", new Dictionary<int, int>(){{0,1},{1, 1}}},
                    { "ALI", new Dictionary<int, int>() { { 0, 1 } } },
                    { "J", new Dictionary<int, int>() { { 0, 1 } } },
                    { "HO", new Dictionary<int, int>() { { 1, 1 } } },
                    { "YO", new Dictionary<int, int>() { { 1, 1 } } }
                }
            },
            new List<int> { 0, 1 }
        };  
    }

    [Theory]
    [MemberData(nameof(GeneratePlusData))]
    public void SearchKey_PlusWord_Should_Return_true(string key, int numberOfFiles, DataSet dataSet,
        List<int> expectedResult)
    {
        var search = new Search(dataSet);
        var expected = search.SearchKey(key);
        
        var x = expected.OrderBy(x=>x).SequenceEqual(expectedResult);
        x.Should().BeTrue();
    }
    
    public static IEnumerable<object[]> GenerateMinusData()
    {
        yield return new object[]
        {
            "-yo haji -hello",
            2,
            new DataSet()
            {
                Dataset =
                {
                    {"HAJI", new Dictionary<int, int>(){{0,1},{1, 1}}},
                    { "ALI", new Dictionary<int, int>() { { 0, 1 } } },
                    { "J", new Dictionary<int, int>() { { 0, 1 } } },
                    { "HO", new Dictionary<int, int>() { { 1, 1 } } },
                    { "YO", new Dictionary<int, int>() { { 1, 1 } } }
                }
            },
            new List<int> { 0 }
        };  
    }

    [Theory]
    [MemberData(nameof(GenerateMinusData))]
    public void SearchKey_MinusWords_Should_Returns_True(string key, int numberOfFiles, DataSet dataSet,
        List<int> expectedResult)
    {
        var search = new Search(dataSet);
        var expected = search.SearchKey(key);
        
        var x = expected.OrderBy(x=>x).SequenceEqual(expectedResult);
        x.Should().BeTrue();
    }
    
    public static IEnumerable<object[]> GenerateEmptyData()
    {
        yield return new object[]
        {
            "",
            2,
            new DataSet()
            {
                NumberOfFiles = 2,
                Dataset =
                {
                    {"HAJI", new Dictionary<int, int>(){{0,1},{1, 1}}},
                    { "ALI", new Dictionary<int, int>() { { 0, 1 } } },
                    { "J", new Dictionary<int, int>() { { 0, 1 } } },
                    { "HO", new Dictionary<int, int>() { { 1, 1 } } },
                    { "YO", new Dictionary<int, int>() { { 1, 1 } } }
                }
            },
            new List<int> { 0 , 1 }
        };  
    }

    [Theory]
    [MemberData(nameof(GenerateEmptyData))]
    public void SearchKey_Empty_Should_Return_True(string key, int numberOfFiles, DataSet dataSet,
        List<int> expectedResult)
    {
        var search = new Search(dataSet);
        var expected = search.SearchKey(key);
        
        var x = expected.OrderBy(x=>x).SequenceEqual(expectedResult);
        x.Should().BeTrue();
    }
    
    public static IEnumerable<object[]> GeneratePlusBareData()
    {
        yield return new object[]
        {
            "+ali +j haji +yo",
            2,
            new DataSet()
            {
                NumberOfFiles = 2,
                Dataset =
                {
                    {"HAJI", new Dictionary<int, int>(){{0,1},{1, 1}}},
                    { "ALI", new Dictionary<int, int>() { { 0, 1 } } },
                    { "J", new Dictionary<int, int>() { { 0, 1 } } },
                    { "HO", new Dictionary<int, int>() { { 1, 1 } } },
                    { "YO", new Dictionary<int, int>() { { 1, 1 } } }
                }
            },
            new List<int> { 0 , 1 }
        };  
    }

    [Theory]
    [MemberData(nameof(GeneratePlusBareData))]
    public void SearchKey_Plus_And_Bare_Should_Return_True(string key, int numberOfFiles, DataSet dataSet,
        List<int> expectedResult)
    {
        var search = new Search(dataSet);
        var expected = search.SearchKey(key);
        
        var x = expected.OrderBy(x=>x).SequenceEqual(expectedResult);
        x.Should().BeTrue();
    }
}