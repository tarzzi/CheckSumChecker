using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
namespace CheckSumChecker
{
    /// <summary>
    /// Checksum checker for text files using MD5 and SHA256
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            bool reset = true;
            do
            {
                Console.Clear();
                Console.WriteLine("CHECKSUM CHECKER\nSelect function:\n1. Text\n2. File\n0. Quit");
                char cin = Console.ReadKey().KeyChar;
                switch (cin)
                {
                    case '1':
                        CheckStr();
                        break;
                    case '2':
                        CheckFile();
                        break;
                    case '0':
                        reset = false;
                        break;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }
            } while (reset);
        }
        private static void CheckStr()
        {
            string reset = "yes";
            while (reset == "yes" || reset == "y")
            {
                Console.Clear();
                string text = "";
                string toMD5 = "";
                string toSHA = "";
                Console.WriteLine("**CHECK TEXT CHECKSUM**\nWrite the text:");
                text = Console.ReadLine();
                if (text.Length > 1)
                {
                    try
                    {
                        toMD5 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(text))).Replace("-", "").ToLowerInvariant();
                        toSHA = BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(text))).Replace("-", "").ToLowerInvariant();
                        Console.WriteLine($"MD5 Checksum: {toMD5}");
                        Console.WriteLine($"SHA256 Checksum: {toSHA}\n\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}\n\n");
                    }                    
                }
                else {
                    Console.WriteLine("Input too short");
                }
                Console.WriteLine("For a new text, type yes");
                reset = Console.ReadLine();
                reset = reset.ToLower();
            }
        }
        private static void CheckFile() {
            string reset = "yes";
            while (reset == "yes" || reset == "y")
            {
                Console.Clear();
                string pathname = "";
                string toMD5 = "";
                string toSHA = "";
                Console.WriteLine("**CHECK FILE CHECKSUM**\nGive pathname of file:");
                pathname = Console.ReadLine();
                try
                {
                    toMD5 = FileMd5(pathname);
                    toSHA = FileSha256(pathname);
                    Console.WriteLine($"MD5 Checksum: {toMD5}");
                    Console.WriteLine($"SHA256 Checksum: {toSHA}\n\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}\n\n");
                }
                Console.WriteLine("For a new file, type yes");
                reset = Console.ReadLine();
                reset = reset.ToLower();
            }
        }
        private static string FileMd5(string pathname)
        {
                using (var md5Instance = MD5.Create())
                {
                    using (var stream = File.OpenRead(pathname))
                    {
                        var hashResult = md5Instance.ComputeHash(stream);
                        return BitConverter.ToString(hashResult).Replace("-", "").ToLowerInvariant();
                    }
                }
        }
        private static string FileSha256(string pathname)
        {
                using (var shaInstance = SHA256.Create())
                {
                    using (var stream = File.OpenRead(pathname))
                    {
                        var hashResult = shaInstance.ComputeHash(stream);
                        return BitConverter.ToString(hashResult).Replace("-", "").ToLowerInvariant();
                    }
                }
        }
    }   
}

