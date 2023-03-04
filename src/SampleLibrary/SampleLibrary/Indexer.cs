using Iveonik.Stemmers;
using SampleLibrary.Interfaces;
using Xunit.Sdk;

namespace SampleLibrary;

public class Indexer: IDataSetMaker
{
    private readonly IFileAble _fileAble;

    public Indexer(IFileAble fileAble)
    {
        _fileAble = fileAble;
    }

    public DataSet MakeDataSet(IWriteAble dataSet)
    {
        var isFolderEmpty = true;
        dataSet.SetNumberOfFiles(_fileAble.GetListOfFiles().Length);
        for (var i = 0; i < _fileAble.GetListOfFiles().Length; i++)
        {
            isFolderEmpty = false;
            var currentFileTerms = File.ReadAllText(_fileAble.GetFileName(i)).Split(" ");
            if(currentFileTerms.Length==0)
                continue;
            AddToDataSet(i, dataSet, currentFileTerms);
        }
        if (isFolderEmpty)
            HandleEmptyFolder();
        var result = (DataSet)dataSet;
        return result;
    }

    private void HandleEmptyFolder()
    {
        var output = new ConsoleInput();
        throw new EmptyException("Entered Directory is Empty!!");
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