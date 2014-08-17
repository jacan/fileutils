using System;
using System.IO;
using System.Linq;

namespace rnfiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = args.FirstOrDefault();

            if (string.IsNullOrEmpty(directory))
            {
                Console.WriteLine("Please enter a directory");
                return;
            }

            if (!Directory.Exists(directory))
            {
                Console.WriteLine("Directory does not exist");
                return;
            }

            try
            {
                Directory.GetFiles(directory).ToList().ForEach(file =>
                {
                    var randomFileName = Path.Combine(directory, Guid.NewGuid().ToString());
                    randomFileName += new FileInfo(file).Extension;

                    Console.WriteLine("Rename {0} => {1}", file, randomFileName);

                    File.Move(file, randomFileName);
                });
            }
            catch (Exception e)
            {
                while (e != null)
                {
                    Console.WriteLine("Reason:");
                    Console.WriteLine(e.Message);

                    Console.WriteLine("Stacktrace:");
                    Console.WriteLine(e.StackTrace);

                    e = e.InnerException;
                }

                Console.WriteLine("Renaming files, aborted.. Please see the above log..");
            }
        }
    }
}