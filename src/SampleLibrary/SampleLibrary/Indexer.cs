using Iveonik.Stemmers;
using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Indexer: IDataSetMaker
{
    private IFileAble _fileAble;

    public Indexer(IFileAble fileAble)
    {
        _fileAble = fileAble;
    }

    public DataSet MakeDataSet(IWriteAble dataSet)
    {
        var isEmpty = true;
        dataSet.SetNumberOfFiles(_fileAble.GetListOfFiles().Length);
        for (var i = 0; i < _fileAble.GetListOfFiles().Length; i++)
        {
            isEmpty = false;
            var currentFileTerms = File.ReadAllText(_fileAble.GetFileName(i)).Split(" ");
            if(currentFileTerms.Length==0)
                continue;
            AddToDataSet(i, dataSet, currentFileTerms);
        }
        if (isEmpty)
            HandleEmptyFolder();
        var result = (DataSet)dataSet;
        return result;
    }

    private void HandleEmptyFolder()
    {
        var output = new ConsoleInput();
        output.OutPut("Entered Directory is Empty!!");
        System.Environment.Exit(0);
    }

    private void AddToDataSet(int index, IWriteAble dataSet, string[] listOfTerms)
    {
        var englishStemmer = new EnglishStemmer();
        foreach (var term in listOfTerms)
        {
            var modifiedTerm = englishStemmer.Stem(term);
            modifiedTerm = modifiedTerm.ToUpper();
            dataSet.Write(modifiedTerm, index);
        }
    }
}