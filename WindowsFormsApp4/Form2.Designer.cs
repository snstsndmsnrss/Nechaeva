namespace MaterialManagementSystem
{
    partial class MaterialForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCurrentQuantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMaterialType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Button btnSelectImage;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurrentQuantity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMaterialType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Indigo;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование материала:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(180, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 23);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Indigo;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Количество на складе:";
            // 
            // txtCurrentQuantity
            // 
            this.txtCurrentQuantity.Location = new System.Drawing.Point(180, 42);
            this.txtCurrentQuantity.Name = "txtCurrentQuantity";
            this.txtCurrentQuantity.Size = new System.Drawing.Size(100, 23);
            this.txtCurrentQuantity.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Indigo;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Тип материала:";
            // 
            // cmbMaterialType
            // 
            this.cmbMaterialType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaterialType.FormattingEnabled = true;
            this.cmbMaterialType.Location = new System.Drawing.Point(180, 72);
            this.cmbMaterialType.Name = "cmbMaterialType";
            this.cmbMaterialType.Size = new System.Drawing.Size(250, 23);
            this.cmbMaterialType.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Indigo;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Цена единицы материала:";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(180, 102);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(100, 23);
            this.txtPrice.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Indigo;
            this.btnSave.Location = new System.Drawing.Point(180, 160);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.ForeColor = System.Drawing.Color.Indigo;
            this.btnCancel.Location = new System.Drawing.Point(330, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Indigo;
            this.label5.Location = new System.Drawing.Point(12, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Путь к изображению:";
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(180, 132);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(200, 23);
            this.txtImagePath.TabIndex = 11;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(386, 132);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(44, 23);
            this.btnSelectImage.TabIndex = 12;
            this.btnSelectImage.Text = "...";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // MaterialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(442, 200);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbMaterialType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCurrentQuantity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.Name = "MaterialForm";
            this.Text = "Добавление материала";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}