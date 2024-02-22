using System;

namespace Ex02
{
    class Game
    {
        private Player m_Player1;
        private Player m_Player2;
        public readonly bool r_VersusComputer;
        public Board m_Board;
        public UserInterface m_UI;

        public Game()
        {
            int numOfRows, numOfCols;

            m_UI = new UserInterface();
            m_UI.GetDimensionsAndEnemy(out numOfRows, out numOfCols, out r_VersusComputer);
            m_Board = new Board(numOfRows, numOfCols);
            m_Player1 = new Player();
            m_Player2 = new Player();
            RunGame();
        }

        public void RunGame()
        {
            bool finishGame = false;

            while (finishGame != true) 
            {
                int winner = RunRound();

                this.m_UI.DeclareResult(m_Player1, m_Player2, winner, this.r_VersusComputer);
                this.m_Board.Clear();
                finishGame = !this.m_UI.AnotherRound();
            }
        }

        public int RunRound()
        {
            int winner = 0;
            bool player1Turn = true;
            bool finishRound = false;

            this.m_UI.PrintCurrentMatrix(m_Board);
            while (finishRound != true) 
            {
                finishRound = RunTurn(ref player1Turn, out winner);
            }

            if (winner == 1)
            {
                m_Player1.PlayerPoints++;
            }
            else if (winner == 2) 
            {
                m_Player2.PlayerPoints++;
            }

            Console.Clear();
            this.m_UI.PrintCurrentMatrix(m_Board);

            return winner;
        }

        public bool RunTurn(ref bool io_Player1turn, out int o_Winner)
        {
            bool finishRound = false;
            char choiceOfPlayer;

            o_Winner = 0;
            if (r_VersusComputer != true || (r_VersusComputer == true && io_Player1turn == true))  // human turn
            {
                this.m_UI.AskPlayerForTurn((io_Player1turn == true) ? '1' : '2', out choiceOfPlayer, this.m_Board);
                if (choiceOfPlayer == 'Q' || choiceOfPlayer == 'q') 
                {
                    finishRound = true;
                    if (io_Player1turn == true)
                    {
                        o_Winner = 2;
                    }
                    else
                    {
                        o_Winner = 1;
                    }
                }
                else
                {
                    bool hasWon = PlayerMove((io_Player1turn == true) ? '1' : '2', (int)choiceOfPlayer - (int)'0');

                    if (hasWon == true) // if player has won with that move
                    {
                        finishRound = true;
                        if (io_Player1turn == true)
                        {
                            o_Winner = 1;
                        }
                        else
                        {
                            o_Winner = 2;
                        }
                    }
                }
            }
            else // Computer Turn
            {
                bool hasWon = AIComputerMove();

                if(hasWon == true)
                {
                    finishRound = true;
                    o_Winner = 2;
                }

                Console.Clear();
            }

            if (m_Board.IsFull() == true) 
            {
                finishRound = true;
            }

            if (finishRound != true) 
            {
                this.m_UI.PrintCurrentMatrix(m_Board);
            }

            io_Player1turn = !io_Player1turn; // end

            return finishRound;
        }

        public bool RandomComputerMove()
        {
            bool isValid = false;
            int choiceOfComputer = 0;
            Random randomNumber = new Random();

            while (isValid != true) 
            {
                choiceOfComputer = randomNumber.Next(1, this.m_Board.NumOfCols + 1);
                if (this.m_Board.IsColumnFull(choiceOfComputer) != true)
                {
                    isValid = true;
                }
            }

            this.m_Board.InsertToken(choiceOfComputer, Board.eToken.O);

            return this.m_Board.CheckIfHasWon(2, choiceOfComputer);
        }

        public bool WillWinNextMove(Board i_Board, int i_ColumnToFill, Board.eToken i_Token)
        {
            Board newBoard = new Board(i_Board);

            newBoard.InsertToken(i_ColumnToFill, i_Token);

            return newBoard.CheckIfHasWon((i_Token == Board.eToken.X) ? 1 : 2, i_ColumnToFill);
        }

        public bool AIComputerMove()
        {
            int choiceOfComputer = 0;
            int midCol = m_Board.NumOfCols / 2;

            for (int i = 1; i <= m_Board.NumOfCols; i++) // if computer can win this move
            {
                if (m_Board.IsColumnFull(i) != true) 
                {
                    if (WillWinNextMove(this.m_Board, i, Board.eToken.O)) 
                    {
                        choiceOfComputer = i;
                        goto Insert;
                    }
                }
            }

            for (int i = 1; i <= m_Board.NumOfCols; i++) // if human can win next move
            {
                if (m_Board.IsColumnFull(i) != true) 
                {
                    if (WillWinNextMove(this.m_Board, i, Board.eToken.X)) 
                    {
                        choiceOfComputer = i;
                        goto Insert;
                    }
                }
            }

            for (int i = 0; midCol + i <= m_Board.NumOfCols; ++i)
            {
                if (m_Board.IsColumnFull(midCol + i) != true)
                {
                    choiceOfComputer = midCol + i;
                    goto Insert;
                }
                else if (midCol - i >= 1) 
                {
                    if (m_Board.IsColumnFull(midCol - i) != true) 
                    {
                        choiceOfComputer = midCol - i;
                        goto Insert;
                    }
                }
            }

            Insert:
            m_Board.InsertToken(choiceOfComputer, Board.eToken.O);
            Console.Write("Computer is thinking.");
            System.Threading.Thread.Sleep(500);
            Console.Write(".");
            System.Threading.Thread.Sleep(500);
            Console.Write(".");
            System.Threading.Thread.Sleep(500);

            return this.m_Board.CheckIfHasWon(2, choiceOfComputer);
        }

        public bool PlayerMove(char i_PlayerNumber, int i_ChoiceOfPLayer)
        {
            this.m_Board.InsertToken(i_ChoiceOfPLayer, (i_PlayerNumber == '1') ? Board.eToken.X : Board.eToken.O);

            return this.m_Board.CheckIfHasWon((int)i_PlayerNumber - (int)'0', i_ChoiceOfPLayer);
        }
    }
}