namespace MNA
{
    partial class MNA
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbMnaList = new System.Windows.Forms.ListBox();
            this.tlRight = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._saveButton = new System.Windows.Forms.Button();
            this.nColumnCaption = new System.Windows.Forms.NumericUpDown();
            this.lColumnCaption = new System.Windows.Forms.Label();
            this.lColumnTag = new System.Windows.Forms.Label();
            this.nColumnTag = new System.Windows.Forms.NumericUpDown();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.dgParameters = new System.Windows.Forms.DataGridView();
            this.tlMain.SuspendLayout();
            this.tlRight.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nColumnCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nColumnTag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgParameters)).BeginInit();
            this.SuspendLayout();
            // 
            // tlMain
            // 
            this.tlMain.ColumnCount = 2;
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlMain.Controls.Add(this.lbMnaList, 0, 0);
            this.tlMain.Controls.Add(this.tlRight, 1, 0);
            this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMain.Location = new System.Drawing.Point(0, 0);
            this.tlMain.Name = "tlMain";
            this.tlMain.RowCount = 1;
            this.tlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMain.Size = new System.Drawing.Size(1265, 481);
            this.tlMain.TabIndex = 0;
            // 
            // lbMnaList
            // 
            this.lbMnaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMnaList.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbMnaList.FormattingEnabled = true;
            this.lbMnaList.ItemHeight = 23;
            this.lbMnaList.Location = new System.Drawing.Point(3, 3);
            this.lbMnaList.Name = "lbMnaList";
            this.lbMnaList.Size = new System.Drawing.Size(394, 475);
            this.lbMnaList.TabIndex = 0;
            // 
            // tlRight
            // 
            this.tlRight.ColumnCount = 1;
            this.tlRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlRight.Controls.Add(this.panel1, 0, 0);
            this.tlRight.Controls.Add(this.dgParameters, 0, 1);
            this.tlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlRight.Location = new System.Drawing.Point(403, 3);
            this.tlRight.Name = "tlRight";
            this.tlRight.RowCount = 2;
            this.tlRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlRight.Size = new System.Drawing.Size(859, 475);
            this.tlRight.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._saveButton);
            this.panel1.Controls.Add(this.nColumnCaption);
            this.panel1.Controls.Add(this.lColumnCaption);
            this.panel1.Controls.Add(this.lColumnTag);
            this.panel1.Controls.Add(this.nColumnTag);
            this.panel1.Controls.Add(this.btnOpenFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(853, 104);
            this.panel1.TabIndex = 0;
            // 
            // _saveButton
            // 
            this._saveButton.Location = new System.Drawing.Point(552, 54);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(75, 23);
            this._saveButton.TabIndex = 5;
            this._saveButton.Text = "button1";
            this._saveButton.UseVisualStyleBackColor = true;
            // 
            // nColumnCaption
            // 
            this.nColumnCaption.Location = new System.Drawing.Point(137, 20);
            this.nColumnCaption.Name = "nColumnCaption";
            this.nColumnCaption.Size = new System.Drawing.Size(120, 20);
            this.nColumnCaption.TabIndex = 4;
            this.nColumnCaption.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lColumnCaption
            // 
            this.lColumnCaption.AutoSize = true;
            this.lColumnCaption.Location = new System.Drawing.Point(29, 23);
            this.lColumnCaption.Name = "lColumnCaption";
            this.lColumnCaption.Size = new System.Drawing.Size(103, 13);
            this.lColumnCaption.TabIndex = 3;
            this.lColumnCaption.Text = "Столбец описания:";
            // 
            // lColumnTag
            // 
            this.lColumnTag.AutoSize = true;
            this.lColumnTag.Location = new System.Drawing.Point(52, 63);
            this.lColumnTag.Name = "lColumnTag";
            this.lColumnTag.Size = new System.Drawing.Size(77, 13);
            this.lColumnTag.TabIndex = 2;
            this.lColumnTag.Text = "Столбец тега:";
            // 
            // nColumnTag
            // 
            this.nColumnTag.Location = new System.Drawing.Point(137, 60);
            this.nColumnTag.Name = "nColumnTag";
            this.nColumnTag.Size = new System.Drawing.Size(120, 20);
            this.nColumnTag.TabIndex = 1;
            this.nColumnTag.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(270, 23);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(226, 56);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Открыть файл Excel";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            // 
            // dgParameters
            // 
            this.dgParameters.AllowUserToAddRows = false;
            this.dgParameters.AllowUserToDeleteRows = false;
            this.dgParameters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgParameters.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgParameters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgParameters.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgParameters.Location = new System.Drawing.Point(3, 113);
            this.dgParameters.Name = "dgParameters";
            this.dgParameters.ReadOnly = true;
            this.dgParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgParameters.Size = new System.Drawing.Size(853, 359);
            this.dgParameters.TabIndex = 1;
            // 
            // MNA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 481);
            this.Controls.Add(this.tlMain);
            this.Name = "MNA";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tlMain.ResumeLayout(false);
            this.tlRight.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nColumnCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nColumnTag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgParameters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lbMnaList;
        private System.Windows.Forms.TableLayoutPanel tlRight;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.DataGridView dgParameters;
        private System.Windows.Forms.TableLayoutPanel tlMain;
        private System.Windows.Forms.NumericUpDown nColumnCaption;
        private System.Windows.Forms.Label lColumnCaption;
        private System.Windows.Forms.Label lColumnTag;
        private System.Windows.Forms.NumericUpDown nColumnTag;
        private System.Windows.Forms.Button _saveButton;
    }
}

