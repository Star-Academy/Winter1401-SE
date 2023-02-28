using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Manager
{
    private readonly IOutPut _outPut;
    private readonly IInput _input;
    private readonly IDataSetMaker _dataSetMaker;
    private readonly IFileAble _fileAble;
    
    public Manager(IOutPut outPut, IInput input, IDataSetMaker dataSetMaker, IFileAble fileAble)
    {
        _outPut = outPut;
        _input = input;
        _dataSetMaker = dataSetMaker;
        _fileAble = fileAble;
    }

    public void Run()
    {
        GetFolder();

        var dataSet = MakeDataset();

        var key = GetInputWord();

        PrintFinalResult(key, dataSet);
    }

    private DataSet MakeDataset()
    {
        
        return _dataSetMaker.MakeDataSet(new DataSet());
    }

    private void GetFolder()
    {
        PrintMessage(_outPut, "Hi\nEnter Directory Path:");

        var pathToFile = InputPath(_input);
        _fileAble.LoadFile(pathToFile!);
    }

    private string? GetInputWord()
    {
        PrintMessage(_outPut, "Enter input word:");

        var key = InputPath(_input);
        return key;
    }

    private void PrintFinalResult(string? key, DataSet dataSet)
    {
        foreach (var item in new Search().SearchKey(key!, dataSet))
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