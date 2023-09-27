using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace FramkeMod4MovieLibrary.Services;

// reading and writing to our movie file
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
        if (File.Exists(fileName))
        {
            _logger.Log(LogLevel.Information, "Reading");
            Console.WriteLine("*** I am reading");

            StreamReader sr = new StreamReader(fileName);

            sr.ReadLine(); // Skips header, not sure if required at this time

            int entryCount = 0;
            while (sr.EndOfStream != true)
            {
                var movieEntry = sr.ReadLine();
                var movieArray = movieEntry.Split(',');
                Console.WriteLine($"{movieArray[0]}, {movieArray[1]}, {movieArray[2]}");

                entryCount++;

                if (entryCount >= 10)
                {
                    Console.Write("Press Enter to continue or 'q' to quit: ");
                    var userInput = Console.ReadLine();
                    if (userInput.ToLower() == "q")
                    {
                        break; // Exit the loop if the user wants to quit
                    }
                    entryCount = 0; // Reset the counter if the user wants to continue
                }
            }
        }
        else
        {
            _logger.LogError("File does not exist: {File}", fileName);
        }
    }

    public void Write(string fileName)
    {

        var header = ("MovieID, Title, Genres");


        List<string> movieData = File.ReadAllLines(fileName).ToList();

        int maxMovieID = GetNextMovieID(movieData);

        maxMovieID++;



        //writing to file
        while (true)
        {
            Console.WriteLine("Please Enter the Movie Title (or type 'exit') :");
        var movieTitle = Console.ReadLine();

        if (movieTitle == "exit")
        {
            break;
        }

        bool isDuplicate = movieData.Any(line => line.Split(',')[1].Trim().Equals(movieTitle, StringComparison.OrdinalIgnoreCase));

            if (isDuplicate)
            {
                Console.WriteLine("That movie has already been entered.");
                _logger.LogError("Duplicate movie title was entered");
            }
            else
            {
                Console.WriteLine("Enter the Movie Genre(s): ");

                int maxGenres = 5;
                string[] movieGenreArray = new string[maxGenres];
                int currentIndex = 0;


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

                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(newMovie);
                }

                _logger.Log(LogLevel.Information, "Writing\n");
                Console.WriteLine("New data saved successfully!\n");

            }

        }
    }
}
