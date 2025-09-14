using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialManagementSystem
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=DESKTOP-3BJR4Q0;Initial Catalog=lollol;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            ApplyStyle();
            LoadMaterials();
        }

        private void ApplyStyle()
        {
            this.BackColor = Color.White;
            this.Font = new Font("Comic Sans MS", 9);

            foreach (Control control in panel1.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = Color.FromArgb(0x54, 0x6F, 0x94);
                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                }
            }
        }

        private void LoadMaterials()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            m.ID_материала,
                            m.Наименование,
                            m.Количество,
                            tm.Наименование_типа as Тип,
                            m.Стоимость,
                            m.Изображение
                        FROM Материалы m
                        INNER JOIN Тип_материала tm ON m.Тип_материала = tm.ID_типа_материала
                        ORDER BY m.Наименование";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewMaterials.DataSource = table;
                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке материалов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dataGridViewMaterials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMaterials.Columns["ID_материала"].Visible = false;
            dataGridViewMaterials.Columns["Изображение"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MaterialForm form = new MaterialForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadMaterials();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewMaterials.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите материал для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int materialId = Convert.ToInt32(dataGridViewMaterials.SelectedRows[0].Cells["ID_материала"].Value);
            MaterialForm form = new MaterialForm(materialId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadMaterials();
            }
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            SuppliersForm form = new SuppliersForm();
            form.ShowDialog();
        }


            private void btnRefresh_Click(object sender, EventArgs e)
            {
            LoadMaterials();
            }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OrdersForm form = new OrdersForm();
            form.ShowDialog();
        }

        private void dataGridViewMaterials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int materialId = Convert.ToInt32(dataGridViewMaterials.Rows[e.RowIndex].Cells["ID_материала"].Value);
                MaterialForm form = new MaterialForm(materialId);
                if (form.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }
    }
}