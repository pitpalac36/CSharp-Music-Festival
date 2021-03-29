namespace csharpMusicFestival
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.purchaserTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.artistNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noAvailableTicketsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noSoldTicketsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.artistNameColumn,
            this.dateColumn,
            this.locationColumn,
            this.noAvailableTicketsColumn,
            this.noSoldTicketsColumn});
            this.dataGridView1.Location = new System.Drawing.Point(46, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(951, 460);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(1046, 246);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(204, 56);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1043, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Purchaser Name";
            // 
            // purchaserTextBox
            // 
            this.purchaserTextBox.Location = new System.Drawing.Point(1046, 147);
            this.purchaserTextBox.Name = "purchaserTextBox";
            this.purchaserTextBox.Size = new System.Drawing.Size(195, 22);
            this.purchaserTextBox.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1087, 388);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 71);
            this.button1.TabIndex = 4;
            this.button1.Text = "SELL TICKETS";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(107, 579);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(480, 583);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 21);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "View Artists";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1106, 565);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 54);
            this.button2.TabIndex = 7;
            this.button2.Text = "LOG OUT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1064, 300);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 8;
            // 
            // idColumn
            // 
            this.idColumn.HeaderText = "ID";
            this.idColumn.MinimumWidth = 6;
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Width = 50;
            // 
            // artistNameColumn
            // 
            this.artistNameColumn.HeaderText = "ARTIST NAME";
            this.artistNameColumn.MinimumWidth = 6;
            this.artistNameColumn.Name = "artistNameColumn";
            this.artistNameColumn.ReadOnly = true;
            this.artistNameColumn.Width = 125;
            // 
            // dateColumn
            // 
            this.dateColumn.HeaderText = "DATE";
            this.dateColumn.MinimumWidth = 6;
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            this.dateColumn.Width = 125;
            // 
            // locationColumn
            // 
            this.locationColumn.HeaderText = "LOCATION";
            this.locationColumn.MinimumWidth = 6;
            this.locationColumn.Name = "locationColumn";
            this.locationColumn.ReadOnly = true;
            this.locationColumn.Width = 125;
            // 
            // noAvailableTicketsColumn
            // 
            this.noAvailableTicketsColumn.HeaderText = "AVAILABLE TICKETS";
            this.noAvailableTicketsColumn.MinimumWidth = 6;
            this.noAvailableTicketsColumn.Name = "noAvailableTicketsColumn";
            this.noAvailableTicketsColumn.ReadOnly = true;
            this.noAvailableTicketsColumn.Width = 125;
            // 
            // noSoldTicketsColumn
            // 
            this.noSoldTicketsColumn.HeaderText = "SOLD TICKETS";
            this.noSoldTicketsColumn.MinimumWidth = 6;
            this.noSoldTicketsColumn.Name = "noSoldTicketsColumn";
            this.noSoldTicketsColumn.ReadOnly = true;
            this.noSoldTicketsColumn.Width = 125;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1067, 309);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tickets : ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 740);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.purchaserTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox purchaserTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn artistNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noAvailableTicketsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noSoldTicketsColumn;
        private System.Windows.Forms.Label label3;
    }
}