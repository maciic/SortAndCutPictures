using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;

public class SortAndCutPictures
{
    public static void Main(string[] args)
    {
        string path = @"C:\Users\focim\OneDrive\Moonlight Printed\Upscaled original\3.png";
        string savePath = @"C:\Users\focim\OneDrive\Moonlight Printed\Upscaled original";
        Bitmap image = new Bitmap(path, true);

        CutImageToProportion(7, 5, image, savePath);
    }

    public static int FindGreatestName(string path)
    {
        string[] subDir = Directory.GetDirectories(path);
        string lastInst = subDir[subDir.Length - 1];
        return int.Parse(lastInst.Substring(lastInst.Length - 3));
    }

    public static void CutImageToProportion(int widthProp, int heightProp, Bitmap imageToCut, string savePath)
    {
        if (imageToCut.Width < imageToCut.Height)
        {
            int newWidth = imageToCut.Width;
            int newHeight = imageToCut.Height;
            int xPos = 0;
            int yPos = 0;

            if (((double)imageToCut.Height / imageToCut.Width) > ((double)widthProp / heightProp))
            {
                newHeight = (imageToCut.Width * widthProp) / heightProp;
                yPos = (imageToCut.Height - newHeight) / 2;
            }
            else
            {
                newWidth = (imageToCut.Height * heightProp) / widthProp;
                xPos = (imageToCut.Width - newWidth) / 2;
            }

            Rectangle cloneRect = new Rectangle(xPos, yPos, newWidth, newHeight);

            System.Drawing.Imaging.PixelFormat format = imageToCut.PixelFormat;
            Bitmap cloneImage = imageToCut.Clone(cloneRect, format);
            cloneImage.Save(savePath + "\\" + heightProp + "x" + widthProp + ".png");
            Console.WriteLine("Process is done...");
        }
        else
        {
            int newWidth = imageToCut.Width;
            int newHeight = imageToCut.Height;
            int xPos = 0;
            int yPos = 0;

            if (((double)imageToCut.Width / imageToCut.Height) > ((double)widthProp / heightProp))
            {
                newWidth = (imageToCut.Height * widthProp) / heightProp;
                xPos = (imageToCut.Width - newWidth) / 2;
            }
            else
            {
                newHeight = (imageToCut.Width * heightProp) / widthProp;
                yPos = (imageToCut.Height - newHeight) / 2;
            }

            Rectangle cloneRect = new Rectangle(xPos, yPos, newWidth, newHeight);

            System.Drawing.Imaging.PixelFormat format = imageToCut.PixelFormat;
            Bitmap cloneImage = imageToCut.Clone(cloneRect, format);
            cloneImage.Save(savePath + "\\" + widthProp + "x" + heightProp + ".png");
            Console.WriteLine("Process is done...");
        }
    }
}