using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace file_io_part1_exercises_pair
{
    class Program
    {
        public static void Main(string[] args)
        { 
            Console.WriteLine("Enter the filesystem path for a text file to be read.");
            string pathToBeRead = Console.ReadLine();
            int countWords = 0;
            int countSenteces = 0;
            try {
                using (StreamReader sr = new StreamReader(pathToBeRead))
                {
                    while (!sr.EndOfStream)
                    {
                        // Read in the line
                        string line = sr.ReadLine();
                       
                        line = line.Trim();
                        string[] wordArray = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                      
                        countWords += wordArray.Length;
                        foreach(string s in wordArray)
                        {
                            if (s.EndsWith(".") || s.EndsWith("!")|| s.EndsWith("?"))
                            {
                                countSenteces++;
                            }
                        }

                        //string[] sentenceArray = line.Split('.', '!', '?');
                        //countSenteces += sentenceArray.Length;
                        
                        // Print it to the screen
                        //Console.WriteLine(line);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Something went wrong");
            }

            Console.WriteLine("Number of words in text file:");
            Console.WriteLine(countWords);
            Console.WriteLine("Number of sentences in file:");
            Console.WriteLine(countSenteces);
            Console.ReadLine();
            }
  
        }
    }
    

