using System;
using System.IO;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using WebApp.AdapterPattern.Services.Interfaces;

namespace WebApp.AdapterPattern.Services.Concretes;

public class ImageProcess : IImageProcess {
    private const float WatermarkPadding = 18f;
    private const string WatermarkFont = "Roboto";
    private const float WatermarkFontSize = 64f;
    public void AddWaterMark(string text, string fileName, Stream imageStream)
    {
        #region Lesson Example

        //// using var img = Image.FromStream(imageStream);
        ////
        ////using var graphic = Graphics.FromImage(img);
        ////
        ////var font = new Font(FontFamily.GenericMonospace, 40, FontStyle.Bold, GraphicsUnit.Pixel);
        ////
        ////var textSize = graphic.MeasureString(text, font);
        ////
        ////var color = Color.FromArgb(128, 255, 255, 255);
        ////var brush = new SolidBrush(color);
        ////
        ////var position = new Point(img.Width - ((int)textSize.Width + 30), img.Height - ((int)textSize.Height + 30));
        ////
        ////graphic.DrawString(text, font, brush, position);
        ////
        ////img.Save("wwwroot/watermarks/" + fileName);
        ////
        ////img.Dispose();
        ////graphic.Dispose();

        #endregion

        var image = Image.Load(imageStream);

        if (!SystemFonts.TryGet(WatermarkFont, out var fontFamily)){
            throw new ArgumentNullException($"Couldn't find font {WatermarkFont}");
        }


        var font = fontFamily.CreateFont(WatermarkFontSize, FontStyle.Regular);

        var options = new TextOptions(font)
        {
            Dpi = 72,
            KerningMode = KerningMode.Standard
        };

        var rect = TextMeasurer.MeasureAdvance(text, options);

        image.Mutate(x => x.DrawText(text,
        font,
        new Color(Rgba32.ParseHex("#FFFFFFEE")),
        new PointF(image.Width - rect.Width - WatermarkPadding, image.Height - rect.Height - WatermarkPadding)));

        image.SaveAsJpeg("wwwroot/watermarks/" + fileName);
    }
}
