/// <summary>
/// exe             : Dominoes_Pieces_Generator.exe
/// Description     : Generate test files for Backtracking_Domino
/// Usage           : In command line, run "Dominoes_Pieces_Generator.exe X Y", where X is the number of pieces to be generated and Y-1 is the max value each side of pieces can reach.
/// Output          : A file with the number of pieces in the first line and the pieces randomly generated. 
/// Author          : Bruno Bragança Mendes <bbmendes@gmail.com>
/// Date            : Mon, 2017 Jun 26, 18:08:26 BRT
/// Version         : 1.1
/// </summary>
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Dominoes_Pieces_Generator.Src
{
    class Generator
    {
        static void Main(string[] args)
        {
            try
            {
                int numberOfPieces = Convert.ToInt32(args[0]);
                int numberOfPiecesCopy = numberOfPieces;
                int maxValueOfPieces = Convert.ToInt32(args[1]);
                double maxPieces = Math.Pow(maxValueOfPieces, 2);
                Dictionary<Int32, String> pieces = new Dictionary<int, string>();
                Random rnd = new Random();
                Stopwatch swatch = new Stopwatch();

                if(maxPieces < numberOfPieces)
                {
                    throw new ArgumentException("Impossible create file. Max number of combination values for pieces has been reached.");
                }

                swatch.Start();
                while (numberOfPiecesCopy > 0)
                {
                    // Sugestão do CAVA
                    //String line;
                    //do
                    //{
                    //    line = String.Format("{0} {1}", rnd.Next(0, maxValueOfPieces), rnd.Next(0, maxValueOfPieces));
                    //}
                    //while (pieces.ContainsValue(line.ToString()));

                    //pieces.Add(numberOfPiecesCopy, line);
                    //numberOfPiecesCopy--;

                    String line = String.Format("{0} {1}", rnd.Next(0, maxValueOfPieces), rnd.Next(0, maxValueOfPieces));
                    if (pieces.ContainsValue(line.ToString()))
                    {
                        Boolean hasAdded = false;
                        while (!hasAdded)
                        {
                            line = String.Format("{0} {1}", rnd.Next(0, maxValueOfPieces), rnd.Next(0, maxValueOfPieces));
                            if (!(pieces.ContainsValue(line.ToString())))
                            {
                                pieces.Add(numberOfPiecesCopy, line);
                                hasAdded = true;
                                numberOfPiecesCopy--;
                            }
                        }
                    }
                    else
                    {
                        pieces.Add(numberOfPiecesCopy, line);
                        numberOfPiecesCopy--;
                    }
                }
                swatch.Stop();

                Console.WriteLine("#################################################################");
                Console.WriteLine("Metrics:");
                Console.WriteLine("Elapsed time to generate {0} pieces: \t {1}", numberOfPieces, swatch.Elapsed.ToString());
                Console.WriteLine("#################################################################\n\n");
                swatch.Reset();

                swatch.Start();
                int numberPart = 1;
                String caseName = String.Format("CASE_{0}.txt", numberPart.ToString().PadLeft(3, '0'));

                if (File.Exists(caseName))
                {
                    Boolean hasFile = true;
                    while (hasFile)
                    {
                        numberPart++;
                        caseName = String.Format("CASE_{0}.txt", numberPart.ToString().PadLeft(3, '0'));
                        if (!File.Exists(caseName))
                        {
                            using (StreamWriter sw = File.AppendText(caseName))
                            {
                                sw.WriteLine(numberOfPieces);
                                foreach (KeyValuePair<Int32, String> piece in pieces)
                                {
                                    sw.WriteLine(piece.Value);
                                }
                            }
                            hasFile = false;
                        }
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(caseName))
                    {
                        sw.WriteLine(numberOfPieces);
                        foreach (KeyValuePair<Int32, String> piece in pieces)
                        {
                            sw.WriteLine(piece.Value);
                        }
                    }
                }
                swatch.Stop();

                Console.WriteLine(String.Format("\nThe file has been created: {0}\n\n\n", caseName));

                Console.WriteLine("#################################################################");
                Console.WriteLine("Metrics:");
                Console.WriteLine("Elapsed time to generate '{0}' file: \t {1}", caseName, swatch.Elapsed.ToString());
                Console.WriteLine("#################################################################");

                Console.ReadKey();

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
