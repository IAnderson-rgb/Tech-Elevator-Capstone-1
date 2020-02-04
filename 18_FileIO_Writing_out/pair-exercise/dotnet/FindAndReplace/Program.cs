using System;
using System.IO;

namespace FindAndReplace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the phrase you would like to replace");
            string targetPhrase = Console.ReadLine();

            Console.WriteLine("Please enter the phrase you would like it to be replaced with");
            string replacePhrase = Console.ReadLine();


            Console.WriteLine("What is the fully qualified name of the file that should be searched?");
            string pathToBeRead = Console.ReadLine();

            Console.WriteLine("Please enter the path destination for your new file?");
            string newFilePath = Console.ReadLine();

            try
            {
                using (StreamReader sr = new StreamReader(pathToBeRead))
                {
                    using (StreamWriter sw = new StreamWriter(newFilePath, true))
                    {
                        while (!sr.EndOfStream)
                        {

                           // string changedLine = "";
                            string line = sr.ReadLine();
                            if (line.Contains(targetPhrase))
                            {
                                line = line.Replace(targetPhrase, replacePhrase);

                            }
                            sw.WriteLine(line);


                        }
                    }
                }
            }
            catch(Exception e)
            {
               Console.WriteLine("Something went wrong");
                Console.WriteLine(e.Message);

            }
        }
    }
}
