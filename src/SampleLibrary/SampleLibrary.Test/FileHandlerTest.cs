using System;
using System.IO;
using System.Linq;

namespace SampleLibrary.Test;

public class FileHandlerTest
{
    [Theory]
    [InlineData(".//TestFolder", "a.txt", "b.txt")]
    public void GetFileName_Test(string pathToFile, string firstFileName, string secondFileName)
    {

        var str1 = pathToFile+$"{Path.DirectorySeparatorChar}"+firstFileName;
        var str2 = pathToFile+$"{Path.DirectorySeparatorChar}"+secondFileName;
        var fileHandler = new FileHandler();
        fileHandler.LoadFile(pathToFile);

        Assert.True(fileHandler.GetListOfFiles().Contains(str1)&&fileHandler.GetListOfFiles().Contains(str2));

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
        
        
        Assert.Equal(listOfFiles.Order(), fileHandler.GetListOfFiles().Order());
    }

}