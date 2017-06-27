/// <summary>
/// exe             : Dominoes_Pieces_Generator.exe
/// Description     : Generate test files for Backtracking_Domino
/// Usage           : In command line, run "Dominoes_Pieces_Generator.exe X Y", where X is the number of pieces to be generated and Y is the max value each side of pieces can reach.
/// Output          : A file with the number of pieces in the first line and the pieces randomly generated. 
/// Author          : Bruno Bragança Mendes <bbmendes@gmail.com>
/// Date            : Mon, 2017 Jun 26, 18:08:26 BRT
/// Version         : 1.0
/// </summary>
/// 
using System;
using System.Collections.Generic;
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
                double maxPieces = Math.Pow(maxValueOfPieces, maxValueOfPieces);
                Boolean preventLoop = false;
                Dictionary<Int32, String> pieces = new Dictionary<int, string>();
                HashSet<String> controlPieces = new HashSet<String>();
                Random rnd = new Random();

                while (numberOfPiecesCopy > 0)
                {
                    String line = String.Format("{0} {1}", rnd.Next(0, maxValueOfPieces), rnd.Next(0, maxValueOfPieces));
                    if (pieces.ContainsValue(line.ToString()))
                    {
                        controlPieces.Add(line);
                        if(controlPieces.Count == maxPieces)
                        {
                            preventLoop = true;
                            break;
                        }
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
                            else
                            {
                                controlPieces.Add(line);
                                numberOfPiecesCopy--;
                                if (controlPieces.Count == maxPieces)
                                {
                                    preventLoop = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        pieces.Add(numberOfPiecesCopy, line);
                        numberOfPiecesCopy--;
                    }
                }

                if(preventLoop == true)
                {
                    Console.WriteLine("Impossible create file. Max number of combination values for pieces has been reached.");
                }
                else
                {
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

                    //File.AppendText(caseName).WriteLine(numberOfPieces);
                    Console.WriteLine(String.Format("\nThe file has been created: {0}", caseName));

                }

                Console.ReadKey();

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
