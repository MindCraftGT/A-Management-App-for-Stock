using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StockManagmentApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            // TO DO: Clear User Credentials afer Logout
            txtUsername.Text = "";
            txtPassword.Clear();
            txtUsername.Focus();
        }
        private void loginButton_Click(object sender, EventArgs e)
        {
            // TO DO: User Authentication & DB Connection initialization
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StockDB;Integrated Security=True");
            SqlDataAdapter sqldata = new SqlDataAdapter(@"SELECT * FROM[dbo].[Login] Where Username = '" +txtUsername.Text +"' and Password = '" + txtPassword.Text +"'", con);
            DataTable dtable = new DataTable();
            sqldata.Fill(dtable);
            if (dtable.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
                //this.Close();
            }
            else
            {
                MessageBox.Show("Username or Password Invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
