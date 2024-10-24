using Logica.Elementos;

namespace Buscaminas
{
    public partial class Form1 : Form
    {
        private Button[,] buttons = new Button[10, 10]; // Matriz para los botones del tablero

        public Form1()
        {
            InitializeComponent();
            Tablero tablerito = new Tablero();
            CrearTablero(tablerito);

            this.Size = new Size(725, 800); //Tamaño de la ventana
            panelTablero.Dock = new DockStyle(); // Fijar el tamño del panel
            panelTablero.Location = new Point(50, 50); //Centrarlo
        }

        private void CrearTablero(Tablero tablero)
        {
            int buttonSize = panelTablero.Width / 10;

            panelTablero.Controls.Clear(); // Limpiar cualquier control existente en el panel

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(buttonSize, buttonSize);
                    btn.Location = new Point(col * buttonSize, row * buttonSize);

                    // Ajuste del tamaño de la fuente
                    btn.Font = new Font("Arial", buttonSize / 2, FontStyle.Bold);

                    //Posicion pos = new Posicion(row, col);
                    //Ficha ficha = gameState.Board[pos];

                    // evento Click a cada botón
                    btn.Click += (sender, e) => OnCellClick(); // POSICIÖN ES CASILLA AGREGAR!!!!

                    // Almacenar el botón en la matriz
                    buttons[row, col] = btn;

                    // Agregar el botón al panel
                    panelTablero.Controls.Add(btn);
                }
            }
        }

        private void OnCellClick()
        {
            MessageBox.Show("QUIERP PENE");
            // !!!!!!!!!!!!!!!!!!!! AGREGAR FUNCION REVELAR (CUANDO ESTE HECHA)

        }

        private void panelTablero_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
