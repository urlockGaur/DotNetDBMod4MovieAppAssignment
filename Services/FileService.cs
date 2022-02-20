using System;

namespace ApplicationTemplate.Services;

/// <summary>
///     This concrete service and method only exists an example.
///     It can either be copied and modified, or deleted.
/// </summary>
public class FileService : IFileService
{
    public void Read()
    {
        Console.WriteLine("*** I am reading");
    }

    public void Write()
    {
        Console.WriteLine("*** I am writing");
    }
}
