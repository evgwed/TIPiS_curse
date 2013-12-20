namespace SimplexMethod
{
    partial class FormSimplex
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox_dan = new System.Windows.Forms.GroupBox();
            this.button_get_ogranich = new System.Windows.Forms.Button();
            this.textBox_count_ogranich = new System.Windows.Forms.TextBox();
            this.textBox_count_x = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_ogranich = new System.Windows.Forms.GroupBox();
            this.button_go_method = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_z_z = new System.Windows.Forms.TextBox();
            this.textBox_z = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label_otv = new System.Windows.Forms.Label();
            this.buttonv2 = new System.Windows.Forms.Button();
            this.button_v1 = new System.Windows.Forms.Button();
            this.button_v3 = new System.Windows.Forms.Button();
            this.statusStrip_status = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox_dan.SuspendLayout();
            this.groupBox_ogranich.SuspendLayout();
            this.statusStrip_status.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_dan
            // 
            this.groupBox_dan.Controls.Add(this.button_get_ogranich);
            this.groupBox_dan.Controls.Add(this.textBox_count_ogranich);
            this.groupBox_dan.Controls.Add(this.textBox_count_x);
            this.groupBox_dan.Controls.Add(this.label2);
            this.groupBox_dan.Controls.Add(this.label1);
            this.groupBox_dan.Location = new System.Drawing.Point(3, 2);
            this.groupBox_dan.Name = "groupBox_dan";
            this.groupBox_dan.Size = new System.Drawing.Size(208, 98);
            this.groupBox_dan.TabIndex = 0;
            this.groupBox_dan.TabStop = false;
            this.groupBox_dan.Text = "Данные";
            // 
            // button_get_ogranich
            // 
            this.button_get_ogranich.Enabled = false;
            this.button_get_ogranich.Location = new System.Drawing.Point(70, 69);
            this.button_get_ogranich.Name = "button_get_ogranich";
            this.button_get_ogranich.Size = new System.Drawing.Size(130, 23);
            this.button_get_ogranich.TabIndex = 4;
            this.button_get_ogranich.Text = "Задать ограничения";
            this.button_get_ogranich.UseVisualStyleBackColor = true;
            this.button_get_ogranich.Click += new System.EventHandler(this.button_get_ogranich_Click);
            // 
            // textBox_count_ogranich
            // 
            this.textBox_count_ogranich.Location = new System.Drawing.Point(155, 43);
            this.textBox_count_ogranich.Name = "textBox_count_ogranich";
            this.textBox_count_ogranich.Size = new System.Drawing.Size(45, 20);
            this.textBox_count_ogranich.TabIndex = 3;
            this.textBox_count_ogranich.TextChanged += new System.EventHandler(this.textBox_count_ogranich_TextChanged);
            // 
            // textBox_count_x
            // 
            this.textBox_count_x.Location = new System.Drawing.Point(156, 17);
            this.textBox_count_x.Name = "textBox_count_x";
            this.textBox_count_x.ReadOnly = true;
            this.textBox_count_x.Size = new System.Drawing.Size(45, 20);
            this.textBox_count_x.TabIndex = 2;
            this.textBox_count_x.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Количество ограничений: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количесттво переменных:";
            // 
            // groupBox_ogranich
            // 
            this.groupBox_ogranich.Controls.Add(this.button_go_method);
            this.groupBox_ogranich.Controls.Add(this.label5);
            this.groupBox_ogranich.Controls.Add(this.textBox_z_z);
            this.groupBox_ogranich.Controls.Add(this.textBox_z);
            this.groupBox_ogranich.Controls.Add(this.label4);
            this.groupBox_ogranich.Controls.Add(this.label3);
            this.groupBox_ogranich.Location = new System.Drawing.Point(217, 2);
            this.groupBox_ogranich.Name = "groupBox_ogranich";
            this.groupBox_ogranich.Size = new System.Drawing.Size(295, 238);
            this.groupBox_ogranich.TabIndex = 1;
            this.groupBox_ogranich.TabStop = false;
            this.groupBox_ogranich.Text = "Ограничения";
            // 
            // button_go_method
            // 
            this.button_go_method.Enabled = false;
            this.button_go_method.Location = new System.Drawing.Point(191, 14);
            this.button_go_method.Name = "button_go_method";
            this.button_go_method.Size = new System.Drawing.Size(70, 23);
            this.button_go_method.TabIndex = 6;
            this.button_go_method.Text = "Расчет";
            this.button_go_method.UseVisualStyleBackColor = true;
            this.button_go_method.Click += new System.EventHandler(this.button_go_method_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(172, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "=";
            // 
            // textBox_z_z
            // 
            this.textBox_z_z.Location = new System.Drawing.Point(191, 45);
            this.textBox_z_z.Name = "textBox_z_z";
            this.textBox_z_z.Size = new System.Drawing.Size(70, 20);
            this.textBox_z_z.TabIndex = 4;
            // 
            // textBox_z
            // 
            this.textBox_z.Location = new System.Drawing.Point(10, 46);
            this.textBox_z.Name = "textBox_z";
            this.textBox_z.Size = new System.Drawing.Size(156, 20);
            this.textBox_z.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Ограничения";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Целевая функция";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "v6";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_otv
            // 
            this.label_otv.AutoSize = true;
            this.label_otv.Location = new System.Drawing.Point(3, 136);
            this.label_otv.Name = "label_otv";
            this.label_otv.Size = new System.Drawing.Size(40, 13);
            this.label_otv.TabIndex = 3;
            this.label_otv.Text = "Ответ:";
            // 
            // buttonv2
            // 
            this.buttonv2.Location = new System.Drawing.Point(38, 106);
            this.buttonv2.Name = "buttonv2";
            this.buttonv2.Size = new System.Drawing.Size(29, 23);
            this.buttonv2.TabIndex = 4;
            this.buttonv2.Text = "v2";
            this.buttonv2.UseVisualStyleBackColor = true;
            this.buttonv2.Click += new System.EventHandler(this.buttonv2_Click);
            // 
            // button_v1
            // 
            this.button_v1.Location = new System.Drawing.Point(73, 106);
            this.button_v1.Name = "button_v1";
            this.button_v1.Size = new System.Drawing.Size(29, 22);
            this.button_v1.TabIndex = 5;
            this.button_v1.Text = "v1";
            this.button_v1.UseVisualStyleBackColor = true;
            this.button_v1.Click += new System.EventHandler(this.button_v1_Click);
            // 
            // button_v3
            // 
            this.button_v3.Location = new System.Drawing.Point(109, 106);
            this.button_v3.Name = "button_v3";
            this.button_v3.Size = new System.Drawing.Size(33, 22);
            this.button_v3.TabIndex = 6;
            this.button_v3.Text = "v17";
            this.button_v3.UseVisualStyleBackColor = true;
            this.button_v3.Click += new System.EventHandler(this.button_v5_Click);
            // 
            // statusStrip_status
            // 
            this.statusStrip_status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip_status.Location = new System.Drawing.Point(0, 244);
            this.statusStrip_status.Name = "statusStrip_status";
            this.statusStrip_status.Size = new System.Drawing.Size(518, 22);
            this.statusStrip_status.TabIndex = 7;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(272, 17);
            this.toolStripStatusLabel1.Text = "Симплекс метод (Прохоров Евгений, ИСТбд-31)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 266);
            this.Controls.Add(this.statusStrip_status);
            this.Controls.Add(this.button_v3);
            this.Controls.Add(this.button_v1);
            this.Controls.Add(this.buttonv2);
            this.Controls.Add(this.label_otv);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox_ogranich);
            this.Controls.Add(this.groupBox_dan);
            this.Name = "Form1";
            this.Text = "Решение задачи линейного программирования";
            this.groupBox_dan.ResumeLayout(false);
            this.groupBox_dan.PerformLayout();
            this.groupBox_ogranich.ResumeLayout(false);
            this.groupBox_ogranich.PerformLayout();
            this.statusStrip_status.ResumeLayout(false);
            this.statusStrip_status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_dan;
        private System.Windows.Forms.Button button_get_ogranich;
        private System.Windows.Forms.TextBox textBox_count_ogranich;
        private System.Windows.Forms.TextBox textBox_count_x;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_ogranich;
        private System.Windows.Forms.TextBox textBox_z_z;
        private System.Windows.Forms.TextBox textBox_z;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_go_method;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_otv;
        private System.Windows.Forms.Button buttonv2;
        private System.Windows.Forms.Button button_v1;
        private System.Windows.Forms.Button button_v3;
        private System.Windows.Forms.StatusStrip statusStrip_status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

