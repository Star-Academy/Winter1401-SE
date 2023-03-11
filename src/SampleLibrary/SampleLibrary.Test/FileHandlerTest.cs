using System.IO;
using FluentAssertions;

namespace SampleLibrary.Test;

public class FileHandlerTest
{
    [Theory]
    [InlineData(".//TestFolder", "a.txt", "b.txt")]
    public void GetFileName_Test(string pathToFile, string firstFileName, string secondFileName)
    {

        firstFileName = pathToFile+$"{Path.DirectorySeparatorChar}"+firstFileName;
        secondFileName = pathToFile+$"{Path.DirectorySeparatorChar}"+secondFileName;
        var fileHandler = new FileHandler();
        fileHandler.LoadFile(pathToFile);
        
        var actualFirstFileName = fileHandler.GetFileName(0);
        var actualSecondFileName = fileHandler.GetFileName(1);

        actualFirstFileName.Equals(firstFileName).Should().Be(true);
        actualSecondFileName.Equals(secondFileName).Should().Be(true);
    }

    [Theory]
    [InlineData(".//TestFolder", new string[] { "a.txt", "b.txt" })]
    public void LoadFiles_GetFileList_Test(string pathToFile, string[] listOfFiles)
    {
        for (var i=0; i<listOfFiles.Length; i++)
        {
            listOfFiles[i] = pathToFile + $"{Path.DirectorySeparatorChar}" + listOfFiles[i];
        }
        var fileHandler = new FileHandler();
        
        fileHandler.LoadFile(pathToFile);
        
        Assert.Equal(listOfFiles, fileHandler.GetListOfFiles());
    }

}