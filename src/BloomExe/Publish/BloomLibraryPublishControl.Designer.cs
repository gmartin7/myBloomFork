﻿using System.Windows.Forms;

namespace Bloom.Publish
{
	partial class BloomLibraryPublishControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this._uploadButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this._L10NSharpExtender = new L10NSharp.UI.L10NSharpExtender(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this._labelAfterLicense = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this._ccLabel = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this._titleLabel = new System.Windows.Forms.Label();
			this._copyrightLabel = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this._languagesLabel = new System.Windows.Forms.Label();
			this._loginLink = new System.Windows.Forms.LinkLabel();
			this._termsLink = new System.Windows.Forms.LinkLabel();
			this._creditsLabel = new System.Windows.Forms.Label();
			this._summaryBox = new System.Windows.Forms.TextBox();
			this._signUpLink = new System.Windows.Forms.LinkLabel();
			this._optional2 = new System.Windows.Forms.Label();
			this._licenseSuggestion = new System.Windows.Forms.Label();
			this._creativeCommonsLink = new System.Windows.Forms.LinkLabel();
			this._licenseNotesLabel = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this._optional1 = new System.Windows.Forms.Label();
			this._progressBox = new Palaso.UI.WindowsForms.Progress.LogBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1a = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this._ccPanel = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this._L10NSharpExtender)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1a.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this._ccPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _uploadButton
			// 
			this._uploadButton.Dock = System.Windows.Forms.DockStyle.Left;
			this._L10NSharpExtender.SetLocalizableToolTip(this._uploadButton, null);
			this._L10NSharpExtender.SetLocalizationComment(this._uploadButton, null);
			this._L10NSharpExtender.SetLocalizingId(this._uploadButton, "Publish.Upload.UploadButton");
			this._uploadButton.Location = new System.Drawing.Point(0, 0);
			this._uploadButton.Name = "_uploadButton";
			this._uploadButton.Size = new System.Drawing.Size(101, 23);
			this._uploadButton.TabIndex = 17;
			this._uploadButton.Text = "Upload Book";
			this._uploadButton.UseVisualStyleBackColor = true;
			this._uploadButton.Click += new System.EventHandler(this._uploadButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = false;
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this.label1, null);
			this._L10NSharpExtender.SetLocalizationComment(this.label1, null);
			this._L10NSharpExtender.SetLocalizingId(this.label1, "Publish.Upload.UploadProgress");
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(300, 15);
			this.label1.TabIndex = 19;
			this.label1.Text = "Upload Progress";
			// 
			// _L10NSharpExtender
			// 
			this._L10NSharpExtender.LocalizationManagerId = "Bloom";
			this._L10NSharpExtender.PrefixForNewItems = "Publish.Upload";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Left;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this.label3, null);
			this._L10NSharpExtender.SetLocalizationComment(this.label3, null);
			this._L10NSharpExtender.SetLocalizingId(this.label3, "Publish.Upload.Credits");
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 15);
			this.label3.TabIndex = 12;
			this.label3.Text = "Credits";
			// 
			// _labelAfterLicense
			// 
			this._labelAfterLicense.AutoSize = false;
			this._labelAfterLicense.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this._labelAfterLicense.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._labelAfterLicense.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this._labelAfterLicense, null);
			this._L10NSharpExtender.SetLocalizationComment(this._labelAfterLicense, null);
			this._L10NSharpExtender.SetLocalizingId(this._labelAfterLicense, "Publish.Upload.Copyright");
			this._labelAfterLicense.Location = new System.Drawing.Point(3, 213);
			this._labelAfterLicense.Name = "_labelAfterLicense";
			this._labelAfterLicense.Size = new System.Drawing.Size(300, 15);
			this._labelAfterLicense.TabIndex = 8;
			this._labelAfterLicense.Text = "Copyright";
			// 
			// label5
			// 
			this.label5.AutoSize = false;
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this.label5, null);
			this._L10NSharpExtender.SetLocalizationComment(this.label5, null);
			this._L10NSharpExtender.SetLocalizingId(this.label5, "Publish.Upload.License");
			this.label5.Location = new System.Drawing.Point(3, 108);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(300, 15);
			this.label5.TabIndex = 5;
			this.label5.Text = "Usage/License";
			// 
			// _ccLabel
			// 
			this._ccLabel.AutoSize = false;
			this._ccLabel.Anchor = AnchorStyles.Left;
			this._ccLabel.Cursor = System.Windows.Forms.Cursors.HSplit;
			this._ccLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this._ccLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._ccLabel, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._ccLabel, L10NSharp.LocalizationPriority.NotLocalizable);
			this._L10NSharpExtender.SetLocalizingId(this._ccLabel, "Publish.Upload.LicenseNotes");
			this._ccLabel.Location = new System.Drawing.Point(0, 0);
			this._ccLabel.Name = "_ccLabel";
			this._ccLabel.Size = new System.Drawing.Size(300, 15);
			this._ccLabel.TabIndex = 7;
			this._ccLabel.Text = "Creative Commons";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Dock = System.Windows.Forms.DockStyle.Left;
			this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this.label7, null);
			this._L10NSharpExtender.SetLocalizationComment(this.label7, null);
			this._L10NSharpExtender.SetLocalizingId(this.label7, "Publish.Upload.Step1");
			this.label7.Location = new System.Drawing.Point(3, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(151, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Step 1: Confirm Metadata";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Dock = System.Windows.Forms.DockStyle.Left;
			this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this.label8, null);
			this._L10NSharpExtender.SetLocalizationComment(this.label8, null);
			this._L10NSharpExtender.SetLocalizingId(this.label8, "Publish.Upload.Step2");
			this.label8.Location = new System.Drawing.Point(0, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(92, 13);
			this.label8.TabIndex = 16;
			this.label8.Text = "Step 2: Upload";
			// 
			// label6
			// 
			this.label6.AutoSize = false;
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this.label6, null);
			this._L10NSharpExtender.SetLocalizationComment(this.label6, null);
			this._L10NSharpExtender.SetLocalizingId(this.label6, "Publish.Upload.Title");
			this.label6.Location = new System.Drawing.Point(3, 21);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(32, 15);
			this.label6.TabIndex = 1;
			this.label6.Text = "Title";
			// 
			// _titleLabel
			// 
			this._titleLabel.AutoSize = true;
			this._titleLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this._titleLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this._titleLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._titleLabel, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._titleLabel, L10NSharp.LocalizationPriority.NotLocalizable);
			this._L10NSharpExtender.SetLocalizingId(this._titleLabel, "Publish.Upload.BloomLibraryPublishControl._titleLabel");
			this._titleLabel.Location = new System.Drawing.Point(3, 34);
			this._titleLabel.Name = "_titleLabel";
			this._titleLabel.Size = new System.Drawing.Size(27, 13);
			this._titleLabel.TabIndex = 2;
			this._titleLabel.Text = "Title";
			// 
			// _copyrightLabel
			// 
			this._copyrightLabel.AutoSize = false;
			this._copyrightLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this._copyrightLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this._copyrightLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._copyrightLabel, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._copyrightLabel, L10NSharp.LocalizationPriority.NotLocalizable);
			this._L10NSharpExtender.SetLocalizingId(this._copyrightLabel, "Publish.Upload.BloomLibraryPublishControl.label9");
			this._copyrightLabel.Location = new System.Drawing.Point(0, 0);
			this._copyrightLabel.Name = "_copyrightLabel";
			this._copyrightLabel.Size = new System.Drawing.Size(604, 15);
			this._copyrightLabel.TabIndex = 9;
			this._copyrightLabel.Text = "Copyright";
			// 
			// label9
			// 
			this.label9.AutoSize = false;
			this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this.label9, null);
			this._L10NSharpExtender.SetLocalizationComment(this.label9, null);
			this._L10NSharpExtender.SetLocalizingId(this.label9, "Publish.Upload.Languages");
			this.label9.Location = new System.Drawing.Point(0, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(300, 15);
			this.label9.TabIndex = 10;
			this.label9.Text = "Languages";
			// 
			// _languagesLabel
			// 
			this._languagesLabel.AutoSize = true;
			this._languagesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._languagesLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this._languagesLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._languagesLabel, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._languagesLabel, L10NSharp.LocalizationPriority.NotLocalizable);
			this._L10NSharpExtender.SetLocalizingId(this._languagesLabel, "");
			this._languagesLabel.Location = new System.Drawing.Point(3, 260);
			this._languagesLabel.Name = "_languagesLabel";
			this._languagesLabel.Size = new System.Drawing.Size(604, 13);
			this._languagesLabel.TabIndex = 11;
			this._languagesLabel.Text = "Langs";
			// 
			// _loginLink
			// 
			this._loginLink.AutoSize = true;
			this._loginLink.Dock = System.Windows.Forms.DockStyle.Right;
			this._L10NSharpExtender.SetLocalizableToolTip(this._loginLink, null);
			this._L10NSharpExtender.SetLocalizationComment(this._loginLink, null);
			this._L10NSharpExtender.SetLocalizingId(this._loginLink, "Publish.Upload.loginLink");
			this._loginLink.Location = new System.Drawing.Point(475, 0);
			this._loginLink.Name = "_loginLink";
			this._loginLink.Size = new System.Drawing.Size(129, 13);
			this._loginLink.TabIndex = 18;
			this._loginLink.TabStop = true;
			this._loginLink.Text = "Log in to BloomLibrary.org";
			this._loginLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this._loginLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._loginLink_LinkClicked);
			// 
			// _termsLink
			// 
			this._termsLink.AutoSize = true;
			this._termsLink.Dock = System.Windows.Forms.DockStyle.Right;
			this._L10NSharpExtender.SetLocalizableToolTip(this._termsLink, null);
			this._L10NSharpExtender.SetLocalizationComment(this._termsLink, null);
			this._L10NSharpExtender.SetLocalizingId(this._termsLink, "Publish.Upload.TermsLink");
			this._termsLink.Location = new System.Drawing.Point(475, 0);
			this._termsLink.Name = "_termsLink";
			this._termsLink.Size = new System.Drawing.Size(129, 13);
			this._termsLink.TabIndex = 19;
			this._termsLink.TabStop = true;
			this._termsLink.Text = "Show Terms of Use";
			this._termsLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this._termsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._termsLink_LinkClicked);
			// 
			// _creditsLabel
			// 
			this._creditsLabel.AutoEllipsis = true;
			this._creditsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._creditsLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this._creditsLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._creditsLabel, null);
			this._L10NSharpExtender.SetLocalizingId(this._creditsLabel, "Publish.Upload.Credits");
			this._creditsLabel.Location = new System.Drawing.Point(3, 300);
			this._creditsLabel.Name = "_creditsLabel";
			this._creditsLabel.Size = new System.Drawing.Size(604, 20);
			this._creditsLabel.TabIndex = 13;
			this._creditsLabel.Text = "credits";
			// 
			// _summaryBox
			// 
			this._summaryBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._L10NSharpExtender.SetLocalizableToolTip(this._summaryBox, null);
			this._L10NSharpExtender.SetLocalizationComment(this._summaryBox, null);
			this._L10NSharpExtender.SetLocalizingId(this._summaryBox, "Publish.Upload.textBox1");
			this._summaryBox.Location = new System.Drawing.Point(3, 77);
			this._summaryBox.Name = "_summaryBox";
			this._summaryBox.Size = new System.Drawing.Size(604, 20);
			this._summaryBox.TabIndex = 4;
			this._summaryBox.TextChanged += new System.EventHandler(this._summaryBox_TextChanged);
			// 
			// _signUpLink
			// 
			this._signUpLink.AutoSize = true;
			this._signUpLink.Dock = System.Windows.Forms.DockStyle.Right;
			this._L10NSharpExtender.SetLocalizableToolTip(this._signUpLink, null);
			this._L10NSharpExtender.SetLocalizationComment(this._signUpLink, null);
			this._L10NSharpExtender.SetLocalizingId(this._signUpLink, "Publish.Upload.signupLink");
			this._signUpLink.Location = new System.Drawing.Point(465, 0);
			this._signUpLink.Name = "_signUpLink";
			this._signUpLink.Size = new System.Drawing.Size(139, 13);
			this._signUpLink.TabIndex = 21;
			this._signUpLink.TabStop = true;
			this._signUpLink.Text = "Sign up for BloomLibrary.org";
			this._signUpLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this._signUpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._signUpLink_LinkClicked);
			// 
			// _optional2
			// 
			this._optional2.AutoSize = true;
			this._optional2.Dock = System.Windows.Forms.DockStyle.Right;
			this._L10NSharpExtender.SetLocalizableToolTip(this._optional2, null);
			this._L10NSharpExtender.SetLocalizationComment(this._optional2, null);
			this._L10NSharpExtender.SetLocalizingId(this._optional2, "Common.Optional");
			this._optional2.Location = new System.Drawing.Point(560, 0);
			this._optional2.Name = "_optional2";
			this._optional2.Size = new System.Drawing.Size(44, 13);
			this._optional2.TabIndex = 23;
			this._optional2.Text = "optional";
			// 
			// _licenseSuggestion
			// 
			this._licenseSuggestion.Dock = System.Windows.Forms.DockStyle.Fill;
			this._licenseSuggestion.ForeColor = System.Drawing.Color.Red;
			this._L10NSharpExtender.SetLocalizableToolTip(this._licenseSuggestion, null);
			this._L10NSharpExtender.SetLocalizationComment(this._licenseSuggestion, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._licenseSuggestion, L10NSharp.LocalizationPriority.NotLocalizable);
			this._L10NSharpExtender.SetLocalizingId(this._licenseSuggestion, "Publish.Upload.BloomLibraryPublishControl._licenseSuggestion");
			this._licenseSuggestion.Location = new System.Drawing.Point(3, 175);
			this._licenseSuggestion.Name = "_licenseSuggestion";
			this._licenseSuggestion.Size = new System.Drawing.Size(604, 30);
			this._licenseSuggestion.TabIndex = 24;
			this._licenseSuggestion.Text = "License Suggestion";
			// 
			// _creativeCommonsLink
			// 
			this._creativeCommonsLink.AutoSize = true;
			this._L10NSharpExtender.SetLocalizableToolTip(this._creativeCommonsLink, null);
			this._L10NSharpExtender.SetLocalizationComment(this._creativeCommonsLink, null);
			this._L10NSharpExtender.SetLocalizingId(this._creativeCommonsLink, "Publish.Upload.ccLink");
			this._creativeCommonsLink.Location = new System.Drawing.Point(128, 0);
			this._creativeCommonsLink.Name = "_creativeCommonsLink";
			this._creativeCommonsLink.Size = new System.Drawing.Size(56, 13);
			this._creativeCommonsLink.TabIndex = 25;
			this._creativeCommonsLink.TabStop = true;
			this._creativeCommonsLink.Text = "CC-BY-NC";
			this._creativeCommonsLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this._creativeCommonsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._creativeCommonsLink_LinkClicked);
			// 
			// _licenseNotesLabel
			// 
			this._licenseNotesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._licenseNotesLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this._licenseNotesLabel, null);
			this._L10NSharpExtender.SetLocalizationComment(this._licenseNotesLabel, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._licenseNotesLabel, L10NSharp.LocalizationPriority.NotLocalizable);
			this._L10NSharpExtender.SetLocalizingId(this._licenseNotesLabel, "Publish.Upload.BloomLibraryPublishControl._licenseSuggestion");
			this._licenseNotesLabel.Location = new System.Drawing.Point(3, 145);
			this._licenseNotesLabel.Name = "_licenseNotesLabel";
			this._licenseNotesLabel.Size = new System.Drawing.Size(604, 30);
			this._licenseNotesLabel.TabIndex = 26;
			this._licenseNotesLabel.Text = "License Notes";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Dock = System.Windows.Forms.DockStyle.Left;
			this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this.label10, null);
			this._L10NSharpExtender.SetLocalizationComment(this.label10, null);
			this._L10NSharpExtender.SetLocalizingId(this.label10, "Publish.Upload.Summary");
			this.label10.Location = new System.Drawing.Point(0, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(57, 13);
			this.label10.TabIndex = 3;
			this.label10.Text = "Summary";

			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Dock = System.Windows.Forms.DockStyle.Left;
			this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this.label11, null);
			this._L10NSharpExtender.SetLocalizationComment(this.label11, null);
			this._L10NSharpExtender.SetLocalizingId(this.label11, "Publish.Upload.Gaurantee");
			this.label11.Location = new System.Drawing.Point(0, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(57, 13);
			this.label11.TabIndex = 3;
			this.label11.Text = "By uploading, you confirm your agreement with the Bloom Library Terms of Use and grant the rights it describes";
			// 
			// _optional1
			// 
			this._optional1.AutoSize = true;
			this._optional1.Dock = System.Windows.Forms.DockStyle.Right;
			this._L10NSharpExtender.SetLocalizableToolTip(this._optional1, null);
			this._L10NSharpExtender.SetLocalizationComment(this._optional1, null);
			this._L10NSharpExtender.SetLocalizingId(this._optional1, "Common.Optional");
			this._optional1.Location = new System.Drawing.Point(560, 0);
			this._optional1.Name = "_optional1";
			this._optional1.Size = new System.Drawing.Size(44, 13);
			this._optional1.TabIndex = 22;
			this._optional1.Text = "optional";
			// 
			// _progressBox
			// 
			this._progressBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._L10NSharpExtender.SetLocalizableToolTip(this._progressBox, null);
			this._L10NSharpExtender.SetLocalizationComment(this._progressBox, null);
			this._L10NSharpExtender.SetLocalizationPriority(this._progressBox, L10NSharp.LocalizationPriority.NotLocalizable);
			this._L10NSharpExtender.SetLocalizingId(this._progressBox, "Publish.Upload.BloomLibraryPublishControl._progressBox");
			this._progressBox.Location = new System.Drawing.Point(3, 393);
			this._progressBox.Name = "_progressBox";
			this._progressBox.Size = new System.Drawing.Size(604, 175);
			this._progressBox.TabIndex = 30;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel1a, 0, 22);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 23);
			this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 24);
			this.tableLayoutPanel1.Controls.Add(this._progressBox, 0, 26);
			this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 19);
			this.tableLayoutPanel1.Controls.Add(this._ccPanel, 0, 9);
			this.tableLayoutPanel1.Controls.Add(this._creditsLabel, 0, 20);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 25);
			this.tableLayoutPanel1.Controls.Add(this._licenseSuggestion, 0, 11);
			this.tableLayoutPanel1.Controls.Add(this._licenseNotesLabel, 0, 10);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this._languagesLabel, 0, 17);
			this.tableLayoutPanel1.Controls.Add(this._titleLabel, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 16);
			this.tableLayoutPanel1.Controls.Add(this._summaryBox, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this._copyrightLabel, 0, 14);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this._labelAfterLicense, 0, 13);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(43, 18);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 27;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(610, 570);
			this.tableLayoutPanel1.TabIndex = 27;
			// 
			// panel1a
			// 
			this.panel1a.Controls.Add(this.label8);
			this.panel1a.Controls.Add(this._signUpLink);
			this.panel1a.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1a.Location = new System.Drawing.Point(3, 331);
			this.panel1a.Name = "panel1a";
			this.panel1a.Size = new System.Drawing.Size(604, 14);
			this.panel1a.TabIndex = 28;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this._uploadButton);
			this.panel2.Controls.Add(this._loginLink);
			this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(604, 25);
			this.panel2.TabIndex = 28;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label3);
			this.panel3.Controls.Add(this._optional2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(3, 284);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(604, 13);
			this.panel3.TabIndex = 28;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.label11);
			this.panel4.Controls.Add(this._termsLink);
			this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(604, 18);
			this.panel4.TabIndex = 29;

			// 
			// _ccPanel
			// 
			this._ccPanel.Controls.Add(this._creativeCommonsLink);
			this._ccPanel.Controls.Add(this._ccLabel);
			this._ccPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._ccPanel.Location = new System.Drawing.Point(3, 124);
			this._ccPanel.Name = "_ccPanel";
			this._ccPanel.Size = new System.Drawing.Size(604, 18);
			this._ccPanel.TabIndex = 28;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this._optional1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 58);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(604, 13);
			this.panel1.TabIndex = 29;
			// 
			// BloomLibraryPublishControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.tableLayoutPanel1);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this._L10NSharpExtender.SetLocalizableToolTip(this, null);
			this._L10NSharpExtender.SetLocalizationComment(this, null);
			this._L10NSharpExtender.SetLocalizingId(this, "Publish.Upload.BloomLibraryPublishControl.BloomLibraryPublishControl");
			this.Name = "BloomLibraryPublishControl";
			this.Size = new System.Drawing.Size(694, 715);
			((System.ComponentModel.ISupportInitialize)(this._L10NSharpExtender)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel1a.ResumeLayout(false);
			this.panel1a.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this._ccPanel.ResumeLayout(false);
			this._ccPanel.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _uploadButton;
		private System.Windows.Forms.Label label1;
		private L10NSharp.UI.L10NSharpExtender _L10NSharpExtender;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label _labelAfterLicense;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label _titleLabel;
		private System.Windows.Forms.Label _copyrightLabel;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label _languagesLabel;
		private System.Windows.Forms.LinkLabel _loginLink;
		private System.Windows.Forms.Label _creditsLabel;
		private System.Windows.Forms.TextBox _summaryBox;
		private System.Windows.Forms.LinkLabel _signUpLink;
		private System.Windows.Forms.Label _optional2;
		private System.Windows.Forms.Label _ccLabel;
		private System.Windows.Forms.Label _licenseSuggestion;
		private System.Windows.Forms.LinkLabel _creativeCommonsLink;
		private System.Windows.Forms.Label _licenseNotesLabel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel _ccPanel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label _optional1;
		private Palaso.UI.WindowsForms.Progress.LogBox _progressBox;
		private System.Windows.Forms.Panel panel1a;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.LinkLabel _termsLink;
		private System.Windows.Forms.Panel panel4;

	}
}
