namespace AI_Assistant
{
    partial class frmPromptSettings
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
            lblPrompts = new Label();
            lstPrompts = new ListBox();
            lblDescription = new Label();
            lblInstruction = new Label();
            txtInstruction = new TextBox();
            lblTemplate = new Label();
            txtTemplate = new TextBox();
            lblBasePrompt = new Label();
            txtBasePrompt = new TextBox();
            btnReset = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            //
            // lblPrompts
            //
            lblPrompts.AutoSize = true;
            lblPrompts.Location = new Point(20, 15);
            lblPrompts.Name = "lblPrompts";
            lblPrompts.Size = new Size(84, 25);
            lblPrompts.TabIndex = 0;
            lblPrompts.Text = "Prompts:";
            //
            // lstPrompts
            //
            lstPrompts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstPrompts.FormattingEnabled = true;
            lstPrompts.ItemHeight = 25;
            lstPrompts.Location = new Point(20, 50);
            lstPrompts.Name = "lstPrompts";
            lstPrompts.Size = new Size(260, 654);
            lstPrompts.TabIndex = 1;
            lstPrompts.SelectedIndexChanged += lstPrompts_SelectedIndexChanged;
            //
            // lblDescription
            //
            lblDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDescription.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDescription.ForeColor = Color.DimGray;
            lblDescription.Location = new Point(300, 15);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(680, 30);
            lblDescription.TabIndex = 2;
            //
            // lblInstruction
            //
            lblInstruction.AutoSize = true;
            lblInstruction.Location = new Point(300, 55);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(470, 25);
            lblInstruction.TabIndex = 3;
            lblInstruction.Text = "System instruction (added after the base system prompt):";
            //
            // txtInstruction
            //
            txtInstruction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtInstruction.Location = new Point(300, 85);
            txtInstruction.Multiline = true;
            txtInstruction.Name = "txtInstruction";
            txtInstruction.ScrollBars = ScrollBars.Vertical;
            txtInstruction.Size = new Size(680, 140);
            txtInstruction.TabIndex = 4;
            //
            // lblTemplate
            //
            lblTemplate.AutoSize = true;
            lblTemplate.Location = new Point(300, 240);
            lblTemplate.Name = "lblTemplate";
            lblTemplate.Size = new Size(641, 25);
            lblTemplate.TabIndex = 5;
            lblTemplate.Text = "User prompt template  ({text} = your text, {language}, {title}):";
            //
            // txtTemplate
            //
            txtTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTemplate.Location = new Point(300, 270);
            txtTemplate.Multiline = true;
            txtTemplate.Name = "txtTemplate";
            txtTemplate.ScrollBars = ScrollBars.Vertical;
            txtTemplate.Size = new Size(680, 140);
            txtTemplate.TabIndex = 6;
            //
            // lblBasePrompt
            //
            lblBasePrompt.AutoSize = true;
            lblBasePrompt.Location = new Point(300, 425);
            lblBasePrompt.Name = "lblBasePrompt";
            lblBasePrompt.Size = new Size(446, 25);
            lblBasePrompt.TabIndex = 7;
            lblBasePrompt.Text = "Base system prompt (shared by all prompts):";
            //
            // txtBasePrompt
            //
            txtBasePrompt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtBasePrompt.Location = new Point(300, 455);
            txtBasePrompt.Multiline = true;
            txtBasePrompt.Name = "txtBasePrompt";
            txtBasePrompt.ScrollBars = ScrollBars.Vertical;
            txtBasePrompt.Size = new Size(680, 249);
            txtBasePrompt.TabIndex = 8;
            //
            // btnReset
            //
            btnReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReset.Location = new Point(20, 725);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(220, 42);
            btnReset.TabIndex = 9;
            btnReset.Text = "Reset to Defaults";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            //
            // btnSave
            //
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(637, 725);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(153, 42);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            //
            // btnCancel
            //
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(827, 725);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(153, 42);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            //
            // frmPromptSettings
            //
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(1000, 790);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(btnReset);
            Controls.Add(txtBasePrompt);
            Controls.Add(lblBasePrompt);
            Controls.Add(txtTemplate);
            Controls.Add(lblTemplate);
            Controls.Add(txtInstruction);
            Controls.Add(lblInstruction);
            Controls.Add(lblDescription);
            Controls.Add(lstPrompts);
            Controls.Add(lblPrompts);
            MinimizeBox = false;
            Name = "frmPromptSettings";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Prompt Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPrompts;
        private ListBox lstPrompts;
        private Label lblDescription;
        private Label lblInstruction;
        private TextBox txtInstruction;
        private Label lblTemplate;
        private TextBox txtTemplate;
        private Label lblBasePrompt;
        private TextBox txtBasePrompt;
        private Button btnReset;
        private Button btnSave;
        private Button btnCancel;
    }
}
