using Iveonik.Stemmers;
using SampleLibrary.Interfaces;

namespace SampleLibrary;

public class Indexer: IDataSetMaker
{
    private readonly IFileAble _fileAble;
    private readonly IWriteAble _dataSet;
    private readonly IStemmer _stemmer;

    public Indexer(IFileAble fileAble, IWriteAble dataSet, IStemmer stemmer)
    {
        _fileAble = fileAble;
        _dataSet = dataSet;
        _stemmer = stemmer;
    }
    
    public DataSet MakeDataSet()
    {
        HandleEmptyDirectory();
        for (var i = 0; i < _fileAble.GetListOfFiles().Length; i++)
        {
            var currentFileTerms = File.ReadAllText(_fileAble.GetFileName(i)).Split(" ");
            if(currentFileTerms.Length==0)
                continue;
            AddToDataSet(i, currentFileTerms);
        }
        return (DataSet)_dataSet;
    }

    private void HandleEmptyDirectory()
    {
        if(_fileAble.GetListOfFiles().Length==0)
            throw new Exception("Entered Directory is Empty!!");
    }

    public void LoadDirectory(string pathToDirectory)
    {
        _fileAble.LoadFile(pathToDirectory);
    }

    private void AddToDataSet(int index, string[] listOfTerms)
    {
        foreach (var term in listOfTerms)
        {
            var modifiedTerm = _stemmer.Stem(term);
            modifiedTerm = modifiedTerm.ToUpper();
            _dataSet.Write(modifiedTerm, index);
        }
    }
}