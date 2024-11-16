using System;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Drawing;
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

        for (int i = 10; i <= 29; i++)
        {
            using (sw = new($"{i}.txt"))
            {
                sw.WriteLine(n.Next(1,50));
                sw.WriteLine(m.Next(1,50));
            }
           

            try
            {
                sr = new($"{i}.txt");
                try
                {
                    int a = int.Parse(sr.ReadLine());
                    int b = int.Parse(sr.ReadLine());
                    try
                    {
                        checked
                        {
                            int ab = a * b;
                            sum += ab;
                            count++;
                        }
                    }
                    catch(OverflowException)
                    {
                        sw = File.AppendText("overflow.txt");
                        sw.WriteLine(i);
                        sw.Close();
                    }
                }
                catch
                {
                    sw = File.AppendText("bad_data.txt");
                    sw.WriteLine(i);
                    sw.Close();
                }
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
        static void Pictures()
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
                    image.Save($"{Path.GetFileName(file)}-mirrored.gif");
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
    }
    static void Main()
    {
        FileProcessing();
    }
}