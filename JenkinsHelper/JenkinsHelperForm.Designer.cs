namespace JenkinsHelper
{
    partial class JenkinsHelperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JenkinsHelperForm));
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAction = new System.Windows.Forms.ComboBox();
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGitRepository = new System.Windows.Forms.TextBox();
            this.txtTenantName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBackupPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkOnDemand = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbBranch
            // 
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Location = new System.Drawing.Point(92, 13);
            this.cbBranch.Name = "cbBranch";
            this.cbBranch.Size = new System.Drawing.Size(237, 21);
            this.cbBranch.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Branch";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Action";
            // 
            // cbAction
            // 
            this.cbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAction.FormattingEnabled = true;
            this.cbAction.Location = new System.Drawing.Point(92, 40);
            this.cbAction.Name = "cbAction";
            this.cbAction.Size = new System.Drawing.Size(237, 21);
            this.cbAction.TabIndex = 3;
            this.cbAction.SelectedIndexChanged += new System.EventHandler(this.cbAction_SelectedIndexChanged);
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(254, 200);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 4;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_ClickAsync);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(173, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Git repository";
            // 
            // txtGitRepository
            // 
            this.txtGitRepository.Location = new System.Drawing.Point(92, 67);
            this.txtGitRepository.Name = "txtGitRepository";
            this.txtGitRepository.Size = new System.Drawing.Size(237, 20);
            this.txtGitRepository.TabIndex = 7;
            // 
            // txtTenantName
            // 
            this.txtTenantName.Location = new System.Drawing.Point(92, 93);
            this.txtTenantName.Name = "txtTenantName";
            this.txtTenantName.Size = new System.Drawing.Size(237, 20);
            this.txtTenantName.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tenant name";
            // 
            // txtBackupPath
            // 
            this.txtBackupPath.Location = new System.Drawing.Point(92, 119);
            this.txtBackupPath.Name = "txtBackupPath";
            this.txtBackupPath.Size = new System.Drawing.Size(237, 20);
            this.txtBackupPath.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Backup path";
            // 
            // chkOnDemand
            // 
            this.chkOnDemand.AutoSize = true;
            this.chkOnDemand.Location = new System.Drawing.Point(161, 163);
            this.chkOnDemand.Name = "chkOnDemand";
            this.chkOnDemand.Size = new System.Drawing.Size(168, 17);
            this.chkOnDemand.TabIndex = 12;
            this.chkOnDemand.Text = "Launch an On Demand build?";
            this.chkOnDemand.UseVisualStyleBackColor = true;
            // 
            // JenkinsHelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 235);
            this.Controls.Add(this.chkOnDemand);
            this.Controls.Add(this.txtBackupPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTenantName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGitRepository);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.cbAction);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbBranch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JenkinsHelperForm";
            this.Text = "Jenkins Helper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbAction;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGitRepository;
        private System.Windows.Forms.TextBox txtTenantName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBackupPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkOnDemand;
    }
}