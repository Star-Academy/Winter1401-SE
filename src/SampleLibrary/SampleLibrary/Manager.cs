using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Manager
{
    private readonly IInputOutput _inputOutput;
    private readonly IDataSetMaker _dataSetMaker;

    public Manager(IInputOutput inputOutput, IDataSetMaker dataSetMaker)
    {
        _inputOutput = inputOutput;
        _dataSetMaker = dataSetMaker;
    }

    public IEnumerable<int> Run(string pathToFolder, string key)
    {
         // var pathToFolder = GetFolderPath();

         LoadDirectory(pathToFolder);

        var dataSet = _dataSetMaker.MakeDataSet();

        // var key = GetInputWord();

        return new Search(dataSet).SearchKey(key!).ToList();
    }

    private string GetFolderPath()
    {
        PrintMessage(_inputOutput, "Hi\nEnter Directory Path:");

        return InputPath(_inputOutput)!;
    }

    private void LoadDirectory(string pathToFile)
    {
        _dataSetMaker.LoadDirectory(pathToFile);
    }

    private string? GetInputWord()
    {
        PrintMessage(_inputOutput, "Enter input word:");

       return InputPath(_inputOutput);
    }

    private void PrintFinalResult(string? key, IReadAble dataSet)
    {
        foreach (var item in new Search(dataSet).SearchKey(key!))
        {
            PrintMessage(_inputOutput, item.ToString());
        }
    }

    private void PrintMessage(IInputOutput output, string outPutMessage)
    {
        output.OutPut(outPutMessage);
    }

    private string? InputPath(IInputOutput input)
    {
        return input.Read();
    }
}