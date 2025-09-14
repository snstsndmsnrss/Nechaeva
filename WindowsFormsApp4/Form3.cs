using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialManagementSystem
{
    public partial class OrdersForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-3BJR4Q0;Initial Catalog=lollol;Integrated Security=True";
        private int? currentOrderId = null;
        private bool isEditMode = false;

        public OrdersForm()
        {
            InitializeComponent();
            ApplyStyle();
            LoadOrders();
            LoadPartners();
            LoadProducts();
            LoadEmployees();
            SetupDataGridViews();
        }

        public OrdersForm(int orderId) : this()
        {
            currentOrderId = orderId;
            isEditMode = true;
            tabControl1.SelectedTab = tabPage2;
            this.Text = "Редактирование сделки";
            btnAdd.Text = "Сохранить изменения";
            LoadOrderData();
        }

        private void SetupDataGridViews()
        {
            dataGridViewOrderItems.Columns.Clear();
            dataGridViewOrderItems.Columns.Add("ProductId", "Артикул");
            dataGridViewOrderItems.Columns.Add("ProductName", "Наименование");
            dataGridViewOrderItems.Columns.Add("Quantity", "Количество");
            dataGridViewOrderItems.Columns.Add("UnitPrice", "Цена за единицу");
            dataGridViewOrderItems.Columns.Add("TotalPrice", "Общая цена");

            dataGridViewOrderItems.Columns["ProductId"].Visible = false;
            dataGridViewOrderItems.Columns["ProductName"].ReadOnly = true;
            dataGridViewOrderItems.Columns["UnitPrice"].ReadOnly = true;
            dataGridViewOrderItems.Columns["TotalPrice"].ReadOnly = true;
        }

        private void ApplyStyle()
        {
            this.BackColor = Color.White;
            this.Font = new Font("Comic Sans MS", 9);

            dataGridViewOrders.BackgroundColor = Color.FromArgb(0xAB, 0xCF, 0xCE);
            dataGridViewOrders.ForeColor = Color.Black;
            dataGridViewOrderItems.BackgroundColor = Color.FromArgb(0xAB, 0xCF, 0xCE);
            dataGridViewOrderItems.ForeColor = Color.Black;

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

        private void LoadOrders()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // УБРАЛ Дата_события из запроса
                    string query = @"
                        SELECT 
                            с.ID_сделки,
                            п.Наименование as Партнер,
                            сот.ФИО as Менеджер,
                            с.Сумма_сделки,
                            с.Оценка_реализации as Статус
                        FROM История_партнера с
                        INNER JOIN Партнеры п ON с.ID_партнера = п.ID_партнера
                        INNER JOIN Сотрудники сот ON с.ID_сотрудника = сот.ID_сотрудника
                        ORDER BY с.ID_сделки DESC"; // Изменил сортировку

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewOrders.DataSource = table;
                    FormatDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке сделок: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderData()
        {
            if (!currentOrderId.HasValue) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            ID_партнера, ID_сотрудника, Сумма_сделки,
                            Оценка_реализации as Статус
                        FROM История_партнера 
                        WHERE ID_сделки = @OrderId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", currentOrderId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cmbPartner.SelectedValue = reader["ID_партнера"];
                                cmbManager.SelectedValue = reader["ID_сотрудника"];
                                txtTotalAmount.Text = Convert.ToDecimal(reader["Сумма_сделки"]).ToString("N2");
                                cmbStatus.SelectedItem = reader["Статус"].ToString();
                            }
                        }
                    }

                    string itemsQuery = @"
                        SELECT 
                            сс.Артикул,
                            пр.Наименование,
                            сс.Количество,
                            пр.Стоимость as Цена_за_единицу,
                            (сс.Количество * пр.Стоимость) as Общая_цена
                        FROM Состав_сделки сс
                        INNER JOIN Продукция пр ON сс.Артикул = пр.Артикул
                        WHERE сс.ID_сделки = @OrderId";

                    SqlDataAdapter itemsAdapter = new SqlDataAdapter(itemsQuery, connection);
                    itemsAdapter.SelectCommand.Parameters.AddWithValue("@OrderId", currentOrderId);
                    DataTable itemsTable = new DataTable();
                    itemsAdapter.Fill(itemsTable);

                    dataGridViewOrderItems.Rows.Clear();
                    foreach (DataRow row in itemsTable.Rows)
                    {
                        dataGridViewOrderItems.Rows.Add(
                            row["Артикул"],
                            row["Наименование"],
                            row["Количество"],
                            Convert.ToDecimal(row["Цена_за_единицу"]).ToString("N2"),
                            Convert.ToDecimal(row["Общая_цена"]).ToString("N2")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных сделки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPartners()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID_партнера, Наименование FROM Партнеры";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    cmbPartner.DataSource = table;
                    cmbPartner.DisplayMember = "Наименование";
                    cmbPartner.ValueMember = "ID_партнера";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке партнеров: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Артикул, Наименование FROM Продукция";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    cmbProduct.DataSource = table;
                    cmbProduct.DisplayMember = "Наименование";
                    cmbProduct.ValueMember = "Артикул";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке продукции: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployees()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID_сотрудника, ФИО FROM Сотрудники";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    cmbManager.DataSource = table;
                    cmbManager.DisplayMember = "ФИО";
                    cmbManager.ValueMember = "ID_сотрудника";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке сотрудников: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateOrderForm())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        if (isEditMode)
                        {
                            string updateQuery = @"
                                UPDATE История_партнера SET
                                    ID_партнера = @PartnerId,
                                    ID_сотрудника = @ManagerId,
                                    Сумма_сделки = @TotalAmount,
                                    Оценка_реализации = @Status
                                WHERE ID_сделки = @OrderId";

                            using (SqlCommand command = new SqlCommand(updateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@PartnerId", cmbPartner.SelectedValue);
                                command.Parameters.AddWithValue("@ManagerId", cmbManager.SelectedValue);
                                command.Parameters.AddWithValue("@TotalAmount", decimal.Parse(txtTotalAmount.Text));
                                command.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                                command.Parameters.AddWithValue("@OrderId", currentOrderId);
                                command.ExecuteNonQuery();
                            }

                            string deleteItemsQuery = "DELETE FROM Состав_сделки WHERE ID_сделки = @OrderId";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteItemsQuery, connection))
                            {
                                deleteCommand.Parameters.AddWithValue("@OrderId", currentOrderId);
                                deleteCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // УБРАЛ Дата_события из INSERT
                            string insertQuery = @"
                                INSERT INTO История_партнера (
                                    ID_партнера, ID_сотрудника, Сумма_сделки, 
                                    Оценка_реализации
                                ) VALUES (
                                    @PartnerId, @ManagerId, @TotalAmount,
                                    @Status
                                );
                                SELECT SCOPE_IDENTITY();";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@PartnerId", cmbPartner.SelectedValue);
                                command.Parameters.AddWithValue("@ManagerId", cmbManager.SelectedValue);
                                command.Parameters.AddWithValue("@TotalAmount", decimal.Parse(txtTotalAmount.Text));
                                command.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                                currentOrderId = Convert.ToInt32(command.ExecuteScalar());
                            }
                        }

                        foreach (DataGridViewRow row in dataGridViewOrderItems.Rows)
                        {
                            if (!row.IsNewRow && row.Cells["ProductId"].Value != null)
                            {
                                string itemQuery = @"
                                    INSERT INTO Состав_сделки (
                                        ID_сделки, Артикул, Количество,
                                        Цена_за_единицу, Общая_цена
                                    ) VALUES (
                                        @OrderId, @ProductId, @Quantity,
                                        @UnitPrice, @TotalPrice
                                    )";

                                using (SqlCommand itemCommand = new SqlCommand(itemQuery, connection))
                                {
                                    itemCommand.Parameters.AddWithValue("@OrderId", currentOrderId);
                                    itemCommand.Parameters.AddWithValue("@ProductId", row.Cells["ProductId"].Value);
                                    itemCommand.Parameters.AddWithValue("@Quantity", Convert.ToInt32(row.Cells["Quantity"].Value));
                                    itemCommand.Parameters.AddWithValue("@UnitPrice", Convert.ToDecimal(row.Cells["UnitPrice"].Value));
                                    itemCommand.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(row.Cells["TotalPrice"].Value));
                                    itemCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        MessageBox.Show(isEditMode ? "Сделка успешно обновлена!" : "Сделка успешно создана!",
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadOrders();
                        ClearForm();
                        tabControl1.SelectedTab = tabPage1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении сделки: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateOrderForm()
        {
            if (cmbPartner.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите партнера", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbManager.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите сотрудника", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dataGridViewOrderItems.Rows.Count <= 1)
            {
                MessageBox.Show("Добавьте хотя бы одну позицию в сделку", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(txtTotalAmount.Text, out decimal totalAmount) || totalAmount <= 0)
            {
                MessageBox.Show("Введите корректную общую сумму", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            currentOrderId = null;
            isEditMode = false;
            cmbPartner.SelectedIndex = -1;
            cmbManager.SelectedIndex = -1;
            cmbStatus.SelectedIndex = 0;
            txtTotalAmount.Text = "";
            dataGridViewOrderItems.Rows.Clear();
            this.Text = "Сделки";
            btnAdd.Text = "Создать сделку";
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Выберите продукт и укажите количество", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите корректное количество", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal unitPrice = GetProductPrice(Convert.ToInt32(cmbProduct.SelectedValue));
            decimal totalPrice = unitPrice * quantity;

            dataGridViewOrderItems.Rows.Add(
                cmbProduct.SelectedValue,
                cmbProduct.Text,
                quantity,
                unitPrice.ToString("N2"),
                totalPrice.ToString("N2")
            );

            CalculateTotalAmount();
            txtQuantity.Text = "";
        }

        private decimal GetProductPrice(int productId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Стоимость FROM Продукция WHERE Артикул = @ProductId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        object result = command.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        private void CalculateTotalAmount()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridViewOrderItems.Rows)
            {
                if (!row.IsNewRow && row.Cells["TotalPrice"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                }
            }
            txtTotalAmount.Text = total.ToString("N2");
        }

        private void dataGridViewOrderItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewOrderItems.Rows[e.RowIndex];
                if (row.Cells["Quantity"].Value != null && row.Cells["UnitPrice"].Value != null)
                {
                    if (int.TryParse(row.Cells["Quantity"].Value.ToString(), out int quantity))
                    {
                        decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                        decimal totalPrice = quantity * unitPrice;
                        row.Cells["TotalPrice"].Value = totalPrice.ToString("N2");
                        CalculateTotalAmount();
                    }
                }
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrderItems.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewOrderItems.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dataGridViewOrderItems.Rows.Remove(row);
                    }
                }
                CalculateTotalAmount();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сделку для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedOrderId = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells["ID_сделки"].Value);
            OrdersForm editForm = new OrdersForm(selectedOrderId);
            editForm.ShowDialog();
            LoadOrders();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сделку для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedOrderId = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells["ID_сделки"].Value);
            string partnerName = dataGridViewOrders.SelectedRows[0].Cells["Партнер"].Value.ToString();

            var result = MessageBox.Show($"Вы уверены, что хотите удалить сделку для партнера '{partnerName}'?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "DELETE FROM История_партнера WHERE ID_сделки = @OrderId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@OrderId", selectedOrderId);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Сделка успешно удалена", "Успех",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadOrders();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении сделки: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}