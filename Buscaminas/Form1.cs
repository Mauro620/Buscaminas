using Logica.Elementos;
using Logica.Mecanicas;

namespace Buscaminas
{
    public partial class Form1 : Form
    {
        // Matriz para los botones del tablero
        static int minasaux;       //Variablñe que almacena el nmero de minas auxiliares
        static Dificultad diff;     //Variable que almacenará el objeto de dificultad
        static Button[,] buttons;       //Matriz de botones, está es diferente a la matriz o tablero donde se almacenaran las minas.
        static Tablero holi;        //'holi' guarda un tablero donde se almacenan las minas
        static Direccion[] dir;       //Objeto de la dirección, se usara para evular los botones
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(950, 800); //Tamaño de la ventana
            //panelTablero.Dock = new DockStyle(); // Fijar el tamño del panel
            //panelTablero.Location = new Point(50, 50); //Centrarlo
        }

        private void CrearTablero(int rowsandcolumns)
        {
            int buttonSize = panelTablero.Width / 15;       //Definir tamaño de botones

            buttons = new Button[rowsandcolumns, rowsandcolumns];       //Tamaño de la matriz de botones

            int panelWidthAndHeight = rowsandcolumns * buttonSize;      //Variable para definir la anchura y tamaño del panel que se encuentra en el forms

            panelTablero.Size = new Size(panelWidthAndHeight, panelWidthAndHeight);     //Definimos el tamaño del tablero con la variable anterior
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

                    // Ajuste del tamaño de la fuente
                    btn.Font = new Font("Arial", buttonSize / 2, FontStyle.Bold);

                    int pos_x = row;
                    int pos_y = col;
                    // evento Click a cada botón
                    btn.Click += (sender, e) => OnCellClick(pos_x, pos_y); // POSICIÖN ES CASILLA AGREGAR!!!!

                    // Almacenar el botón en la matriz
                    buttons[row, col] = btn;

                    //Metodo temporal para ver la posicion de las minas===========
                    if (holi[row, col].HasMine)
                    {
                        buttons[row, col].Text = "B";
                    }
                    //========================================

                    // Agregar el botón al panel
                    panelTablero.Controls.Add(btn);
                }
            }
        }

        private void OnCellClick(int pos_x, int pos_y)
        {
            if (holi[pos_x, pos_y].HasMine)     //Verifica si en la posicion clickeada 'holi' tiene valor booleano true(contiene mina)
            {
                MessageBox.Show("Mina");
            }
            else
            {
                Position posClickeada = new Position(pos_x, pos_y);     //Creamos un objeto de posici´on nuevo que tendrá de magnitud los valores de la casilla que hayamos clickeado
                int contMinas = 0;
                buttons[pos_x,pos_y].Text = (minasalrededor(holi, posClickeada, contMinas)).ToString();

                if((minasalrededor(holi, posClickeada, contMinas)) == 0)
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
            minasaux = diff.dificultadYminas().Item2;      //Valor de minas auxiliar, por si se necesita llamar en algún método
        }

        public int minasalrededor(Tablero board, Position posiActual, int contMinas)
        {
            dir = new Direccion[]       // Se define la variable dir, esta variable contendr{a los posibles direcciones que verificará.
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
                Position posVerificar = posiActual + direccion;     //Se crea una nueva posición, esta se dá por la sumatoria de las posiciones; actual y direccion unitaria
                if (posVerificar.Row >= 0 && posVerificar.Row < diff.dificultadYminas().Item1 && posVerificar.Column >= 0 
                    && posVerificar.Column < diff.dificultadYminas().Item1)     //Asegura que la verificación no se desfase de los limites del tablero
                {
                    if (holi[posVerificar].HasMine)     //Evaúa qué la casilla "nueva", con magnitudes de posVerificar, su valor booleano sea true(que contenga mina).
                    {
                         contMinas++;
                    }
                    else
                    {
                        buttons[posVerificar.Row, posVerificar.Column].BackColor = Color.AliceBlue;     /*Colorea las casillas donde su valor booleano sea false. Solo colorea
                                                                                                         * las casillas dentro del radio de direcciones*/
                        //int contInicio = 0;
                        //int contFinal = 2;

                        //while (contInicio < contFinal)
                        //{
                        //    minasalrededor(holi, holi[posVerificar], contMinas);
                        //    contInicio++;
                        //    break;
                        //}
                    }
                }
            }
            return contMinas;
        }
    }
}
