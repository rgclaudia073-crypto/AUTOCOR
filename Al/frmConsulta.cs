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
    public partial class frmConsulta : Form
    {
        public frmConsulta()
        {
            InitializeComponent();
            ConfigurarGrilla();

            // Cargar marcas
            cmbMarca.Items.AddRange(new object[] { "FIAT (F)", "RENAULT (R)", "PEUGEOT (P)" });
            cmbMarca.SelectedIndex = 0;
        }

        private void rbNacional_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void ConfigurarGrilla()
        {
            c.Columns.Clear();
            c.Columns.Add("Código", "Código");
            c.Columns.Add("Nombre", "Nombre");
            c.Columns.Add("Marca", "Marca");
            c.Columns.Add("Precio", "Precio");
            c.Columns.Add("Origen", "Origen");
            c.Columns["Código"].Width = 100;
            c.Columns["Nombre"].Width = 200;
            c.Columns["Marca"].Width = 100;
            c.Columns["Precio"].Width = 100;
            c.Columns["Origen"].Width = 100;
            c.AllowUserToAddRows = false;
            c.AllowUserToDeleteRows = false;
            c.ReadOnly = true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            c.Rows.Clear();

            if (!File.Exists("repuestos.txt"))
            {
                MessageBox.Show("No existe el archivo de repuestos.", "Consulta",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string marcaFiltro = cmbMarca.SelectedItem?.ToString();
            string origenFiltro = rbNacional.Checked ? "Nacional" : rbImportado.Checked ? "Importado" : "Ambos";
            MessageBox.Show("Marca filtro: '" + marcaFiltro + "'\nOrigen filtro: '" + origenFiltro + "'");

            var lista = new List<string[]>();

            foreach (string linea in File.ReadAllLines("repuestos.txt"))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                string[] p = linea.Split('|');
                if (p.Length != 5) continue;

                bool marcaOk = p[2].Equals(marcaFiltro, StringComparison.OrdinalIgnoreCase);
                bool origenOk = origenFiltro == "Ambos" ||
                                p[4].Equals(origenFiltro, StringComparison.OrdinalIgnoreCase);

                if (marcaOk && origenOk)
                    lista.Add(p);
            }

            // Ordenar alfabéticamente por Nombre (índice 1)
            lista.Sort((a, b) => string.Compare(a[1], b[1], StringComparison.OrdinalIgnoreCase));

            if (lista.Count == 0)
            {
                MessageBox.Show("No se encontraron repuestos con los criterios seleccionados.",
                    "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (string[] p in lista)
            {
                c.Rows.Add(p[0], p[1], p[2], p[4],  p[3]);


            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConsulta_Load(object sender, EventArgs e)
        {

        }
    }
}
