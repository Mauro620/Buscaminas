using Logica.Elementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Mecanicas
{
    // La clase direccion es un objeto que nos permite hacer operaciones con matrices para facilitar otros metodos como el de 
    // minasAlrededor, almacenamos informacion de cambios en todas las direcciones de una posición en la matriz
    public class Direccion
    {
        public readonly static Direccion North = new Direccion(-1, 0);
        public readonly static Direccion South = new Direccion(1, 0);
        public readonly static Direccion East = new Direccion(0, 1);
        public readonly static Direccion West = new Direccion(0, -1);
        public readonly static Direccion NorthEast = North + East;
        public readonly static Direccion NorthWest = North + West;
        public readonly static Direccion SouthEast = South + East;
        public readonly static Direccion SouthWest = South + West;

        public int RowDelta { get; set; }
        public int ColumnDelta { get; set; }

        public Direccion(int rowDelta, int columnDelta)
        {
            RowDelta = rowDelta;
            ColumnDelta = columnDelta;
        }
        public static Direccion operator +(Direccion dir1, Direccion dir2) //Facilita las operaciones entre direcciones solo en suma (Basicamente sumar direcciones)
        {
            return new Direccion(dir1.RowDelta + dir2.RowDelta, dir1.ColumnDelta + dir2.ColumnDelta);
        }
        public static Direccion operator *(int escalar, Direccion direccion) //Simplfica un movimiento multiplicando uno de sus axis por un escalar
        {
            return new Direccion(escalar * direccion.RowDelta, escalar * direccion.ColumnDelta);
        }

        public static Position operator +(Position pos, Direccion dir)
        {
            return new Position(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);
        }

    }
}
