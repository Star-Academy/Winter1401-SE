// See https://aka.ms/new-console-template for more information

using SampleLibrary;

var fileHandler = new FileHandler();
new Manager(new ConsoleInput(), new ConsoleInput(), new Indexer(fileHandler),fileHandler).Run();
Console.WriteLine("Hello, World!");