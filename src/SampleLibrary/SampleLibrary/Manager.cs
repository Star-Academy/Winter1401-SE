using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Manager
{
    private readonly IOutPut _outPut;
    private readonly IInput _input;
    private readonly IDataSetMaker _dataSetMaker;

    public Manager(IOutPut outPut, IInput input, IDataSetMaker dataSetMaker)
    {
        _outPut = outPut;
        _input = input;
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
        PrintMessage(_outPut, "Hi\nEnter Directory Path:");

        return InputPath(_input)!;
    }

    private void LoadDirectory(string pathToFile)
    {
        _dataSetMaker.LoadDirectory(pathToFile);
    }

    private string? GetInputWord()
    {
        PrintMessage(_outPut, "Enter input word:");

       return InputPath(_input);
    }

    private void PrintFinalResult(string? key, IReadAble dataSet)
    {
        foreach (var item in new Search(dataSet).SearchKey(key!))
        {
            PrintMessage(_outPut, item.ToString());
        }
    }

    private void PrintMessage(IOutPut output, string outPutMessage)
    {
        output.OutPut(outPutMessage);
    }

    private string? InputPath(IInput input)
    {
        return input.Read();
    }
}