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
              new List<int> { 0 }
          };

          yield return new object[]
          {
              "ho yo",
              2,
              new List<int> { 1 }
          };
        
        yield return new object[]
        {
            "haji",
            2,
            new List<int> { 0, 1 }
        };
        
        yield return new object[]
        {
            "dfhasjflsdjf",
            2,
            new List<int> {  }
        };
        yield return new object[]
        {
            "haji j",
            2,
            new List<int> { 0 }
        };
        
    }
    
    [Theory]
    [MemberData(nameof(GenerateBareData))]
    public void Search_BareWords_Should_Return_True(string key, int numberOfFiles, List<int> expectedResult)
    {
        var search = new Search(DataSetGenerator.MakeExampleDataSet());
        
        var expected = search.SearchKey(key);
        
        expected.SequenceEqual(expectedResult).Should().BeTrue();
    }

    public static IEnumerable<object[]> GenerateBadData()
    {
        yield return new object[]
        {
            "hajij",
            2,
            new List<int> {  }
        };
        yield return new object[]
        {
            "haji hello",
            2,
            new List<int> {  }
        };
    }

    [Theory]
    [MemberData(nameof(GenerateBadData))]
    public void Search_AbsentKeys_Should_Return_Nothing(string key, int numberOfFiles,
        List<int> expectedResult)
    {
        var search = new Search(DataSetGenerator.MakeExampleDataSet());

        var actual = search.SearchKey(key);
        
        actual.SequenceEqual(expectedResult).Should().BeTrue();
    }
    
    public static IEnumerable<object[]> GeneratePlusData()
    {
        yield return new object[]
        {
            "+hello +yo +haji",
            2,
            new List<int> { 0, 1 }
        };  
        
        yield return new object[]
        {
            "+hello +jfasdkljfklsdj",
            2,
            new List<int> {  }
        };  
    }

    [Theory]
    [MemberData(nameof(GeneratePlusData))]
    public void SearchKey_PlusWord_Should_Return_true(string key, int numberOfFiles,
        List<int> expectedResult)
    {
        var search = new Search(DataSetGenerator.MakeExampleDataSet());
        
        var expected = search.SearchKey(key);
        
        expected.OrderBy(x=>x).SequenceEqual(expectedResult).Should().BeTrue();
    }
    
    public static IEnumerable<object[]> GenerateMinusData()
    {
        yield return new object[]
        {
            "-yo haji -hello",
            2,
            new List<int> { 0 }
        };
        
        yield return new object[]
        {
            "-hello",
            2,
            new List<int> { 0, 1 }
        };  
    }

    [Theory]
    [MemberData(nameof(GenerateMinusData))]
    public void SearchKey_MinusWords_Should_Returns_True(string key, int numberOfFiles,
        List<int> expectedResult)
    {
        var search = new Search(DataSetGenerator.MakeExampleDataSet());
        
        var expected = search.SearchKey(key);
        
        expected.OrderBy(x=>x).SequenceEqual(expectedResult).Should().BeTrue();
    }
    
    public static IEnumerable<object[]> GenerateEmptyData()
    {
        yield return new object[]
        {
            "",
            2,
            new List<int> {  }
        };  
    }

    [Theory]
    [MemberData(nameof(GenerateEmptyData))]
    public void SearchKey_Empty_Should_Return_True(string key, int numberOfFiles,
        List<int> expectedResult)
    {
        var search = new Search(DataSetGenerator.MakeExampleDataSet());
        
        var expected = search.SearchKey(key);
        
        expected.SequenceEqual(expectedResult).Should().BeTrue();
    }
    
    public static IEnumerable<object[]> GeneratePlusBareData()
    {
        yield return new object[]
        {
            "+ali +j haji +yo",
            2,
            new List<int> { 0 , 1 }
        };  
    }

    [Theory]
    [MemberData(nameof(GeneratePlusBareData))]
    public void SearchKey_Plus_And_Bare_Should_Return_True(string key, int numberOfFiles,
        List<int> expectedResult)
    {
        var search = new Search( DataSetGenerator.MakeExampleDataSet());
        
        var expected = search.SearchKey(key);
        
        expected.OrderBy(x=>x).SequenceEqual(expectedResult).Should().BeTrue();
    }
}