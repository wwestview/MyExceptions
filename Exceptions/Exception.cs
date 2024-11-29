using System;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Net;
using System.Drawing.Imaging;
class Exceptions
{
    static void FileProcessing()
    {
        StreamReader sr;
        StreamWriter sw;
        double sum = 0;
        int count = 0;
        var n = new Random();
        var m = new Random();
        File.Create("no_file.txt").Close();
        File.Create("overflow.txt").Close();
        File.Create("bad_data.txt").Close();
        // 10 - bad_data; 15 - bad_data; 16 - bad_data; 19 - no_file; 29 - overflow;

        for (int i = 10; i <= 29; i++)
        {
           /* using (sw = new($"{i}.txt"))
            {
                sw.WriteLine(n.Next(1,50));
                sw.WriteLine(m.Next(1,50));
            }
           */
               
            try
            {
                sr = new($"{i}.txt");
                int a = int.Parse(sr.ReadLine());
                int b = int.Parse(sr.ReadLine());
                checked
                {
                    int ab = a * b;
                    sum += ab;
                    count++;
                }
            }
            catch (OverflowException)
            {
                sw = File.AppendText("overflow.txt");
                sw.WriteLine(i);
                sw.Close();
            }
            catch
            {
                sw = File.AppendText("no_file.txt");
                sw.WriteLine(i);
                sw.Close();
            }
            

        }


        try
        {
            double avg = sum/ count;
            Console.WriteLine(avg);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }


    }
    public static void Pictures()
    {
        
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            Bitmap image;
            Regex searchPattern = new Regex("^((bmp)|(gif)|(tiff?)|(jpe?g)|(png))$", RegexOptions.IgnoreCase);
            foreach (var file in files)
            {
                try
                {
                    image = new Bitmap(file);
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    image.Save($"{Path.GetFileNameWithoutExtension(file)}-mirrored.gif",ImageFormat.Gif);
                }
                catch
                {
                    if (searchPattern.IsMatch(Path.GetExtension(file)))
                    {
                        Console.WriteLine($"Image {file} can not read..");
                    }

                }
            }
        
        
    }
    static void Main()
    {
       // FileProcessing();
        Console.WriteLine("The attempt was a success");
        Pictures();
    }
}