using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace UseCaseMaker
{
	/// <summary>
	/// Descrizione di riepilogo per frmAbout.
	/// </summary>
	public class frmAbout : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblUCMTitle;
		private System.Windows.Forms.Label lblUCMVersion;
		private System.Windows.Forms.Panel pnlSep1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblUCMWebSiteTitle;
		private System.Windows.Forms.LinkLabel lnkLblUMCCredit1Mail;
		private System.Windows.Forms.Label lblUCMCredit1Name;
		private System.Windows.Forms.Label lblUCMCreditsTitle;
		private System.Windows.Forms.LinkLabel lnkLblUCMWebSite;
		private System.Windows.Forms.LinkLabel lnkLblUCMAuthorMail;
		private System.Windows.Forms.Label lblUCMAuthorName;
		private System.Windows.Forms.Label lblUCMAuthorTitle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		/// <summary>
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmAbout()
		{
			//
			// Necessario per il supporto di Progettazione Windows Form
			//
			InitializeComponent();

			//
			// TODO: aggiungere il codice del costruttore dopo la chiamata a InitializeComponent
			//
			lblUCMVersion.Text = Application.ProductVersion;
		}

		/// <summary>
		/// Pulire le risorse in uso.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Codice generato da Progettazione Windows Form
		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmAbout));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblUCMTitle = new System.Windows.Forms.Label();
			this.lblUCMVersion = new System.Windows.Forms.Label();
			this.pnlSep1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.lblUCMWebSiteTitle = new System.Windows.Forms.Label();
			this.lnkLblUMCCredit1Mail = new System.Windows.Forms.LinkLabel();
			this.lblUCMCredit1Name = new System.Windows.Forms.Label();
			this.lblUCMCreditsTitle = new System.Windows.Forms.Label();
			this.lnkLblUCMWebSite = new System.Windows.Forms.LinkLabel();
			this.lnkLblUCMAuthorMail = new System.Windows.Forms.LinkLabel();
			this.lblUCMAuthorName = new System.Windows.Forms.Label();
			this.lblUCMAuthorTitle = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.pnlSep1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.pictureBox1.Location = new System.Drawing.Point(0, 64);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(304, 256);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lblUCMTitle
			// 
			this.lblUCMTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCMTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
			this.lblUCMTitle.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(153)), ((System.Byte)(0)));
			this.lblUCMTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblUCMTitle.Location = new System.Drawing.Point(8, 8);
			this.lblUCMTitle.Name = "lblUCMTitle";
			this.lblUCMTitle.Size = new System.Drawing.Size(296, 24);
			this.lblUCMTitle.TabIndex = 1;
			this.lblUCMTitle.Text = "Use Case Maker";
			// 
			// lblUCMVersion
			// 
			this.lblUCMVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCMVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.lblUCMVersion.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(153)), ((System.Byte)(0)));
			this.lblUCMVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblUCMVersion.Location = new System.Drawing.Point(8, 40);
			this.lblUCMVersion.Name = "lblUCMVersion";
			this.lblUCMVersion.Size = new System.Drawing.Size(296, 16);
			this.lblUCMVersion.TabIndex = 2;
			this.lblUCMVersion.Text = "[0.0.0]";
			// 
			// pnlSep1
			// 
			this.pnlSep1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlSep1.Controls.Add(this.label1);
			this.pnlSep1.Controls.Add(this.linkLabel1);
			this.pnlSep1.Controls.Add(this.lblUCMWebSiteTitle);
			this.pnlSep1.Controls.Add(this.lnkLblUMCCredit1Mail);
			this.pnlSep1.Controls.Add(this.lblUCMCredit1Name);
			this.pnlSep1.Controls.Add(this.lblUCMCreditsTitle);
			this.pnlSep1.Controls.Add(this.lnkLblUCMWebSite);
			this.pnlSep1.Controls.Add(this.lnkLblUCMAuthorMail);
			this.pnlSep1.Controls.Add(this.lblUCMAuthorName);
			this.pnlSep1.Controls.Add(this.lblUCMAuthorTitle);
			this.pnlSep1.Location = new System.Drawing.Point(312, 8);
			this.pnlSep1.Name = "pnlSep1";
			this.pnlSep1.Size = new System.Drawing.Size(232, 280);
			this.pnlSep1.TabIndex = 14;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(233)), ((System.Byte)(237)), ((System.Byte)(154)));
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(8, 192);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 16);
			this.label1.TabIndex = 23;
			this.label1.Text = "GNU GPL & LGPL licenses:";
			this.label1.UseMnemonic = false;
			// 
			// linkLabel1
			// 
			this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Blue;
			this.linkLabel1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.linkLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 27);
			this.linkLabel1.LinkColor = System.Drawing.Color.Blue;
			this.linkLabel1.Location = new System.Drawing.Point(16, 208);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(216, 16);
			this.linkLabel1.TabIndex = 22;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://www.fsf.org/licenses";
			this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// lblUCMWebSiteTitle
			// 
			this.lblUCMWebSiteTitle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(233)), ((System.Byte)(237)), ((System.Byte)(154)));
			this.lblUCMWebSiteTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCMWebSiteTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblUCMWebSiteTitle.Location = new System.Drawing.Point(8, 144);
			this.lblUCMWebSiteTitle.Name = "lblUCMWebSiteTitle";
			this.lblUCMWebSiteTitle.Size = new System.Drawing.Size(216, 16);
			this.lblUCMWebSiteTitle.TabIndex = 21;
			this.lblUCMWebSiteTitle.Text = "Web Site:";
			// 
			// lnkLblUMCCredit1Mail
			// 
			this.lnkLblUMCCredit1Mail.ActiveLinkColor = System.Drawing.Color.Blue;
			this.lnkLblUMCCredit1Mail.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lnkLblUMCCredit1Mail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lnkLblUMCCredit1Mail.LinkArea = new System.Windows.Forms.LinkArea(0, 31);
			this.lnkLblUMCCredit1Mail.LinkColor = System.Drawing.Color.Blue;
			this.lnkLblUMCCredit1Mail.Location = new System.Drawing.Point(16, 104);
			this.lnkLblUMCCredit1Mail.Name = "lnkLblUMCCredit1Mail";
			this.lnkLblUMCCredit1Mail.Size = new System.Drawing.Size(216, 16);
			this.lnkLblUMCCredit1Mail.TabIndex = 20;
			this.lnkLblUMCCredit1Mail.TabStop = true;
			this.lnkLblUMCCredit1Mail.Text = "rufinelli@users.sourceforge.net";
			this.lnkLblUMCCredit1Mail.VisitedLinkColor = System.Drawing.Color.Blue;
			this.lnkLblUMCCredit1Mail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblUMCCredit1Mail_LinkClicked);
			// 
			// lblUCMCredit1Name
			// 
			this.lblUCMCredit1Name.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(233)), ((System.Byte)(237)), ((System.Byte)(154)));
			this.lblUCMCredit1Name.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCMCredit1Name.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblUCMCredit1Name.Location = new System.Drawing.Point(16, 88);
			this.lblUCMCredit1Name.Name = "lblUCMCredit1Name";
			this.lblUCMCredit1Name.Size = new System.Drawing.Size(216, 16);
			this.lblUCMCredit1Name.TabIndex = 19;
			this.lblUCMCredit1Name.Text = "Marco Rufinelli";
			// 
			// lblUCMCreditsTitle
			// 
			this.lblUCMCreditsTitle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(233)), ((System.Byte)(237)), ((System.Byte)(154)));
			this.lblUCMCreditsTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCMCreditsTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblUCMCreditsTitle.Location = new System.Drawing.Point(8, 72);
			this.lblUCMCreditsTitle.Name = "lblUCMCreditsTitle";
			this.lblUCMCreditsTitle.Size = new System.Drawing.Size(216, 16);
			this.lblUCMCreditsTitle.TabIndex = 18;
			this.lblUCMCreditsTitle.Text = "Credits:";
			// 
			// lnkLblUCMWebSite
			// 
			this.lnkLblUCMWebSite.ActiveLinkColor = System.Drawing.Color.Blue;
			this.lnkLblUCMWebSite.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lnkLblUCMWebSite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lnkLblUCMWebSite.LinkArea = new System.Windows.Forms.LinkArea(0, 37);
			this.lnkLblUCMWebSite.LinkColor = System.Drawing.Color.Blue;
			this.lnkLblUCMWebSite.Location = new System.Drawing.Point(16, 160);
			this.lnkLblUCMWebSite.Name = "lnkLblUCMWebSite";
			this.lnkLblUCMWebSite.Size = new System.Drawing.Size(216, 16);
			this.lnkLblUCMWebSite.TabIndex = 17;
			this.lnkLblUCMWebSite.TabStop = true;
			this.lnkLblUCMWebSite.Text = "http://use-case-maker.sourceforge.net";
			this.lnkLblUCMWebSite.VisitedLinkColor = System.Drawing.Color.Blue;
			this.lnkLblUCMWebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblUCMWebSite_LinkClicked);
			// 
			// lnkLblUCMAuthorMail
			// 
			this.lnkLblUCMAuthorMail.ActiveLinkColor = System.Drawing.Color.Blue;
			this.lnkLblUCMAuthorMail.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lnkLblUCMAuthorMail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lnkLblUCMAuthorMail.LinkArea = new System.Windows.Forms.LinkArea(0, 31);
			this.lnkLblUCMAuthorMail.LinkColor = System.Drawing.Color.Blue;
			this.lnkLblUCMAuthorMail.Location = new System.Drawing.Point(16, 40);
			this.lnkLblUCMAuthorMail.Name = "lnkLblUCMAuthorMail";
			this.lnkLblUCMAuthorMail.Size = new System.Drawing.Size(216, 16);
			this.lnkLblUCMAuthorMail.TabIndex = 16;
			this.lnkLblUCMAuthorMail.TabStop = true;
			this.lnkLblUCMAuthorMail.Text = "gaspardis@users.sourceforge.net";
			this.lnkLblUCMAuthorMail.VisitedLinkColor = System.Drawing.Color.Blue;
			this.lnkLblUCMAuthorMail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblUCMAuthorMail_LinkClicked);
			// 
			// lblUCMAuthorName
			// 
			this.lblUCMAuthorName.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(233)), ((System.Byte)(237)), ((System.Byte)(154)));
			this.lblUCMAuthorName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCMAuthorName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblUCMAuthorName.Location = new System.Drawing.Point(16, 24);
			this.lblUCMAuthorName.Name = "lblUCMAuthorName";
			this.lblUCMAuthorName.Size = new System.Drawing.Size(216, 16);
			this.lblUCMAuthorName.TabIndex = 15;
			this.lblUCMAuthorName.Text = "Gabriele Gaspardis";
			// 
			// lblUCMAuthorTitle
			// 
			this.lblUCMAuthorTitle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(233)), ((System.Byte)(237)), ((System.Byte)(154)));
			this.lblUCMAuthorTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCMAuthorTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblUCMAuthorTitle.Location = new System.Drawing.Point(8, 8);
			this.lblUCMAuthorTitle.Name = "lblUCMAuthorTitle";
			this.lblUCMAuthorTitle.Size = new System.Drawing.Size(216, 16);
			this.lblUCMAuthorTitle.TabIndex = 14;
			this.lblUCMAuthorTitle.Text = "Author:";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnOK.Location = new System.Drawing.Point(464, 296);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(80, 24);
			this.btnOK.TabIndex = 15;
			this.btnOK.Text = "OK";
			// 
			// frmAbout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(233)), ((System.Byte)(237)), ((System.Byte)(154)));
			this.ClientSize = new System.Drawing.Size(554, 324);
			this.ControlBox = false;
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.pnlSep1);
			this.Controls.Add(this.lblUCMVersion);
			this.Controls.Add(this.lblUCMTitle);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAbout";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Use Case Maker";
			this.pnlSep1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void lnkLblUCMAuthorMail_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:gaspardis@users.sourceforge.net");
		}

		private void lnkLblUMCCredit1Mail_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:rufinelli@users.sourceforge.net");
		}

		private void lnkLblUCMWebSite_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://use-case-maker.sourceforge.net");
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.fsf.org/licenses");
		}
	}
}
