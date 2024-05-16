namespace QuanLyCuaHangQuanAo.SHOP
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrandCategory = new System.Windows.Forms.Button();
            this.lblUserType = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.btnMain = new System.Windows.Forms.Button();
            this.btnAccont = new System.Windows.Forms.Button();
            this.btnBill = new System.Windows.Forms.Button();
            this.btnCloth = new System.Windows.Forms.Button();
            this.sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            this.userHome1 = new QuanLyCuaHangQuanAo.SHOP.UserHome();
            this.userBill1 = new QuanLyCuaHangQuanAo.SHOP.UserBill();
            this.userUser1 = new QuanLyCuaHangQuanAo.SHOP.UserUser();
            this.userControlBrandCategory1 = new QuanLyCuaHangQuanAo.SHOP.UserControlBrandCategory();
            this.userCloth1 = new QuanLyCuaHangQuanAo.SHOP.UserCloth();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnBrandCategory);
            this.panel1.Controls.Add(this.lblUserType);
            this.panel1.Controls.Add(this.lblUsername);
            this.panel1.Controls.Add(this.btnMain);
            this.panel1.Controls.Add(this.btnAccont);
            this.panel1.Controls.Add(this.btnBill);
            this.panel1.Controls.Add(this.btnCloth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 944);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(33, 887);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chức Vụ:";
            // 
            // btnBrandCategory
            // 
            this.btnBrandCategory.Location = new System.Drawing.Point(8, 124);
            this.btnBrandCategory.Name = "btnBrandCategory";
            this.btnBrandCategory.Size = new System.Drawing.Size(250, 50);
            this.btnBrandCategory.TabIndex = 0;
            this.btnBrandCategory.Text = "The Loai/ Hang";
            this.btnBrandCategory.UseVisualStyleBackColor = true;
            this.btnBrandCategory.Click += new System.EventHandler(this.btnBrandCategory_Click);
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserType.Location = new System.Drawing.Point(147, 887);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(43, 29);
            this.lblUserType.TabIndex = 0;
            this.lblUserType.Text = "{?}";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(100, 833);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(41, 29);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "(?)";
            // 
            // btnMain
            // 
            this.btnMain.Location = new System.Drawing.Point(10, 12);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(250, 50);
            this.btnMain.TabIndex = 0;
            this.btnMain.Text = "Trang Chủ";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // btnAccont
            // 
            this.btnAccont.Location = new System.Drawing.Point(10, 236);
            this.btnAccont.Name = "btnAccont";
            this.btnAccont.Size = new System.Drawing.Size(250, 50);
            this.btnAccont.TabIndex = 0;
            this.btnAccont.Text = "Quan Ly";
            this.btnAccont.UseVisualStyleBackColor = true;
            this.btnAccont.Click += new System.EventHandler(this.btnAccont_Click);
            // 
            // btnBill
            // 
            this.btnBill.Location = new System.Drawing.Point(10, 180);
            this.btnBill.Name = "btnBill";
            this.btnBill.Size = new System.Drawing.Size(250, 50);
            this.btnBill.TabIndex = 0;
            this.btnBill.Text = "Hoa Don";
            this.btnBill.UseVisualStyleBackColor = true;
            this.btnBill.Click += new System.EventHandler(this.btnBill_Click);
            // 
            // btnCloth
            // 
            this.btnCloth.Location = new System.Drawing.Point(10, 68);
            this.btnCloth.Name = "btnCloth";
            this.btnCloth.Size = new System.Drawing.Size(250, 50);
            this.btnCloth.TabIndex = 0;
            this.btnCloth.Text = "Quan Ao";
            this.btnCloth.UseVisualStyleBackColor = true;
            this.btnCloth.Click += new System.EventHandler(this.btnCloth_Click);
            // 
            // userHome1
            // 
            this.userHome1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.userHome1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userHome1.Location = new System.Drawing.Point(266, 0);
            this.userHome1.Name = "userHome1";
            this.userHome1.Size = new System.Drawing.Size(1212, 944);
            this.userHome1.TabIndex = 4;
            // 
            // userBill1
            // 
            this.userBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userBill1.Location = new System.Drawing.Point(266, 0);
            this.userBill1.Name = "userBill1";
            this.userBill1.Size = new System.Drawing.Size(1212, 944);
            this.userBill1.TabIndex = 3;
            // 
            // userUser1
            // 
            this.userUser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userUser1.Location = new System.Drawing.Point(266, 0);
            this.userUser1.Name = "userUser1";
            this.userUser1.Size = new System.Drawing.Size(1212, 944);
            this.userUser1.TabIndex = 0;
            // 
            // userControlBrandCategory1
            // 
            this.userControlBrandCategory1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlBrandCategory1.Location = new System.Drawing.Point(266, 0);
            this.userControlBrandCategory1.Name = "userControlBrandCategory1";
            this.userControlBrandCategory1.Size = new System.Drawing.Size(1212, 944);
            this.userControlBrandCategory1.TabIndex = 2;
            // 
            // userCloth1
            // 
            this.userCloth1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userCloth1.Location = new System.Drawing.Point(266, 0);
            this.userCloth1.Name = "userCloth1";
            this.userCloth1.Size = new System.Drawing.Size(1212, 944);
            this.userCloth1.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1478, 944);
            this.Controls.Add(this.userHome1);
            this.Controls.Add(this.userBill1);
            this.Controls.Add(this.userUser1);
            this.Controls.Add(this.userControlBrandCategory1);
            this.Controls.Add(this.userCloth1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCloth;
        private System.Windows.Forms.Button btnAccont;
        private System.Windows.Forms.Button btnBill;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
        private UserCloth userCloth1;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblUserType;
        private System.Windows.Forms.Button btnBrandCategory;
        private UserControlBrandCategory userControlBrandCategory1;
        private UserUser userUser1;
        private UserBill userBill1;
        private UserHome userHome1;
        private System.Windows.Forms.Label label1;
    }
}