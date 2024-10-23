namespace Buscaminas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CrearTablero(Board board)
        {
            int buttonSize = panelTablero.Width / 8;

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

                    Posicion pos = new Posicion(row, col);
                    Ficha ficha = gameState.Board[pos];


                    // evento Click a cada botón
                    btn.Click += (sender, e) => OnCellClick(pos);

                    // Almacenar el botón en la matriz
                    buttons[row, col] = btn;

                    // Agregar el botón al panel
                    panelTablero.Controls.Add(btn);
                }
            }
        }

    }
}
