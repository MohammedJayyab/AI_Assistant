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
            btnSend = new Button();
            label1 = new Label();
            label2 = new Label();
            txtPrompt = new TextBox();
            btnNew = new Button();
            btnUpdateSize = new Button();
            pnlHeader = new Panel();
            lnkBrowsImg = new LinkLabel();
            btnCaptureAndAskImage = new Button();
            txtImageUrl = new TextBox();
            btnImage = new Button();
            btnPrompt = new Button();
            radioDe = new RadioButton();
            radioEn = new RadioButton();
            radioAr = new RadioButton();
            btnStandard = new Button();
            btnEmailResponse = new Button();
            btnSearch = new Button();
            btnOnlyKeyPoints = new Button();
            btnExplain = new Button();
            btnTranslate = new Button();
            btnSummarize = new Button();
            pnlBody = new Panel();
            pnlFooter = new Panel();
            pnlHeader.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Dock = DockStyle.Right;
            btnSend.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSend.Location = new Point(1910, 3);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(120, 218);
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
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(67, 38);
            label1.TabIndex = 1;
            label1.Text = "Ask:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(3, 3);
            label2.Name = "label2";
            label2.Size = new Size(200, 38);
            label2.TabIndex = 1;
            label2.Text = "LLM Response:";
            // 
            // txtPrompt
            // 
            txtPrompt.BackColor = SystemColors.Info;
            txtPrompt.Dock = DockStyle.Fill;
            txtPrompt.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPrompt.Location = new Point(70, 3);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.ScrollBars = ScrollBars.Vertical;
            txtPrompt.Size = new Size(1840, 218);
            txtPrompt.TabIndex = 3;
            txtPrompt.KeyDown += txtPrompt_KeyDown;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(12, 16);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(185, 52);
            btnNew.TabIndex = 4;
            btnNew.Text = "+ New Chat";
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // btnUpdateSize
            // 
            btnUpdateSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUpdateSize.Location = new Point(7777, 16);
            btnUpdateSize.Name = "btnUpdateSize";
            btnUpdateSize.Size = new Size(185, 52);
            btnUpdateSize.TabIndex = 5;
            btnUpdateSize.Text = "Update Size";
            btnUpdateSize.UseVisualStyleBackColor = true;
            btnUpdateSize.Click += btnUpdateSize_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.AutoScroll = true;
            pnlHeader.BackColor = Color.FromArgb(224, 224, 224);
            pnlHeader.Controls.Add(lnkBrowsImg);
            pnlHeader.Controls.Add(btnCaptureAndAskImage);
            pnlHeader.Controls.Add(txtImageUrl);
            pnlHeader.Controls.Add(btnImage);
            pnlHeader.Controls.Add(btnPrompt);
            pnlHeader.Controls.Add(radioDe);
            pnlHeader.Controls.Add(radioEn);
            pnlHeader.Controls.Add(radioAr);
            pnlHeader.Controls.Add(btnStandard);
            pnlHeader.Controls.Add(btnEmailResponse);
            pnlHeader.Controls.Add(btnSearch);
            pnlHeader.Controls.Add(btnOnlyKeyPoints);
            pnlHeader.Controls.Add(btnExplain);
            pnlHeader.Controls.Add(btnTranslate);
            pnlHeader.Controls.Add(btnSummarize);
            pnlHeader.Controls.Add(btnNew);
            pnlHeader.Controls.Add(btnUpdateSize);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(3, 3);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(2033, 162);
            pnlHeader.TabIndex = 6;
            // 
            // lnkBrowsImg
            // 
            lnkBrowsImg.AutoSize = true;
            lnkBrowsImg.Location = new Point(1502, 89);
            lnkBrowsImg.Name = "lnkBrowsImg";
            lnkBrowsImg.Size = new Size(69, 25);
            lnkBrowsImg.TabIndex = 20;
            lnkBrowsImg.TabStop = true;
            lnkBrowsImg.Text = "Browse";
            lnkBrowsImg.LinkClicked += lnkBrowsImg_LinkClicked;
            // 
            // btnCaptureAndAskImage
            // 
            btnCaptureAndAskImage.BackColor = Color.FromArgb(192, 255, 255);
            btnCaptureAndAskImage.Location = new Point(1802, 72);
            btnCaptureAndAskImage.Name = "btnCaptureAndAskImage";
            btnCaptureAndAskImage.Size = new Size(185, 51);
            btnCaptureAndAskImage.TabIndex = 19;
            btnCaptureAndAskImage.Text = "P>>";
            btnCaptureAndAskImage.UseVisualStyleBackColor = false;
            btnCaptureAndAskImage.Click += btnCaptureAndAskImage_Click;
            // 
            // txtImageUrl
            // 
            txtImageUrl.Location = new Point(797, 84);
            txtImageUrl.Name = "txtImageUrl";
            txtImageUrl.Size = new Size(696, 31);
            txtImageUrl.TabIndex = 18;
            txtImageUrl.Text = "C:\\Users\\mjayy\\source\\repos\\LLMOrchestratorApp\\LLMOrchestratorApp\\image.jpg";
            // 
            // btnImage
            // 
            btnImage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnImage.ForeColor = Color.ForestGreen;
            btnImage.Location = new Point(1596, 74);
            btnImage.Name = "btnImage";
            btnImage.Size = new Size(185, 46);
            btnImage.TabIndex = 17;
            btnImage.Text = "Explain Img";
            btnImage.UseVisualStyleBackColor = true;
            btnImage.Click += btnImage_Click;
            // 
            // btnPrompt
            // 
            btnPrompt.Location = new Point(1596, 16);
            btnPrompt.Name = "btnPrompt";
            btnPrompt.Size = new Size(185, 52);
            btnPrompt.TabIndex = 16;
            btnPrompt.Text = "LLM Prompt";
            btnPrompt.UseVisualStyleBackColor = true;
            btnPrompt.Click += btnPrompt_Click;
            // 
            // radioDe
            // 
            radioDe.AutoSize = true;
            radioDe.Location = new Point(690, 74);
            radioDe.Name = "radioDe";
            radioDe.Size = new Size(59, 29);
            radioDe.TabIndex = 15;
            radioDe.TabStop = true;
            radioDe.Text = "DE";
            radioDe.UseVisualStyleBackColor = true;
            radioDe.CheckedChanged += radioDe_CheckedChanged;
            // 
            // radioEn
            // 
            radioEn.AutoSize = true;
            radioEn.Location = new Point(629, 74);
            radioEn.Name = "radioEn";
            radioEn.Size = new Size(56, 29);
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
            radioAr.Location = new Point(568, 74);
            radioAr.Name = "radioAr";
            radioAr.Size = new Size(55, 29);
            radioAr.TabIndex = 13;
            radioAr.TabStop = true;
            radioAr.Text = "Ar";
            radioAr.UseVisualStyleBackColor = true;
            radioAr.CheckedChanged += radioAr_CheckedChanged;
            // 
            // btnStandard
            // 
            btnStandard.Location = new Point(210, 16);
            btnStandard.Name = "btnStandard";
            btnStandard.Size = new Size(185, 52);
            btnStandard.TabIndex = 12;
            btnStandard.Text = "Standard";
            btnStandard.UseVisualStyleBackColor = true;
            btnStandard.Click += btnStandard_Click;
            // 
            // btnEmailResponse
            // 
            btnEmailResponse.Location = new Point(1398, 16);
            btnEmailResponse.Name = "btnEmailResponse";
            btnEmailResponse.Size = new Size(185, 52);
            btnEmailResponse.TabIndex = 11;
            btnEmailResponse.Text = "Response Email";
            btnEmailResponse.UseVisualStyleBackColor = true;
            btnEmailResponse.Click += btnEmailResponse_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(1200, 16);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(185, 52);
            btnSearch.TabIndex = 10;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnOnlyKeyPoints
            // 
            btnOnlyKeyPoints.Location = new Point(1002, 16);
            btnOnlyKeyPoints.Name = "btnOnlyKeyPoints";
            btnOnlyKeyPoints.Size = new Size(185, 52);
            btnOnlyKeyPoints.TabIndex = 9;
            btnOnlyKeyPoints.Text = "KeyPoints/Important";
            btnOnlyKeyPoints.UseVisualStyleBackColor = true;
            btnOnlyKeyPoints.Click += btnOnlyKeyPoints_Click;
            // 
            // btnExplain
            // 
            btnExplain.Location = new Point(804, 16);
            btnExplain.Name = "btnExplain";
            btnExplain.Size = new Size(185, 52);
            btnExplain.TabIndex = 8;
            btnExplain.Text = "Explain";
            btnExplain.UseVisualStyleBackColor = true;
            btnExplain.Click += btnExplain_Click;
            // 
            // btnTranslate
            // 
            btnTranslate.Location = new Point(606, 16);
            btnTranslate.Name = "btnTranslate";
            btnTranslate.Size = new Size(185, 52);
            btnTranslate.TabIndex = 7;
            btnTranslate.Text = "Translate";
            btnTranslate.UseVisualStyleBackColor = true;
            btnTranslate.Click += btnTranslate_Click;
            // 
            // btnSummarize
            // 
            btnSummarize.Location = new Point(408, 16);
            btnSummarize.Name = "btnSummarize";
            btnSummarize.Size = new Size(185, 52);
            btnSummarize.TabIndex = 6;
            btnSummarize.Text = "Summarize";
            btnSummarize.UseVisualStyleBackColor = true;
            btnSummarize.Click += btnSummarize_Click;
            // 
            // pnlBody
            // 
            pnlBody.AutoScroll = true;
            pnlBody.BackColor = Color.LightGray;
            pnlBody.Controls.Add(label2);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(3, 165);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(3);
            pnlBody.Size = new Size(2033, 753);
            pnlBody.TabIndex = 7;
            // 
            // pnlFooter
            // 
            pnlFooter.AutoScroll = true;
            pnlFooter.Controls.Add(txtPrompt);
            pnlFooter.Controls.Add(btnSend);
            pnlFooter.Controls.Add(label1);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(3, 918);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(3);
            pnlFooter.Size = new Size(2033, 224);
            pnlFooter.TabIndex = 8;
            // 
            // frmAssistant
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2039, 1145);
            Controls.Add(pnlBody);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Name = "frmAssistant";
            Padding = new Padding(3);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AI Assistant";
            Load += frmAssistant_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSend;
        private Label label1;
        private Label label2;
        private TextBox txtPrompt;
        private Button btnNew;
        private Button btnUpdateSize;
        private Panel pnlHeader;
        private Panel pnlBody;
        private Panel pnlFooter;
        private Button btnSummarize;
        private Button btnSearch;
        private Button btnOnlyKeyPoints;
        private Button btnExplain;
        private Button btnTranslate;
        private Button btnStandard;
        private Button btnEmailResponse;
        private RadioButton radioDe;
        private RadioButton radioEn;
        private RadioButton radioAr;
        private Button btnPrompt;
        private Button btnImage;
        private TextBox txtImageUrl;
        private Button btnCaptureAndAskImage;
        private LinkLabel lnkBrowsImg;
    }
}
