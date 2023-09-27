using System;

namespace FramkeMod4MovieLibrary.Services;


public class MainService : IMainService
{
    string fileName = "movies.csv";
    private readonly IFileService _fileService;
    public MainService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public void Invoke()
    {
        string choice;
        do
        {
            Console.WriteLine("1) Add Movie");
            Console.WriteLine("2) Display All Movies");
            Console.WriteLine("X) Quit");
            choice = Console.ReadLine().ToUpper(); // in case user enters x

          
            if (choice == "1")
            {
                _fileService.Write(fileName);
            }
            else if (choice == "2")
            {
                _fileService.Read(fileName);
            }
        }
        while (choice != "X");
    }
}
