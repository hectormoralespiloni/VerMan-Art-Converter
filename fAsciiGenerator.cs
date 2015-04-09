using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace asciiart
{
	public class fAsciiGenerator : System.Windows.Forms.Form
	{
		#region System Variables
		private System.Windows.Forms.OpenFileDialog openDlg;
		private System.Windows.Forms.TextBox imagePath;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboResolution;
		private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.LinkLabel labelProcess;
		private System.Windows.Forms.LinkLabel labelPiloni;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboType;
		private System.Windows.Forms.Label lInputText;
		private System.Windows.Forms.TextBox inputText;
		private System.Windows.Forms.Label lColorMode;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboColorMode;
		private System.Windows.Forms.FontDialog fontDlg;
		private System.Windows.Forms.TextBox fontName;
		private System.Windows.Forms.LinkLabel labelFont;
		#endregion

		private Bitmap bmp;			//Contains the source bitmap
		private Rectangle bounds;	//Used to represent bitmap's width and height
		private bool cancelled;		//To know whenever user press cancel button
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.SaveFileDialog saveAsciiDlg;
		private System.Windows.Forms.SaveFileDialog saveMosaicDlg;
		private System.Windows.Forms.ToolTip toolTip1;
        private Label label11;
        private Label label13;
        private GroupBox gFontSize;
        private RadioButton radioLuminance;
        private RadioButton radioFixed;
        private Button bLoadText;
        private LinkLabel labelLoad;
        private OpenFileDialog openTextFile;
		
		//DEFINE YOUR DEFAULT FONT HERE!
		private Font myFont = new Font("Impact",10);

		private enum Resolution {ULTRAHIGH, HIGH, NORMAL, LOW, ULTRALOW };
		private enum Type {ASCII, MOSAIC};
		private enum Colormode {FULLCOLOR, GRAYSCALE};
		private enum ImageFilter {JPEG=1, BMP};

		public fAsciiGenerator() 
		{
			InitializeComponent();
			comboResolution.SelectedIndex = (int)Resolution.NORMAL;
			comboType.SelectedIndex = (int)Type.ASCII;
			comboColorMode.SelectedIndex = (int)Colormode.FULLCOLOR;
			cancelled = false;
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fAsciiGenerator));
            this.openDlg = new System.Windows.Forms.OpenFileDialog();
            this.imagePath = new System.Windows.Forms.TextBox();
            this.comboResolution = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.saveAsciiDlg = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelProcess = new System.Windows.Forms.LinkLabel();
            this.labelPiloni = new System.Windows.Forms.LinkLabel();
            this.bCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.lInputText = new System.Windows.Forms.Label();
            this.inputText = new System.Windows.Forms.TextBox();
            this.lColorMode = new System.Windows.Forms.Label();
            this.comboColorMode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fontDlg = new System.Windows.Forms.FontDialog();
            this.fontName = new System.Windows.Forms.TextBox();
            this.labelFont = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveMosaicDlg = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.radioFixed = new System.Windows.Forms.RadioButton();
            this.radioLuminance = new System.Windows.Forms.RadioButton();
            this.bLoadText = new System.Windows.Forms.Button();
            this.labelLoad = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.gFontSize = new System.Windows.Forms.GroupBox();
            this.openTextFile = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gFontSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // openDlg
            // 
            this.openDlg.Filter = "Image files (*.jpg, *.gif. *.bmp) |*.jpg;*.gif;*.bmp";
            this.openDlg.Title = "Select your source image";
            // 
            // imagePath
            // 
            this.imagePath.Location = new System.Drawing.Point(10, 28);
            this.imagePath.Name = "imagePath";
            this.imagePath.ReadOnly = true;
            this.imagePath.Size = new System.Drawing.Size(604, 22);
            this.imagePath.TabIndex = 1;
            // 
            // comboResolution
            // 
            this.comboResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboResolution.Enabled = false;
            this.comboResolution.Items.AddRange(new object[] {
            "1. Ultra High Resolution",
            "2. High Resolution",
            "3. Normal Resolution",
            "4. Low Resolution",
            "5. Ultra Low Resolution"});
            this.comboResolution.Location = new System.Drawing.Point(10, 308);
            this.comboResolution.Name = "comboResolution";
            this.comboResolution.Size = new System.Drawing.Size(600, 24);
            this.comboResolution.TabIndex = 4;
            this.toolTip1.SetToolTip(this.comboResolution, "For text mosaic you should use Ultra High quality");
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 287);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "3. Select image quality...";
            // 
            // saveAsciiDlg
            // 
            this.saveAsciiDlg.DefaultExt = "txt";
            this.saveAsciiDlg.Filter = "Text files (*.txt)|*.txt";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(10, 366);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(604, 18);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 7;
            // 
            // labelProcess
            // 
            this.labelProcess.Enabled = false;
            this.labelProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProcess.Location = new System.Drawing.Point(10, 344);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(604, 19);
            this.labelProcess.TabIndex = 9;
            this.labelProcess.TabStop = true;
            this.labelProcess.Text = "4. Process image";
            this.toolTip1.SetToolTip(this.labelProcess, "Click to process your image");
            this.labelProcess.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ProcessImage);
            // 
            // labelPiloni
            // 
            this.labelPiloni.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPiloni.Location = new System.Drawing.Point(467, 477);
            this.labelPiloni.Name = "labelPiloni";
            this.labelPiloni.Size = new System.Drawing.Size(147, 18);
            this.labelPiloni.TabIndex = 13;
            this.labelPiloni.TabStop = true;
            this.labelPiloni.Text = "http://www.piloni.net";
            this.labelPiloni.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelPiloni.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenBrowser);
            // 
            // bCancel
            // 
            this.bCancel.Enabled = false;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCancel.ForeColor = System.Drawing.Color.Red;
            this.bCancel.Image = ((System.Drawing.Image)(resources.GetObject("bCancel.Image")));
            this.bCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCancel.Location = new System.Drawing.Point(261, 390);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(105, 35);
            this.bCancel.TabIndex = 14;
            this.bCancel.Text = "&Cancel";
            this.bCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bCancel.Click += new System.EventHandler(this.CancelProcess);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 18);
            this.label1.TabIndex = 15;
            this.label1.Text = "2. Select output type...";
            // 
            // comboType
            // 
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.Enabled = false;
            this.comboType.Items.AddRange(new object[] {
            "1. ASCII Art",
            "2. Text Mosaic"});
            this.comboType.Location = new System.Drawing.Point(10, 83);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(600, 24);
            this.comboType.TabIndex = 16;
            this.comboType.SelectedIndexChanged += new System.EventHandler(this.SelectOutput);
            // 
            // lInputText
            // 
            this.lInputText.Enabled = false;
            this.lInputText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lInputText.ForeColor = System.Drawing.Color.SeaGreen;
            this.lInputText.Location = new System.Drawing.Point(10, 118);
            this.lInputText.Name = "lInputText";
            this.lInputText.Size = new System.Drawing.Size(297, 19);
            this.lInputText.TabIndex = 17;
            this.lInputText.Text = "2.1 Input Text";
            // 
            // inputText
            // 
            this.inputText.Enabled = false;
            this.inputText.Location = new System.Drawing.Point(10, 137);
            this.inputText.Multiline = true;
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(473, 24);
            this.inputText.TabIndex = 18;
            this.toolTip1.SetToolTip(this.inputText, "Type here the text you want to produce the image from");
            // 
            // lColorMode
            // 
            this.lColorMode.Enabled = false;
            this.lColorMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lColorMode.ForeColor = System.Drawing.Color.SeaGreen;
            this.lColorMode.Location = new System.Drawing.Point(10, 230);
            this.lColorMode.Name = "lColorMode";
            this.lColorMode.Size = new System.Drawing.Size(297, 18);
            this.lColorMode.TabIndex = 22;
            this.lColorMode.Text = "2.4 Color Mode";
            // 
            // comboColorMode
            // 
            this.comboColorMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboColorMode.Enabled = false;
            this.comboColorMode.Items.AddRange(new object[] {
            "1. Full color (24 bit)",
            "2. Grayscale"});
            this.comboColorMode.Location = new System.Drawing.Point(10, 251);
            this.comboColorMode.Name = "comboColorMode";
            this.comboColorMode.Size = new System.Drawing.Size(600, 24);
            this.comboColorMode.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(483, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 19);
            this.label5.TabIndex = 24;
            this.label5.Text = "(text mosaic only)";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(189, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 18);
            this.label7.TabIndex = 25;
            this.label7.Text = "(text mosaic only)";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(10, 440);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(254, 18);
            this.label8.TabIndex = 26;
            this.label8.Text = "VerMan\'s Art Generator v1.5";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(140, 477);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 18);
            this.label9.TabIndex = 28;
            this.label9.Text = "Powered!";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(10, 458);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 37);
            this.label10.TabIndex = 29;
            this.label10.Text = "C#";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(70, 461);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 30;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(467, 458);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 19);
            this.label3.TabIndex = 32;
            this.label3.Text = "Héctor Morales Piloni";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fontName
            // 
            this.fontName.Font = new System.Drawing.Font("Impact", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontName.Location = new System.Drawing.Point(10, 193);
            this.fontName.Name = "fontName";
            this.fontName.ReadOnly = true;
            this.fontName.Size = new System.Drawing.Size(297, 24);
            this.fontName.TabIndex = 34;
            this.fontName.Text = "Impact";
            this.toolTip1.SetToolTip(this.fontName, "Impact 8 produces very good results. You may try another one...");
            this.fontName.WordWrap = false;
            // 
            // labelFont
            // 
            this.labelFont.Enabled = false;
            this.labelFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFont.Location = new System.Drawing.Point(10, 172);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(297, 18);
            this.labelFont.TabIndex = 35;
            this.labelFont.TabStop = true;
            this.labelFont.Text = "2.2 Select Font";
            this.toolTip1.SetToolTip(this.labelFont, "Click to select a different font");
            this.labelFont.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SelectFont);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(280, 458);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 31);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PayPal);
            // 
            // saveMosaicDlg
            // 
            this.saveMosaicDlg.Filter = "JPEG Files (*.jpg)|*.jpg|BMP Files (*.bmp)|*.bmp";
            // 
            // radioFixed
            // 
            this.radioFixed.AutoSize = true;
            this.radioFixed.Checked = true;
            this.radioFixed.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioFixed.ForeColor = System.Drawing.Color.Black;
            this.radioFixed.Location = new System.Drawing.Point(6, 21);
            this.radioFixed.Name = "radioFixed";
            this.radioFixed.Size = new System.Drawing.Size(62, 21);
            this.radioFixed.TabIndex = 0;
            this.radioFixed.TabStop = true;
            this.radioFixed.Text = "Fixed";
            this.toolTip1.SetToolTip(this.radioFixed, "The font size of the generated image will be of the same size of the selected fon" +
                    "t");
            this.radioFixed.UseVisualStyleBackColor = true;
            // 
            // radioLuminance
            // 
            this.radioLuminance.AutoSize = true;
            this.radioLuminance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioLuminance.ForeColor = System.Drawing.Color.Black;
            this.radioLuminance.Location = new System.Drawing.Point(74, 21);
            this.radioLuminance.Name = "radioLuminance";
            this.radioLuminance.Size = new System.Drawing.Size(98, 21);
            this.radioLuminance.TabIndex = 1;
            this.radioLuminance.Text = "Luminance";
            this.toolTip1.SetToolTip(this.radioLuminance, "The font size of the generated image will change according to original image lumi" +
                    "nance");
            this.radioLuminance.UseVisualStyleBackColor = true;
            // 
            // bLoadText
            // 
            this.bLoadText.Enabled = false;
            this.bLoadText.Location = new System.Drawing.Point(489, 137);
            this.bLoadText.Name = "bLoadText";
            this.bLoadText.Size = new System.Drawing.Size(121, 23);
            this.bLoadText.TabIndex = 43;
            this.bLoadText.Text = "Load from file...";
            this.toolTip1.SetToolTip(this.bLoadText, "Load text from a file (UTF8)");
            this.bLoadText.UseVisualStyleBackColor = true;
            this.bLoadText.Click += new System.EventHandler(this.bLoadText_Click);
            // 
            // labelLoad
            // 
            this.labelLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoad.Location = new System.Drawing.Point(10, 9);
            this.labelLoad.Name = "labelLoad";
            this.labelLoad.Size = new System.Drawing.Size(297, 19);
            this.labelLoad.TabIndex = 8;
            this.labelLoad.TabStop = true;
            this.labelLoad.Text = "1. Load source image";
            this.toolTip1.SetToolTip(this.labelLoad, "Click to load the image you want to convert");
            this.labelLoad.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LoadImage);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(189, 171);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 19);
            this.label11.TabIndex = 37;
            this.label11.Text = "(text mosaic only)";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(173, -1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 18);
            this.label13.TabIndex = 41;
            this.label13.Text = "(text mosaic only)";
            // 
            // gFontSize
            // 
            this.gFontSize.Controls.Add(this.radioLuminance);
            this.gFontSize.Controls.Add(this.label13);
            this.gFontSize.Controls.Add(this.radioFixed);
            this.gFontSize.Enabled = false;
            this.gFontSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gFontSize.ForeColor = System.Drawing.Color.SeaGreen;
            this.gFontSize.Location = new System.Drawing.Point(313, 172);
            this.gFontSize.Name = "gFontSize";
            this.gFontSize.Size = new System.Drawing.Size(297, 45);
            this.gFontSize.TabIndex = 42;
            this.gFontSize.TabStop = false;
            this.gFontSize.Text = "2.3 Font size";
            // 
            // openTextFile
            // 
            this.openTextFile.FileName = "openTextFile";
            this.openTextFile.Filter = "Text files (*.txt) |*.txt";
            this.openTextFile.Title = "Select your input text file";
            // 
            // fAsciiGenerator
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(626, 503);
            this.Controls.Add(this.bLoadText);
            this.Controls.Add(this.gFontSize);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelFont);
            this.Controls.Add(this.fontName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.labelPiloni);
            this.Controls.Add(this.labelProcess);
            this.Controls.Add(this.labelLoad);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboResolution);
            this.Controls.Add(this.imagePath);
            this.Controls.Add(this.comboColorMode);
            this.Controls.Add(this.lColorMode);
            this.Controls.Add(this.inputText);
            this.Controls.Add(this.lInputText);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "fAsciiGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VerMan\'s Art Generator v1.5";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gFontSize.ResumeLayout(false);
            this.gFontSize.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new fAsciiGenerator());
		}


		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//frees the bitmap if still loaded
			if(bmp != null)
				bmp.Dispose();
		}

		private void LoadImage(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			#region Load Source Image
			if(openDlg.ShowDialog() == DialogResult.OK)
			{
				imagePath.Text = openDlg.FileName;
				Image img = Image.FromFile(imagePath.Text);
				bmp = new Bitmap(img, new Size(img.Width, img.Height));
				bounds = new Rectangle(0,0,bmp.Width,bmp.Height);
				img.Dispose();
				labelProcess.Enabled = true;
				comboType.Enabled = true;
				comboResolution.Enabled = true;
			}
			#endregion		
		}

		private void ProcessImage(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(comboType.SelectedIndex == (int)Type.ASCII)
				doASCII();
			else
				doMOSAIC();
		}

		private void doASCII()
		{
			StringBuilder asciiArt;	//to store our ASCII art
			Stream myStream;		//stream to write the output file to
			Bitmap temp;			//used to grayscale the original bmp

			temp = new Bitmap(bmp);
			asciiArt = new StringBuilder();
			labelLoad.Enabled = false;
			labelProcess.Enabled = false;
			comboType.Enabled = false;
			comboResolution.Enabled = false;
			bCancel.Enabled = true;

			#region Grayscale image
			ColorMatrix matrix = new ColorMatrix();

			matrix[0,0] = 1/3f;
			matrix[0,1] = 1/3f;
			matrix[0,2] = 1/3f;
			matrix[1,0] = 1/3f;
			matrix[1,1] = 1/3f;
			matrix[1,2] = 1/3f;
			matrix[2,0] = 1/3f;
			matrix[2,1] = 1/3f;
			matrix[2,2] = 1/3f;

			ImageAttributes attributes = new ImageAttributes();
			attributes.SetColorMatrix(matrix);

			Graphics gphGrey = Graphics.FromImage(temp);
			gphGrey.DrawImage(temp, bounds, 0, 0, temp.Width, temp.Height, GraphicsUnit.Pixel, attributes);
			gphGrey.Dispose();
			#endregion

			#region ASCII image
			int pixwidth;

			switch (comboResolution.SelectedIndex) 
			{
				case (int)Resolution.ULTRAHIGH:
					pixwidth = 1;
					break;
				case (int)Resolution.HIGH:
					pixwidth = 2;
					break;
				case (int)Resolution.NORMAL:
					pixwidth = 3;
					break;
				case (int)Resolution.LOW:
					pixwidth = 6;
					break;
				case (int)Resolution.ULTRALOW:
					pixwidth = 8;
					break;
				default:
					pixwidth = 3;
					break;
			}

			int pixhight = pixwidth * 2;
			int pixseg = pixwidth * pixhight;

			progressBar1.Maximum = temp.Height/pixhight;
			for(int h=0; h < temp.Height/pixhight; h++)
			{
				Application.DoEvents();
				if(cancelled)
					break;

				progressBar1.Increment(1);
				// segment hight
				int startY = (h * pixhight);
				// segment width
				for(int w=0; w < temp.Width/pixwidth; w++)
				{
					int startX = (w * pixwidth);
					int allBrightness = 0;

					// each pix of this segment
					for(int y=0; y<pixwidth; y++)
					{
						for(int x=0; x < pixhight; x++)
						{
							int cY = y + startY;
							int cX = x + startX;
							try 
							{
								Color c = temp.GetPixel(cX,cY);
								int b = (int)(c.GetBrightness() * 100);
								allBrightness = allBrightness + b;
							}
							catch 
							{
								allBrightness = (allBrightness + 50);
							}
						}
					}

					int sb = (allBrightness / pixseg);
					if (sb < 10 )
						asciiArt.Append("#");
					else if (sb < 17 )
						asciiArt.Append("@");
					else if (sb < 24)
						asciiArt.Append("&");
					else if (sb < 31)
						asciiArt.Append("$");
					else if (sb < 38)
						asciiArt.Append("%");
					else if (sb < 45)
						asciiArt.Append("|");
					else if (sb < 52)
						asciiArt.Append("!");
					else if (sb < 59)
						asciiArt.Append(";");
					else if (sb < 66)
						asciiArt.Append(":");
					else if (sb < 73)
						asciiArt.Append("'");
					else if (sb < 80)
						asciiArt.Append("`");
					else if (sb < 87)
						asciiArt.Append(".");
					else
						asciiArt.Append(" ");
				}//for
				asciiArt.Append("\r\n");
			}
			#endregion		

			#region Save Stream
			if((saveAsciiDlg.ShowDialog() == DialogResult.OK) && !cancelled)
			{
				if((myStream = saveAsciiDlg.OpenFile()) != null)
				{
					ASCIIEncoding ascii = new ASCIIEncoding();
					Byte[] bytes = ascii.GetBytes(asciiArt.ToString());
					myStream.Write(bytes,0,bytes.Length);
					myStream.Close();
				}
			}
			#endregion		

			labelLoad.Enabled = true;
			labelProcess.Enabled = true;
			comboType.Enabled = true;
			comboResolution.Enabled = true;
			bCancel.Enabled = false;
			cancelled = false;
			progressBar1.Value = 0;
			temp.Dispose();
		}

		private void doMOSAIC()
		{
			if(inputText.Text.Trim().Length == 0) {
				MessageBox.Show("You must enter at least 1 character!","Error");
				return;
			}

			Color bitmapColor;		//to store source bmp pixel colors
			Bitmap mosaicBmp;		//to store our Text Mosaic
			Bitmap temp;			//used to grayscale the original bmp 
			Graphics g;				//graphics canvas which represents the mosaic bitmap
			SolidBrush drawBrush;	//default brush style for drawing mosaic bitmap
			FontStyle drawStyle;	//default font style for drawing mosaic bitmap
			string message;			//this is the message used to draw mosaic bitmap
			int posX, posY;			//used to position the characters of mosaic bitmap in the canvas
			int messageIndex;		//used to traverse the message (to draw 1 character at a time)
			int stepX, stepY;
			int pixwidth, pixhight;

			labelLoad.Enabled = false;
			labelProcess.Enabled = false;
			comboType.Enabled = false;
			comboResolution.Enabled = false;
			comboColorMode.Enabled = false;
			inputText.Enabled = false;
			labelFont.Enabled = false;
            lInputText.Enabled = false;
            bLoadText.Enabled = false;
            gFontSize.Enabled = false;
            lColorMode.Enabled = false;
			bCancel.Enabled = true;

            //get the line spacing of this font
            stepY = myFont.Height;
			//get the char spacing of this font
            stepX = (int)Math.Ceiling(myFont.SizeInPoints);

			switch (comboResolution.SelectedIndex) 
			{
				case (int)Resolution.ULTRAHIGH:
					pixwidth = 1;
					break;
				case (int)Resolution.HIGH:
					pixwidth = 2;
					break;
				case (int)Resolution.NORMAL:
					pixwidth = 3;
					break;
				case (int)Resolution.LOW:
					pixwidth = 6;
					break;
				case (int)Resolution.ULTRALOW:
					pixwidth = 8;
					break;
				default:
					pixwidth = 3;
					break;
			}
			pixhight = pixwidth * 2;
			mosaicBmp = new Bitmap(stepX*bmp.Width/pixwidth,stepY*bmp.Height/pixhight,PixelFormat.Format24bppRgb);
			temp = new Bitmap(bmp);

			g = Graphics.FromImage(mosaicBmp);
			g.Clear(Color.Black);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			drawBrush = new SolidBrush(Color.Empty);
			drawStyle = myFont.Style;
			bitmapColor = Color.Empty;

			//replace ENTER key for space
			inputText.Text = inputText.Text.Replace("\r\n"," ");
			//don't let two spaces be together
			inputText.Text = inputText.Text.Replace("  "," ");
			message = inputText.Text.Trim() + "";

			messageIndex = posX = posY = 0;

			#region Grayscale image
			if(comboColorMode.SelectedIndex == (int)Colormode.GRAYSCALE)
			{
				ColorMatrix matrix = new ColorMatrix();

				matrix[0,0] = 1/3f;
				matrix[0,1] = 1/3f;
				matrix[0,2] = 1/3f;
				matrix[1,0] = 1/3f;
				matrix[1,1] = 1/3f;
				matrix[1,2] = 1/3f;
				matrix[2,0] = 1/3f;
				matrix[2,1] = 1/3f;
				matrix[2,2] = 1/3f;

				ImageAttributes attributes = new ImageAttributes();
				attributes.SetColorMatrix(matrix);

				Graphics gphGrey = Graphics.FromImage(temp);
				gphGrey.DrawImage(temp, bounds, 0, 0, temp.Width, temp.Height, GraphicsUnit.Pixel, attributes);
				gphGrey.Dispose();
			}
			#endregion

			#region Text MOSAIC
			int pixseg = pixwidth * pixhight;

			progressBar1.Maximum = temp.Height/pixhight;
			for(int h=0; h < temp.Height/pixhight; h++)
			{
				Application.DoEvents();
				if(cancelled)
					break;

				progressBar1.Increment(1);
				// segment height
				int startY = (h * pixhight);

				// segment width
				for(int w=0; w < temp.Width/pixwidth; w++)
				{
					int startX = (w * pixwidth);
					int allBrightness = 0;

					// each pix of this segment
					for(int y=0; y<pixwidth; y++)
					{
						for(int x=0; x < pixhight; x++)
						{
							int cY = y + startY;
							int cX = x + startX;
							try 
							{
								bitmapColor = temp.GetPixel(cX,cY);
								int b = (int)(bitmapColor.GetBrightness() * 100);
								allBrightness = allBrightness + b;
							}
							catch 
							{
								allBrightness = (allBrightness + 50);
							}
						}
					}

					drawBrush.Color = bitmapColor;
					int sb = (allBrightness / pixseg);
                    
                    //compute new font size according to brightness
                    int newSize = (int)myFont.Size;

                    //if fontsize is Luminance, adjust the % of new font size increment according 
                    //to brightness. Maximum size (when sb = 100) will be 60% bigger than base size
                    double growth;
                    if (radioLuminance.Checked)
                        growth = sb * 0.006;
                    else
                        growth = 0;
                    newSize += (int)Math.Ceiling(myFont.Size*growth);

                    //create new font
                    Font newFont = new Font(myFont.Name, newSize, drawStyle);

                    //draw this character
                    g.DrawString(message[messageIndex].ToString(), newFont, drawBrush, posX, posY);

                    /*
					if (sb < 10 )
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size,drawStyle),drawBrush,posX,posY+6);
					else if (sb < 17 )
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size,drawStyle),drawBrush,posX,posY+6);
					else if (sb < 24)
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+1,drawStyle),drawBrush,posX,posY+5);
					else if (sb < 31)
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+1,drawStyle),drawBrush,posX,posY+5);
					else if (sb < 38)
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+2,drawStyle),drawBrush,posX,posY+4);
					else if (sb < 45)
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+2,drawStyle),drawBrush,posX,posY+4);
					else if (sb < 52)
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+3,drawStyle),drawBrush,posX,posY+3);
					else if (sb < 59)
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+3,drawStyle),drawBrush,posX,posY+3);
					else if (sb < 66)
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+4,drawStyle),drawBrush,posX,posY+2);
					else if (sb < 73)
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+4,drawStyle),drawBrush,posX,posY+2);
					else if (sb < 80)
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+5,drawStyle),drawBrush,posX,posY+1);
					else if (sb < 87)
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+5,drawStyle),drawBrush,posX,posY+1);
					else
						g.DrawString(message[messageIndex].ToString(),new Font(myFont.Name,myFont.Size+6,drawStyle),drawBrush,posX,posY);
					*/
                    posX+=stepX;
					messageIndex++;
					if(messageIndex >= message.Length)
						messageIndex = 0;
				}//for
				posY+=stepY;
				posX=0;
			}
			#endregion		

			#region Save Image
			if((saveMosaicDlg.ShowDialog() == DialogResult.OK) && !cancelled)
			{
				if(saveMosaicDlg.FilterIndex == (int)ImageFilter.JPEG) 
				{
					saveMosaicDlg.DefaultExt = "jpg";
					mosaicBmp.Save(saveMosaicDlg.FileName,ImageFormat.Jpeg);
				}
				else 
				{
					saveMosaicDlg.DefaultExt = "bmp";
					mosaicBmp.Save(saveMosaicDlg.FileName,ImageFormat.Bmp);
				}
			}
			#endregion		

			labelLoad.Enabled = true;
			labelProcess.Enabled = true;
			comboType.Enabled = true;
			comboResolution.Enabled = true;
			comboColorMode.Enabled = true;
			inputText.Enabled = true;
			labelFont.Enabled = true;
            lInputText.Enabled = true;
            bLoadText.Enabled = true;
            gFontSize.Enabled = true;
            lColorMode.Enabled = true;
			bCancel.Enabled = false;
			cancelled = false;
			progressBar1.Value = 0;			
			mosaicBmp.Dispose();
			temp.Dispose();
			g.Dispose();
		}

		private void SelectOutput(object sender, System.EventArgs e)
		{
			#region Select Output type
			if(comboType.SelectedIndex == (int)Type.MOSAIC)
			{
				//enable input text box & colormode
				inputText.Enabled = true;
                lInputText.Enabled = true;
                bLoadText.Enabled = true;
                labelFont.Enabled = true;
                gFontSize.Enabled = true;
                lColorMode.Enabled = true;
				comboColorMode.Enabled = true;

				//select Ultra High Resolution by default
				comboResolution.SelectedIndex = (int)Resolution.ULTRAHIGH;
			}
			else
			{
				//disable input text box & colormode
                inputText.Enabled = false;
                lInputText.Enabled = false;
                bLoadText.Enabled = false;
                labelFont.Enabled = false;
                gFontSize.Enabled = false;
                lColorMode.Enabled = false;
                comboColorMode.Enabled = false;

				//default resolution for ASCII Art is Normal
				comboResolution.SelectedIndex = (int)Resolution.NORMAL;
			}
			#endregion
		}
		
		private void SelectFont(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			#region Select Font
			fontDlg.ShowColor = false;
			fontDlg.ShowEffects = false;
			fontDlg.ShowHelp = false;
            fontDlg.Font = new Font(myFont.Name, myFont.Size, myFont.Style);
			if(fontDlg.ShowDialog() == DialogResult.OK)
			{
				fontName.Text = fontDlg.Font.Name;
				fontName.Font = fontDlg.Font;
				//change default font to selected font
				myFont = fontDlg.Font;
			}
			#endregion
		}
		
		private void CancelProcess(object sender, System.EventArgs e)
		{
			cancelled = true;
		}

		private void OpenBrowser(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.piloni.net");
		}

		private void PayPal(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.piloni.net/donate.html");
		}

		private void ShowAbout(object sender, System.EventArgs e)
		{
			if(this.Height < 300)
				this.Height = 320;
			else
				this.Height = 256;
		}

        private void bLoadText_Click(object sender, EventArgs e)
        {
            #region Load text from file
            if (openTextFile.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = File.OpenText(openTextFile.FileName))
                {
                    inputText.Text = reader.ReadToEnd();
                }
            }
            #endregion
        }
	}
}
