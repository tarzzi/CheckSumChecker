using System;
using System.IO;
using System.Security.Cryptography;

namespace CheckSumChecker
{
    class Program
    {
        static void Main(string[] args)
        {
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
                    toMD5 = Md5(pathname);
                    toSHA = Sha256(pathname);
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
        private static string Md5(string pathname)
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
        private static string Sha256(string pathname)
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

