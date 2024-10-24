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
        private readonly Position[,] posicion = new Position[10,10]; // matriz de fichas donde se almacenaran todas nuestras fichas y sus posiciones

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

          public static Tablero Initial() 
        { 
            Tablero board = new Tablero();
            return board;
        }
    }
}
