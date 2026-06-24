using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Al
{
    public partial class frmAUTOCOR : Form
    {
        public frmAUTOCOR()
        {
            InitializeComponent();
        }

        private void frmAUTOCOR_Load(object sender, EventArgs e)
        {
             private GroupBox grpRepuesto;
        private Label lblCodigo, lblNombre, lblMarca, lblPrecio, lblOrigen;
        private TextBox txtCodigo, txtNombre, txtPrecio;
        private ComboBox cmbMarca;
        private RadioButton rbNacional, rbImportado;
        private Button btnAceptar, btnCancelar, btnConsultar, btnSalir;

        public FormRepuestos()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "AUTOCOR-Repuestos";
            this.Size = new System.Drawing.Size(480, 280);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // GroupBox
            grpRepuesto = new GroupBox();
            grpRepuesto.Text = "Repuesto";
            grpRepuesto.Location = new System.Drawing.Point(12, 12);
            grpRepuesto.Size = new System.Drawing.Size(340, 220);

            // Código
            lblCodigo = new Label() { Text = "Código", Location = new System.Drawing.Point(10, 30), AutoSize = true };
            txtCodigo = new TextBox() { Location = new System.Drawing.Point(90, 27), Width = 120 };

            // Nombre
            lblNombre = new Label() { Text = "Nombre", Location = new System.Drawing.Point(10, 65), AutoSize = true };
            txtNombre = new TextBox() { Location = new System.Drawing.Point(90, 62), Width = 230 };

            // Marca
            lblMarca = new Label() { Text = "Marca", Location = new System.Drawing.Point(10, 100), AutoSize = true };
            cmbMarca = new ComboBox()
            {
                Location = new System.Drawing.Point(90, 97),
                Width = 230,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbMarca.Items.AddRange(new string[] { "", "Toyota", "Ford", "Chevrolet", "Honda", "Volkswagen" });
            cmbMarca.SelectedIndex = 0;

            // Precio
            lblPrecio = new Label() { Text = "Precio", Location = new System.Drawing.Point(10, 135), AutoSize = true };
            txtPrecio = new TextBox() { Location = new System.Drawing.Point(90, 132), Width = 120 };

            // Origen
            lblOrigen = new Label() { Text = "Origen", Location = new System.Drawing.Point(10, 175), AutoSize = true };
            rbNacional = new RadioButton() { Text = "Nacional", Location = new System.Drawing.Point(90, 172), AutoSize = true };
            rbImportado = new RadioButton() { Text = "Importado", Location = new System.Drawing.Point(185, 172), AutoSize = true };

            grpRepuesto.Controls.AddRange(new Control[] {
                lblCodigo, txtCodigo,
                lblNombre, txtNombre,
                lblMarca, cmbMarca,
                lblPrecio, txtPrecio,
                lblOrigen, rbNacional, rbImportado
            });

            // Botones
            btnAceptar = new Button() { Text = "Aceptar", Location = new System.Drawing.Point(370, 20), Width = 90 };
            btnCancelar = new Button() { Text = "Cancelar", Location = new System.Drawing.Point(370, 55), Width = 90 };
            btnConsultar = new Button() { Text = "Consultar", Location = new System.Drawing.Point(370, 90), Width = 90 };
            btnSalir = new Button() { Text = "Salir", Location = new System.Drawing.Point(370, 125), Width = 90 };

            btnAceptar.Click += BtnAceptar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnConsultar.Click += BtnConsultar_Click;
            btnSalir.Click += BtnSalir_Click;

            this.Controls.AddRange(new Control[] {
                grpRepuesto,
                btnAceptar, btnCancelar, btnConsultar, btnSalir
            });
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Complete todos los campos obligatorios.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un valor numérico.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string origen = rbNacional.Checked ? "Nacional" : rbImportado.Checked ? "Importado" : "No especificado";

            MessageBox.Show(
                $"Repuesto guardado:\n\nCódigo: {txtCodigo.Text}\nNombre: {txtNombre.Text}\n" +
                $"Marca: {cmbMarca.Text}\nPrecio: {precio:C2}\nOrigen: {origen}",
                "Aceptar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            cmbMarca.SelectedIndex = 0;
            rbNacional.Checked = false;
            rbImportado.Checked = false;
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Ingrese un código para consultar.", "Consultar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // TODO: implementar búsqueda en base de datos
            MessageBox.Show($"Consultando código: {txtCodigo.Text}", "Consultar",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormRepuestos());
        }
    }
}
