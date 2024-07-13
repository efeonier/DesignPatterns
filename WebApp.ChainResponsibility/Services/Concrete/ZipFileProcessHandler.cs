using System.IO;
using System.IO.Compression;
using WebbApp.ChainResponsibility.Services.Abstract;

namespace WebbApp.ChainResponsibility.Services.Concrete;

public class ZipFileProcessHandler<T> : ProcessHandler
{
    public override object Handle(object o)
    {
        if (o is not MemoryStream excelMs)
            return base.Handle(o);

        excelMs.Position = 0;
        using var zipStream = new MemoryStream();
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true);
        var zipfile = archive.CreateEntry($"{typeof(T).Name}.xlsx");
        using var zipEntry = zipfile.Open();
        excelMs.CopyTo(zipEntry);
        return base.Handle(zipStream);
    }
}
