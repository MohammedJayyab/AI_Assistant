namespace AI_Assistant
{
    partial class frmAssistant
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssistant));
            btnSend = new Button();
            label1 = new Label();
            lblLLM = new Label();
            txtPrompt = new TextBox();
            btnNew = new Button();
            pnlHeader = new Panel();
            btnSettings = new Button();
            btnPrompt = new Button();
            radioDe = new RadioButton();
            radioEn = new RadioButton();
            radioAr = new RadioButton();
            btnEmailResponse = new Button();
            btnOnlyKeyPoints = new Button();
            btnExplain = new Button();
            btnTranslate = new Button();
            btnSummarize = new Button();
            txtImageUrl = new TextBox();
            pnlBody = new Panel();
            pnlFooter = new Panel();
            panel1 = new Panel();
            label2 = new Label();
            btnAddImage = new Button();
            imageList1 = new ImageList(components);
            pnlHeader.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlFooter.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSend.Location = new Point(4, 4);
            btnSend.Margin = new Padding(4, 4, 4, 4);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(221, 77);
            btnSend.TabIndex = 0;
            btnSend.Text = "Send >";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Left;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(4, 4);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(90, 51);
            label1.TabIndex = 1;
            label1.Text = "Ask:";
            // 
            // lblLLM
            // 
            lblLLM.AutoSize = true;
            lblLLM.Dock = DockStyle.Top;
            lblLLM.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLLM.Location = new Point(6, 6);
            lblLLM.Margin = new Padding(4, 0, 4, 0);
            lblLLM.Name = "lblLLM";
            lblLLM.Size = new Size(88, 32);
            lblLLM.TabIndex = 1;
            lblLLM.Text = "Model:";
            // 
            // txtPrompt
            // 
            txtPrompt.BackColor = SystemColors.Info;
            txtPrompt.Dock = DockStyle.Fill;
            txtPrompt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrompt.Location = new Point(94, 4);
            txtPrompt.Margin = new Padding(4, 4, 4, 4);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.ScrollBars = ScrollBars.Vertical;
            txtPrompt.Size = new Size(1471, 279);
            txtPrompt.TabIndex = 3;
            txtPrompt.TextChanged += txtPrompt_TextChanged;
            txtPrompt.KeyDown += txtPrompt_KeyDown;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(16, 20);
            btnNew.Margin = new Padding(4, 4, 4, 4);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(240, 67);
            btnNew.TabIndex = 4;
            btnNew.Text = "+ New Chat";
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.AutoScroll = true;
            pnlHeader.BackColor = Color.FromArgb(224, 224, 224);
            pnlHeader.Controls.Add(btnSettings);
            pnlHeader.Controls.Add(btnPrompt);
            pnlHeader.Controls.Add(radioDe);
            pnlHeader.Controls.Add(radioEn);
            pnlHeader.Controls.Add(radioAr);
            pnlHeader.Controls.Add(btnEmailResponse);
            pnlHeader.Controls.Add(btnOnlyKeyPoints);
            pnlHeader.Controls.Add(btnExplain);
            pnlHeader.Controls.Add(btnTranslate);
            pnlHeader.Controls.Add(btnSummarize);
            pnlHeader.Controls.Add(btnNew);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(4, 4);
            pnlHeader.Margin = new Padding(4, 4, 4, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1794, 207);
            pnlHeader.TabIndex = 6;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSettings.Image = Properties.Resources.settings;
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(1585, 20);
            btnSettings.Margin = new Padding(4, 4, 4, 4);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(187, 67);
            btnSettings.TabIndex = 21;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnPrompt
            // 
            btnPrompt.Location = new Point(1364, 20);
            btnPrompt.Margin = new Padding(4, 4, 4, 4);
            btnPrompt.Name = "btnPrompt";
            btnPrompt.Size = new Size(203, 67);
            btnPrompt.TabIndex = 16;
            btnPrompt.Text = "LLM Prompt </>";
            btnPrompt.UseVisualStyleBackColor = true;
            btnPrompt.Click += btnPrompt_Click;
            // 
            // radioDe
            // 
            radioDe.AutoSize = true;
            radioDe.Location = new Point(655, 95);
            radioDe.Margin = new Padding(4, 4, 4, 4);
            radioDe.Name = "radioDe";
            radioDe.Size = new Size(74, 36);
            radioDe.TabIndex = 15;
            radioDe.TabStop = true;
            radioDe.Text = "DE";
            radioDe.UseVisualStyleBackColor = true;
            radioDe.CheckedChanged += radioDe_CheckedChanged;
            // 
            // radioEn
            // 
            radioEn.AutoSize = true;
            radioEn.Location = new Point(576, 95);
            radioEn.Margin = new Padding(4, 4, 4, 4);
            radioEn.Name = "radioEn";
            radioEn.Size = new Size(71, 36);
            radioEn.TabIndex = 14;
            radioEn.TabStop = true;
            radioEn.Text = "En";
            radioEn.UseVisualStyleBackColor = true;
            radioEn.CheckedChanged += radioEn_CheckedChanged;
            // 
            // radioAr
            // 
            radioAr.AutoSize = true;
            radioAr.Checked = true;
            radioAr.Location = new Point(497, 95);
            radioAr.Margin = new Padding(4, 4, 4, 4);
            radioAr.Name = "radioAr";
            radioAr.Size = new Size(68, 36);
            radioAr.TabIndex = 13;
            radioAr.TabStop = true;
            radioAr.Text = "Ar";
            radioAr.UseVisualStyleBackColor = true;
            radioAr.CheckedChanged += radioAr_CheckedChanged;
            // 
            // btnEmailResponse
            // 
            btnEmailResponse.Location = new Point(1148, 20);
            btnEmailResponse.Margin = new Padding(4, 4, 4, 4);
            btnEmailResponse.Name = "btnEmailResponse";
            btnEmailResponse.Size = new Size(203, 67);
            btnEmailResponse.TabIndex = 11;
            btnEmailResponse.Text = "Response Email";
            btnEmailResponse.UseVisualStyleBackColor = true;
            btnEmailResponse.Click += btnEmailResponse_Click;
            // 
            // btnOnlyKeyPoints
            // 
            btnOnlyKeyPoints.Location = new Point(928, 20);
            btnOnlyKeyPoints.Margin = new Padding(4, 4, 4, 4);
            btnOnlyKeyPoints.Name = "btnOnlyKeyPoints";
            btnOnlyKeyPoints.Size = new Size(203, 67);
            btnOnlyKeyPoints.TabIndex = 9;
            btnOnlyKeyPoints.Text = "KeyPoints";
            btnOnlyKeyPoints.UseVisualStyleBackColor = true;
            btnOnlyKeyPoints.Click += btnOnlyKeyPoints_Click;
            // 
            // btnExplain
            // 
            btnExplain.Location = new Point(712, 20);
            btnExplain.Margin = new Padding(4, 4, 4, 4);
            btnExplain.Name = "btnExplain";
            btnExplain.Size = new Size(203, 67);
            btnExplain.TabIndex = 8;
            btnExplain.Text = "Explain";
            btnExplain.UseVisualStyleBackColor = true;
            btnExplain.Click += btnExplain_Click;
            // 
            // btnTranslate
            // 
            btnTranslate.Location = new Point(497, 20);
            btnTranslate.Margin = new Padding(4, 4, 4, 4);
            btnTranslate.Name = "btnTranslate";
            btnTranslate.Size = new Size(203, 67);
            btnTranslate.TabIndex = 7;
            btnTranslate.Text = "Translate";
            btnTranslate.UseVisualStyleBackColor = true;
            btnTranslate.Click += btnTranslate_Click;
            // 
            // btnSummarize
            // 
            btnSummarize.Location = new Point(281, 20);
            btnSummarize.Margin = new Padding(4, 4, 4, 4);
            btnSummarize.Name = "btnSummarize";
            btnSummarize.Size = new Size(203, 67);
            btnSummarize.TabIndex = 6;
            btnSummarize.Text = "Summarize";
            btnSummarize.UseVisualStyleBackColor = true;
            btnSummarize.Click += btnSummarize_Click;
            // 
            // txtImageUrl
            // 
            txtImageUrl.Location = new Point(8, 170);
            txtImageUrl.Margin = new Padding(4, 4, 4, 4);
            txtImageUrl.Name = "txtImageUrl";
            txtImageUrl.Size = new Size(182, 39);
            txtImageUrl.TabIndex = 18;
            txtImageUrl.Visible = false;
            txtImageUrl.TextChanged += txtImageUrl_TextChanged;
            // 
            // pnlBody
            // 
            pnlBody.AutoScroll = true;
            pnlBody.BackColor = Color.LightGray;
            pnlBody.Controls.Add(lblLLM);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(4, 211);
            pnlBody.Margin = new Padding(4, 4, 4, 4);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(6, 6, 6, 6);
            pnlBody.Size = new Size(1794, 964);
            pnlBody.TabIndex = 7;
            // 
            // pnlFooter
            // 
            pnlFooter.AutoScroll = true;
            pnlFooter.Controls.Add(txtPrompt);
            pnlFooter.Controls.Add(panel1);
            pnlFooter.Controls.Add(label1);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(4, 1175);
            pnlFooter.Margin = new Padding(4, 4, 4, 4);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(4, 4, 4, 4);
            pnlFooter.Size = new Size(1794, 287);
            pnlFooter.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnAddImage);
            panel1.Controls.Add(btnSend);
            panel1.Controls.Add(txtImageUrl);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1565, 4);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(225, 279);
            panel1.TabIndex = 4;
            // 
            // label2
            // 
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(20, 82);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(187, 77);
            label2.TabIndex = 19;
            label2.Text = "(Shift + Enter for New Line)";
            // 
            // btnAddImage
            // 
            btnAddImage.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddImage.ImageIndex = 0;
            btnAddImage.ImageList = imageList1;
            btnAddImage.Location = new Point(8, 212);
            btnAddImage.Margin = new Padding(4, 4, 4, 4);
            btnAddImage.Name = "btnAddImage";
            btnAddImage.Size = new Size(204, 49);
            btnAddImage.TabIndex = 1;
            btnAddImage.Text = " Add Image";
            btnAddImage.TextAlign = ContentAlignment.MiddleRight;
            btnAddImage.UseVisualStyleBackColor = true;
            btnAddImage.Click += btnAddImage_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "add_image.png");
            imageList1.Images.SetKeyName(1, "del_image.png");
            // 
            // frmAssistant
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1802, 1466);
            Controls.Add(pnlBody);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            Name = "frmAssistant";
            Padding = new Padding(4, 4, 4, 4);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AI Assistant";
            Load += frmAssistant_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSend;
        private Label label1;
        private Label lblLLM;
        private TextBox txtPrompt;
        private Button btnNew;
        private Panel pnlHeader;
        private Panel pnlBody;
        private Panel pnlFooter;
        private Button btnSummarize;
        private Button btnOnlyKeyPoints;
        private Button btnExplain;
        private Button btnTranslate;
        private Button btnEmailResponse;
        private RadioButton radioDe;
        private RadioButton radioEn;
        private RadioButton radioAr;
        private Button btnPrompt;
        private TextBox txtImageUrl;
        private Button btnSettings;
        private Panel panel1;
        private Button btnAddImage;
        private ImageList imageList1;
        private Label label2;
    }
}
