using Logica.Elementos;
using Logica.Mecanicas;
using System.ComponentModel;

namespace Buscaminas
{
    public partial class Form1 : Form
    {
        // Matriz para los botones del tablero
        static int minasaux;       //Variabl�e que almacena el nmero de minas auxiliares
        static Dificultad diff;     //Variable que almacenar� el objeto de dificultad
        static Button[,] buttons;       //Matriz de botones, est� es diferente a la matriz o tablero donde se almacenaran las minas.
        static Tablero holi;        //'holi' guarda un tablero donde se almacenan las minas
        static Direccion[] dir;       //Objeto de la direcci�n, se usara para evular los botones
        bool[,] visited;
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(950, 800); //Tama�o de la ventana
            //panelTablero.Dock = new DockStyle(); // Fijar el tam�o del panel
            //panelTablero.Location = new Point(50, 50); //Centrarlo
        }

        private void CrearTablero(int rowsandcolumns)
        {
            int buttonSize = panelTablero.Width / 15;       //Definir tama�o de botones

            buttons = new Button[rowsandcolumns, rowsandcolumns];       //Tama�o de la matriz de botones

            int panelWidthAndHeight = rowsandcolumns * buttonSize;      //Variable para definir la anchura y tama�o del panel que se encuentra en el forms

            panelTablero.Size = new Size(panelWidthAndHeight, panelWidthAndHeight);     //Definimos el tama�o del tablero con la variable anterior
            panelTablero.Controls.Clear();
            panelTablero.Location = new Point((this.ClientSize.Width - panelTablero.Width) / 2, (this.ClientSize.Height - panelTablero.Height) / 2); //Centramos el tablero

            panelTablero.Controls.Clear();      //Limpiar el panel (verifica que el tablero no tenga elementos para cuando se reinicie el juego o se cambie el modo) 


            for (int row = 0; row < rowsandcolumns; row++)
            {
                for (int col = 0; col < rowsandcolumns; col++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(buttonSize, buttonSize);
                    btn.Location = new Point(col * buttonSize, row * buttonSize);

                    // Ajuste del tama�o de la fuente
                    btn.Font = new Font("Arial", buttonSize / 2, FontStyle.Bold);

                    int pos_x = row;
                    int pos_y = col;
                    // evento Click a cada bot�n
                    btn.Click += (sender, e) => OnCellClick(pos_x, pos_y); // POSICI�N ES CASILLA AGREGAR!!!!

                    // Almacenar el bot�n en la matriz
                    buttons[row, col] = btn;

                    //Metodo temporal para ver la posicion de las minas===========
                    if (holi[row, col].HasMine)
                    {
                        buttons[row, col].Text = "B";
                    }
                    //========================================

                    // Agregar el bot�n al panel
                    panelTablero.Controls.Add(btn);
                }
            }
            visited = new bool[rowsandcolumns, rowsandcolumns];
        }

        private void OnCellClick(int pos_x, int pos_y)
        {
            if (holi[pos_x, pos_y].HasMine)     //Verifica si en la posicion clickeada 'holi' tiene valor booleano true(contiene mina)
            {
                MessageBox.Show("Mina");
                PaneEnabled();
            }

            else
            {
                Position posClickeada = new Position(pos_x, pos_y);     //Creamos un objeto de posici�on nuevo que tendr� de magnitud los valores de la casilla que hayamos clickeado
                int contMinas = 0;
                //buttons[pos_x,pos_y].Text = (minasalrededor(holi, posClickeada, contMinas)).ToString();

                if ((minasalrededor(holi, posClickeada, contMinas)) == 0)
                {
                    buttons[pos_x, pos_y].BackColor = Color.AliceBlue;
                }
            }
        }

        private void panelTablero_Paint(object sender, PaintEventArgs e)
        {
            //METODO QUE NO SE PUEDE ELIMINAR XD
        }

        private void cmbxDifficult_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dificultad = (string) cmbxDifficult.SelectedItem;        //Toma el valor string de la dificultad seleccionada
            diff = new Dificultad(dificultad);      //Crea un objeto de dificultad tomando el valor dado de lcomvbobox
            holi = Tablero.Initial(diff.dificultadYminas().Item1, diff.dificultadYminas().Item1, diff.dificultadYminas().Item2);     /*Definimos holi, la variable del
            tablero. Esta la inicializamos con el metodo del mismo nombre, segun las filas y columnas de la dificultad y le pasamos el valor de minas*/

            CrearTablero(diff.dificultadYminas().Item1);        //Creamos el 'tablero' de botones, estos son los que interactuan con el usuairo
            minasaux = diff.dificultadYminas().Item2;      //Valor de minas auxiliar, por si se necesita llamar en alg�n m�todo
        }

        public int minasalrededor(Tablero board, Position posiActual, int contMinas)
        {
            if (visited[posiActual.Row, posiActual.Column])
            {
                return 0;
            }

            visited[posiActual.Row, posiActual.Column] = true;

            dir = new Direccion[]
            {
                Direccion.NorthEast, 
                Direccion.NorthWest, 
                Direccion.SouthEast, 
                Direccion.SouthWest,
                Direccion.North, 
                Direccion.South, 
                Direccion.East,
                Direccion.West
            };
            
            foreach (var direccion in dir)
            {
                Position posVerificar = posiActual + direccion;
                if (posVerificar.Row >= 0 && posVerificar.Row < diff.dificultadYminas().Item1 &&
                    posVerificar.Column >= 0 && posVerificar.Column < diff.dificultadYminas().Item1)
                {
                    if (board[posVerificar].HasMine)
                    {
                        contMinas++;
                    }
                }
            }

            if (contMinas == 0)
            {
                buttons[posiActual.Row, posiActual.Column].BackColor = Color.AliceBlue;

                foreach (var direccion in dir)
                {
                    Position posVerificar = posiActual + direccion;

                    if (posVerificar.Row >= 0 && posVerificar.Row < diff.dificultadYminas().Item1 &&
                        posVerificar.Column >= 0 && posVerificar.Column < diff.dificultadYminas().Item1)
                    {
                        if (!board[posVerificar].HasMine)
                        {
                            minasalrededor(board, posVerificar, contMinas);  
                        }
                    }
                }
            }
            else
            {
                buttons[posiActual.Row, posiActual.Column].BackColor = Color.AliceBlue;
                buttons[posiActual.Row, posiActual.Column].Text = contMinas.ToString();
            }

            return contMinas;
        }

        public void PaneEnabled()
        {
            for (int row = 0; row < diff.dificultadYminas().Item1; row++)
            {
                for (int col = 0; col < diff.dificultadYminas().Item1; col++)
                {
                    buttons[row, col].Enabled = false;
                }
            }
        }
    }
}
