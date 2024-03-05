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
        const string targetDir = @"C:\Users\focim\OneDrive\Moonlight Printed\For sale";
        const string sourceDir = @"C:\Users\focim\OneDrive\Moonlight Printed\Upscaled";
        int lastImageNumber;

        if (Directory.Exists(targetDir) && Directory.Exists(sourceDir))
        {
            // This path is a directory
            lastImageNumber = FindGreatestName(targetDir);
            CreateAndCopy(targetDir, sourceDir, lastImageNumber);
        }
        else
        {
            Console.WriteLine("{0} is not a valid file or directory.");
        }
    }

    public static void CreateAndCopy(string targetDir, string sourceDir, int lastImageNumber)
    {

        string[] fileEntries = Directory.GetFiles(sourceDir);

        foreach (string fileDir in fileEntries)
        {
            lastImageNumber++;

            string newPath = targetDir + "\\" + lastImageNumber.ToString().PadLeft(3, '0');
            Directory.CreateDirectory(newPath);
            Directory.CreateDirectory(newPath + "\\Mockup");

            newPath = newPath + "\\Pictures";
            Directory.CreateDirectory(newPath);

            try
            {
                File.Copy(fileDir, newPath + "\\Original.png");

                Bitmap image = new Bitmap(newPath + "\\Original.png", true);

                CutImageToProportion(1, 1, image, newPath);
                CutImageToProportion(3, 2, image, newPath);
                CutImageToProportion(4, 3, image, newPath);
                CutImageToProportion(7, 5, image, newPath);
                CutImageToProportion(16, 9, image, newPath);

                Console.WriteLine("{0} folder is done...", lastImageNumber.ToString().PadLeft(3, '0'));
            }
            catch (IOException iox)
            {
                Console.WriteLine("Eror: " + iox.Message);
            }
        }
    }

    /// <summary>
    /// Function that return the last 3 character of a given path converted to int
    /// </summary>
    /// <param name="path">The path name that we want to use</param>
    /// <returns>Return the last 3 character of the path converted to int</returns>
    public static int FindGreatestName(string path)
    {
        string[] subDir = Directory.GetDirectories(path);
        string lastInst = subDir[subDir.Length - 1];
        return int.Parse(lastInst.Substring(lastInst.Length - 3));
    }

    /// <summary>
    /// Method to crop a given image to a given proportion using the max possible size of the
    /// original image.
    /// </summary>
    /// <param name="widthProp">The width of the wanted proportion</param>
    /// <param name="heightProp">The height of the wanted proportion</param>
    /// <param name="imageToCut">The image that need to be cropped. Previously loaded to Bitmap and that object is given here.</param>
    /// <param name="savePath">The folder where we want to save the cropped image</param>
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
        }
    }
}