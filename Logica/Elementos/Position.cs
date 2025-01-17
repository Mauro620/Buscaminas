﻿using Logica.Elementos;
using System.Runtime.CompilerServices;

namespace Logica.Elementos
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }
        public bool HasMine { get; set; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
            HasMine = false;
        }

        public override bool Equals(object obj)
        {
            return obj is Position posicion &&
                   Row == posicion.Row &&
                   Column == posicion.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

    }
}

