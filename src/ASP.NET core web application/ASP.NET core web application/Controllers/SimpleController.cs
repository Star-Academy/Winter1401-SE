using Iveonik.Stemmers;
using SampleLibrary;
using Console = SampleLibrary.Console;

namespace ASP.NET_core_web_application.Controllers;


using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]/[Action]")]
public class SimpleController : ControllerBase
{
    [HttpGet]
    public List<int> Get()
    {
        return new Manager(new Console(), new Console(), new Indexer(new FileHandler(), new DataSet(), new EnglishStemmer())).Run().ToList();
    }
}