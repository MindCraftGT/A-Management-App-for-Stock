using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagmentApp
{
    public partial class StockMain : Form
    {
        public StockMain()
        {
            InitializeComponent();
        }
        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            products.MdiParent = this;
            products.Show();

        }
        private void Stockmain(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
