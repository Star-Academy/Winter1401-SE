// namespace SampleLibrary.Test;
//
// public class FileHandlerTest
// {
//     [Theory]
//     [InlineData(".//TestFolder", ".//TestFolder\\.txt", ".//TestFolder\\a.txt")]
//     public void GetFileName_Test(string pathToFile, string firstFileName, string secondFileName)
//     {
//         var fileHandler = new FileHandler();
//         fileHandler.LoadFile(pathToFile);
//
//         Assert.Equal(firstFileName, fileHandler.GetFileName(0));
//         Assert.Equal(secondFileName, fileHandler.GetFileName(1));
//
//     }
//
//     [Theory]
//     [InlineData(".//TestFolder", new string[] { ".//TestFolder\\.txt", ".//TestFolder\\a.txt" })]
//     public void LoadFiles_GetFileList_Test(string pathToFile, string[] listOfFiles)
//     {
//         var fileHandler = new FileHandler();
//         fileHandler.LoadFile(pathToFile);
//         
//         
//         Assert.Equal(listOfFiles, fileHandler.GetListOfFiles());
//     }
//
// }