using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Al
{
    public partial class frmAUTOCOR : Form
    {
        private const string ArchivoRepuestos = "repuestos.txt";
        public frmAUTOCOR()
        {
            InitializeComponent();
        }

        private void frmAUTOCOR_Load(object sender, EventArgs e)
        {
        }

        // ── Estado inicial ────────────────────────────────────────────────────
        private void ConfigurarEstadoInicial()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            cmbMarca.SelectedIndex = 0;
            rbNacional.Checked = true;
            rbImportado.Checked = false;
            btnAceptar.Enabled = false;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim();

            if (CodigoExiste(codigo))
            {
                MessageBox.Show(
                    $"El código '{codigo}' ya existe. No se permiten códigos repetidos.",
                    "Código duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return;
            }

            string nombre = txtNombre.Text.Trim();
            string marca = cmbMarca.SelectedItem.ToString();
            string precio = txtPrecio.Text.Trim();
            string origen = rbNacional.Checked ? "Nacional" : "Importado";

            string linea = $"{codigo}|{nombre}|{marca}|{precio}|{origen}";

            try
            {
                {
                    File.AppendAllText(ArchivoRepuestos, linea + Environment.NewLine);
                    MessageBox.Show("Repuesto grabado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ConfigurarEstadoInicial();
                    txtCodigo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al grabar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        

        // ── Cancelar ──────────────────────────────────────────────────────────
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ConfigurarEstadoInicial();
            txtCodigo.Focus();
        }

        // ── Consultar ─────────────────────────────────────────────────────────
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            frmConsulta consulta = new frmConsulta();
            consulta.ShowDialog();
        }

        // ── Salir ─────────────────────────────────────────────────────────────
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir de la aplicación?", "Salir",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        // ── Auxiliar ──────────────────────────────────────────────────────────
        private bool CodigoExiste(string codigo)
        {
            if (!File.Exists(ArchivoRepuestos)) return false;

            foreach (string linea in File.ReadAllLines(ArchivoRepuestos))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                string[] partes = linea.Split('|');
                if (partes.Length > 0 &&
                    partes[0].Equals(codigo, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
