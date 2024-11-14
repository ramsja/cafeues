using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace cafeues
{
    public partial class Form1 : Form
    {
        private string connectionString = "server=your_server;database=your_database;user=your_user;password=your_password";

        private Label labelNombre;
        private TextBox textBoxNombre;
        private Label labelEmail;
        private TextBox textBoxEmail;
        private Label labelContrasena;
        private TextBox textBoxContrasena;
        private Label labelTipo;
        private ComboBox comboBoxTipo;
        private Button buttonRegistrarUsuario;
        private DataGridView dataGridViewProductos;
        private Button buttonRealizarPedido;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents(); // Inicializa los controles
            LoadProducts(); // Carga los productos al iniciar
        }

        private void InitializeCustomComponents()
        {
            // Configuración de controles
            this.labelNombre = new Label() { Text = "Nombre:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            this.textBoxNombre = new TextBox() { Location = new System.Drawing.Point(100, 20), Width = 200 };

            this.labelEmail = new Label() { Text = "Email:", Location = new System.Drawing.Point(20, 60), AutoSize = true };
            this.textBoxEmail = new TextBox() { Location = new System.Drawing.Point(100, 60), Width = 200 };

            this.labelContrasena = new Label() { Text = "Contraseña:", Location = new System.Drawing.Point(20, 100), AutoSize = true };
            this.textBoxContrasena = new TextBox() { Location = new System.Drawing.Point(100, 100), Width = 200, PasswordChar = '*' };

            this.labelTipo = new Label() { Text = "Tipo:", Location = new System.Drawing.Point(20, 140), AutoSize = true };
            this.comboBoxTipo = new ComboBox() { Location = new System.Drawing.Point(100, 140), Width = 200 };
            this.comboBoxTipo.Items.AddRange(new string[] { "Estudiante", "Docente", "Administrativo" });

            this.buttonRegistrarUsuario = new Button() { Text = "Registrar Usuario", Location = new System.Drawing.Point(100, 180) };
            this.buttonRegistrarUsuario.Click += new EventHandler(buttonRegistrarUsuario_Click);

            this.dataGridViewProductos = new DataGridView() { Location = new System.Drawing.Point(20, 220), Width = 400, Height = 200 };
            this.dataGridViewProductos.Columns.Add("Nombre", "Nombre");
            this.dataGridViewProductos.Columns.Add("Precio", "Precio");

            this.buttonRealizarPedido = new Button() { Text = "Realizar Pedido", Location = new System.Drawing.Point(20, 430) };
            this.buttonRealizarPedido.Click += new EventHandler(buttonRealizarPedido_Click);

            // Agregar controles al formulario
            this.Controls.Add(this.labelNombre);
            this.Controls.Add(this.textBoxNombre);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.labelContrasena);
            this.Controls.Add(this.textBoxContrasena);
            this.Controls.Add(this.labelTipo);
            this.Controls.Add(this.comboBoxTipo);
            this.Controls.Add(this.buttonRegistrarUsuario);
            this.Controls.Add(this.dataGridViewProductos);
            this.Controls.Add(this.buttonRealizarPedido);
        }

        private void LoadProducts()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var query = "SELECT Nombre, Precio FROM Productos"; // Asegúrate de que la tabla Productos tenga datos
                var cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Agregar productos al DataGridView
                    dataGridViewProductos.Rows.Add(reader["Nombre"], reader["Precio"]);
                }
            }
        }

        private void buttonRegistrarUsuario_Click(object sender, EventArgs e)
        {
            string nombre = textBoxNombre.Text;
            string email