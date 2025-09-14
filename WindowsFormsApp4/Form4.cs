using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialManagementSystem
{
    public partial class SuppliersForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-3BJR4Q0;Initial Catalog=lollol;Integrated Security=True";
        private int? supplierId = null;
        private bool isEditMode = false;

        public SuppliersForm()
        {
            InitializeComponent();
            ApplyStyle();
            LoadSuppliers();
        }

        public SuppliersForm(int id) : this()
        {
            supplierId = id;
            isEditMode = true;
            this.Text = "Редактирование поставщика";
            LoadSupplierData();
        }

        private void ApplyStyle()
        {
            this.BackColor = Color.White;
            this.Font = new Font("Comic Sans MS", 9);

            foreach (Control control in this.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = Color.FromArgb(0x54, 0x6F, 0x94);
                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                }
                else if (control is TextBox textBox || control is ComboBox comboBox)
                {
                    control.BackColor = Color.FromArgb(0xAB, 0xCF, 0xCE);
                }
            }

            dataGridViewSuppliers.BackgroundColor = Color.FromArgb(0xAB, 0xCF, 0xCE);
            dataGridViewSuppliers.ForeColor = Color.Black;
        }

        private void LoadSuppliers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            ID_поставщика,
                            ИНН,
                            Юридический_адрес,
                            Контактные_данные
                        FROM Поставщики
                        ORDER BY ИНН";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewSuppliers.DataSource = table;
                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке поставщиков: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dataGridViewSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSuppliers.Columns["ID_поставщика"].Visible = false;
        }

        private void LoadSupplierData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            ИНН,
                            Юридический_адрес,
                            Контактные_данные
                        FROM Поставщики 
                        WHERE ID_поставщика = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", supplierId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtINN.Text = reader["ИНН"].ToString();
                                txtLegalAddress.Text = reader["Юридический_адрес"].ToString();
                                txtContactInfo.Text = reader["Контактные_данные"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных поставщика: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query;

                        if (isEditMode)
                        {
                            query = @"
                                UPDATE Поставщики SET
                                    ИНН = @INN,
                                    Юридический_адрес = @LegalAddress,
                                    Контактные_данные = @ContactInfo
                                WHERE ID_поставщика = @Id";
                        }
                        else
                        {
                            query = @"
                                INSERT INTO Поставщики (
                                    ИНН, Юридический_адрес, Контактные_данные
                                ) VALUES (
                                    @INN, @LegalAddress, @ContactInfo
                                )";
                        }

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@INN", txtINN.Text);
                            command.Parameters.AddWithValue("@LegalAddress", txtLegalAddress.Text);
                            command.Parameters.AddWithValue("@ContactInfo", txtContactInfo.Text);

                            if (isEditMode)
                            {
                                command.Parameters.AddWithValue("@Id", supplierId);
                            }

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show(isEditMode ? "Поставщик успешно обновлен!" : "Поставщик успешно добавлен!",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadSuppliers();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtINN.Text))
            {
                MessageBox.Show("Введите ИНН поставщика", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLegalAddress.Text))
            {
                MessageBox.Show("Введите юридический адрес", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            supplierId = null;
            isEditMode = false;
            txtINN.Text = "";
            txtLegalAddress.Text = "";
            txtContactInfo.Text = "";
            this.Text = "Поставщики";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            tabControl1.SelectedTab = tabPage2;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewSuppliers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите поставщика для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedSupplierId = Convert.ToInt32(dataGridViewSuppliers.SelectedRows[0].Cells["ID_поставщика"].Value);
            SuppliersForm editForm = new SuppliersForm(selectedSupplierId);
            editForm.ShowDialog();
            LoadSuppliers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSuppliers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите поставщика для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedSupplierId = Convert.ToInt32(dataGridViewSuppliers.SelectedRows[0].Cells["ID_поставщика"].Value);
            string inn = dataGridViewSuppliers.SelectedRows[0].Cells["ИНН"].Value.ToString();

            var result = MessageBox.Show($"Вы уверены, что хотите удалить поставщика с ИНН '{inn}'?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM Поставщики WHERE ID_поставщика = @SupplierId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@SupplierId", selectedSupplierId);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Поставщик успешно удален", "Успех",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadSuppliers();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении поставщика: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void dataGridViewSuppliers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int supplierId = Convert.ToInt32(dataGridViewSuppliers.Rows[e.RowIndex].Cells["ID_поставщика"].Value);
                SuppliersForm editForm = new SuppliersForm(supplierId);
                editForm.ShowDialog();
                LoadSuppliers();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                LoadSuppliers();
            }
        }

        private void dataGridViewSuppliers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
