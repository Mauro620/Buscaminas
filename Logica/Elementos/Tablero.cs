using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Elementos;

namespace Logica.Elementos
{
    public class Tablero
    {
        private readonly Position[,] posicion; // matriz de posiciones donde se almacenaran todas nuestras minas y sus posiciones

        public Tablero(int rows, int columns)
        {
            posicion = new Position[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    posicion[row, col] = new Position(row, col);
                }
            }
        }

        public Position this[int row, int col] //Constructor de tipo ficha donde podremos obtener la fila y columna actuales y cambiarlas a un valor establecido 
        {
            get { return posicion[row, col];  }
            set { posicion[row, col] = value; }
        }

        public Position this[Position pos] // Retorna posicion actual
        {
            get { return posicion[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

        public static Tablero Initial(int rows, int columns, int numMines)
        {
            Tablero board = new Tablero(rows, columns);
            Random rand = new Random();

            int minesPlaced = 0;
            while (minesPlaced < numMines)
            {
                int row = rand.Next(rows);
                int col = rand.Next(columns);

                // Colocar una mina si la celda no tiene ya una
                if (!board[row, col].HasMine)
                {
                    board[row, col].HasMine = true;
                    minesPlaced++;
                }
            }

            return board;
        }
    }
}
