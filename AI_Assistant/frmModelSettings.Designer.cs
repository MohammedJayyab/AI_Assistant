namespace AI_Assistant
{
    partial class frmModelSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModelSettings));
            groupBox1 = new GroupBox();
            txtMaxMessages = new TextBox();
            txtDefaultTemperature = new TextBox();
            txtDefaultMaxTokens = new TextBox();
            label5 = new Label();
            cmbReasoningEffort = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            cmbDefaultLLM = new ComboBox();
            label1 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            btnOpenJson = new Button();
            btnLoad = new Button();
            btnEditPrompts = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbReasoningEffort);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtMaxMessages);
            groupBox1.Controls.Add(txtDefaultTemperature);
            groupBox1.Controls.Add(txtDefaultMaxTokens);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cmbDefaultLLM);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(23, 30);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(625, 306);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "LLM Setting ::Json";
            // 
            // txtMaxMessages
            // 
            txtMaxMessages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtMaxMessages.Location = new Point(236, 232);
            txtMaxMessages.Name = "txtMaxMessages";
            txtMaxMessages.Size = new Size(230, 31);
            txtMaxMessages.TabIndex = 7;
            // 
            // txtDefaultTemperature
            // 
            txtDefaultTemperature.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtDefaultTemperature.Location = new Point(236, 138);
            txtDefaultTemperature.Name = "txtDefaultTemperature";
            txtDefaultTemperature.Size = new Size(230, 31);
            txtDefaultTemperature.TabIndex = 5;
            // 
            // txtDefaultMaxTokens
            // 
            txtDefaultMaxTokens.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtDefaultMaxTokens.Location = new Point(236, 91);
            txtDefaultMaxTokens.Name = "txtDefaultMaxTokens";
            txtDefaultMaxTokens.Size = new Size(230, 31);
            txtDefaultMaxTokens.TabIndex = 4;
            // 
            // cmbReasoningEffort
            //
            cmbReasoningEffort.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbReasoningEffort.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReasoningEffort.FlatStyle = FlatStyle.System;
            cmbReasoningEffort.FormattingEnabled = true;
            cmbReasoningEffort.Items.AddRange(new object[] { "Default", "None", "Low", "Medium", "High" });
            cmbReasoningEffort.Location = new Point(236, 183);
            cmbReasoningEffort.Name = "cmbReasoningEffort";
            cmbReasoningEffort.Size = new Size(233, 33);
            cmbReasoningEffort.TabIndex = 6;
            //
            // label5
            //
            label5.AutoSize = true;
            label5.Location = new Point(81, 186);
            label5.Name = "label5";
            label5.Size = new Size(132, 25);
            label5.TabIndex = 3;
            label5.Text = "Thinking Level:";
            //
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(81, 235);
            label4.Name = "label4";
            label4.Size = new Size(132, 25);
            label4.TabIndex = 3;
            label4.Text = "Max Messages:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(99, 141);
            label3.Name = "label3";
            label3.Size = new Size(114, 25);
            label3.TabIndex = 3;
            label3.Text = "Temperature:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(113, 94);
            label2.Name = "label2";
            label2.Size = new Size(100, 25);
            label2.TabIndex = 2;
            label2.Text = "Max Token:";
            // 
            // cmbDefaultLLM
            // 
            cmbDefaultLLM.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbDefaultLLM.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDefaultLLM.FlatStyle = FlatStyle.System;
            cmbDefaultLLM.FormattingEnabled = true;
            cmbDefaultLLM.Location = new Point(236, 42);
            cmbDefaultLLM.Name = "cmbDefaultLLM";
            cmbDefaultLLM.Size = new Size(233, 33);
            cmbDefaultLLM.TabIndex = 1;
            cmbDefaultLLM.SelectedIndexChanged += cmbDefaultLLM_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(69, 44);
            label1.Name = "label1";
            label1.Size = new Size(144, 25);
            label1.TabIndex = 0;
            label1.Text = "Default Provider:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(485, 351);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(153, 42);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(485, 399);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(153, 42);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOpenJson
            // 
            btnOpenJson.Location = new Point(182, 351);
            btnOpenJson.Name = "btnOpenJson";
            btnOpenJson.Size = new Size(153, 42);
            btnOpenJson.TabIndex = 3;
            btnOpenJson.Text = "Edit Json File";
            btnOpenJson.UseVisualStyleBackColor = true;
            btnOpenJson.Click += btnOpenJson_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(23, 351);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(153, 42);
            btnLoad.TabIndex = 4;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            //
            // btnEditPrompts
            //
            btnEditPrompts.Location = new Point(23, 399);
            btnEditPrompts.Name = "btnEditPrompts";
            btnEditPrompts.Size = new Size(312, 42);
            btnEditPrompts.TabIndex = 5;
            btnEditPrompts.Text = "Edit Prompts";
            btnEditPrompts.UseVisualStyleBackColor = true;
            btnEditPrompts.Click += btnEditPrompts_Click;
            //
            // frmModelSettings
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(669, 479);
            Controls.Add(btnEditPrompts);
            Controls.Add(btnLoad);
            Controls.Add(btnOpenJson);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmModelSettings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Model Settings";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnSave;
        private Button btnCancel;
        private Label label3;
        private Label label2;
        private ComboBox cmbDefaultLLM;
        private Label label1;
        private TextBox txtMaxMessages;
        private TextBox txtDefaultTemperature;
        private TextBox txtDefaultMaxTokens;
        private Label label4;
        private Label label5;
        private ComboBox cmbReasoningEffort;
        private Button btnOpenJson;
        private Button btnLoad;
        private Button btnEditPrompts;
    }
}