using System;

namespace Ex02
{
    class Player
    {
        private int m_PlayerPoints;

        public int PlayerPoints
        {
            get { return this.m_PlayerPoints; }
            set { this.m_PlayerPoints = value; }
        }

        public Player()
        {
            this.m_PlayerPoints = 0;
        }
    }
}