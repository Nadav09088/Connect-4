using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace Ex02
{
    class UserInterface
    {
        public void Goodbye()
        {
            Console.WriteLine("Come back soon, Bye Bye!");
        }

        public bool AnotherRound()
        {
            StringBuilder output = new StringBuilder();

            output.Append("Would you like to play another round?");
            output.AppendLine();
            output.Append("[1] Yes");
            output.AppendLine();
            output.Append("[2] No");
            Console.WriteLine(output.ToString());
            bool isValid = false;
            int option = 2; // No

            while(isValid!=true)
            {
                isValid = int.TryParse(Console.ReadLine(), out option);
                Console.Clear();
                if ((isValid == true) && (option == 1 || option == 2)) 
                {
                    if (option == 2)
                    {
                        Goodbye();
                    }

                    break;
                }
                else 
                {
                    if (isValid == true) 
                    {
                        isValid = false;
                        Console.WriteLine("No such option, please try again:");
                    }
                    else
                    {
                        Console.WriteLine("Invalid number, please try again:");
                    }

                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("...");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine(output.ToString());
                }
            }

            return (option == 1) ? true : false;
        }
        public void DeclareResult(Player i_Player1, Player i_Player2, int i_Winner, bool i_VersusComputer)
        {
            string playerName;

            Console.WriteLine();
            if (i_Winner == 1) 
            {
                playerName = "Player 1";
            }
            else
            {
                if (i_VersusComputer == true)
                {
                    playerName = "PC";
                }
                else
                {
                    playerName = "Player 2";
                }
            }

            if(i_Winner == 0)
            {
                Console.WriteLine("         ====It's a tie====");
            }
            else
            {
                Console.WriteLine("        Wow wow wow!   {0} has won!", playerName);
            }

            StringBuilder output = new StringBuilder();

            output.AppendLine();
            output.AppendFormat("        Score:    Player 1: [{0}]", i_Player1.PlayerPoints);
            output.AppendLine();
            output.AppendFormat("                  {0} [{1}]", (i_VersusComputer == true) ? "PC:      " : "Player 2:", i_Player2.PlayerPoints);
            output.AppendLine();
            Console.WriteLine(output.ToString());
        }

        public void GetDimensionsAndEnemy(out int o_NumOfRows, out int o_NumOfCols, out bool o_VersusComputer)
        {
            StringBuilder rowsOutput = new StringBuilder();

            rowsOutput.Append("Enter Rows number:");
            rowsOutput.AppendLine();
            Console.WriteLine(rowsOutput.ToString());
            o_NumOfRows = GetNumberOfRowsAndCols();
            Console.Clear();
            StringBuilder colsOutput = new StringBuilder();

            colsOutput.Append("Enter Columns number:");
            colsOutput.AppendLine();
            Console.WriteLine(colsOutput.ToString());
            o_NumOfCols = GetNumberOfRowsAndCols();
            Console.Clear();
            StringBuilder enemyOutput = new StringBuilder();

            enemyOutput.AppendLine("[1] Play against computer");
            enemyOutput.AppendLine("[2] Play against each other");
            Console.WriteLine(enemyOutput.ToString());
            o_VersusComputer = (ChooseEnemy(enemyOutput) == 1) ? true : false;
            Console.Clear();
        }

        public int ChooseEnemy(StringBuilder i_EnemyOutput)
        {
            int chosenEnemy = 1;
            bool isValidEnemy = false;

            while (isValidEnemy != true) 
            {
                isValidEnemy = int.TryParse(Console.ReadLine(), out chosenEnemy);
                if (isValidEnemy == true && (chosenEnemy < 1 || chosenEnemy > 2))  
                {
                    isValidEnemy = false;
                    Console.Clear();
                    Console.WriteLine("No such option, please try again:");
                    Console.WriteLine();
                    Console.WriteLine(i_EnemyOutput.ToString());
                }
                else if (isValidEnemy == false) 
                {
                    Console.Clear();
                    Console.WriteLine("Invalid number, please try again:");
                    Console.WriteLine();
                    Console.WriteLine(i_EnemyOutput.ToString());
                }
            }

            return chosenEnemy;
        }

        public void PrintCurrentMatrix(Board i_Board)
        {
            i_Board.PrintMatrix();
        }

        public void AskPlayerForTurn(char i_Name, out char o_ChoiceOfPlayer, Board i_Board)
        {
            bool isValid = false;
            StringBuilder output = new StringBuilder();

            o_ChoiceOfPlayer = '0';
            output.AppendFormat("Turn Of Player {0}:", i_Name);
            output.AppendLine();
            output.AppendFormat("Choose a number between 1 to {0}, or press Q to surrender:", i_Board.NumOfCols);
            Console.WriteLine(output.ToString());
            while (isValid != true) 
            {
                string input = Console.ReadLine();

                if ((input == "Q") || (input == "q")) 
                {
                    isValid = true;
                    o_ChoiceOfPlayer = 'q';
                    Console.Clear();
                }
                else
                {
                    int choice;

                    isValid = int.TryParse(input, out choice);
                    if (isValid != true)
                    {
                        Console.WriteLine("Invalid input, please try again:");
                    }
                    else
                    {
                        if ((int)choice < 1 || (int)choice > i_Board.NumOfCols)
                        {
                            isValid = false;
                            Console.WriteLine("Number out of range, please try again:");
                        }
                        else if (i_Board.IsColumnFull(choice)) 
                        {
                            isValid = false;
                            Console.WriteLine("Column is full, please choose another column");
                        }
                        else
                        {
                            Console.Clear();
                            o_ChoiceOfPlayer = input[0];
                        }
                    }
                }
            }
        }

        public static int GetNumberOfRowsAndCols()
        {
            int size = 0;
            bool isANumber = false;

            while (isANumber != true) 
            {
                Console.WriteLine("Enter a number between 4-8: ");
                isANumber = int.TryParse(Console.ReadLine(), out size);
                if (isANumber == true) 
                {
                    if (size < 4 || size > 8) 
                    {
                        isANumber = false;
                        Console.Clear();
                        Console.WriteLine("Number not in range, please try again:");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Not a number, please try again");
                }
            }

            return size;
        }
    }
}