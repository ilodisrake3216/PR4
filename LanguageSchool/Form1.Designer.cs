namespace LanguageSchool
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            loadButton = new Button();
            cancelButton = new Button();
            syncLoadButton = new Button();
            progressBar = new ProgressBar();
            statusLabel = new Label();
            equipmentGrid = new DataGridView();
            groupBox1 = new GroupBox();
            addButton = new Button();
            numericUpDown1 = new NumericUpDown();
            label5 = new Label();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            btnDelete = new Button();
            errorProvider1 = new ErrorProvider(components);
            helpProvider1 = new HelpProvider();
            toolTip1 = new ToolTip(components);
            menuStrip1 = new MenuStrip();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            оПрограммеToolStripMenuItem = new ToolStripMenuItem();
            руководтсвоПользователяToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)equipmentGrid).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // loadButton
            // 
            loadButton.BackColor = Color.FromArgb(255, 255, 192);
            loadButton.Location = new Point(16, 41);
            loadButton.Margin = new Padding(4);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(135, 36);
            loadButton.TabIndex = 0;
            loadButton.Text = "Загрузить";
            loadButton.UseVisualStyleBackColor = false;
            loadButton.Click += loadButton_Click_1;
            // 
            // cancelButton
            // 
            cancelButton.BackColor = Color.FromArgb(255, 255, 192);
            cancelButton.Location = new Point(160, 41);
            cancelButton.Margin = new Padding(4);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(125, 36);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Click += cancelButton_Click_1;
            // 
            // syncLoadButton
            // 
            syncLoadButton.BackColor = Color.FromArgb(255, 255, 192);
            syncLoadButton.ForeColor = Color.Red;
            syncLoadButton.Location = new Point(295, 41);
            syncLoadButton.Margin = new Padding(4);
            syncLoadButton.Name = "syncLoadButton";
            syncLoadButton.Size = new Size(188, 36);
            syncLoadButton.TabIndex = 2;
            syncLoadButton.Text = "Эксперимент";
            syncLoadButton.UseVisualStyleBackColor = false;
            syncLoadButton.Click += experiment_Click_1;
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.FromArgb(255, 255, 192);
            progressBar.Location = new Point(707, 36);
            progressBar.Margin = new Padding(4);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(208, 36);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 3;
            progressBar.Visible = false;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(923, 47);
            statusLabel.Margin = new Padding(4, 0, 4, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(78, 25);
            statusLabel.TabIndex = 4;
            statusLabel.Text = "Готово";
            // 
            // equipmentGrid
            // 
            equipmentGrid.AllowUserToAddRows = false;
            equipmentGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            equipmentGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            equipmentGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            equipmentGrid.Location = new Point(21, 85);
            equipmentGrid.Margin = new Padding(4);
            equipmentGrid.Name = "equipmentGrid";
            equipmentGrid.ReadOnly = true;
            equipmentGrid.RowHeadersWidth = 62;
            equipmentGrid.RowTemplate.Height = 28;
            equipmentGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            equipmentGrid.Size = new Size(1035, 329);
            equipmentGrid.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(addButton);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(21, 410);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(1033, 176);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Новая группа";
            // 
            // addButton
            // 
            addButton.BackColor = Color.FromArgb(255, 255, 192);
            addButton.Location = new Point(421, 140);
            addButton.Margin = new Padding(4);
            addButton.Name = "addButton";
            addButton.Size = new Size(140, 36);
            addButton.TabIndex = 9;
            addButton.Text = "Добавить";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click_1;
            // 
            // numericUpDown1
            // 
            numericUpDown1.BackColor = Color.FromArgb(255, 255, 192);
            numericUpDown1.Location = new Point(160, 100);
            numericUpDown1.Margin = new Padding(4);
            numericUpDown1.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(305, 31);
            numericUpDown1.TabIndex = 8;
            numericUpDown1.ThousandsSeparator = true;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(520, 108);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(82, 25);
            label5.TabIndex = 7;
            label5.Text = "Ссылка";
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(255, 255, 192);
            textBox4.Location = new Point(741, 101);
            textBox4.Margin = new Padding(4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(283, 31);
            textBox4.TabIndex = 6;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(255, 255, 192);
            textBox3.Location = new Point(741, 40);
            textBox3.Margin = new Padding(4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(283, 31);
            textBox3.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(515, 48);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(158, 25);
            label4.TabIndex = 4;
            label4.Text = "Преподаватель";
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 101);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(133, 25);
            label3.TabIndex = 2;
            label3.Text = "Средний бал";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(255, 255, 192);
            textBox1.Location = new Point(160, 44);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(304, 31);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 44);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(60, 25);
            label2.TabIndex = 0;
            label2.Text = "Язык";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(255, 255, 192);
            btnDelete.ForeColor = SystemColors.ControlText;
            btnDelete.Location = new Point(490, 41);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(187, 36);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // helpProvider1
            // 
            helpProvider1.HelpNamespace = "";
            // 
            // toolTip1
            // 
            toolTip1.Tag = "";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { справкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1067, 33);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { оПрограммеToolStripMenuItem, руководтсвоПользователяToolStripMenuItem });
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(97, 29);
            справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            оПрограммеToolStripMenuItem.Size = new Size(336, 34);
            оПрограммеToolStripMenuItem.Text = "О программе";
            оПрограммеToolStripMenuItem.Click += about_Click;
            // 
            // руководтсвоПользователяToolStripMenuItem
            // 
            руководтсвоПользователяToolStripMenuItem.Name = "руководтсвоПользователяToolStripMenuItem";
            руководтсвоПользователяToolStripMenuItem.Size = new Size(336, 34);
            руководтсвоПользователяToolStripMenuItem.Text = "Руководтсво пользователя";
            руководтсвоПользователяToolStripMenuItem.Click += userGuide_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 192, 128);
            ClientSize = new Size(1067, 588);
            Controls.Add(btnDelete);
            Controls.Add(groupBox1);
            Controls.Add(equipmentGrid);
            Controls.Add(statusLabel);
            Controls.Add(progressBar);
            Controls.Add(syncLoadButton);
            Controls.Add(cancelButton);
            Controls.Add(loadButton);
            Controls.Add(menuStrip1);
            Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Школа изучения иностранных языков - набор группы";
            ((System.ComponentModel.ISupportInitialize)equipmentGrid).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button syncLoadButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.DataGridView equipmentGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private Button btnDelete;
        private ErrorProvider errorProvider1;
        private HelpProvider helpProvider1;
        private ToolTip toolTip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
        private ToolStripMenuItem руководтсвоПользователяToolStripMenuItem;
    }
}