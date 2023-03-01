namespace SampleLibrary.Test;

public class FileHandlerTest
{
    [Theory]
    [InlineData(".//TestFolder", ".//TestFolder\\.txt", ".//TestFolder\\a.txt")]
    public void GetFileName_Test(string pathToFile, string firstFileName, string secondFileName)
    {
        var fileHandler = new FileHandler();
        fileHandler.LoadFile(pathToFile);
        

        Assert.True(firstFileName==fileHandler.GetFileName(0)||secondFileName==fileHandler.GetFileName(0));
        Assert.True(secondFileName==fileHandler.GetFileName(1)||firstFileName==fileHandler.GetFileName(1));

    }

    [Theory]
    [InlineData(".//TestFolder", new string[] { ".//TestFolder\\.txt", ".//TestFolder\\a.txt" })]
    public void LoadFiles_GetFileList_Test(string pathToFile, string[] listOfFiles)
    {
        var fileHandler = new FileHandler();
        fileHandler.LoadFile(pathToFile);
        
        
        Assert.True(listOfFiles[0]==fileHandler.GetFileName(0)||listOfFiles[0]==fileHandler.GetFileName(1));
        Assert.True(listOfFiles[1]==fileHandler.GetFileName(0)||listOfFiles[1]==fileHandler.GetFileName(1))
        
    }

}