using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ApplicationTemplate.Services;

/// <summary>
///     This concrete service and method only exists an example.
///     It can either be copied and modified, or deleted.
/// </summary>
public class FileService : IFileService
{
    private readonly ILogger<IFileService> _logger;

    private static int GetNextMovieID(List<string> movieData)
    {
        int maxMovieID = 0;

        foreach (string line in movieData.Skip(1))
        {
            string[] lines = line.Split(','); // Assuming movies are separated by commas
            if (lines.Length >= 1)
            {
                int movieID;
                if (int.TryParse(lines[0], out movieID))
                {
                    maxMovieID = Math.Max(maxMovieID, movieID);
                }
            }
        }

        return maxMovieID;
    }

    public FileService(ILogger<IFileService> logger)
    {
        _logger = logger;
    }
    public void Read(string fileName)
    {
        _logger.Log(LogLevel.Information, "Reading");
        Console.WriteLine("*** I am reading");
    }

    public void Write(string fileName)
    {

        var header = ("MovieID, Title, Genres");


        List<string> movieData = File.ReadAllLines(fileName).ToList();

        int maxMovieID = GetNextMovieID(movieData);

        maxMovieID++;

        //new stream
        StreamWriter sw = new StreamWriter(fileName, true);

        //writing to file
        sw.WriteLine(header);

        Console.WriteLine("Please Enter the Movie Title");
        var movieTitle = Console.ReadLine();

        Console.WriteLine("Enter the Movie Genre(s)");
        
        int maxGenres = 5;
        string[] movieGenreArray = new string[maxGenres];
        int currentIndex = 0;

        while (true)
        {
            Console.WriteLine("Enter a genre (or type 'exit') : ");
            string movieGenre = Console.ReadLine();

            if (movieGenre.ToLower() == "exit")
            {
                break;
            }
            if (currentIndex < maxGenres) 
            {
                movieGenreArray[currentIndex] = movieGenre;
                currentIndex++;
            }
            else
            {
                Console.WriteLine("The max number of genres have been added.");
                break;
            }
        }

        string movieGenres = string.Join("|", movieGenreArray.Take(currentIndex));

        string newMovie = ($"{maxMovieID}, {movieTitle}, {movieGenres}");

        sw.WriteLine(newMovie);

        sw.Close();

        _logger.Log(LogLevel.Information, "Writing\n");
        Console.WriteLine("New data saved successfully!\n");





    }
}
