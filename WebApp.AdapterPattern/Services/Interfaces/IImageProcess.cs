using System.IO;

namespace WebApp.AdapterPattern.Services.Interfaces;

public interface IImageProcess {
    void AddWaterMark(string text, string fileName, Stream imageStream);
}
