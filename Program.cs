using System;
using FramkeMod4MovieLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FramkeMod4MovieLibrary;

public class Program
{
  
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        try
        {
            var startup = new Startup();
            var serviceProvider = startup.ConfigureServices();
            var service = serviceProvider.GetService<IMainService>();

            service?.Invoke();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
        }
    }
}
