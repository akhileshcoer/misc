using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var guid = Guid.NewGuid();
            Console.WriteLine($"My name: {guid.ToString()} ");

            var retryMaxCount = 6;
            var delayInMilliseconds = 200;
            var retryCount = 0;

            var dateTime = DateTime.Parse(args[0]);
            var sleepTime = dateTime.Subtract(DateTime.Now);

            Console.WriteLine($"waiting for {sleepTime.ToString()}");
            Task.Delay(sleepTime).Wait();

            do
            {
                try
                {
                    Console.WriteLine("Checking key directory exists");

                    var keyDirectory = Path.Combine(Directory.GetCurrentDirectory(), "key");
                    var keylockerFile = Path.Combine(Directory.GetCurrentDirectory(), "key", "keylocker.dat");

                    if (!Directory.Exists(keyDirectory))
                    {
                        Directory.CreateDirectory(keyDirectory);
                    }
                                        
                    Console.WriteLine($"Trying file open/creation at {keylockerFile}");
                    using (FileStream fileStream = new FileStream(keylockerFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                    {
                        Console.WriteLine("file lock attained, enter any key to release file lock.");

                        var msg = $"{DateTime.Now.ToString()} this record is created by {guid.ToString()}{Environment.NewLine}";
                        var msgBytes = Encoding.Unicode.GetBytes(msg);
                        var byteCount = msgBytes.Length;

                        fileStream.Seek(fileStream.Length, SeekOrigin.Begin);
                        fileStream.Write(msgBytes, 0, byteCount);

                        // Generate key file here.....
                        Task.Delay(delayInMilliseconds).Wait();

                        fileStream.Close();

                        Console.WriteLine($"message written in file: '{msg}'");
                        Console.WriteLine("file-lock released");
                    }
                    break;
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine($"IOException is caught");
                    //Console.Error.WriteLine(ioEx.ToString());
                    if (retryCount <= retryMaxCount)
                    {
                        retryCount++;
                        Task.Delay(delayInMilliseconds).Wait();
                        Console.WriteLine($"re-trying -- count#{retryCount}");
                    }
                    else
                    {
                        Console.WriteLine("Retrycount is exhausted.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception");
                    Console.Error.WriteLine(e.ToString());
                    break;
                }
            } while (retryCount < retryMaxCount);

            Console.WriteLine("enter any key to exit");
            Console.Read();
        }
    }
}
