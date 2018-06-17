namespace DuplicateRecordsProject
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lstDatabaseNames = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdCheckForDuplicates = new System.Windows.Forms.Button();
            this.lstTableNames = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clbColumns = new System.Windows.Forms.CheckedListBox();
            this.cboOrderBy = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstDatabaseNames
            // 
            this.lstDatabaseNames.FormattingEnabled = true;
            this.lstDatabaseNames.Location = new System.Drawing.Point(12, 38);
            this.lstDatabaseNames.Name = "lstDatabaseNames";
            this.lstDatabaseNames.Size = new System.Drawing.Size(218, 316);
            this.lstDatabaseNames.TabIndex = 1;
            this.lstDatabaseNames.SelectedIndexChanged += new System.EventHandler(this.lstDatabaseNames_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdCheckForDuplicates);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 50);
            this.panel1.TabIndex = 2;
            // 
            // cmdCheckForDuplicates
            // 
            this.cmdCheckForDuplicates.Location = new System.Drawing.Point(12, 12);
            this.cmdCheckForDuplicates.Name = "cmdCheckForDuplicates";
            this.cmdCheckForDuplicates.Size = new System.Drawing.Size(137, 23);
            this.cmdCheckForDuplicates.TabIndex = 7;
            this.cmdCheckForDuplicates.Text = "Duplicate check";
            this.cmdCheckForDuplicates.UseVisualStyleBackColor = true;
            this.cmdCheckForDuplicates.Click += new System.EventHandler(this.cmdCheckForDuplicates_Click);
            // 
            // lstTableNames
            // 
            this.lstTableNames.FormattingEnabled = true;
            this.lstTableNames.Location = new System.Drawing.Point(247, 38);
            this.lstTableNames.Name = "lstTableNames";
            this.lstTableNames.Size = new System.Drawing.Size(177, 316);
            this.lstTableNames.TabIndex = 3;
            this.lstTableNames.SelectedIndexChanged += new System.EventHandler(this.lstTableNames_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Available databases";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tables for selected database";
            // 
            // clbColumns
            // 
            this.clbColumns.FormattingEnabled = true;
            this.clbColumns.Location = new System.Drawing.Point(441, 38);
            this.clbColumns.Name = "clbColumns";
            this.clbColumns.Size = new System.Drawing.Size(194, 319);
            this.clbColumns.TabIndex = 6;
            // 
            // cboOrderBy
            // 
            this.cboOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderBy.FormattingEnabled = true;
            this.cboOrderBy.Location = new System.Drawing.Point(441, 363);
            this.cboOrderBy.Name = "cboOrderBy";
            this.cboOrderBy.Size = new System.Drawing.Size(194, 21);
            this.cboOrderBy.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(438, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Columns for selected table";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(388, 366);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Order by";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboOrderBy);
            this.Controls.Add(this.clbColumns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstTableNames);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lstDatabaseNames);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code sample";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstDatabaseNames;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lstTableNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbColumns;
        private System.Windows.Forms.Button cmdCheckForDuplicates;
        private System.Windows.Forms.ComboBox cboOrderBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

