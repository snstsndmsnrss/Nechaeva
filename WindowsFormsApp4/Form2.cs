using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialManagementSystem
{
    public partial class MaterialForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-3BJR4Q0;Initial Catalog=lollol;Integrated Security=True";
        private int? materialId = null;
        private bool isEditMode = false;

        public MaterialForm()
        {
            InitializeComponent();
            ApplyStyle();
            LoadComboBoxData();
        }

        public MaterialForm(int id) : this()
        {
            materialId = id;
            isEditMode = true;
            this.Text = "Редактирование материала";
            LoadMaterialData();
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
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string typeQuery = "SELECT ID_типа_материала, Наименование_типа FROM Тип_материала";
                    SqlDataAdapter typeAdapter = new SqlDataAdapter(typeQuery, connection);
                    DataTable typeTable = new DataTable();
                    typeAdapter.Fill(typeTable);
                    cmbMaterialType.DataSource = typeTable;
                    cmbMaterialType.DisplayMember = "Наименование_типа";
                    cmbMaterialType.ValueMember = "ID_типа_материала";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMaterialData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            Наименование,
                            Количество,
                            Тип_материала,
                            Стоимость,
                            Изображение
                        FROM Материалы 
                        WHERE ID_материала = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", materialId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["Наименование"].ToString();
                                txtCurrentQuantity.Text = reader["Количество"].ToString();
                                cmbMaterialType.SelectedValue = reader["Тип_материала"];
                                txtPrice.Text = reader["Стоимость"].ToString();
                                txtImagePath.Text = reader["Изображение"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных материала: {ex.Message}", "Ошибка",
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
                                UPDATE Материалы SET
                                    Наименование = @Name,
                                    Количество = @Quantity,
                                    Тип_материала = @TypeId,
                                    Стоимость = @Price,
                                    Изображение = @ImagePath
                                WHERE ID_материала = @Id";
                        }
                        else
                        {
                            query = @"
                                INSERT INTO Материалы (
                                    Наименование, Количество, Тип_материала, 
                                    Стоимость, Изображение
                                ) VALUES (
                                    @Name, @Quantity, @TypeId, 
                                    @Price, @ImagePath
                                )";
                        }

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Name", txtName.Text);
                            command.Parameters.AddWithValue("@Quantity", int.Parse(txtCurrentQuantity.Text));
                            command.Parameters.AddWithValue("@TypeId", cmbMaterialType.SelectedValue);
                            command.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                            command.Parameters.AddWithValue("@ImagePath", txtImagePath.Text);

                            if (isEditMode)
                            {
                                command.Parameters.AddWithValue("@Id", materialId);
                            }

                            command.ExecuteNonQuery();
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
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
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите наименование материала", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(txtCurrentQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Количество должно быть положительным числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Цена должна быть положительным числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtImagePath.Text = openFileDialog.FileName;
            }
        }
    }
}
