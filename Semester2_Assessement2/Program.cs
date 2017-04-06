using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2_Assessement2
{
    class Program
    {
        /*
         * This Program is designed to read in from a data text sheet named accordingly. 
         * It'll display a menu the the user and the user will choose between 4 options (1-4)
         *  
         * 
         * 
         * 
         * Cathal McBride
         * Finished: 03/04/2017
         * 
         */
        static void Main(string[] args)
        {

            int selector;
            bool repeat = true;
            int[,] array = new int[3, 3];

            string[] playerInfo = File.ReadAllLines(@"C: \Users\Cahar\Documents\First_Year\C#_Programming\Semester2_Assessement2\scores.txt");






            while (repeat == true)
            {
                selector = BringUpMenuDisplay();

                switch (selector)
                {
                    case 1:
                        Console.Clear();
                        PrintPlayerReport(playerInfo);
                        break;

                    case 2:
                        PrintAnalysisRep(playerInfo);
                        break;

                    case 3:
                        PrintSearchForPlayer(playerInfo);
                        break;

                    case 4:
                        repeat = false;
                        break;

                   
                }
                
            }
        }


        //Below is the Central Menu Display which out puts a value
        static int BringUpMenuDisplay()
        {
            bool checker = false;
            int choice = 0;

            while (checker == false)
            {
                Console.Clear();
                Console.WriteLine("{0} \n", "Menu");
                Console.WriteLine("1. Player Report");
                Console.WriteLine("2. Score Analysis Report");
                Console.WriteLine("3. Search for a Player");
                Console.WriteLine("4. Exit \n");
                Console.Write("Enter Choice: ");

                checker = int.TryParse(Console.ReadLine().Trim(), out choice);

                if (choice == 0 || choice > 4)
                {
                    checker = false;
                }
                
                
            }

            return choice;
        }


        //Method for printing a overall player report
        static void PrintPlayerReport(string[] playerInfo)
        {
            int overallScore = 0 , overallSquare = 0 , topScore = 0 ;
            double avgScore = 0 , standardD = 0;
            string tPlayer = "Null";
            
            int playerCount = 0;

            Console.WriteLine("{0,-15} {1,-15} {2}", "Player Name" , "Score" , "Star Rating");
            //
            foreach (string individual in playerInfo)
            {
                string[] record = individual.Split(',');
                overallScore += int.Parse(record[3]);
                Console.WriteLine("{0,-15} {1,-15} {2}", nameParser(record[1], 0), record[3], AssignStarRaing(record[3]));
                playerCount++;
                overallSquare += (int.Parse(record[3])) * (int.Parse(record[3]));
                
                if (topScore < int.Parse(record[3]))
                {
                    topScore = int.Parse(record[3]);
                    tPlayer = nameParser(record[1], 1);
                }
            }
            //
            avgScore = overallScore / playerCount;
            standardD = Math.Sqrt((overallSquare / playerCount) - (avgScore*avgScore));
            Console.WriteLine("Average Score: {0,-25}", avgScore);
            Console.WriteLine("Pop Standard Deviation: {0,-25:n}", standardD); // Remember to format
            Console.WriteLine("Top Player: {0,-25}", tPlayer);
            Console.ReadLine();

        }


        // Used to allocate the star rating of each player
        static string AssignStarRaing( string inputScore)
        {
            string stars;
            int score = int.Parse(inputScore);
            


            if (score < 400)
            {
                stars = "*";
                
            }
            else if ( score < 600)
            {
                stars = "**";
            }
            else if (score < 700)
            {
                stars = "***";
            }
            else if (score < 1000)
            {
                stars = "****";
            }
            else if (score < 1000000)
            {
                stars = "*****";
            }
            else
            {
                stars = "Cheated";
            }

            return stars;
        }


        // An overall table of all the players 
        static void PrintAnalysisRep(string[] playerInfo)
        {
            
            int totalCount = 0, totalIrishCount = 0 , totalNonIrishCount = 0;
            int[,] scoreTable = new int[5, 3];

            foreach (string individual in playerInfo)
            {
                
                bool isIrish;
                string[] record = individual.Split(',');
                isIrish = nationalityDeterminator(record, ref totalIrishCount, ref totalNonIrishCount);
                analysisCount(record , isIrish , scoreTable );
            }
            totalCount = totalIrishCount + totalNonIrishCount;
            Console.Clear();
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", "Score Range" , "Count", "Non-Irish" , "Irish" );
            Console.WriteLine();
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", "Under 400" , scoreTable[0, 0], scoreTable[0, 1] , scoreTable[0, 2]);
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", "400-599" , scoreTable[1, 0], scoreTable[1, 1], scoreTable[1, 2]);
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", "600-699", scoreTable[2, 0], scoreTable[2, 1], scoreTable[2, 2]);
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", "700-999",  scoreTable[3, 0], scoreTable[3, 1], scoreTable[3, 2]);
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", "1000 +" ,  scoreTable[4, 0], scoreTable[4, 1], scoreTable[4, 2]);
            Console.WriteLine();
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", "Totals" , totalCount , totalNonIrishCount , totalIrishCount);
            Console.ReadLine();
        }


        //The counting mechanism to create the values to be shown in within the previous table
        static void analysisCount(string[] record, bool isIrish , int[,] scoreTable)
        {
            

            if (int.Parse(record[3]) < 400)
            {
                scoreTable[0, 0] += 1;
                if (isIrish == true)
                {
                    scoreTable[0, 2] += 1;
                }
                else
                {
                    scoreTable[0, 1] += 1;
                }
            }
            else if (int.Parse(record[3]) < 600)
            {
                scoreTable[1, 0] += 1;
                if (isIrish == true)
                {
                    scoreTable[1, 2] += 1;
                }
                else
                {
                    scoreTable[1, 1] += 1;
                }
            }
            else if (int.Parse(record[3]) < 700)
            {
                scoreTable[2, 0] += 1;
                if (isIrish == true)
                {
                    scoreTable[2, 2] += 1;
                }
                else
                {
                    scoreTable[2, 1] += 1;
                }
            }
            else if (int.Parse(record[3]) < 1000)
            {
                scoreTable[3, 0] += 1;
                if (isIrish == true)
                {
                    scoreTable[3, 2] += 1;
                }
                else
                {
                    scoreTable[3, 1] += 1;
                }
            }
            else
            {
                scoreTable[4, 0] += 1;
                if (isIrish == true)
                {
                    scoreTable[4, 2] += 1;
                }
                else
                {
                    scoreTable[4, 1] += 1;
                }
            }

        }


        //Checks the users nationality inorder to increment the correct value in the scoreTable[]
        static bool nationalityDeterminator(string[] record, ref int totalIrishCount, ref int totalNonIrishCount)
        {
            bool irish;
            if (record[2].ToUpper() == "IRISH")
            {
                totalIrishCount++;
                irish = true;
            }
            else
            {
                totalNonIrishCount++;
                irish = false;
            }
            return irish;
        }


        //Method for the search a player in the text file
        static void PrintSearchForPlayer(string[] playerInfo)
        {
            bool repeat = true, playerFound;
            string playerNumber;
            while (repeat == true)
            {
                Console.Clear();
                playerFound = false;
                Console.Write("\n Enter Player Number: ");
                playerNumber = Console.ReadLine().ToUpper();
                Console.WriteLine();

                foreach (string individual in playerInfo)
                {
                    string[] record = individual.Split(',');
                    if (record[0] ==playerNumber)
                    {
                        
                        playerFound = true;
                        Console.WriteLine("{0,14} {1} \n", "Employee name:", nameParser(record[1], 3));
                        Console.WriteLine("{0,14} {1}", "Score:" ,record[3]);
                        Console.ReadLine();
                        repeat = tryAgain();
                        break;
                        
                    }
                    
                }
                if (playerFound == false)
                {
                    Console.WriteLine("No match found");
                    repeat = tryAgain();
                }
            }
        }


        //Checks if the user wishes to search for another player
        static bool tryAgain()
        {
            bool reply = true, repeat = true;

            while (repeat == true)
            {
                Console.WriteLine("Do you wish to search again? Y/N");
                string answer = Console.ReadLine().ToUpper();
                if (answer == "Y" || answer == "YES")
                {
                    reply = true;
                    repeat = false;
                }

               else if (answer == "N" || answer == "NO")
                {
                    reply = false;
                    repeat = false;
                }
               
              
            }
            return reply;
        }


        //This method outputs the various ways that the program wishes to display the player names
        static string nameParser(string name, int selectedInput)
        {
            string parsedName;
            string[] segments = name.Split(' ');

            if (selectedInput == 0)
            {
                parsedName = segments[0][0] + ". " + segments[1];
            }
            else if (selectedInput == 1)
            {
                parsedName = segments[0][0] + "." + segments[1][0];
            }
            else if (selectedInput == 2)
            {
                parsedName = segments[0];
            }
            else if (selectedInput == 3)
            {
                parsedName = segments[0];
            }
            else
            {
                parsedName = name;
            }
            return parsedName;
        }

        
    }
}
