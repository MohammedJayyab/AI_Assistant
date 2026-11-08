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
            txtPrompt = new TextBox();
            btnNew = new Button();
            pnlHeader = new Panel();
            btnYoutubeSummary = new Button();
            btnYoutubeScript = new Button();
            btnMindmap = new Button();
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
            menuStrip1 = new MenuStrip();
            mnuFile = new ToolStripMenuItem();
            mnuNewChat = new ToolStripMenuItem();
            mnuAddImage = new ToolStripMenuItem();
            mnuFileSeparator = new ToolStripSeparator();
            mnuHistory = new ToolStripMenuItem();
            mnuHistoryEmpty = new ToolStripMenuItem();
            mnuOpenHistoryFolder = new ToolStripMenuItem();
            mnuOpenImagesFolder = new ToolStripMenuItem();
            mnuClearImages = new ToolStripMenuItem();
            mnuOpenAppLocation = new ToolStripMenuItem();
            mnuFileSeparator2 = new ToolStripSeparator();
            mnuExit = new ToolStripMenuItem();
            mnuTasks = new ToolStripMenuItem();
            mnuSummarize = new ToolStripMenuItem();
            mnuTranslate = new ToolStripMenuItem();
            mnuExplain = new ToolStripMenuItem();
            mnuKeyPoints = new ToolStripMenuItem();
            mnuEmailResponse = new ToolStripMenuItem();
            mnuPrompt = new ToolStripMenuItem();
            mnuMindMap = new ToolStripMenuItem();
            mnuLanguage = new ToolStripMenuItem();
            mnuLangArabic = new ToolStripMenuItem();
            mnuLangEnglish = new ToolStripMenuItem();
            mnuLangGerman = new ToolStripMenuItem();
            mnuYoutube = new ToolStripMenuItem();
            mnuYoutubeScript = new ToolStripMenuItem();
            mnuYoutubeSummary = new ToolStripMenuItem();
            mnuTools = new ToolStripMenuItem();
            mnuSettings = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            progressRequest = new ToolStripProgressBar();
            btnCopyLast = new ToolStripButton();
            btnCompact = new ToolStripButton();
            lblCompacted = new ToolStripStatusLabel();
            lblProvider = new ToolStripStatusLabel();
            lblModel = new ToolStripStatusLabel();
            lblMaxTokens = new ToolStripStatusLabel();
            lblTemperature = new ToolStripStatusLabel();
            lblReasoning = new ToolStripStatusLabel();
            lblMessages = new ToolStripStatusLabel();
            lblTokens = new ToolStripStatusLabel();
            tmrRequest = new System.Windows.Forms.Timer(components);
            pnlHeader.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlFooter.SuspendLayout();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSend.Location = new Point(3, 3);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(170, 60);
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
            // txtPrompt
            // 
            txtPrompt.BackColor = SystemColors.Info;
            txtPrompt.Dock = DockStyle.Fill;
            txtPrompt.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrompt.Location = new Point(70, 3);
            txtPrompt.MaxLength = 0;
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.ScrollBars = ScrollBars.Vertical;
            txtPrompt.Size = new Size(1372, 218);
            txtPrompt.TabIndex = 3;
            txtPrompt.TextChanged += txtPrompt_TextChanged;
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
            // pnlHeader
            // 
            pnlHeader.AutoScroll = true;
            pnlHeader.BackColor = Color.FromArgb(224, 224, 224);
            pnlHeader.Controls.Add(btnYoutubeSummary);
            pnlHeader.Controls.Add(btnYoutubeScript);
            pnlHeader.Controls.Add(btnMindmap);
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
            pnlHeader.Location = new Point(3, 36);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1618, 162);
            pnlHeader.TabIndex = 6;
            // 
            // btnYoutubeSummary
            // 
            btnYoutubeSummary.Location = new Point(1428, 74);
            btnYoutubeSummary.Name = "btnYoutubeSummary";
            btnYoutubeSummary.Size = new Size(170, 52);
            btnYoutubeSummary.TabIndex = 24;
            btnYoutubeSummary.Text = "Youtube Summary";
            btnYoutubeSummary.UseVisualStyleBackColor = true;
            btnYoutubeSummary.Click += btnYoutubeSummary_Click;
            // 
            // btnYoutubeScript
            // 
            btnYoutubeScript.Location = new Point(1242, 74);
            btnYoutubeScript.Name = "btnYoutubeScript";
            btnYoutubeScript.Size = new Size(160, 52);
            btnYoutubeScript.TabIndex = 23;
            btnYoutubeScript.Text = "Youtube Script";
            btnYoutubeScript.UseVisualStyleBackColor = true;
            btnYoutubeScript.Click += btnYoutubeScript_Click;
            // 
            // btnMindmap
            // 
            btnMindmap.Location = new Point(1242, 16);
            btnMindmap.Name = "btnMindmap";
            btnMindmap.Size = new Size(160, 52);
            btnMindmap.TabIndex = 22;
            btnMindmap.Text = "Mind Map -+-";
            btnMindmap.UseVisualStyleBackColor = true;
            btnMindmap.Click += btnMindmap_Click;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSettings.Image = Properties.Resources.settings;
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(1454, 16);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(144, 52);
            btnSettings.TabIndex = 21;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnPrompt
            // 
            btnPrompt.Location = new Point(1049, 16);
            btnPrompt.Name = "btnPrompt";
            btnPrompt.Size = new Size(156, 52);
            btnPrompt.TabIndex = 16;
            btnPrompt.Text = "LLM Prompt </>";
            btnPrompt.UseVisualStyleBackColor = true;
            btnPrompt.Click += btnPrompt_Click;
            // 
            // radioDe
            // 
            radioDe.AutoSize = true;
            radioDe.Location = new Point(504, 74);
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
            radioEn.Location = new Point(443, 74);
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
            radioAr.Location = new Point(382, 74);
            radioAr.Name = "radioAr";
            radioAr.Size = new Size(55, 29);
            radioAr.TabIndex = 13;
            radioAr.TabStop = true;
            radioAr.Text = "Ar";
            radioAr.UseVisualStyleBackColor = true;
            radioAr.CheckedChanged += radioAr_CheckedChanged;
            // 
            // btnEmailResponse
            // 
            btnEmailResponse.Location = new Point(883, 16);
            btnEmailResponse.Name = "btnEmailResponse";
            btnEmailResponse.Size = new Size(156, 52);
            btnEmailResponse.TabIndex = 11;
            btnEmailResponse.Text = "Response Email";
            btnEmailResponse.UseVisualStyleBackColor = true;
            btnEmailResponse.Click += btnEmailResponse_Click;
            // 
            // btnOnlyKeyPoints
            // 
            btnOnlyKeyPoints.Location = new Point(714, 16);
            btnOnlyKeyPoints.Name = "btnOnlyKeyPoints";
            btnOnlyKeyPoints.Size = new Size(156, 52);
            btnOnlyKeyPoints.TabIndex = 9;
            btnOnlyKeyPoints.Text = "KeyPoints";
            btnOnlyKeyPoints.UseVisualStyleBackColor = true;
            btnOnlyKeyPoints.Click += btnOnlyKeyPoints_Click;
            // 
            // btnExplain
            // 
            btnExplain.Location = new Point(548, 16);
            btnExplain.Name = "btnExplain";
            btnExplain.Size = new Size(156, 52);
            btnExplain.TabIndex = 8;
            btnExplain.Text = "Explain";
            btnExplain.UseVisualStyleBackColor = true;
            btnExplain.Click += btnExplain_Click;
            // 
            // btnTranslate
            // 
            btnTranslate.Location = new Point(382, 16);
            btnTranslate.Name = "btnTranslate";
            btnTranslate.Size = new Size(156, 52);
            btnTranslate.TabIndex = 7;
            btnTranslate.Text = "Translate";
            btnTranslate.UseVisualStyleBackColor = true;
            btnTranslate.Click += btnTranslate_Click;
            // 
            // btnSummarize
            // 
            btnSummarize.Location = new Point(216, 16);
            btnSummarize.Name = "btnSummarize";
            btnSummarize.Size = new Size(156, 52);
            btnSummarize.TabIndex = 6;
            btnSummarize.Text = "Summarize";
            btnSummarize.UseVisualStyleBackColor = true;
            btnSummarize.Click += btnSummarize_Click;
            // 
            // txtImageUrl
            // 
            txtImageUrl.Location = new Point(6, 133);
            txtImageUrl.Name = "txtImageUrl";
            txtImageUrl.Size = new Size(141, 31);
            txtImageUrl.TabIndex = 18;
            txtImageUrl.Visible = false;
            txtImageUrl.TextChanged += txtImageUrl_TextChanged;
            // 
            // pnlBody
            // 
            pnlBody.AutoScroll = true;
            pnlBody.BackColor = Color.LightGray;
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Location = new Point(3, 198);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(5);
            pnlBody.Size = new Size(1618, 720);
            pnlBody.TabIndex = 7;
            // 
            // pnlFooter
            // 
            pnlFooter.AutoScroll = true;
            pnlFooter.Controls.Add(txtPrompt);
            pnlFooter.Controls.Add(panel1);
            pnlFooter.Controls.Add(label1);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(3, 918);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(3);
            pnlFooter.Size = new Size(1618, 224);
            pnlFooter.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnAddImage);
            panel1.Controls.Add(btnSend);
            panel1.Controls.Add(txtImageUrl);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1442, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(173, 218);
            panel1.TabIndex = 4;
            // 
            // label2
            // 
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(15, 64);
            label2.Name = "label2";
            label2.Size = new Size(144, 60);
            label2.TabIndex = 19;
            label2.Text = "(Shift + Enter for New Line)";
            // 
            // btnAddImage
            // 
            btnAddImage.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddImage.ImageIndex = 0;
            btnAddImage.ImageList = imageList1;
            btnAddImage.Location = new Point(6, 166);
            btnAddImage.Name = "btnAddImage";
            btnAddImage.Size = new Size(157, 38);
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
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuFile, mnuTasks, mnuLanguage, mnuYoutube, mnuTools });
            menuStrip1.Location = new Point(3, 3);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1618, 33);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuNewChat, mnuAddImage, mnuFileSeparator, mnuHistory, mnuOpenHistoryFolder, mnuOpenImagesFolder, mnuClearImages, mnuOpenAppLocation, mnuFileSeparator2, mnuExit });
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new Size(54, 29);
            mnuFile.Text = "&File";
            // 
            // mnuNewChat
            // 
            mnuNewChat.Name = "mnuNewChat";
            mnuNewChat.ShortcutKeys = Keys.Control | Keys.N;
            mnuNewChat.Size = new Size(340, 34);
            mnuNewChat.Text = "&New Chat";
            mnuNewChat.Click += btnNew_Click;
            // 
            // mnuAddImage
            // 
            mnuAddImage.Name = "mnuAddImage";
            mnuAddImage.ShortcutKeys = Keys.Control | Keys.I;
            mnuAddImage.Size = new Size(340, 34);
            mnuAddImage.Text = "Add / Remove &Image";
            mnuAddImage.Click += btnAddImage_Click;
            // 
            // mnuFileSeparator
            // 
            mnuFileSeparator.Name = "mnuFileSeparator";
            mnuFileSeparator.Size = new Size(337, 6);
            // 
            // mnuHistory
            // 
            mnuHistory.DropDownItems.AddRange(new ToolStripItem[] { mnuHistoryEmpty });
            mnuHistory.Name = "mnuHistory";
            mnuHistory.Size = new Size(340, 34);
            mnuHistory.Text = "&History";
            mnuHistory.DropDownOpening += mnuHistory_DropDownOpening;
            // 
            // mnuHistoryEmpty
            // 
            mnuHistoryEmpty.Enabled = false;
            mnuHistoryEmpty.Name = "mnuHistoryEmpty";
            mnuHistoryEmpty.Size = new Size(236, 34);
            mnuHistoryEmpty.Text = "(No history yet)";
            // 
            // mnuOpenHistoryFolder
            // 
            mnuOpenHistoryFolder.Name = "mnuOpenHistoryFolder";
            mnuOpenHistoryFolder.Size = new Size(340, 34);
            mnuOpenHistoryFolder.Text = "Open History &Folder";
            mnuOpenHistoryFolder.Click += mnuOpenHistoryFolder_Click;
            // 
            // mnuOpenImagesFolder
            // 
            mnuOpenImagesFolder.Name = "mnuOpenImagesFolder";
            mnuOpenImagesFolder.Size = new Size(340, 34);
            mnuOpenImagesFolder.Text = "Open I&mages Folder";
            mnuOpenImagesFolder.ToolTipText = "The pasted images; they are deleted when the app closes";
            mnuOpenImagesFolder.Click += mnuOpenImagesFolder_Click;
            // 
            // mnuClearImages
            // 
            mnuClearImages.Name = "mnuClearImages";
            mnuClearImages.Size = new Size(340, 34);
            mnuClearImages.Text = "&Clear Images...";
            mnuClearImages.ToolTipText = "Delete all pasted images";
            mnuClearImages.Click += mnuClearImages_Click;
            // 
            // mnuOpenAppLocation
            // 
            mnuOpenAppLocation.Name = "mnuOpenAppLocation";
            mnuOpenAppLocation.Size = new Size(340, 34);
            mnuOpenAppLocation.Text = "Open &Application Location";
            mnuOpenAppLocation.Click += mnuOpenAppLocation_Click;
            // 
            // mnuFileSeparator2
            // 
            mnuFileSeparator2.Name = "mnuFileSeparator2";
            mnuFileSeparator2.Size = new Size(337, 6);
            // 
            // mnuExit
            // 
            mnuExit.Name = "mnuExit";
            mnuExit.ShortcutKeyDisplayString = "Alt+F4";
            mnuExit.Size = new Size(340, 34);
            mnuExit.Text = "E&xit";
            mnuExit.Click += mnuExit_Click;
            // 
            // mnuTasks
            // 
            mnuTasks.DropDownItems.AddRange(new ToolStripItem[] { mnuSummarize, mnuTranslate, mnuExplain, mnuKeyPoints, mnuEmailResponse, mnuPrompt, mnuMindMap });
            mnuTasks.Name = "mnuTasks";
            mnuTasks.Size = new Size(91, 29);
            mnuTasks.Text = "&AI Tasks";
            // 
            // mnuSummarize
            // 
            mnuSummarize.Name = "mnuSummarize";
            mnuSummarize.Size = new Size(237, 34);
            mnuSummarize.Text = "&Summarize";
            mnuSummarize.Click += btnSummarize_Click;
            // 
            // mnuTranslate
            // 
            mnuTranslate.Name = "mnuTranslate";
            mnuTranslate.Size = new Size(237, 34);
            mnuTranslate.Text = "&Translate";
            mnuTranslate.Click += btnTranslate_Click;
            // 
            // mnuExplain
            // 
            mnuExplain.Name = "mnuExplain";
            mnuExplain.Size = new Size(237, 34);
            mnuExplain.Text = "&Explain";
            mnuExplain.Click += btnExplain_Click;
            // 
            // mnuKeyPoints
            // 
            mnuKeyPoints.Name = "mnuKeyPoints";
            mnuKeyPoints.Size = new Size(237, 34);
            mnuKeyPoints.Text = "&Key Points";
            mnuKeyPoints.Click += btnOnlyKeyPoints_Click;
            // 
            // mnuEmailResponse
            // 
            mnuEmailResponse.Name = "mnuEmailResponse";
            mnuEmailResponse.Size = new Size(237, 34);
            mnuEmailResponse.Text = "Response E&mail";
            mnuEmailResponse.Click += btnEmailResponse_Click;
            // 
            // mnuPrompt
            // 
            mnuPrompt.Name = "mnuPrompt";
            mnuPrompt.Size = new Size(237, 34);
            mnuPrompt.Text = "LLM &Prompt";
            mnuPrompt.Click += btnPrompt_Click;
            // 
            // mnuMindMap
            // 
            mnuMindMap.Name = "mnuMindMap";
            mnuMindMap.Size = new Size(237, 34);
            mnuMindMap.Text = "Mind M&ap";
            mnuMindMap.Click += btnMindmap_Click;
            // 
            // mnuLanguage
            // 
            mnuLanguage.DropDownItems.AddRange(new ToolStripItem[] { mnuLangArabic, mnuLangEnglish, mnuLangGerman });
            mnuLanguage.Name = "mnuLanguage";
            mnuLanguage.Size = new Size(105, 29);
            mnuLanguage.Text = "&Language";
            // 
            // mnuLangArabic
            // 
            mnuLangArabic.Checked = true;
            mnuLangArabic.CheckState = CheckState.Checked;
            mnuLangArabic.Name = "mnuLangArabic";
            mnuLangArabic.Size = new Size(176, 34);
            mnuLangArabic.Text = "&Arabic";
            mnuLangArabic.Click += mnuLangArabic_Click;
            // 
            // mnuLangEnglish
            // 
            mnuLangEnglish.Name = "mnuLangEnglish";
            mnuLangEnglish.Size = new Size(176, 34);
            mnuLangEnglish.Text = "&English";
            mnuLangEnglish.Click += mnuLangEnglish_Click;
            // 
            // mnuLangGerman
            // 
            mnuLangGerman.Name = "mnuLangGerman";
            mnuLangGerman.Size = new Size(176, 34);
            mnuLangGerman.Text = "&German";
            mnuLangGerman.Click += mnuLangGerman_Click;
            // 
            // mnuYoutube
            // 
            mnuYoutube.DropDownItems.AddRange(new ToolStripItem[] { mnuYoutubeScript, mnuYoutubeSummary });
            mnuYoutube.Name = "mnuYoutube";
            mnuYoutube.Size = new Size(96, 29);
            mnuYoutube.Text = "&YouTube";
            // 
            // mnuYoutubeScript
            // 
            mnuYoutubeScript.Name = "mnuYoutubeScript";
            mnuYoutubeScript.Size = new Size(206, 34);
            mnuYoutubeScript.Text = "Fetch &Script";
            mnuYoutubeScript.Click += btnYoutubeScript_Click;
            // 
            // mnuYoutubeSummary
            // 
            mnuYoutubeSummary.Name = "mnuYoutubeSummary";
            mnuYoutubeSummary.Size = new Size(206, 34);
            mnuYoutubeSummary.Text = "S&ummary";
            mnuYoutubeSummary.Click += btnYoutubeSummary_Click;
            // 
            // mnuTools
            // 
            mnuTools.DropDownItems.AddRange(new ToolStripItem[] { mnuSettings });
            mnuTools.Name = "mnuTools";
            mnuTools.Size = new Size(69, 29);
            mnuTools.Text = "&Tools";
            // 
            // mnuSettings
            // 
            mnuSettings.Image = Properties.Resources.settings;
            mnuSettings.Name = "mnuSettings";
            mnuSettings.Size = new Size(178, 34);
            mnuSettings.Text = "&Settings";
            mnuSettings.Click += btnSettings_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Segoe UI", 11F);
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus, progressRequest, btnCopyLast, btnCompact, lblCompacted, lblProvider, lblModel, lblMaxTokens, lblTemperature, lblReasoning, lblMessages, lblTokens });
            statusStrip1.Location = new Point(3, 1112);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1618, 30);
            statusStrip1.TabIndex = 10;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(703, 25);
            lblStatus.Spring = true;
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // progressRequest
            // 
            progressRequest.MarqueeAnimationSpeed = 30;
            progressRequest.Name = "progressRequest";
            progressRequest.Size = new Size(150, 24);
            progressRequest.Style = ProgressBarStyle.Marquee;
            progressRequest.Visible = false;
            // 
            // btnCopyLast
            // 
            btnCopyLast.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnCopyLast.Enabled = false;
            btnCopyLast.Name = "btnCopyLast";
            btnCopyLast.Size = new Size(190, 28);
            btnCopyLast.Text = "Copy Last Response";
            btnCopyLast.ToolTipText = "Copy the last response to the clipboard";
            btnCopyLast.Click += btnCopyLast_Click;
            // 
            // btnCompact
            // 
            btnCompact.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnCompact.Enabled = false;
            btnCompact.Name = "btnCompact";
            btnCompact.Size = new Size(90, 28);
            btnCompact.Text = "Compact";
            btnCompact.ToolTipText = "Summarize the conversation and send only the summary to the LLM from now on. The chat stays visible and fully saved in the history.";
            btnCompact.Click += btnCompact_Click;
            // 
            // lblCompacted
            // 
            lblCompacted.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblCompacted.BorderStyle = Border3DStyle.Etched;
            lblCompacted.ForeColor = Color.DarkOrange;
            lblCompacted.Name = "lblCompacted";
            lblCompacted.Size = new Size(100, 25);
            lblCompacted.Text = "Compacted";
            lblCompacted.ToolTipText = "The older messages were replaced by a summary. They are still shown here and saved in the history, but they are no longer sent to the LLM.";
            lblCompacted.Visible = false;
            // 
            // lblProvider
            // 
            lblProvider.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblProvider.BorderStyle = Border3DStyle.Etched;
            lblProvider.Name = "lblProvider";
            lblProvider.Size = new Size(100, 25);
            lblProvider.Text = "Provider:";
            lblProvider.ToolTipText = "Click to open the settings";
            lblProvider.Click += btnSettings_Click;
            lblProvider.MouseEnter += lblSettings_MouseEnter;
            lblProvider.MouseLeave += lblSettings_MouseLeave;
            // 
            // lblModel
            // 
            lblModel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblModel.BorderStyle = Border3DStyle.Etched;
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(80, 25);
            lblModel.Text = "Model:";
            lblModel.ToolTipText = "Click to open the settings";
            lblModel.Click += btnSettings_Click;
            lblModel.MouseEnter += lblSettings_MouseEnter;
            lblModel.MouseLeave += lblSettings_MouseLeave;
            // 
            // lblMaxTokens
            // 
            lblMaxTokens.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblMaxTokens.BorderStyle = Border3DStyle.Etched;
            lblMaxTokens.Name = "lblMaxTokens";
            lblMaxTokens.Size = new Size(120, 25);
            lblMaxTokens.Text = "Max Tokens:";
            lblMaxTokens.ToolTipText = "Click to open the settings";
            lblMaxTokens.Click += btnSettings_Click;
            lblMaxTokens.MouseEnter += lblSettings_MouseEnter;
            lblMaxTokens.MouseLeave += lblSettings_MouseLeave;
            // 
            // lblTemperature
            // 
            lblTemperature.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblTemperature.BorderStyle = Border3DStyle.Etched;
            lblTemperature.Name = "lblTemperature";
            lblTemperature.Size = new Size(70, 25);
            lblTemperature.Text = "Temp.:";
            lblTemperature.ToolTipText = "Click to open the settings";
            lblTemperature.Click += btnSettings_Click;
            lblTemperature.MouseEnter += lblSettings_MouseEnter;
            lblTemperature.MouseLeave += lblSettings_MouseLeave;
            //
            // lblReasoning
            //
            lblReasoning.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblReasoning.BorderStyle = Border3DStyle.Etched;
            lblReasoning.Name = "lblReasoning";
            lblReasoning.Size = new Size(120, 25);
            lblReasoning.Text = "Thinking:";
            lblReasoning.ToolTipText = "Click to open the settings";
            lblReasoning.Click += btnSettings_Click;
            lblReasoning.MouseEnter += lblSettings_MouseEnter;
            lblReasoning.MouseLeave += lblSettings_MouseLeave;
            // 
            // lblMessages
            // 
            lblMessages.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblMessages.BorderStyle = Border3DStyle.Etched;
            lblMessages.Name = "lblMessages";
            lblMessages.Size = new Size(140, 25);
            lblMessages.Text = "Messages:";
            lblMessages.ToolTipText = "Click to open the settings";
            lblMessages.Click += btnSettings_Click;
            lblMessages.MouseEnter += lblSettings_MouseEnter;
            lblMessages.MouseLeave += lblSettings_MouseLeave;
            // 
            // lblTokens
            // 
            lblTokens.BorderSides = ToolStripStatusLabelBorderSides.Left;
            lblTokens.BorderStyle = Border3DStyle.Etched;
            lblTokens.Name = "lblTokens";
            lblTokens.Size = new Size(160, 25);
            lblTokens.Text = "Tokens: -";
            lblTokens.ToolTipText = "Tokens of the last request (in) and its response (out, including the thinking, shown in brackets), as reported by the provider. ~ marks an estimate: while waiting, or when the provider reports none.";
            // 
            // tmrRequest
            // 
            tmrRequest.Interval = 100;
            tmrRequest.Tick += tmrRequest_Tick;
            // 
            // frmAssistant
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1624, 1145);
            Controls.Add(pnlBody);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "frmAssistant";
            Padding = new Padding(3);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AI Assistant";
            FormClosed += frmAssistant_FormClosed;
            Load += frmAssistant_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSend;
        private Label label1;
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
        private Button btnMindmap;
        private Button btnYoutubeScript;
        private Button btnYoutubeSummary;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuNewChat;
        private ToolStripMenuItem mnuAddImage;
        private ToolStripSeparator mnuFileSeparator;
        private ToolStripMenuItem mnuHistory;
        private ToolStripMenuItem mnuHistoryEmpty;
        private ToolStripMenuItem mnuOpenHistoryFolder;
        private ToolStripMenuItem mnuOpenImagesFolder;
        private ToolStripMenuItem mnuClearImages;
        private ToolStripMenuItem mnuOpenAppLocation;
        private ToolStripSeparator mnuFileSeparator2;
        private ToolStripMenuItem mnuExit;
        private ToolStripMenuItem mnuTasks;
        private ToolStripMenuItem mnuSummarize;
        private ToolStripMenuItem mnuTranslate;
        private ToolStripMenuItem mnuExplain;
        private ToolStripMenuItem mnuKeyPoints;
        private ToolStripMenuItem mnuEmailResponse;
        private ToolStripMenuItem mnuPrompt;
        private ToolStripMenuItem mnuMindMap;
        private ToolStripMenuItem mnuLanguage;
        private ToolStripMenuItem mnuLangArabic;
        private ToolStripMenuItem mnuLangEnglish;
        private ToolStripMenuItem mnuLangGerman;
        private ToolStripMenuItem mnuYoutube;
        private ToolStripMenuItem mnuYoutubeScript;
        private ToolStripMenuItem mnuYoutubeSummary;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuSettings;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripProgressBar progressRequest;
        private ToolStripButton btnCopyLast;
        private ToolStripButton btnCompact;
        private ToolStripStatusLabel lblCompacted;
        private ToolStripStatusLabel lblProvider;
        private ToolStripStatusLabel lblModel;
        private ToolStripStatusLabel lblMaxTokens;
        private ToolStripStatusLabel lblTemperature;
        private ToolStripStatusLabel lblReasoning;
        private ToolStripStatusLabel lblMessages;
        private ToolStripStatusLabel lblTokens;
        private System.Windows.Forms.Timer tmrRequest;
    }
}
