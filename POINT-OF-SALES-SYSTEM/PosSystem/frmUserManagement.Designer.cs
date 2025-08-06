
namespace PosSystem
{
    partial class frmUserManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserManagement));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblUserType = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.cmbUserType = new System.Windows.Forms.ComboBox();
            this.lblPasswod = new System.Windows.Forms.Label();
            this.lblEntry = new System.Windows.Forms.Label();
            this.radioEntry = new MetroFramework.Controls.MetroRadioButton();
            this.radioUpdate = new MetroFramework.Controls.MetroRadioButton();
            this.panelRadio = new MetroFramework.Controls.MetroPanel();
            this.radioDelete = new MetroFramework.Controls.MetroRadioButton();
            this.btnLoadUser = new MetroFramework.Controls.MetroButton();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblDelete = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelRadio.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(132)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.labelTitle);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 38);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(415, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(16, 8);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(113, 21);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Manage Users";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(66, 165);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(85, 20);
            this.lblUserName.TabIndex = 3;
            this.lblUserName.Text = "User Name";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(166, 162);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(169, 27);
            this.txtUserName.TabIndex = 1;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(75, 254);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(76, 20);
            this.lblFirstName.TabIndex = 3;
            this.lblFirstName.Text = "Firstname";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(166, 295);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(169, 27);
            this.txtLastName.TabIndex = 4;
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Location = new System.Drawing.Point(75, 210);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(76, 20);
            this.lblUserType.TabIndex = 3;
            this.lblUserType.Text = "User Type";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(226, 440);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(134, 30);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(78, 298);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(73, 20);
            this.lblLastName.TabIndex = 16;
            this.lblLastName.Text = "Lastname";
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(19, 387);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(132, 20);
            this.lblConfirmPassword.TabIndex = 18;
            this.lblConfirmPassword.Text = "Confirm Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(166, 340);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(169, 27);
            this.txtPassword.TabIndex = 5;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(166, 251);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(169, 27);
            this.txtFirstName.TabIndex = 3;
            // 
            // cmbUserType
            // 
            this.cmbUserType.FormattingEnabled = true;
            this.cmbUserType.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cmbUserType.Location = new System.Drawing.Point(166, 207);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.Size = new System.Drawing.Size(169, 28);
            this.cmbUserType.TabIndex = 2;
            // 
            // lblPasswod
            // 
            this.lblPasswod.AutoSize = true;
            this.lblPasswod.Location = new System.Drawing.Point(78, 343);
            this.lblPasswod.Name = "lblPasswod";
            this.lblPasswod.Size = new System.Drawing.Size(73, 20);
            this.lblPasswod.TabIndex = 25;
            this.lblPasswod.Text = "Password";
            // 
            // lblEntry
            // 
            this.lblEntry.AutoSize = true;
            this.lblEntry.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntry.Location = new System.Drawing.Point(154, 50);
            this.lblEntry.Name = "lblEntry";
            this.lblEntry.Size = new System.Drawing.Size(125, 28);
            this.lblEntry.TabIndex = 26;
            this.lblEntry.Text = "USER ENTRY";
            // 
            // radioEntry
            // 
            this.radioEntry.AutoSize = true;
            this.radioEntry.BackColor = System.Drawing.Color.Gray;
            this.radioEntry.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.radioEntry.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.radioEntry.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioEntry.Location = new System.Drawing.Point(12, 10);
            this.radioEntry.Name = "radioEntry";
            this.radioEntry.Size = new System.Drawing.Size(69, 19);
            this.radioEntry.TabIndex = 27;
            this.radioEntry.Text = "ENTRY";
            this.radioEntry.UseSelectable = true;
            this.radioEntry.CheckedChanged += new System.EventHandler(this.radioEntry_CheckedChanged);
            // 
            // radioUpdate
            // 
            this.radioUpdate.AutoSize = true;
            this.radioUpdate.BackColor = System.Drawing.Color.Gray;
            this.radioUpdate.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.radioUpdate.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.radioUpdate.Location = new System.Drawing.Point(88, 10);
            this.radioUpdate.Name = "radioUpdate";
            this.radioUpdate.Size = new System.Drawing.Size(78, 19);
            this.radioUpdate.TabIndex = 28;
            this.radioUpdate.Text = "UPDATE";
            this.radioUpdate.UseSelectable = true;
            this.radioUpdate.CheckedChanged += new System.EventHandler(this.radioUpdate_CheckedChanged);
            // 
            // panelRadio
            // 
            this.panelRadio.BackColor = System.Drawing.Color.Snow;
            this.panelRadio.Controls.Add(this.radioDelete);
            this.panelRadio.Controls.Add(this.radioUpdate);
            this.panelRadio.Controls.Add(this.radioEntry);
            this.panelRadio.HorizontalScrollbarBarColor = true;
            this.panelRadio.HorizontalScrollbarHighlightOnWheel = false;
            this.panelRadio.HorizontalScrollbarSize = 10;
            this.panelRadio.Location = new System.Drawing.Point(77, 96);
            this.panelRadio.Name = "panelRadio";
            this.panelRadio.Size = new System.Drawing.Size(258, 44);
            this.panelRadio.TabIndex = 29;
            this.panelRadio.VerticalScrollbarBarColor = true;
            this.panelRadio.VerticalScrollbarHighlightOnWheel = false;
            this.panelRadio.VerticalScrollbarSize = 10;
            // 
            // radioDelete
            // 
            this.radioDelete.AutoSize = true;
            this.radioDelete.BackColor = System.Drawing.Color.Gray;
            this.radioDelete.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.radioDelete.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.radioDelete.Location = new System.Drawing.Point(175, 10);
            this.radioDelete.Name = "radioDelete";
            this.radioDelete.Size = new System.Drawing.Size(71, 19);
            this.radioDelete.TabIndex = 29;
            this.radioDelete.Text = "DELETE";
            this.radioDelete.UseSelectable = true;
            this.radioDelete.CheckedChanged += new System.EventHandler(this.radioDelete_CheckedChanged);
            // 
            // btnLoadUser
            // 
            this.btnLoadUser.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnLoadUser.Location = new System.Drawing.Point(341, 162);
            this.btnLoadUser.Name = "btnLoadUser";
            this.btnLoadUser.Size = new System.Drawing.Size(84, 27);
            this.btnLoadUser.TabIndex = 2;
            this.btnLoadUser.Text = "Load User";
            this.btnLoadUser.UseSelectable = true;
            this.btnLoadUser.Click += new System.EventHandler(this.btnLoadItem_Click);
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdate.Location = new System.Drawing.Point(154, 50);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(138, 28);
            this.lblUpdate.TabIndex = 31;
            this.lblUpdate.Text = "USER UPDATE";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightCoral;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(80, 440);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(129, 30);
            this.btnClear.TabIndex = 32;
            this.btnClear.Text = "Clear Fields";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblDelete
            // 
            this.lblDelete.AutoSize = true;
            this.lblDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDelete.Location = new System.Drawing.Point(154, 50);
            this.lblDelete.Name = "lblDelete";
            this.lblDelete.Size = new System.Drawing.Size(130, 28);
            this.lblDelete.TabIndex = 33;
            this.lblDelete.Text = "USER DELETE";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(226, 440);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(134, 30);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(166, 384);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(169, 27);
            this.txtConfirmPassword.TabIndex = 6;
            // 
            // frmUserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(445, 498);
            this.ControlBox = false;
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblUpdate);
            this.Controls.Add(this.btnLoadUser);
            this.Controls.Add(this.panelRadio);
            this.Controls.Add(this.lblEntry);
            this.Controls.Add(this.lblPasswod);
            this.Controls.Add(this.cmbUserType);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblUserType);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmUserManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelRadio.ResumeLayout(false);
            this.panelRadio.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblFirstName;
        public System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblUserType;
        public System.Windows.Forms.Button btnSubmit;
        public System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblConfirmPassword;
        public System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.ComboBox cmbUserType;
        private System.Windows.Forms.Label lblPasswod;
        private System.Windows.Forms.Label lblEntry;
        private MetroFramework.Controls.MetroRadioButton radioEntry;
        private MetroFramework.Controls.MetroRadioButton radioUpdate;
        private MetroFramework.Controls.MetroPanel panelRadio;
        private MetroFramework.Controls.MetroButton btnLoadUser;
        private System.Windows.Forms.Label lblUpdate;
        public System.Windows.Forms.Button btnClear;
        private MetroFramework.Controls.MetroRadioButton radioDelete;
        private System.Windows.Forms.Label lblDelete;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.TextBox txtConfirmPassword;
    }
}