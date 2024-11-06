using Logica.Elementos;
using Logica.Mecanicas;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

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
        bool[,] visited;
        private Timer timerBlanco = new Timer();
        private int tiempoRestanteBlanco = 0;


        private int revealedCells = 0; // Lleva la cuenta de casillas reveladas lo estaremos llamando en el metodo "minasalrededor"

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(950, 800); //Tamaño de la ventana

        }

        // ---------------- Metodo para inicializar el tablero como una matriz de botones -------------------
        private void CrearTablero(int rowsandcolumns)
        {
            int buttonSize = panelTablero.Width / 15;  // Definir tamaño de botones

            buttons = new Button[rowsandcolumns, rowsandcolumns];  // Tamaño de la matriz de botones

            int panelWidthAndHeight = rowsandcolumns * buttonSize;  // Definir anchura y altura del panel

            panelTablero.Size = new Size(panelWidthAndHeight, panelWidthAndHeight);  // Definir el tamaño del panel
            panelTablero.Controls.Clear();  // Limpiar el panel
            panelTablero.Location = new Point(
                (this.ClientSize.Width - panelTablero.Width) / 2,
                (this.ClientSize.Height - panelTablero.Height) / 2);  // Centrar el tablero

            for (int row = 0; row < rowsandcolumns; row++)
            {
                for (int col = 0; col < rowsandcolumns; col++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(col * buttonSize, row * buttonSize),
                        Font = new Font("Arial", buttonSize / 2, FontStyle.Bold),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = (row + col) % 2 == 0 ? Color.LightGreen : Color.DarkSeaGreen,  // Colores alternos
                        Margin = new Padding(0),
                        FlatAppearance = { BorderSize = 0 }
                    };

                    int pos_x = row;
                    int pos_y = col;

                    // Manejar el clic en el botón
                    btn.Click += (sender, e) => OnCellClick(pos_x, pos_y);
                    btn.MouseDown += (sender, e) => OnCellRightClick(sender, e, pos_x, pos_y);

                    // Almacenar el botón en la matriz
                    buttons[row, col] = btn;

                    // Agregar el botón al panel
                    panelTablero.Controls.Add(btn);
                }
            }
            visited = new bool[rowsandcolumns, rowsandcolumns];  // Inicializar la matriz de visitados
        }

        Color colorAnterior = Color.AliceBlue;
        // ---------------- metodo que Añade una bandera cuando le demos click derecho, esto permitira marcar donde creamos que hay minas --------
        private void OnCellRightClick(object sender, MouseEventArgs e, int pos_x, int pos_y)
        {
            if (e.Button == MouseButtons.Right && !visited[pos_x, pos_y])
            {
                Button btn = buttons[pos_x, pos_y];
                if (btn.Text == "")
                {
                    colorAnterior = btn.BackColor;
                    btn.Text = "🚩";
                    btn.BackColor = Color.Red;
                }
                else // solo para quitar la bandera en caso de que ya haya 
                {
                    btn.Text = "";
                    btn.BackColor = colorAnterior;
                }
            }
        }

        private void TimerBlanco_Tick(object sender, EventArgs e)
        {
            if (tiempoRestanteBlanco <= 72000000)
            {
                tiempoRestanteBlanco++;
                temporizador.Text = FormatearTiempo(tiempoRestanteBlanco);
            }
        }


        // ---------------- Metodo Landa que se ejecutara cuando hagamos click en un boton, en caso de ser una mina acaba el juego --------
        private async void OnCellClick(int pos_x, int pos_y)
        {
            if (holi[pos_x, pos_y].HasMine)
            {
                timerBlanco.Stop();
                MessageBox.Show("Has revelado una mina, el juego termina.");
                PaneEnabled(); // Deshabilita el tablero
                await RevelarMinasSecuencia(); // Revela todas las minas de forma secuencial
                
            }
            else
            {
                if (!visited[pos_x, pos_y]) // Verifica si la celda ya ha sido revelada
                {
                    Position posClickeada = new Position(pos_x, pos_y);
                    int contMinas = minasalrededor(holi, posClickeada, 0);

                    if (contMinas == 0)
                    {
                        buttons[pos_x, pos_y].BackColor = Color.SandyBrown;
                    }
                    // Metodo temporal para comprobar si todas las celdas a excepcion de las minas fueron reveladas
                    if (revealedCells == (diff.dificultadYminas().Item1 * diff.dificultadYminas().Item1) - minasaux)
                    {
                        MessageBox.Show($"¡Felicidades, ganaste! Celdas reveladas: {revealedCells}, Total de celdas sin minas: {(diff.dificultadYminas().Item1 * diff.dificultadYminas().Item1) - minasaux}");
                        timerBlanco.Stop();
                    }
                }
            }
        }

        // ------------------------- Funcion para revelar todas las minas en el tablero -----------------------
        private async Task RevelarMinasSecuencia()
        {
            // Itera sobre todas las celdas en el tablero
            for (int row = 0; row < diff.dificultadYminas().Item1; row++)
            {
                for (int col = 0; col < diff.dificultadYminas().Item1; col++)
                {
                    // Si la celda tiene una mina, la revela
                    if (holi[row, col].HasMine)
                    {
                        buttons[row, col].Text = "💣";
                        buttons[row, col].BackColor = Color.Red;

                        await Task.Delay(300);
                    }
                }
            }
        }

        // ---------------- Metodo random que no hemos podido eliminar :( ------------------------
        private void panelTablero_Paint(object sender, PaintEventArgs e)
        {
            //METODO QUE NO SE PUEDE ELIMINAR XD
        }
        // ---------------- ComboBox de dificultad donde nos almacena la información que nos interesa del tablero segun la dificultad ---------


        private string FormatearTiempo(int segundos)
        {
            int minutos = segundos / 60;
            int segundosRestantes = segundos % 60;
            return $"{minutos:D2}:{segundosRestantes:D2}";
        }

        private void cmbxDifficult_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dificultad = (string)cmbxDifficult.SelectedItem;        //Toma el valor string de la dificultad seleccionada
            diff = new Dificultad(dificultad);      //Crea un objeto de dificultad tomando el valor dado de lcomvbobox
            holi = Tablero.Initial(diff.dificultadYminas().Item1, diff.dificultadYminas().Item1, diff.dificultadYminas().Item2);     /*Definimos holi, la variable del
            tablero. Esta la inicializamos con el metodo del mismo nombre, segun las filas y columnas de la dificultad y le pasamos el valor de minas*/

            CrearTablero(diff.dificultadYminas().Item1);        //Creamos el 'tablero' de botones, estos son los que interactuan con el usuairo
            minasaux = diff.dificultadYminas().Item2;      //Valor de minas auxiliar, por si se necesita llamar en algún método
            timerBlanco.Interval = 1000; // 1 segundo
            timerBlanco.Tick += TimerBlanco_Tick;

            temporizador.Text = "00:00";
            timerBlanco.Start();
        }


        // ------------esta funcion permite revelar la cantidad de minas que se encuentran al rededor de una casilla vacia ------
        public int minasalrededor(Tablero board, Position posiActual, int contMinas)
        {
            if (visited[posiActual.Row, posiActual.Column])
            {
                return 0;
            }

            visited[posiActual.Row, posiActual.Column] = true;

            // Incrementar revealedCells aquí también al revelar una celda
            revealedCells++;

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

            // Controlamos que el metodo no se salga de los limites del tablero
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
                buttons[posiActual.Row, posiActual.Column].BackColor = Color.SandyBrown;

                foreach (var direccion in dir)
                {
                    Position posVerificar = posiActual + direccion;

                    if (posVerificar.Row >= 0 && posVerificar.Row < diff.dificultadYminas().Item1 &&
                        posVerificar.Column >= 0 && posVerificar.Column < diff.dificultadYminas().Item1)
                    {
                        if (!board[posVerificar].HasMine && !visited[posVerificar.Row, posVerificar.Column])
                        {
                            minasalrededor(board, posVerificar, contMinas);
                        }
                        if (buttons[posVerificar.Row, posVerificar.Column].Text == "🚩")
                        {
                            buttons[posVerificar.Row, posVerificar.Column].Text = "";
                        }
                    }
                }
            }
            else
            {
                buttons[posiActual.Row, posiActual.Column].BackColor = Color.SandyBrown;
                buttons[posiActual.Row, posiActual.Column].Text = contMinas.ToString();
            }

            // Verificar si ha ganado después de cada revelación de celdas
            if (revealedCells == (diff.dificultadYminas().Item1 * diff.dificultadYminas().Item1) - minasaux)
            {
                MessageBox.Show("¡Felicidades, ganaste!");
                PaneEnabled();
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
