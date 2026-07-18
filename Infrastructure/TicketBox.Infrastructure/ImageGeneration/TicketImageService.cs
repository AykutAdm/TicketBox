using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Interfaces.Services;
using TicketBox.Application.Models;

namespace TicketBox.Infrastructure.ImageGeneration
{
    public class TicketImageService : ITicketImageService
    {
        public byte[] GenerateTicketImage(TicketImageData data)
        {
            using var bitmap = new SKBitmap(700, 320);
            using var canvas = new SKCanvas(bitmap);

            canvas.Clear(SKColors.Transparent);

            var cardRect = new SKRoundRect(new SKRect(0, 0, 700, 320), 24);
            using var cardPaint = new SKPaint { Color = new SKColor(21, 21, 31), IsAntialias = true };
            canvas.DrawRoundRect(cardRect, cardPaint);

            canvas.Save();
            canvas.ClipRoundRect(cardRect, antialias: true);

            // ---- Sol şerit (yeşil geçiş) ----
            using var accentPaint = new SKPaint { IsAntialias = true };
            accentPaint.Shader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0), new SKPoint(0, 320),
                new[] { new SKColor(110, 231, 183), new SKColor(5, 150, 105) },
                null, SKShaderTileMode.Clamp);
            canvas.DrawRect(new SKRect(0, 0, 90, 320), accentPaint);

            canvas.Save();
            canvas.Translate(60, 280);
            canvas.RotateDegrees(-90);
            using var sideFont = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold), 15);
            using var sidePaint = new SKPaint { Color = SKColors.White.WithAlpha(210), IsAntialias = true };
            canvas.DrawText("TICKETBOX", 0, 0, sideFont, sidePaint);
            canvas.Restore();

            // ---- Etkinlik adı ----
            using var titleFont = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold), 28);
            using var titlePaint = new SKPaint { Color = SKColors.White, IsAntialias = true };
            canvas.DrawText(data.EventTitle, 130, 70, titleFont, titlePaint);

            // ---- Tarih / lokasyon ----
            using var metaFont = new SKFont(SKTypeface.FromFamilyName("Arial"), 15);
            using var mutedPaint = new SKPaint { Color = new SKColor(165, 169, 190), IsAntialias = true };
            canvas.DrawText(data.EventDate, 130, 100, metaFont, mutedPaint);
            canvas.DrawText(data.Location, 130, 122, metaFont, mutedPaint);

            // ---- Kesikli yırtım çizgisi ----
            using var dashPaint = new SKPaint
            {
                Color = new SKColor(55, 55, 72),
                StrokeWidth = 2,
                IsAntialias = true,
                PathEffect = SKPathEffect.CreateDash(new float[] { 8, 6 }, 0)
            };
            canvas.DrawLine(130, 190, 660, 190, dashPaint);

            // ---- PNR ----
            using var pnrFont = new SKFont(SKTypeface.FromFamilyName("Consolas", SKFontStyle.Bold), 32);
            using var pnrPaint = new SKPaint { Color = new SKColor(240, 180, 41), IsAntialias = true };
            canvas.DrawText(data.Pnr, 130, 240, pnrFont, pnrPaint);

            // ---- Kullanıcı adı ----
            using var nameFont = new SKFont(SKTypeface.FromFamilyName("Arial"), 15);
            using var namePaint = new SKPaint { Color = new SKColor(165, 169, 190), IsAntialias = true };
            canvas.DrawText(data.UserName, 130, 275, nameFont, namePaint);

            canvas.Restore();

            using var image = SKImage.FromBitmap(bitmap);
            using var skData = image.Encode(SKEncodedImageFormat.Png, 100);

            return skData.ToArray();
        }

        public string SaveTicketImage(TicketImageData data, string webRootPath)
        {
            var bytes = GenerateTicketImage(data);
            var folder = Path.Combine(webRootPath, "tickets");
            Directory.CreateDirectory(folder);
            var fileName = $"{data.Pnr}.png";
            var fullPath = Path.Combine(folder, fileName);
            File.WriteAllBytes(fullPath, bytes);
            return $"/tickets/{fileName}";
        }
    }
}
