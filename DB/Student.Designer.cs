namespace DB
{
    partial class Student
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOrderQuantity = new System.Windows.Forms.Button();
            this.btnWorkerProfit = new System.Windows.Forms.Button();
            this.btnFirmProfit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClass = new System.Windows.Forms.Button();
            this.btnOrderType = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnObject = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOrderByDate = new System.Windows.Forms.Button();
            this.btnOrdersByWorker = new System.Windows.Forms.Button();
            this.btnOrderByWorker = new System.Windows.Forms.Button();
            this.tbParam1 = new System.Windows.Forms.TextBox();
            this.ComBoxLimitRows = new System.Windows.Forms.ComboBox();
            this.BtnBack = new System.Windows.Forms.Button();
            this.BtnForward = new System.Windows.Forms.Button();
            this.LabPageNumber = new System.Windows.Forms.Label();
            this.BtnBackAll = new System.Windows.Forms.Button();
            this.BtnForwardAll = new System.Windows.Forms.Button();
            this.ComBoxAll = new System.Windows.Forms.ComboBox();
            this.ComBoxName = new System.Windows.Forms.ComboBox();
            this.ComBoxSurname = new System.Windows.Forms.ComboBox();
            this.ComBoxPatronymic = new System.Windows.Forms.ComboBox();
            this.BtnConnectFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOrderQuantity
            // 
            this.btnOrderQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrderQuantity.Location = new System.Drawing.Point(1, 87);
            this.btnOrderQuantity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOrderQuantity.Name = "btnOrderQuantity";
            this.btnOrderQuantity.Size = new System.Drawing.Size(208, 32);
            this.btnOrderQuantity.TabIndex = 0;
            this.btnOrderQuantity.Text = "Выбор";
            this.btnOrderQuantity.UseVisualStyleBackColor = true;
            this.btnOrderQuantity.Click += new System.EventHandler(this.btnOrderQuantity_Click);
            // 
            // btnWorkerProfit
            // 
            this.btnWorkerProfit.Location = new System.Drawing.Point(105, 135);
            this.btnWorkerProfit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWorkerProfit.Name = "btnWorkerProfit";
            this.btnWorkerProfit.Size = new System.Drawing.Size(51, 22);
            this.btnWorkerProfit.TabIndex = 1;
            this.btnWorkerProfit.Text = "Доход работника";
            this.btnWorkerProfit.UseVisualStyleBackColor = true;
            this.btnWorkerProfit.Visible = false;
            this.btnWorkerProfit.Click += new System.EventHandler(this.btnWorkerProfit_Click);
            // 
            // btnFirmProfit
            // 
            this.btnFirmProfit.Location = new System.Drawing.Point(151, 135);
            this.btnFirmProfit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFirmProfit.Name = "btnFirmProfit";
            this.btnFirmProfit.Size = new System.Drawing.Size(51, 22);
            this.btnFirmProfit.TabIndex = 2;
            this.btnFirmProfit.Text = "Прибыль организации";
            this.btnFirmProfit.UseVisualStyleBackColor = true;
            this.btnFirmProfit.Visible = false;
            this.btnFirmProfit.Click += new System.EventHandler(this.btnFirmProfit_Click);
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 14);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1144, 657);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(1166, 48);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(197, 22);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClass
            // 
            this.btnClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClass.Location = new System.Drawing.Point(0, 73);
            this.btnClass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClass.Name = "btnClass";
            this.btnClass.Size = new System.Drawing.Size(197, 26);
            this.btnClass.TabIndex = 5;
            this.btnClass.Text = "Класс";
            this.btnClass.UseVisualStyleBackColor = true;
            this.btnClass.Click += new System.EventHandler(this.btnClass_Click);
            // 
            // btnOrderType
            // 
            this.btnOrderType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrderType.Location = new System.Drawing.Point(1166, 113);
            this.btnOrderType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOrderType.Name = "btnOrderType";
            this.btnOrderType.Size = new System.Drawing.Size(197, 30);
            this.btnOrderType.TabIndex = 6;
            this.btnOrderType.Text = "Предметы";
            this.btnOrderType.UseVisualStyleBackColor = true;
            this.btnOrderType.Click += new System.EventHandler(this.btnObject_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrders.Location = new System.Drawing.Point(1166, 151);
            this.btnOrders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(197, 26);
            this.btnOrders.TabIndex = 7;
            this.btnOrders.Text = "Ученик";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnStudents_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnObject);
            this.groupBox1.Controls.Add(this.btnClass);
            this.groupBox1.Location = new System.Drawing.Point(1166, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(197, 221);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ввод и редактирование данных";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnObject
            // 
            this.btnObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObject.Location = new System.Drawing.Point(0, 189);
            this.btnObject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnObject.Name = "btnObject";
            this.btnObject.Size = new System.Drawing.Size(197, 29);
            this.btnObject.TabIndex = 10;
            this.btnObject.Text = "Выбор ученика";
            this.btnObject.UseVisualStyleBackColor = true;
            this.btnObject.Click += new System.EventHandler(this.btnStudentChoice_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnOrderByDate);
            this.groupBox2.Controls.Add(this.btnOrdersByWorker);
            this.groupBox2.Controls.Add(this.btnOrderByWorker);
            this.groupBox2.Controls.Add(this.tbParam1);
            this.groupBox2.Controls.Add(this.btnOrderQuantity);
            this.groupBox2.Controls.Add(this.btnFirmProfit);
            this.groupBox2.Controls.Add(this.btnWorkerProfit);
            this.groupBox2.Location = new System.Drawing.Point(1162, 233);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(208, 161);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Отчеты";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // btnOrderByDate
            // 
            this.btnOrderByDate.Location = new System.Drawing.Point(65, 135);
            this.btnOrderByDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOrderByDate.Name = "btnOrderByDate";
            this.btnOrderByDate.Size = new System.Drawing.Size(51, 22);
            this.btnOrderByDate.TabIndex = 3;
            this.btnOrderByDate.Text = "Колчество заказов на дату";
            this.btnOrderByDate.UseVisualStyleBackColor = true;
            this.btnOrderByDate.Visible = false;
            this.btnOrderByDate.Click += new System.EventHandler(this.btnOrderByDate_Click);
            // 
            // btnOrdersByWorker
            // 
            this.btnOrdersByWorker.Location = new System.Drawing.Point(33, 135);
            this.btnOrdersByWorker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOrdersByWorker.Name = "btnOrdersByWorker";
            this.btnOrdersByWorker.Size = new System.Drawing.Size(40, 22);
            this.btnOrdersByWorker.TabIndex = 2;
            this.btnOrdersByWorker.Text = "Заказы сотрудника";
            this.btnOrdersByWorker.UseVisualStyleBackColor = true;
            this.btnOrdersByWorker.Visible = false;
            this.btnOrdersByWorker.Click += new System.EventHandler(this.btnOrderByWorker_Click);
            // 
            // btnOrderByWorker
            // 
            this.btnOrderByWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrderByWorker.Location = new System.Drawing.Point(1, 45);
            this.btnOrderByWorker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOrderByWorker.Name = "btnOrderByWorker";
            this.btnOrderByWorker.Size = new System.Drawing.Size(208, 35);
            this.btnOrderByWorker.TabIndex = 1;
            this.btnOrderByWorker.Text = "Ученики";
            this.btnOrderByWorker.UseVisualStyleBackColor = true;
            this.btnOrderByWorker.Click += new System.EventHandler(this.btnWorkersOrder_Click);
            // 
            // tbParam1
            // 
            this.tbParam1.Location = new System.Drawing.Point(0, 19);
            this.tbParam1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbParam1.Name = "tbParam1";
            this.tbParam1.Size = new System.Drawing.Size(201, 22);
            this.tbParam1.TabIndex = 0;
            this.tbParam1.Visible = false;
            this.tbParam1.TextChanged += new System.EventHandler(this.tbParam1_TextChanged);
            // 
            // ComBoxLimitRows
            // 
            this.ComBoxLimitRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ComBoxLimitRows.FormattingEnabled = true;
            this.ComBoxLimitRows.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "50",
            "100"});
            this.ComBoxLimitRows.Location = new System.Drawing.Point(1157, 402);
            this.ComBoxLimitRows.Name = "ComBoxLimitRows";
            this.ComBoxLimitRows.Size = new System.Drawing.Size(121, 24);
            this.ComBoxLimitRows.TabIndex = 10;
            this.ComBoxLimitRows.SelectedIndexChanged += new System.EventHandler(this.ComBoxLimitRows_SelectedIndexChanged);
            // 
            // BtnBack
            // 
            this.BtnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBack.Enabled = false;
            this.BtnBack.Location = new System.Drawing.Point(1157, 433);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(49, 22);
            this.BtnBack.TabIndex = 11;
            this.BtnBack.Text = "<";
            this.BtnBack.UseVisualStyleBackColor = true;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // BtnForward
            // 
            this.BtnForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnForward.Enabled = false;
            this.BtnForward.Location = new System.Drawing.Point(1212, 433);
            this.BtnForward.Name = "BtnForward";
            this.BtnForward.Size = new System.Drawing.Size(49, 22);
            this.BtnForward.TabIndex = 12;
            this.BtnForward.Text = ">";
            this.BtnForward.UseVisualStyleBackColor = true;
            this.BtnForward.Click += new System.EventHandler(this.BtnForward_Click);
            // 
            // LabPageNumber
            // 
            this.LabPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabPageNumber.AutoSize = true;
            this.LabPageNumber.Location = new System.Drawing.Point(1163, 500);
            this.LabPageNumber.Name = "LabPageNumber";
            this.LabPageNumber.Size = new System.Drawing.Size(72, 17);
            this.LabPageNumber.TabIndex = 13;
            this.LabPageNumber.Text = "Странц: 0";
            this.LabPageNumber.Click += new System.EventHandler(this.LabPageNumber_Click);
            // 
            // BtnBackAll
            // 
            this.BtnBackAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBackAll.Location = new System.Drawing.Point(1157, 462);
            this.BtnBackAll.Name = "BtnBackAll";
            this.BtnBackAll.Size = new System.Drawing.Size(49, 22);
            this.BtnBackAll.TabIndex = 14;
            this.BtnBackAll.Text = "<<";
            this.BtnBackAll.UseVisualStyleBackColor = true;
            this.BtnBackAll.Click += new System.EventHandler(this.BtnBackAll_Click);
            // 
            // BtnForwardAll
            // 
            this.BtnForwardAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnForwardAll.Location = new System.Drawing.Point(1212, 463);
            this.BtnForwardAll.Name = "BtnForwardAll";
            this.BtnForwardAll.Size = new System.Drawing.Size(49, 22);
            this.BtnForwardAll.TabIndex = 15;
            this.BtnForwardAll.Text = ">>";
            this.BtnForwardAll.UseVisualStyleBackColor = true;
            this.BtnForwardAll.Click += new System.EventHandler(this.BtnForwardAll_Click);
            // 
            // ComBoxAll
            // 
            this.ComBoxAll.FormattingEnabled = true;
            this.ComBoxAll.Location = new System.Drawing.Point(1166, 543);
            this.ComBoxAll.Name = "ComBoxAll";
            this.ComBoxAll.Size = new System.Drawing.Size(121, 24);
            this.ComBoxAll.TabIndex = 16;
            // 
            // ComBoxName
            // 
            this.ComBoxName.FormattingEnabled = true;
            this.ComBoxName.Location = new System.Drawing.Point(1166, 574);
            this.ComBoxName.Name = "ComBoxName";
            this.ComBoxName.Size = new System.Drawing.Size(121, 24);
            this.ComBoxName.TabIndex = 17;
            // 
            // ComBoxSurname
            // 
            this.ComBoxSurname.FormattingEnabled = true;
            this.ComBoxSurname.Location = new System.Drawing.Point(1166, 605);
            this.ComBoxSurname.Name = "ComBoxSurname";
            this.ComBoxSurname.Size = new System.Drawing.Size(121, 24);
            this.ComBoxSurname.TabIndex = 18;
            // 
            // ComBoxPatronymic
            // 
            this.ComBoxPatronymic.FormattingEnabled = true;
            this.ComBoxPatronymic.Location = new System.Drawing.Point(1166, 636);
            this.ComBoxPatronymic.Name = "ComBoxPatronymic";
            this.ComBoxPatronymic.Size = new System.Drawing.Size(121, 24);
            this.ComBoxPatronymic.TabIndex = 19;
            // 
            // BtnConnectFilter
            // 
            this.BtnConnectFilter.Enabled = false;
            this.BtnConnectFilter.Location = new System.Drawing.Point(1390, 319);
            this.BtnConnectFilter.Name = "BtnConnectFilter";
            this.BtnConnectFilter.Size = new System.Drawing.Size(89, 33);
            this.BtnConnectFilter.TabIndex = 20;
            this.BtnConnectFilter.Text = "Связанный";
            this.BtnConnectFilter.UseVisualStyleBackColor = true;
            this.BtnConnectFilter.Click += new System.EventHandler(this.BtnConnectFilter_Click);
            // 
            // Student
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1491, 681);
            this.Controls.Add(this.BtnConnectFilter);
            this.Controls.Add(this.ComBoxPatronymic);
            this.Controls.Add(this.ComBoxSurname);
            this.Controls.Add(this.ComBoxName);
            this.Controls.Add(this.ComBoxAll);
            this.Controls.Add(this.BtnForwardAll);
            this.Controls.Add(this.BtnBackAll);
            this.Controls.Add(this.LabPageNumber);
            this.Controls.Add(this.BtnForward);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.ComBoxLimitRows);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.btnOrderType);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Student";
            this.Text = "Ученик";
            this.Load += new System.EventHandler(this.Student_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOrderQuantity;
        private System.Windows.Forms.Button btnWorkerProfit;
        private System.Windows.Forms.Button btnFirmProfit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClass;
        private System.Windows.Forms.Button btnOrderType;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOrderByWorker;
        private System.Windows.Forms.TextBox tbParam1;
        private System.Windows.Forms.Button btnOrdersByWorker;
        private System.Windows.Forms.Button btnOrderByDate;
        private System.Windows.Forms.Button btnObject;
        private System.Windows.Forms.ComboBox ComBoxLimitRows;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.Button BtnForward;
        private System.Windows.Forms.Label LabPageNumber;
        private System.Windows.Forms.Button BtnBackAll;
        private System.Windows.Forms.Button BtnForwardAll;
        private System.Windows.Forms.ComboBox ComBoxAll;
        private System.Windows.Forms.ComboBox ComBoxName;
        private System.Windows.Forms.ComboBox ComBoxSurname;
        private System.Windows.Forms.ComboBox ComBoxPatronymic;
        private System.Windows.Forms.Button BtnConnectFilter;
    }
}

