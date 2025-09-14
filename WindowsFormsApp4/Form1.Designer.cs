namespace MaterialManagementSystem
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSuppliers;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.DataGridView dataGridViewMaterials;
        private System.Windows.Forms.Panel panel1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.dataGridViewMaterials = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterials)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAdd.Location = new System.Drawing.Point(12, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.BackgroundImage")));
            this.btnEdit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEdit.Location = new System.Drawing.Point(118, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSuppliers.BackgroundImage")));
            this.btnSuppliers.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSuppliers.Location = new System.Drawing.Point(244, 12);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(150, 30);
            this.btnSuppliers.TabIndex = 2;
            this.btnSuppliers.Text = "Поставщики";
            this.btnSuppliers.UseVisualStyleBackColor = true;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(400, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOrders.BackgroundImage")));
            this.btnOrders.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOrders.Location = new System.Drawing.Point(506, 12);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(150, 30);
            this.btnOrders.TabIndex = 4;
            this.btnOrders.Text = "Сделки";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // dataGridViewMaterials
            // 
            this.dataGridViewMaterials.AllowUserToAddRows = false;
            this.dataGridViewMaterials.AllowUserToDeleteRows = false;
            this.dataGridViewMaterials.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMaterials.BackgroundColor = System.Drawing.Color.Lavender;
            this.dataGridViewMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMaterials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMaterials.Location = new System.Drawing.Point(0, 54);
            this.dataGridViewMaterials.Name = "dataGridViewMaterials";
            this.dataGridViewMaterials.ReadOnly = true;
            this.dataGridViewMaterials.Size = new System.Drawing.Size(984, 557);
            this.dataGridViewMaterials.TabIndex = 5;
            this.dataGridViewMaterials.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMaterials_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnSuppliers);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnOrders);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 54);
            this.panel1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.dataGridViewMaterials);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "Form1";
            this.Text = "Система управления материалами";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterials)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}