using SampleLibrary;

namespace ASP.NET_core_web_application.Controllers;


using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]/[Action]")]
public class SimpleController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Hello world!";
        new Manager(new ConsoleInput(), new ConsoleInput(), new Indexer(new FileHandler()), new FileHandler()).Run();
    }
}