using System;
using System.IO;

namespace SampleLibrary.Test;

public class FileHandlerTest
{
    [Theory]
    [InlineData(".//TestFolder", ".txt", "a.txt")]
    public void GetFileName_Test(string pathToFile, string firstFileName, string secondFileName)
    {

        var str1 = pathToFile+$"{Path.DirectorySeparatorChar}"+firstFileName;
        var str2 = pathToFile+$"{Path.DirectorySeparatorChar}"+secondFileName;
        var fileHandler = new FileHandler();
        fileHandler.LoadFile(pathToFile);

        Assert.Equal(str1, fileHandler.GetFileName(0));
        Assert.Equal(str2, fileHandler.GetFileName(1));

    }

    [Theory]
    [InlineData(".//TestFolder", new string[] { ".txt", "a.txt" })]
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