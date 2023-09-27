using System.Collections.Generic;

namespace FramkeMod4MovieLibrary.Services;

public interface IFileService
{
    void Read(string fileName);
    void Write(string fileName);

   
}
