using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagmentApp
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }
        private void Products_Load(object sender, EventArgs e)
        {
            statusComboBox.SelectedIndex = 0;
            LoadData();
            //txtProductCode.Focus();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StockDB;Integrated Security=True");
            //INSERT Data into the SQL DB(StockDB) the Product Table
            con.Open();
            bool status = false;
            if(statusComboBox.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            var sqlQuery = "";
            if (checkingIfProductIsInSQLDB(con, txtProductCode.Text))
            {
                sqlQuery = @"UPDATE [Product] SET [ProductName] = '" + txtPoductName.Text + "'" +
                    ",[ProductStatus] = '" + status + "' WHERE [ProductCode] = '" + txtProductCode.Text + "'";
            }
            else
            { 
                sqlQuery = @"INSERT INTO [Product]([ProductCode],[ProductName],[ProductStatus])VALUES
           ('" + txtProductCode.Text + "', '" + txtPoductName.Text + "', '" + status + "')";
            }
            SqlCommand command = new SqlCommand(sqlQuery, con);
            command.ExecuteNonQuery();
            con.Close();

            //Getting Stored data from StockDB to Display in the DataGridView
            LoadData();
        }
        private bool checkingIfProductIsInSQLDB(SqlConnection con, string ProductCode)
        {
            SqlDataAdapter sqlData = new SqlDataAdapter("SELECT 1 FROM [Product] WHERE [ProductCode] = '" + ProductCode +"'", con);
            DataTable dtable = new DataTable();
            sqlData.Fill(dtable);
            if (dtable.Rows.Count > 0)
                return true;
            else
               return false;
                 
        }
        public void LoadData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StockDB;Integrated Security=True");
            SqlDataAdapter sqlData = new SqlDataAdapter(@"SELECT * FROM [dbo].[Product]", con);
            DataTable dtable = new DataTable();
            sqlData.Fill(dtable);
            productDataGridView.Rows.Clear();

            //Filling in data into the datagridview after checking resepctive rows and cell with respect to SQL DB

            foreach (DataRow item in dtable.Rows)
            {
                int n = productDataGridView.Rows.Add();
                productDataGridView.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                productDataGridView.Rows[n].Cells[1].Value = item["ProductName"].ToString();

                if ((bool)item["ProductStatus"])
                {
                    productDataGridView.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    productDataGridView.Rows[n].Cells[2].Value = "Inactive";
                }
            }
        }

        private void productDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtProductCode.Text = productDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            txtPoductName.Text = productDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            if (productDataGridView.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                statusComboBox.SelectedIndex = 0;
            }
            else
            {
                statusComboBox.SelectedIndex = 1;
            }
            //statusComboBox.SelectedText = productDataGridView.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StockDB;Integrated Security=True");
            var sqlQuery = "";
            if (checkingIfProductIsInSQLDB(con, txtProductCode.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Product]  WHERE [ProductCode] = '" + txtProductCode.Text + "'";
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Successfully Delete!", "Successful Deletion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                con.Close();
            }
            else
            {
                MessageBox.Show("Non Existing Entry", "Record not Available", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Getting Stored data from StockDB to Display in the DataGridView
            LoadData();
        }
    }
}
