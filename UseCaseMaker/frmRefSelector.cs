using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using UseCaseMakerLibrary;

namespace UseCaseMaker
{
	/// <summary>
	/// Descrizione di riepilogo per frmRefSelector.
	/// </summary>
	public class frmRefSelector : System.Windows.Forms.Form
	{
		private Model model;
		private UseCase caller;
		private UseCase selected;
		private Localizer localizer;

		private System.Windows.Forms.Label lblStereotypeTitle;
		private System.Windows.Forms.TextBox tbStereotype;
		private System.Windows.Forms.Label lblUpperUseCase;
		private System.Windows.Forms.Label lblLowerUseCase;
		private System.Windows.Forms.Label lblDepFromTitle;
		private System.Windows.Forms.Label lblSelectUseCaseTitle;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnSwapUseCases;
		private System.Windows.Forms.TreeView tvModelBrowser;
		private System.Windows.Forms.ImageList imgListModelBrowser;
		private System.Windows.Forms.GroupBox gbRelationship;
		private System.ComponentModel.IContainer components;

		public frmRefSelector(UseCase caller, Model model, Localizer localizer)
		{
			//
			// Necessario per il supporto di Progettazione Windows Form
			//
			InitializeComponent();

			//
			// TODO: aggiungere il codice del costruttore dopo la chiamata a InitializeComponent
			//
			this.caller = caller;
			this.model = model;
			this.localizer = localizer;
			this.localizer.LocalizeControls(this);

			this.lblUpperUseCase.Text = caller.Name;

			BuildView(this.model);
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmRefSelector));
			this.lblStereotypeTitle = new System.Windows.Forms.Label();
			this.tbStereotype = new System.Windows.Forms.TextBox();
			this.lblSelectUseCaseTitle = new System.Windows.Forms.Label();
			this.tvModelBrowser = new System.Windows.Forms.TreeView();
			this.gbRelationship = new System.Windows.Forms.GroupBox();
			this.btnSwapUseCases = new System.Windows.Forms.Button();
			this.lblDepFromTitle = new System.Windows.Forms.Label();
			this.lblLowerUseCase = new System.Windows.Forms.Label();
			this.lblUpperUseCase = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.imgListModelBrowser = new System.Windows.Forms.ImageList(this.components);
			this.gbRelationship.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblStereotypeTitle
			// 
			this.lblStereotypeTitle.Location = new System.Drawing.Point(8, 8);
			this.lblStereotypeTitle.Name = "lblStereotypeTitle";
			this.lblStereotypeTitle.Size = new System.Drawing.Size(120, 16);
			this.lblStereotypeTitle.TabIndex = 0;
			this.lblStereotypeTitle.Text = "[Stereotype]";
			// 
			// tbStereotype
			// 
			this.tbStereotype.Location = new System.Drawing.Point(136, 8);
			this.tbStereotype.Name = "tbStereotype";
			this.tbStereotype.Size = new System.Drawing.Size(328, 20);
			this.tbStereotype.TabIndex = 1;
			this.tbStereotype.Text = "";
			// 
			// lblSelectUseCaseTitle
			// 
			this.lblSelectUseCaseTitle.Location = new System.Drawing.Point(8, 40);
			this.lblSelectUseCaseTitle.Name = "lblSelectUseCaseTitle";
			this.lblSelectUseCaseTitle.Size = new System.Drawing.Size(120, 32);
			this.lblSelectUseCaseTitle.TabIndex = 2;
			this.lblSelectUseCaseTitle.Text = "[Select use case]";
			// 
			// tvModelBrowser
			// 
			this.tvModelBrowser.ImageList = this.imgListModelBrowser;
			this.tvModelBrowser.Location = new System.Drawing.Point(136, 40);
			this.tvModelBrowser.Name = "tvModelBrowser";
			this.tvModelBrowser.Size = new System.Drawing.Size(328, 160);
			this.tvModelBrowser.TabIndex = 3;
			this.tvModelBrowser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvModelBrowser_AfterSelect);
			// 
			// gbRelationship
			// 
			this.gbRelationship.Controls.Add(this.btnSwapUseCases);
			this.gbRelationship.Controls.Add(this.lblDepFromTitle);
			this.gbRelationship.Controls.Add(this.lblLowerUseCase);
			this.gbRelationship.Controls.Add(this.lblUpperUseCase);
			this.gbRelationship.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.gbRelationship.Location = new System.Drawing.Point(8, 208);
			this.gbRelationship.Name = "gbRelationship";
			this.gbRelationship.Size = new System.Drawing.Size(456, 112);
			this.gbRelationship.TabIndex = 4;
			this.gbRelationship.TabStop = false;
			this.gbRelationship.Text = "[Relationship]";
			// 
			// btnSwapUseCases
			// 
			this.btnSwapUseCases.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSwapUseCases.Location = new System.Drawing.Point(344, 24);
			this.btnSwapUseCases.Name = "btnSwapUseCases";
			this.btnSwapUseCases.Size = new System.Drawing.Size(104, 80);
			this.btnSwapUseCases.TabIndex = 4;
			this.btnSwapUseCases.Text = "[Swap]";
			this.btnSwapUseCases.Click += new System.EventHandler(this.btnSwapUseCases_Click);
			// 
			// lblDepFromTitle
			// 
			this.lblDepFromTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblDepFromTitle.Location = new System.Drawing.Point(8, 56);
			this.lblDepFromTitle.Name = "lblDepFromTitle";
			this.lblDepFromTitle.Size = new System.Drawing.Size(328, 24);
			this.lblDepFromTitle.TabIndex = 6;
			this.lblDepFromTitle.Text = "[depends from]";
			this.lblDepFromTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblLowerUseCase
			// 
			this.lblLowerUseCase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLowerUseCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLowerUseCase.Location = new System.Drawing.Point(8, 80);
			this.lblLowerUseCase.Name = "lblLowerUseCase";
			this.lblLowerUseCase.Size = new System.Drawing.Size(328, 24);
			this.lblLowerUseCase.TabIndex = 3;
			// 
			// lblUpperUseCase
			// 
			this.lblUpperUseCase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblUpperUseCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUpperUseCase.Location = new System.Drawing.Point(8, 24);
			this.lblUpperUseCase.Name = "lblUpperUseCase";
			this.lblUpperUseCase.Size = new System.Drawing.Size(328, 24);
			this.lblUpperUseCase.TabIndex = 2;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.CausesValidation = false;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(240, 336);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(120, 24);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "[Cancel]";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(112, 336);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(120, 24);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "[OK]";
			// 
			// imgListModelBrowser
			// 
			this.imgListModelBrowser.ImageSize = new System.Drawing.Size(16, 16);
			this.imgListModelBrowser.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListModelBrowser.ImageStream")));
			this.imgListModelBrowser.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// frmRefSelector
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(474, 368);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.gbRelationship);
			this.Controls.Add(this.tvModelBrowser);
			this.Controls.Add(this.lblSelectUseCaseTitle);
			this.Controls.Add(this.tbStereotype);
			this.Controls.Add(this.lblStereotypeTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmRefSelector";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "[Use case dependency selector]";
			this.gbRelationship.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	
		private void BuildView(object element)
		{
			if(element.GetType() == typeof(Model))
			{
				Model model = (Model)element;
				AddElement(model,null);
				foreach(UseCase useCase in model.UseCases.Sorted("ID"))
				{
					useCase.Owner = model;
					AddElement(useCase,useCase.Owner);
				}
				foreach(Package subPackage in model.Packages.Sorted("ID"))
				{
					subPackage.Owner = model;
					BuildView(subPackage);
				}
			}
			if(element.GetType() == typeof(Package))
			{
				Package package = (Package)element;
				AddElement(package,package.Owner);
				foreach(UseCase useCase in package.UseCases.Sorted("ID"))
				{
					useCase.Owner = package;
					AddElement(useCase,useCase.Owner);
				}
				foreach(Package subPackage in package.Packages.Sorted("ID"))
				{
					subPackage.Owner = package;
					BuildView(subPackage);
				}
			}
		}

		private void AddElement(object element, object owner)
		{
			String ownerUniqueID = String.Empty;

			if(element.GetType() == typeof(Model))
			{
				Model model = (Model)element;
				tvModelBrowser.Nodes.Clear();
				TreeNode node = new TreeNode(model.Name + " (" + model.ElementID + ")");
				node.Tag = model.UniqueID;
				tvModelBrowser.Nodes.Add(node);
				tvModelBrowser.SelectedNode = node;
				TreeNode ownerNode = node;
				node = new TreeNode(this.localizer.GetValue("Globals","UseCases"),1,1);
				node.Tag = model.UseCases.UniqueID;
				ownerNode.Nodes.Add(node);
			}

			if(element.GetType() == typeof(Package))
			{
				Package package = (Package)element;
				ownerUniqueID = ((Package)owner).UniqueID;
				TreeNode node = new TreeNode(package.Name + " (" + package.ElementID + ")");
				node.Tag = package.UniqueID;
				TreeNode ownerNode = this.FindNode(null,ownerUniqueID);
				if(ownerNode != null)
				{
					ownerNode.Nodes.Add(node);
					tvModelBrowser.SelectedNode = node;
					ownerNode = node;
					node = new TreeNode(this.localizer.GetValue("Globals","UseCases"),1,1);
					node.Tag = package.UseCases.UniqueID;
					ownerNode.Nodes.Add(node);
				}
			}
			if(element.GetType() == typeof(UseCase))
			{
				UseCase useCase = (UseCase)element;
				Package package = (Package)owner;
				TreeNode node = new TreeNode(useCase.Name + " (" + useCase.ElementID + ")",2,2);
				node.Tag = useCase.UniqueID;
				TreeNode ownerNode = this.FindNode(null,package.UniqueID);
				if(ownerNode != null)
				{
					foreach(TreeNode subNode in ownerNode.Nodes)
					{
						if((String)subNode.Tag == package.UseCases.UniqueID)
						{
							subNode.Nodes.Add(node);
							tvModelBrowser.SelectedNode = node;
							break;
						}
					}
				}
			}
		}

		private TreeNode FindNode(TreeNode parent, String parentUniqueID)
		{
			TreeNode node = null;
			TreeNode retNode = null;

			if(tvModelBrowser.Nodes.Count == 0)
			{
				return null;
			}
			
			if(parent == null)
			{
				node = tvModelBrowser.Nodes[0];
			}
			else
			{
				node = parent;
			}

			if((String)node.Tag == parentUniqueID)
			{
				return node;
			}

			foreach(TreeNode child in node.Nodes)
			{
				if((String)child.Tag == parentUniqueID)
				{
					retNode = child;
					break;
				}
				if(child.Nodes.Count > 0)
				{
					retNode = this.FindNode(child,parentUniqueID);
					if(retNode != null)
					{
						break;
					}
				}
			}

			return retNode;
		}

		private void tvModelBrowser_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode node = tvModelBrowser.SelectedNode;
			object element = model.FindElementByUniqueID((String)node.Tag);
			if(element.GetType() == typeof(UseCase))
			{
				selected = (UseCase)element;
				this.lblUpperUseCase.Text = caller.Name;
				this.lblLowerUseCase.Text = selected.Name;
			}
		}

		private void btnSwapUseCases_Click(object sender, System.EventArgs e)
		{
			string tmp = this.lblUpperUseCase.Text;
			this.lblUpperUseCase.Text = this.lblLowerUseCase.Text;
			this.lblLowerUseCase.Text = tmp;
		}
	}
}
