using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logica.Elementos
{
    public class Dificultad
    {
        public string difficult { get; }
        public Dificultad(string difficult)
        {
            this.difficult = difficult;
        }
        public Tuple<int, int> dificultadYminas()
        {
            int minas;
            int rowsAndColumns;

            if (difficult.Equals("Fácil"))
            {
                rowsAndColumns = 10;
                minas = 20;
            }
            else if (difficult.Equals("Medio"))
            {
                rowsAndColumns = 15;
                minas = 30;
            }
            else
            {
                rowsAndColumns = 20;
                minas = 50;
            }

            return Tuple.Create(rowsAndColumns, minas);
        }
    }
}
