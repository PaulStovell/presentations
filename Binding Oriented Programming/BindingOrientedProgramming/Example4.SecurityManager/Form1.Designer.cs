namespace WindowsApplication9 {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            WindowsApplication9.SecurityManager securityManager1 = new WindowsApplication9.SecurityManager();
            this.adminPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.securityManagerBindingSource1 = new WindowsApplication9.SecurityManagerBindingSource();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.adminPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.securityManagerBindingSource1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // adminPanel
            // 
            this.adminPanel.Controls.Add(this.label1);
            this.adminPanel.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.securityManagerBindingSource1, "IsAdministrator", true));
            this.adminPanel.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.securityManagerBindingSource1, "IsAdministrator", true));
            this.adminPanel.Location = new System.Drawing.Point(35, 29);
            this.adminPanel.Name = "adminPanel";
            this.adminPanel.Size = new System.Drawing.Size(209, 44);
            this.adminPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This panel is only visible to Admins";
            // 
            // securityManagerBindingSource1
            // 
            this.securityManagerBindingSource1.DataSource = securityManager1;
            this.securityManagerBindingSource1.Position = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(35, 97);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(209, 44);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "This panel is visible to anyone";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 413);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.adminPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.adminPanel.ResumeLayout(false);
            this.adminPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.securityManagerBindingSource1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel adminPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private SecurityManagerBindingSource securityManagerBindingSource1;
    }
}

