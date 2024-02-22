using System;
using System.Text;

namespace Ex02
{
    class Board
    {
        private readonly int r_NumOfRows;
        private readonly int r_NumOfColumns;
        private eToken[,] m_Matrix;
        private int[] m_LastEmptyIndexInAColumn;

        public int NumOfCols
        {
            get{ return r_NumOfColumns; }
        }

        public enum eToken
        {
            Empty=' ',
            X='X',
            O='O'
        }

        public Board(Board i_BoardToCopy)
        {
            this.r_NumOfRows = i_BoardToCopy.r_NumOfRows;
            this.r_NumOfColumns = i_BoardToCopy.r_NumOfColumns;
            this.m_Matrix = (eToken[,])i_BoardToCopy.m_Matrix.Clone();
            this.m_LastEmptyIndexInAColumn = (int[])i_BoardToCopy.m_LastEmptyIndexInAColumn.Clone();
        }

        public Board(int i_NumOfRows, int i_NumOfColumns)
        {
            this.r_NumOfRows = i_NumOfRows;
            this.r_NumOfColumns = i_NumOfColumns;
            this.m_Matrix = new eToken[i_NumOfRows, i_NumOfColumns];
            this.m_LastEmptyIndexInAColumn = new int[i_NumOfColumns];
            this.Clear();
        }

        public bool CheckIfHasWon(int i_NumberOfPlayer, int i_LastMoveColumn)
        {
            bool hasWon = false;
            eToken token = (i_NumberOfPlayer == 1) ? eToken.X : eToken.O;
            int counter = 0;

            if (i_LastMoveColumn >= 4)  // horizontal row to left 
            {
                counter = 0;
                for (int i = this.m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1, j = i_LastMoveColumn - 1; j >= i_LastMoveColumn - 4; j--) 
                {
                    if (m_Matrix[i, j] == token) 
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }

                if(counter==4)
                {
                    hasWon = true;
                }
            }

            if (i_LastMoveColumn <= this.r_NumOfColumns-3) // horizontal row to right
            {
                counter = 0;
                for (int i = this.m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1, j = i_LastMoveColumn - 1; j <= i_LastMoveColumn + 2; j++) 
                {
                    if (m_Matrix[i, j] == token)
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (counter == 4)
                {
                    hasWon = true;
                }
            }

            if (r_NumOfRows - 1 - m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1 >= 3)   // vertical down
            {
                counter = 0;
                for (int i = m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1, j = i_LastMoveColumn - 1; i <= r_NumOfRows - 1; i++) 
                {
                    if (m_Matrix[i, j] == token)
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (counter == 4)
                {
                    hasWon = true;
                }
            }

            if ((i_LastMoveColumn >= 4) && (this.m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1 >= 3))  // diagonal to up-left
            {
                counter = 0;
                for (int i = m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1, j = i_LastMoveColumn - 1; i >= 0 && j >= 0; i--, j--) 
                {
                    if (m_Matrix[i, j] == token) 
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (counter == 4) 
                {
                    hasWon = true;
                }
            }

            if ((i_LastMoveColumn >= 4) && (r_NumOfRows - 1 - this.m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1 >= 3))   // diagonol to bottom-left
            {
                counter = 0;
                for (int i = m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1, j = i_LastMoveColumn - 1; i <= r_NumOfRows - 1 && j >= 0; i++, j--)  
                {
                    if (m_Matrix[i, j] == token)
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (counter == 4)
                {
                    hasWon = true;
                }
            }

            if ((NumOfCols - 1 - i_LastMoveColumn - 1 >= 3) && (m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1 >= 3))   // diagonal to up-right
            {
                counter = 0;
                for (int i = m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1, j = i_LastMoveColumn - 1; i >= 0 && j <= NumOfCols; i--, j++) 
                {
                    if (m_Matrix[i, j] == token)
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (counter == 4)
                {
                    hasWon = true;
                }
            }

            if ((NumOfCols - 1 - i_LastMoveColumn - 1 >= 3) && (r_NumOfRows - 1 - m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1 >= 3))  // diagonal to bottom-right
            {
                counter = 0;
                for (int i = m_LastEmptyIndexInAColumn[i_LastMoveColumn - 1] + 1, j = i_LastMoveColumn - 1; i <= r_NumOfRows - 1 && j <= NumOfCols; i++, j++) 
                {
                    if (m_Matrix[i, j] == token)
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (counter == 4)
                {
                    hasWon = true;
                }
            }

            return hasWon;
        }

        public void InsertToken(int i_ColumnToFill, eToken i_Token)
        {
            this.m_Matrix[m_LastEmptyIndexInAColumn[i_ColumnToFill - 1], i_ColumnToFill - 1] = i_Token;
            if (this.IsColumnFull(i_ColumnToFill) != true) 
            {
                m_LastEmptyIndexInAColumn[i_ColumnToFill - 1]--;
            }
        }

        public bool IsFull()
        {
            bool isFull = true;

            for (int i = 1; i <= this.NumOfCols; i++) 
            {
                if (this.IsColumnFull(i) == false) 
                {
                    isFull = false;
                    break;
                }
            }

            return isFull;
        }

        public bool IsColumnFull(int i_NumOfCol)
        {
            bool isColumnFull = false;

            if (m_LastEmptyIndexInAColumn[i_NumOfCol - 1] == -1) 
            {
                isColumnFull = true;
            }

            return isColumnFull;
        }

        public void PrintMatrix()
        {
            int i, j;
            StringBuilder output = new StringBuilder();

            for (j = 1; j <= r_NumOfColumns; j++) 
            {
                output.AppendFormat("  {0} ", j);
            }

            output.AppendLine();
            for (i = 1; i <= r_NumOfRows; i++) 
            {
                for (j = 1; j <= r_NumOfColumns; j++)
                {
                    output.AppendFormat("| {0} ", (char)m_Matrix[i - 1, j - 1]);
                }

                output.Append("|");
                output.AppendLine();
                for (int k = 1; k <= r_NumOfColumns; k++) 
                {
                    output.Append("====");
                }

                output.AppendLine();
            }

            Console.Write(output.ToString());
        }

        public void Clear()
        {
            for (int i = 0; i < r_NumOfRows; i++) 
            {
                for (int j = 0; j < r_NumOfColumns; j++) 
                {
                    this.m_Matrix[i, j] = eToken.Empty;
                }
            }

            for (int i = 0; i < NumOfCols; i++) 
            {
                this.m_LastEmptyIndexInAColumn[i] = this.r_NumOfRows - 1;
            }
        }
    }
}