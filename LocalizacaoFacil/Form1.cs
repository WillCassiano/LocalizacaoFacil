using LocalizacaoFacil.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalizacaoFacil
{
    public partial class Form1 : Form
    {
        BancoFirebird _db;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _db.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtFabricante.Text))
            {
                txtFabricante.Text = "0";
            }
            int fabricante = Convert.ToInt32(txtFabricante.Text);
            dataGridView1.DataSource = Produto.GetProdutos(_db, txtMarca.Text, fabricante);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _db = new BancoFirebird();
        }
    }
}
