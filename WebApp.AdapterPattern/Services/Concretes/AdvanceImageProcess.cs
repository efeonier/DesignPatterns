using System.Drawing;
using System.IO;
using LazZiya.ImageResize;
using WebApp.AdapterPattern.Services.Interfaces;

namespace WebApp.AdapterPattern.Services.Concretes;

public class AdvanceImageProcess : IAdvanceImageProcess {
    public void AddWatermarkImage(Stream stream, string text, string filePath, Color color, Color outLineColor)
    {
        
        ////MacOS not working
        using var img = Image.FromStream(stream);
        var tOps = new TextWatermarkOptions
        {
            // Change text color and opacity
            // Text opacity range depends on Color's alpha channel (0 - 255)
            TextColor = color,

            // Add text outline
            // Outline color opacity range depends on Color's alpha channel (0 - 255)
            OutlineColor = outLineColor
        };

        img.AddTextWatermark(text, tOps)
            .SaveAs(filePath);
    }
}
