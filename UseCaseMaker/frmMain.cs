using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using UseCaseMakerLibrary;
using UseCaseMakerControls;

/**
 * @defgroup user_interface Interfaccia utente
 */

namespace UseCaseMaker
{
	/**
	 * @brief Finestra principale
	 */
	public class frmMain : System.Windows.Forms.Form
	{
		[DllImport("user32")] public static extern int LockWindowUpdate(IntPtr hwnd);

		private const string defaultUCPrefix = "UC";
		private const string defaultPPrefix = "P";
		private const string defaultAPrefix = "A";
		private const string defaultMPrefix = "M";
		private const string defaultGPrefix = "G";

		private Model model = null;
		private object currentElement = null;
		private bool lockUpdate = false;
		private SepararatorCollection separators = new SepararatorCollection();
		private HighLightDescriptorCollection hdc = new HighLightDescriptorCollection();
		private string modelFilePath = string.Empty;
		private string modelFileName = string.Empty;
		private bool modified = false;
		private bool modifiedLocked = false;
		private Localizer localizer = new Localizer();
		private ApplicationSettings appSettings = new ApplicationSettings();

		ToolTip lvRelatedDocsTooltip = new ToolTip();
		ToolTip lvActorsTooltip = new ToolTip();

		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.Splitter splLeft;
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.TabControl tabUseCase;
		private System.Windows.Forms.TabPage pgFlowOfEvents;
		private System.Windows.Forms.TabPage pgProse;
		private System.Windows.Forms.TabPage pgDetails;
		private System.Windows.Forms.TabPage pgAttributes;
		private System.Windows.Forms.TabPage pgHistory;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.TextBox tbPreconditions;
		private System.Windows.Forms.Label lblPreconditionsTitle;
		private System.Windows.Forms.TextBox tbPostconditions;
		private System.Windows.Forms.Label lblPostconditionsTitle;
		private System.Windows.Forms.Button btnSetPrimaryActor;
		private System.Windows.Forms.Button btnRemoveActor;
		private System.Windows.Forms.Button btnAddActor;
		private System.Windows.Forms.ListView lvActors;
		private System.Windows.Forms.ColumnHeader chActorName;
		private System.Windows.Forms.ColumnHeader chActorPrimary;
		private System.Windows.Forms.TextBox tbPriority;
		private System.Windows.Forms.Label lblPriorityTitle;
		private System.Windows.Forms.Label lblLevelTitle;
		private System.Windows.Forms.ComboBox cmbLevel;
		private System.Windows.Forms.ComboBox cmbComplexity;
		private System.Windows.Forms.Label lblComplexityTitle;
		private System.Windows.Forms.ComboBox cmbStatus;
		private System.Windows.Forms.Label lblStatusTitle;
		private System.Windows.Forms.Button btnStatusToHistory;
		private System.Windows.Forms.Button btnImplToHistory;
		private System.Windows.Forms.Label lblImplTitle;
		private System.Windows.Forms.Label lblAssignedToTitle;
		private System.Windows.Forms.TextBox tbRelease;
		private System.Windows.Forms.Button btnRemoveOpenIssue;
		private System.Windows.Forms.Button btnAddOpenIssue;
		private System.Windows.Forms.TextBox tbDescription;
		private System.Windows.Forms.TextBox tbNotes;
		private System.Windows.Forms.Button btnRemoveRelatedDoc;
		private System.Windows.Forms.Button btnAddRelatedDoc;
		private System.Windows.Forms.ListView lvRelatedDocs;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Label lblStepsTitle;
		private System.Windows.Forms.ListView lvHistory;
		private System.Windows.Forms.ColumnHeader chDate;
		private System.Windows.Forms.ColumnHeader chType;
		private System.Windows.Forms.ColumnHeader chAction;
		private System.Windows.Forms.TabPage pgRequirements;
		private System.Windows.Forms.TabPage pgUCGeneral;
		private System.Windows.Forms.Label lblUCIDTitle;
		private System.Windows.Forms.Label lblUCID;
		private System.Windows.Forms.Label lblUCOwner;
		private System.Windows.Forms.Label lblUCOwnerTitle;
		private System.Windows.Forms.Label lblUCNameTitle;
		private System.Windows.Forms.TabPage pgAGeneral;
		private System.Windows.Forms.TabPage pgMain;
		private System.Windows.Forms.Label lblANameTitle;
		private System.Windows.Forms.Label lblAOwner;
		private System.Windows.Forms.Label lblAOwnerTitle;
		private System.Windows.Forms.Label lblAID;
		private System.Windows.Forms.Label lblAIDTitle;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuFileNew;
		private System.Windows.Forms.MenuItem mnuFileOpen;
		private System.Windows.Forms.MenuItem mnuFileSep1;
		private System.Windows.Forms.MenuItem mnuFileSave;
		private System.Windows.Forms.MenuItem mnuFileSep2;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.MenuItem mnuHelpAbout;
		private System.Windows.Forms.TreeView tvModelBrowser;
		private System.Windows.Forms.Panel pnlModelBrowser;
		private System.Windows.Forms.Label lblModelBrowser;
		private System.Windows.Forms.ToolBarButton tbBtnNew;
		private System.Windows.Forms.ToolBarButton tbBtnOpen;
		private System.Windows.Forms.ToolBarButton tbBtnSave;
		private System.Windows.Forms.ToolBarButton tbBtnSep1;
		private System.Windows.Forms.ToolBarButton tbBtnAddActor;
		private System.Windows.Forms.ToolBarButton tbBtnRemoveActor;
		private System.Windows.Forms.ToolBarButton tbBtnAddUseCase;
		private System.Windows.Forms.ToolBarButton tbBtnRemoveUseCase;
		private System.Windows.Forms.ImageList imgListToolBar;
		private System.Windows.Forms.ImageList imgListModelBrowser;
		private System.Windows.Forms.ColumnHeader chNotes;
		private System.Windows.Forms.Panel pnlFullPathContainer;
		private System.Windows.Forms.Panel pnlPackagesContainer;
		private System.Windows.Forms.Label lblPackages;
		private System.Windows.Forms.Label lblPackagesTitle;
		private System.Windows.Forms.Panel pnlActorsContainer;
		private System.Windows.Forms.Label lblActors;
		private System.Windows.Forms.Label lblActorsTitle;
		private System.Windows.Forms.Panel pnlUseCasesContainer;
		private System.Windows.Forms.Label lblUseCases;
		private System.Windows.Forms.Label lblUseCasesTitle;
		private System.Windows.Forms.Label lblFullPath;
		private System.Windows.Forms.Label lblFullPathTitle;
		private System.Windows.Forms.TabPage pgPGeneral;
		private System.Windows.Forms.Label lblPNameTitle;
		private System.Windows.Forms.Label lblPOwner;
		private System.Windows.Forms.Label lblPOwnerTitle;
		private System.Windows.Forms.Label lblPID;
		private System.Windows.Forms.Label lblPIDTitle;
		private System.Windows.Forms.ToolBarButton tbBtnSep3;
		private System.Windows.Forms.ToolBarButton tbBtnAddPackage;
		private System.Windows.Forms.ToolBarButton tbBtnRemovePackage;
		private System.Windows.Forms.ToolBarButton tbBtnSep2;
		private System.Windows.Forms.OpenFileDialog openModelFileDialog;
		private System.Windows.Forms.SaveFileDialog saveModelFileDialog;
		private System.Windows.Forms.Button btnAddAltStep;
		private System.Windows.Forms.Button btnRemoveStep;
		private System.Windows.Forms.Button btnAddStep;
		private System.Windows.Forms.Button btnInsertAltStep;
		private System.Windows.Forms.Button btnInsertStep;
		private System.Windows.Forms.Button btnRemoveRequirement;
		private System.Windows.Forms.Button btnAddRequirement;
		private System.Windows.Forms.Label lblAName;
		private System.Windows.Forms.Button btnANameChange;
		private System.Windows.Forms.Button btnUCNameChange;
		private System.Windows.Forms.Label lblUCName;
		private System.Windows.Forms.Label lblPName;
		private System.Windows.Forms.Button btnPNameChange;
		private System.Windows.Forms.MenuItem mnuEditRemovePackage;
		private System.Windows.Forms.MenuItem mnuEditSep1;
		private System.Windows.Forms.MenuItem mnuEditRemoveUseCase;
		private System.Windows.Forms.MenuItem mnuEditSep2;
		private System.Windows.Forms.MenuItem mnuFileSaveAs;
		private System.Windows.Forms.MenuItem mnuEditAddUseCase;
		private System.Windows.Forms.MenuItem mnuEditAddActor;
		private System.Windows.Forms.MenuItem mnuEditAddPackage;
		private System.Windows.Forms.MenuItem mnuEditRemoveActor;
		private System.Windows.Forms.TextBox tbAssignedTo;
		private System.Windows.Forms.ComboBox cmbImplementation;
		private System.Windows.Forms.OpenFileDialog selectDocFileDialog;
		private System.Windows.Forms.Button btnOpenRelatedDoc;
		private System.Windows.Forms.ContextMenu modelBrowserCtxMenu;
		private System.Windows.Forms.MenuItem mnuCtxMBAddPackage;
		private System.Windows.Forms.MenuItem mnuCtxMBRemovePackage;
		private System.Windows.Forms.MenuItem mnuCtxMBSep1;
		private System.Windows.Forms.MenuItem mnuCtxMBAddActor;
		private System.Windows.Forms.MenuItem mnuCtxMBRemoveActor;
		private System.Windows.Forms.MenuItem mnuCtxMBSep2;
		private System.Windows.Forms.MenuItem mnuCtxMBAddUseCase;
		private System.Windows.Forms.MenuItem mnuCtxMBRemoveUseCase;
		private System.Windows.Forms.Panel pnlAbout;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.MenuItem mnuTools;
		private System.Windows.Forms.MenuItem mnuToolsHtmlExport;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Label lblReleaseTitle;
		private System.Windows.Forms.Label lblOpenIssuesTitle;
		private System.Windows.Forms.Label lblUCActorsTitle;
		private System.Windows.Forms.Label lblRelatedDocsTitle;
		private System.Windows.Forms.Label lblNotesTitle;
		private System.Windows.Forms.Label lblDescriptionTitle;
		private System.Windows.Forms.Button btnRemoveHistoryItem;
		private System.Windows.Forms.MenuItem mnuFileRecent1;
		private System.Windows.Forms.MenuItem mnuFileRecent2;
		private System.Windows.Forms.MenuItem mnuFileRecent3;
		private System.Windows.Forms.MenuItem mnuFileRecent4;
		private System.Windows.Forms.MenuItem mnuFileSep3;
		private System.Windows.Forms.TabPage pgGlossary;
		private System.Windows.Forms.Button btnRemoveGlossaryItem;
		private System.Windows.Forms.Button btnAddGlossaryItem;
		private System.Windows.Forms.Button btnChangeGlossaryItem;
		private System.Windows.Forms.MenuItem mnuToolsSep1;
		private System.Windows.Forms.MenuItem mnuToolsOptions;
		private System.Windows.Forms.MenuItem mnuToolsSep2;
		private System.Windows.Forms.MenuItem mnuToolsXMIExport;
		private System.Windows.Forms.MenuItem mnuToolsPDFExport;
		private UseCaseMakerControls.IndexedList RList;
		private UseCaseMakerControls.IndexedList GList;
		private UseCaseMakerControls.LinkEnabledRTB tbProse;
		private UseCaseMakerControls.IndexedList OIList;
		private UseCaseMakerControls.IndexedList UCList;
		private System.Windows.Forms.MenuItem mnuCtxETGoToDefinition;
		private System.Windows.Forms.ContextMenu elementTokenCtxMenu;
		private System.ComponentModel.IContainer components;

		public frmMain(string openFromCmdLine)
		{
			//
			// Necessario per il supporto di Progettazione Windows Form
			//
			Application.EnableVisualStyles();
			Application.DoEvents();
			InitializeComponent();

			// Clear the design time added tab pages
			tabUseCase.TabPages.Clear();
			
			//
			// TODO: aggiungere il codice del costruttore dopo la chiamata a InitializeComponent
			//
			if(openFromCmdLine != string.Empty)
			{
				this.modelFilePath = Path.GetDirectoryName(openFromCmdLine);
				this.modelFileName = Path.GetFileName(openFromCmdLine);
			}
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

		#region Codice generato da Progettazione Windows Form
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuFileNew = new System.Windows.Forms.MenuItem();
			this.mnuFileOpen = new System.Windows.Forms.MenuItem();
			this.mnuFileSep1 = new System.Windows.Forms.MenuItem();
			this.mnuFileSave = new System.Windows.Forms.MenuItem();
			this.mnuFileSaveAs = new System.Windows.Forms.MenuItem();
			this.mnuFileSep2 = new System.Windows.Forms.MenuItem();
			this.mnuFileRecent1 = new System.Windows.Forms.MenuItem();
			this.mnuFileRecent2 = new System.Windows.Forms.MenuItem();
			this.mnuFileRecent3 = new System.Windows.Forms.MenuItem();
			this.mnuFileRecent4 = new System.Windows.Forms.MenuItem();
			this.mnuFileSep3 = new System.Windows.Forms.MenuItem();
			this.mnuFileExit = new System.Windows.Forms.MenuItem();
			this.mnuEdit = new System.Windows.Forms.MenuItem();
			this.mnuEditAddPackage = new System.Windows.Forms.MenuItem();
			this.mnuEditRemovePackage = new System.Windows.Forms.MenuItem();
			this.mnuEditSep1 = new System.Windows.Forms.MenuItem();
			this.mnuEditAddActor = new System.Windows.Forms.MenuItem();
			this.mnuEditRemoveActor = new System.Windows.Forms.MenuItem();
			this.mnuEditSep2 = new System.Windows.Forms.MenuItem();
			this.mnuEditAddUseCase = new System.Windows.Forms.MenuItem();
			this.mnuEditRemoveUseCase = new System.Windows.Forms.MenuItem();
			this.mnuTools = new System.Windows.Forms.MenuItem();
			this.mnuToolsHtmlExport = new System.Windows.Forms.MenuItem();
			this.mnuToolsPDFExport = new System.Windows.Forms.MenuItem();
			this.mnuToolsSep1 = new System.Windows.Forms.MenuItem();
			this.mnuToolsXMIExport = new System.Windows.Forms.MenuItem();
			this.mnuToolsSep2 = new System.Windows.Forms.MenuItem();
			this.mnuToolsOptions = new System.Windows.Forms.MenuItem();
			this.mnuHelp = new System.Windows.Forms.MenuItem();
			this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
			this.tvModelBrowser = new System.Windows.Forms.TreeView();
			this.modelBrowserCtxMenu = new System.Windows.Forms.ContextMenu();
			this.mnuCtxMBAddPackage = new System.Windows.Forms.MenuItem();
			this.mnuCtxMBRemovePackage = new System.Windows.Forms.MenuItem();
			this.mnuCtxMBSep1 = new System.Windows.Forms.MenuItem();
			this.mnuCtxMBAddActor = new System.Windows.Forms.MenuItem();
			this.mnuCtxMBRemoveActor = new System.Windows.Forms.MenuItem();
			this.mnuCtxMBSep2 = new System.Windows.Forms.MenuItem();
			this.mnuCtxMBAddUseCase = new System.Windows.Forms.MenuItem();
			this.mnuCtxMBRemoveUseCase = new System.Windows.Forms.MenuItem();
			this.imgListModelBrowser = new System.Windows.Forms.ImageList(this.components);
			this.pnlModelBrowser = new System.Windows.Forms.Panel();
			this.lblModelBrowser = new System.Windows.Forms.Label();
			this.splLeft = new System.Windows.Forms.Splitter();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.tbBtnNew = new System.Windows.Forms.ToolBarButton();
			this.tbBtnOpen = new System.Windows.Forms.ToolBarButton();
			this.tbBtnSave = new System.Windows.Forms.ToolBarButton();
			this.tbBtnSep1 = new System.Windows.Forms.ToolBarButton();
			this.tbBtnAddPackage = new System.Windows.Forms.ToolBarButton();
			this.tbBtnRemovePackage = new System.Windows.Forms.ToolBarButton();
			this.tbBtnSep2 = new System.Windows.Forms.ToolBarButton();
			this.tbBtnAddActor = new System.Windows.Forms.ToolBarButton();
			this.tbBtnRemoveActor = new System.Windows.Forms.ToolBarButton();
			this.tbBtnSep3 = new System.Windows.Forms.ToolBarButton();
			this.tbBtnAddUseCase = new System.Windows.Forms.ToolBarButton();
			this.tbBtnRemoveUseCase = new System.Windows.Forms.ToolBarButton();
			this.imgListToolBar = new System.Windows.Forms.ImageList(this.components);
			this.tabUseCase = new System.Windows.Forms.TabControl();
			this.pgMain = new System.Windows.Forms.TabPage();
			this.pnlAbout = new System.Windows.Forms.Panel();
			this.lblVersion = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlUseCasesContainer = new System.Windows.Forms.Panel();
			this.lblUseCases = new System.Windows.Forms.Label();
			this.lblUseCasesTitle = new System.Windows.Forms.Label();
			this.pnlActorsContainer = new System.Windows.Forms.Panel();
			this.lblActors = new System.Windows.Forms.Label();
			this.lblActorsTitle = new System.Windows.Forms.Label();
			this.pnlPackagesContainer = new System.Windows.Forms.Panel();
			this.lblPackages = new System.Windows.Forms.Label();
			this.lblPackagesTitle = new System.Windows.Forms.Label();
			this.pnlFullPathContainer = new System.Windows.Forms.Panel();
			this.lblFullPath = new System.Windows.Forms.Label();
			this.lblFullPathTitle = new System.Windows.Forms.Label();
			this.pgGlossary = new System.Windows.Forms.TabPage();
			this.GList = new UseCaseMakerControls.IndexedList();
			this.btnChangeGlossaryItem = new System.Windows.Forms.Button();
			this.btnRemoveGlossaryItem = new System.Windows.Forms.Button();
			this.btnAddGlossaryItem = new System.Windows.Forms.Button();
			this.pgHistory = new System.Windows.Forms.TabPage();
			this.btnRemoveHistoryItem = new System.Windows.Forms.Button();
			this.lvHistory = new System.Windows.Forms.ListView();
			this.chDate = new System.Windows.Forms.ColumnHeader();
			this.chType = new System.Windows.Forms.ColumnHeader();
			this.chAction = new System.Windows.Forms.ColumnHeader();
			this.chNotes = new System.Windows.Forms.ColumnHeader();
			this.pgDetails = new System.Windows.Forms.TabPage();
			this.OIList = new UseCaseMakerControls.IndexedList();
			this.btnRemoveOpenIssue = new System.Windows.Forms.Button();
			this.btnAddOpenIssue = new System.Windows.Forms.Button();
			this.lblOpenIssuesTitle = new System.Windows.Forms.Label();
			this.tbRelease = new System.Windows.Forms.TextBox();
			this.lblReleaseTitle = new System.Windows.Forms.Label();
			this.tbAssignedTo = new System.Windows.Forms.TextBox();
			this.lblAssignedToTitle = new System.Windows.Forms.Label();
			this.btnImplToHistory = new System.Windows.Forms.Button();
			this.cmbImplementation = new System.Windows.Forms.ComboBox();
			this.lblImplTitle = new System.Windows.Forms.Label();
			this.btnStatusToHistory = new System.Windows.Forms.Button();
			this.cmbStatus = new System.Windows.Forms.ComboBox();
			this.lblStatusTitle = new System.Windows.Forms.Label();
			this.cmbComplexity = new System.Windows.Forms.ComboBox();
			this.lblComplexityTitle = new System.Windows.Forms.Label();
			this.cmbLevel = new System.Windows.Forms.ComboBox();
			this.lblLevelTitle = new System.Windows.Forms.Label();
			this.tbPriority = new System.Windows.Forms.TextBox();
			this.lblPriorityTitle = new System.Windows.Forms.Label();
			this.pgFlowOfEvents = new System.Windows.Forms.TabPage();
			this.UCList = new UseCaseMakerControls.IndexedList();
			this.btnInsertAltStep = new System.Windows.Forms.Button();
			this.btnInsertStep = new System.Windows.Forms.Button();
			this.btnAddAltStep = new System.Windows.Forms.Button();
			this.btnRemoveStep = new System.Windows.Forms.Button();
			this.btnAddStep = new System.Windows.Forms.Button();
			this.lblStepsTitle = new System.Windows.Forms.Label();
			this.pgProse = new System.Windows.Forms.TabPage();
			this.tbProse = new UseCaseMakerControls.LinkEnabledRTB();
			this.pgAttributes = new System.Windows.Forms.TabPage();
			this.btnOpenRelatedDoc = new System.Windows.Forms.Button();
			this.btnRemoveRelatedDoc = new System.Windows.Forms.Button();
			this.btnAddRelatedDoc = new System.Windows.Forms.Button();
			this.lvRelatedDocs = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.lblRelatedDocsTitle = new System.Windows.Forms.Label();
			this.tbNotes = new System.Windows.Forms.TextBox();
			this.lblNotesTitle = new System.Windows.Forms.Label();
			this.tbDescription = new System.Windows.Forms.TextBox();
			this.lblDescriptionTitle = new System.Windows.Forms.Label();
			this.pgRequirements = new System.Windows.Forms.TabPage();
			this.RList = new UseCaseMakerControls.IndexedList();
			this.btnRemoveRequirement = new System.Windows.Forms.Button();
			this.btnAddRequirement = new System.Windows.Forms.Button();
			this.pgUCGeneral = new System.Windows.Forms.TabPage();
			this.lblUCName = new System.Windows.Forms.Label();
			this.btnUCNameChange = new System.Windows.Forms.Button();
			this.btnSetPrimaryActor = new System.Windows.Forms.Button();
			this.btnRemoveActor = new System.Windows.Forms.Button();
			this.btnAddActor = new System.Windows.Forms.Button();
			this.lvActors = new System.Windows.Forms.ListView();
			this.chActorName = new System.Windows.Forms.ColumnHeader();
			this.chActorPrimary = new System.Windows.Forms.ColumnHeader();
			this.lblUCActorsTitle = new System.Windows.Forms.Label();
			this.tbPostconditions = new System.Windows.Forms.TextBox();
			this.lblPostconditionsTitle = new System.Windows.Forms.Label();
			this.tbPreconditions = new System.Windows.Forms.TextBox();
			this.lblPreconditionsTitle = new System.Windows.Forms.Label();
			this.lblUCNameTitle = new System.Windows.Forms.Label();
			this.lblUCOwner = new System.Windows.Forms.Label();
			this.lblUCOwnerTitle = new System.Windows.Forms.Label();
			this.lblUCID = new System.Windows.Forms.Label();
			this.lblUCIDTitle = new System.Windows.Forms.Label();
			this.pgAGeneral = new System.Windows.Forms.TabPage();
			this.btnANameChange = new System.Windows.Forms.Button();
			this.lblAName = new System.Windows.Forms.Label();
			this.lblANameTitle = new System.Windows.Forms.Label();
			this.lblAOwner = new System.Windows.Forms.Label();
			this.lblAOwnerTitle = new System.Windows.Forms.Label();
			this.lblAID = new System.Windows.Forms.Label();
			this.lblAIDTitle = new System.Windows.Forms.Label();
			this.pgPGeneral = new System.Windows.Forms.TabPage();
			this.btnPNameChange = new System.Windows.Forms.Button();
			this.lblPName = new System.Windows.Forms.Label();
			this.lblPNameTitle = new System.Windows.Forms.Label();
			this.lblPOwner = new System.Windows.Forms.Label();
			this.lblPOwnerTitle = new System.Windows.Forms.Label();
			this.lblPID = new System.Windows.Forms.Label();
			this.lblPIDTitle = new System.Windows.Forms.Label();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.openModelFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveModelFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.selectDocFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.elementTokenCtxMenu = new System.Windows.Forms.ContextMenu();
			this.mnuCtxETGoToDefinition = new System.Windows.Forms.MenuItem();
			this.pnlModelBrowser.SuspendLayout();
			this.tabUseCase.SuspendLayout();
			this.pgMain.SuspendLayout();
			this.pnlAbout.SuspendLayout();
			this.pnlUseCasesContainer.SuspendLayout();
			this.pnlActorsContainer.SuspendLayout();
			this.pnlPackagesContainer.SuspendLayout();
			this.pnlFullPathContainer.SuspendLayout();
			this.pgGlossary.SuspendLayout();
			this.pgHistory.SuspendLayout();
			this.pgDetails.SuspendLayout();
			this.pgFlowOfEvents.SuspendLayout();
			this.pgProse.SuspendLayout();
			this.pgAttributes.SuspendLayout();
			this.pgRequirements.SuspendLayout();
			this.pgUCGeneral.SuspendLayout();
			this.pgAGeneral.SuspendLayout();
			this.pgPGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mnuFile,
																					 this.mnuEdit,
																					 this.mnuTools,
																					 this.mnuHelp});
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuFileNew,
																					this.mnuFileOpen,
																					this.mnuFileSep1,
																					this.mnuFileSave,
																					this.mnuFileSaveAs,
																					this.mnuFileSep2,
																					this.mnuFileRecent1,
																					this.mnuFileRecent2,
																					this.mnuFileRecent3,
																					this.mnuFileRecent4,
																					this.mnuFileSep3,
																					this.mnuFileExit});
			this.mnuFile.Text = "[File]";
			// 
			// mnuFileNew
			// 
			this.mnuFileNew.Index = 0;
			this.mnuFileNew.Text = "[New]";
			this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
			// 
			// mnuFileOpen
			// 
			this.mnuFileOpen.Index = 1;
			this.mnuFileOpen.Text = "[Open]";
			this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
			// 
			// mnuFileSep1
			// 
			this.mnuFileSep1.Index = 2;
			this.mnuFileSep1.Text = "-";
			// 
			// mnuFileSave
			// 
			this.mnuFileSave.Index = 3;
			this.mnuFileSave.Text = "[Save]";
			this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
			// 
			// mnuFileSaveAs
			// 
			this.mnuFileSaveAs.Index = 4;
			this.mnuFileSaveAs.Text = "[Save As]";
			this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
			// 
			// mnuFileSep2
			// 
			this.mnuFileSep2.Index = 5;
			this.mnuFileSep2.Text = "-";
			// 
			// mnuFileRecent1
			// 
			this.mnuFileRecent1.Index = 6;
			this.mnuFileRecent1.Text = "[Recent1]";
			this.mnuFileRecent1.Visible = false;
			this.mnuFileRecent1.Click += new System.EventHandler(this.mnuFileRecent1_Click);
			// 
			// mnuFileRecent2
			// 
			this.mnuFileRecent2.Index = 7;
			this.mnuFileRecent2.Text = "[Recent2]";
			this.mnuFileRecent2.Visible = false;
			this.mnuFileRecent2.Click += new System.EventHandler(this.mnuFileRecent2_Click);
			// 
			// mnuFileRecent3
			// 
			this.mnuFileRecent3.Index = 8;
			this.mnuFileRecent3.Text = "[Recent3]";
			this.mnuFileRecent3.Visible = false;
			this.mnuFileRecent3.Click += new System.EventHandler(this.mnuFileRecent3_Click);
			// 
			// mnuFileRecent4
			// 
			this.mnuFileRecent4.Index = 9;
			this.mnuFileRecent4.Text = "[Recent4]";
			this.mnuFileRecent4.Visible = false;
			this.mnuFileRecent4.Click += new System.EventHandler(this.mnuFileRecent4_Click);
			// 
			// mnuFileSep3
			// 
			this.mnuFileSep3.Index = 10;
			this.mnuFileSep3.Text = "-";
			this.mnuFileSep3.Visible = false;
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Index = 11;
			this.mnuFileExit.Text = "[Exit]";
			this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
			// 
			// mnuEdit
			// 
			this.mnuEdit.Index = 1;
			this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuEditAddPackage,
																					this.mnuEditRemovePackage,
																					this.mnuEditSep1,
																					this.mnuEditAddActor,
																					this.mnuEditRemoveActor,
																					this.mnuEditSep2,
																					this.mnuEditAddUseCase,
																					this.mnuEditRemoveUseCase});
			this.mnuEdit.Text = "[Edit]";
			// 
			// mnuEditAddPackage
			// 
			this.mnuEditAddPackage.Index = 0;
			this.mnuEditAddPackage.Text = "[Add package]";
			this.mnuEditAddPackage.Click += new System.EventHandler(this.mnuEditAddPackage_Click);
			// 
			// mnuEditRemovePackage
			// 
			this.mnuEditRemovePackage.Index = 1;
			this.mnuEditRemovePackage.Text = "[Remove package]";
			this.mnuEditRemovePackage.Click += new System.EventHandler(this.mnuEditRemovePackage_Click);
			// 
			// mnuEditSep1
			// 
			this.mnuEditSep1.Index = 2;
			this.mnuEditSep1.Text = "-";
			// 
			// mnuEditAddActor
			// 
			this.mnuEditAddActor.Index = 3;
			this.mnuEditAddActor.Text = "[Add actor]";
			this.mnuEditAddActor.Click += new System.EventHandler(this.mnuEditAddActor_Click);
			// 
			// mnuEditRemoveActor
			// 
			this.mnuEditRemoveActor.Index = 4;
			this.mnuEditRemoveActor.Text = "[Remove actor]";
			this.mnuEditRemoveActor.Click += new System.EventHandler(this.mnuEditRemoveActor_Click);
			// 
			// mnuEditSep2
			// 
			this.mnuEditSep2.Index = 5;
			this.mnuEditSep2.Text = "-";
			// 
			// mnuEditAddUseCase
			// 
			this.mnuEditAddUseCase.Index = 6;
			this.mnuEditAddUseCase.Text = "[Add use case]";
			this.mnuEditAddUseCase.Click += new System.EventHandler(this.mnuEditAddUseCase_Click);
			// 
			// mnuEditRemoveUseCase
			// 
			this.mnuEditRemoveUseCase.Index = 7;
			this.mnuEditRemoveUseCase.Text = "[Remove use case]";
			this.mnuEditRemoveUseCase.Click += new System.EventHandler(this.mnuEditRemoveUseCase_Click);
			// 
			// mnuTools
			// 
			this.mnuTools.Index = 2;
			this.mnuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mnuToolsHtmlExport,
																					 this.mnuToolsPDFExport,
																					 this.mnuToolsSep1,
																					 this.mnuToolsXMIExport,
																					 this.mnuToolsSep2,
																					 this.mnuToolsOptions});
			this.mnuTools.Text = "[Tools]";
			// 
			// mnuToolsHtmlExport
			// 
			this.mnuToolsHtmlExport.Index = 0;
			this.mnuToolsHtmlExport.Text = "[HTLM Export]";
			this.mnuToolsHtmlExport.Click += new System.EventHandler(this.mnuToolsHtmlExport_Click);
			// 
			// mnuToolsPDFExport
			// 
			this.mnuToolsPDFExport.Index = 1;
			this.mnuToolsPDFExport.Text = "[PDF Export]";
			this.mnuToolsPDFExport.Click += new System.EventHandler(this.mnuToolsPDFExport_Click);
			// 
			// mnuToolsSep1
			// 
			this.mnuToolsSep1.Index = 2;
			this.mnuToolsSep1.Text = "-";
			// 
			// mnuToolsXMIExport
			// 
			this.mnuToolsXMIExport.Index = 3;
			this.mnuToolsXMIExport.Text = "[XMI 1.1 Export]";
			this.mnuToolsXMIExport.Click += new System.EventHandler(this.mnuXMIExport_Click);
			// 
			// mnuToolsSep2
			// 
			this.mnuToolsSep2.Index = 4;
			this.mnuToolsSep2.Text = "-";
			// 
			// mnuToolsOptions
			// 
			this.mnuToolsOptions.Index = 5;
			this.mnuToolsOptions.Text = "[Options]";
			this.mnuToolsOptions.Click += new System.EventHandler(this.mnuToolsOptions_Click);
			// 
			// mnuHelp
			// 
			this.mnuHelp.Index = 3;
			this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuHelpAbout});
			this.mnuHelp.Text = "[Help]";
			// 
			// mnuHelpAbout
			// 
			this.mnuHelpAbout.Index = 0;
			this.mnuHelpAbout.Text = "[About]";
			this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
			// 
			// tvModelBrowser
			// 
			this.tvModelBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tvModelBrowser.ContextMenu = this.modelBrowserCtxMenu;
			this.tvModelBrowser.HideSelection = false;
			this.tvModelBrowser.ImageList = this.imgListModelBrowser;
			this.tvModelBrowser.Location = new System.Drawing.Point(3, 28);
			this.tvModelBrowser.Name = "tvModelBrowser";
			this.tvModelBrowser.Size = new System.Drawing.Size(125, 344);
			this.tvModelBrowser.TabIndex = 0;
			this.tvModelBrowser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvModelBrowser_MouseDown);
			this.tvModelBrowser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelect);
			// 
			// modelBrowserCtxMenu
			// 
			this.modelBrowserCtxMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								this.mnuCtxMBAddPackage,
																								this.mnuCtxMBRemovePackage,
																								this.mnuCtxMBSep1,
																								this.mnuCtxMBAddActor,
																								this.mnuCtxMBRemoveActor,
																								this.mnuCtxMBSep2,
																								this.mnuCtxMBAddUseCase,
																								this.mnuCtxMBRemoveUseCase});
			// 
			// mnuCtxMBAddPackage
			// 
			this.mnuCtxMBAddPackage.Index = 0;
			this.mnuCtxMBAddPackage.Text = "[Add package]";
			this.mnuCtxMBAddPackage.Click += new System.EventHandler(this.mnuCtxMBAddPackage_Click);
			// 
			// mnuCtxMBRemovePackage
			// 
			this.mnuCtxMBRemovePackage.Index = 1;
			this.mnuCtxMBRemovePackage.Text = "[Remove package]";
			this.mnuCtxMBRemovePackage.Click += new System.EventHandler(this.mnuCtxMBRemovePackage_Click);
			// 
			// mnuCtxMBSep1
			// 
			this.mnuCtxMBSep1.Index = 2;
			this.mnuCtxMBSep1.Text = "-";
			// 
			// mnuCtxMBAddActor
			// 
			this.mnuCtxMBAddActor.Index = 3;
			this.mnuCtxMBAddActor.Text = "[Add actor]";
			this.mnuCtxMBAddActor.Click += new System.EventHandler(this.mnuCtxMBAddActor_Click);
			// 
			// mnuCtxMBRemoveActor
			// 
			this.mnuCtxMBRemoveActor.Index = 4;
			this.mnuCtxMBRemoveActor.Text = "[Remove actor]";
			this.mnuCtxMBRemoveActor.Click += new System.EventHandler(this.mnuCtxMBRemoveActor_Click);
			// 
			// mnuCtxMBSep2
			// 
			this.mnuCtxMBSep2.Index = 5;
			this.mnuCtxMBSep2.Text = "-";
			// 
			// mnuCtxMBAddUseCase
			// 
			this.mnuCtxMBAddUseCase.Index = 6;
			this.mnuCtxMBAddUseCase.Text = "[Add use case]";
			this.mnuCtxMBAddUseCase.Click += new System.EventHandler(this.mnuCtxMBAddUseCase_Click);
			// 
			// mnuCtxMBRemoveUseCase
			// 
			this.mnuCtxMBRemoveUseCase.Index = 7;
			this.mnuCtxMBRemoveUseCase.Text = "[Remove use case]";
			this.mnuCtxMBRemoveUseCase.Click += new System.EventHandler(this.mnuCtxMBRemoveUseCase_Click);
			// 
			// imgListModelBrowser
			// 
			this.imgListModelBrowser.ImageSize = new System.Drawing.Size(16, 16);
			this.imgListModelBrowser.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListModelBrowser.ImageStream")));
			this.imgListModelBrowser.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// pnlModelBrowser
			// 
			this.pnlModelBrowser.Controls.Add(this.lblModelBrowser);
			this.pnlModelBrowser.Controls.Add(this.tvModelBrowser);
			this.pnlModelBrowser.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlModelBrowser.DockPadding.Bottom = 3;
			this.pnlModelBrowser.DockPadding.Left = 3;
			this.pnlModelBrowser.DockPadding.Right = 3;
			this.pnlModelBrowser.DockPadding.Top = 3;
			this.pnlModelBrowser.ForeColor = System.Drawing.SystemColors.ControlText;
			this.pnlModelBrowser.Location = new System.Drawing.Point(0, 28);
			this.pnlModelBrowser.Name = "pnlModelBrowser";
			this.pnlModelBrowser.Size = new System.Drawing.Size(130, 374);
			this.pnlModelBrowser.TabIndex = 2;
			// 
			// lblModelBrowser
			// 
			this.lblModelBrowser.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblModelBrowser.Location = new System.Drawing.Point(3, 3);
			this.lblModelBrowser.Name = "lblModelBrowser";
			this.lblModelBrowser.Size = new System.Drawing.Size(124, 23);
			this.lblModelBrowser.TabIndex = 3;
			this.lblModelBrowser.Text = "[Model Browser]";
			this.lblModelBrowser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// splLeft
			// 
			this.splLeft.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.splLeft.Location = new System.Drawing.Point(130, 28);
			this.splLeft.MinExtra = 490;
			this.splLeft.MinSize = 130;
			this.splLeft.Name = "splLeft";
			this.splLeft.Size = new System.Drawing.Size(3, 374);
			this.splLeft.TabIndex = 3;
			this.splLeft.TabStop = false;
			this.splLeft.SplitterMoving += new System.Windows.Forms.SplitterEventHandler(this.splLeft_SplitterMoving);
			// 
			// toolBar
			// 
			this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.tbBtnNew,
																					   this.tbBtnOpen,
																					   this.tbBtnSave,
																					   this.tbBtnSep1,
																					   this.tbBtnAddPackage,
																					   this.tbBtnRemovePackage,
																					   this.tbBtnSep2,
																					   this.tbBtnAddActor,
																					   this.tbBtnRemoveActor,
																					   this.tbBtnSep3,
																					   this.tbBtnAddUseCase,
																					   this.tbBtnRemoveUseCase});
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.imgListToolBar;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(628, 28);
			this.toolBar.TabIndex = 1;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// tbBtnNew
			// 
			this.tbBtnNew.ImageIndex = 0;
			// 
			// tbBtnOpen
			// 
			this.tbBtnOpen.ImageIndex = 1;
			// 
			// tbBtnSave
			// 
			this.tbBtnSave.ImageIndex = 2;
			// 
			// tbBtnSep1
			// 
			this.tbBtnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbBtnAddPackage
			// 
			this.tbBtnAddPackage.ImageIndex = 7;
			// 
			// tbBtnRemovePackage
			// 
			this.tbBtnRemovePackage.ImageIndex = 8;
			// 
			// tbBtnSep2
			// 
			this.tbBtnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbBtnAddActor
			// 
			this.tbBtnAddActor.ImageIndex = 3;
			// 
			// tbBtnRemoveActor
			// 
			this.tbBtnRemoveActor.ImageIndex = 4;
			// 
			// tbBtnSep3
			// 
			this.tbBtnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbBtnAddUseCase
			// 
			this.tbBtnAddUseCase.ImageIndex = 5;
			// 
			// tbBtnRemoveUseCase
			// 
			this.tbBtnRemoveUseCase.ImageIndex = 6;
			// 
			// imgListToolBar
			// 
			this.imgListToolBar.ImageSize = new System.Drawing.Size(16, 16);
			this.imgListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListToolBar.ImageStream")));
			this.imgListToolBar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabUseCase
			// 
			this.tabUseCase.Controls.Add(this.pgMain);
			this.tabUseCase.Controls.Add(this.pgGlossary);
			this.tabUseCase.Controls.Add(this.pgDetails);
			this.tabUseCase.Controls.Add(this.pgProse);
			this.tabUseCase.Controls.Add(this.pgRequirements);
			this.tabUseCase.Controls.Add(this.pgAGeneral);
			this.tabUseCase.Controls.Add(this.pgHistory);
			this.tabUseCase.Controls.Add(this.pgFlowOfEvents);
			this.tabUseCase.Controls.Add(this.pgAttributes);
			this.tabUseCase.Controls.Add(this.pgUCGeneral);
			this.tabUseCase.Controls.Add(this.pgPGeneral);
			this.tabUseCase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabUseCase.Location = new System.Drawing.Point(130, 28);
			this.tabUseCase.Name = "tabUseCase";
			this.tabUseCase.SelectedIndex = 0;
			this.tabUseCase.Size = new System.Drawing.Size(498, 374);
			this.tabUseCase.TabIndex = 4;
			// 
			// pgMain
			// 
			this.pgMain.Controls.Add(this.pnlAbout);
			this.pgMain.Controls.Add(this.pnlUseCasesContainer);
			this.pgMain.Controls.Add(this.pnlActorsContainer);
			this.pgMain.Controls.Add(this.pnlPackagesContainer);
			this.pgMain.Controls.Add(this.pnlFullPathContainer);
			this.pgMain.Location = new System.Drawing.Point(4, 22);
			this.pgMain.Name = "pgMain";
			this.pgMain.Size = new System.Drawing.Size(490, 348);
			this.pgMain.TabIndex = 8;
			this.pgMain.Text = "[Main]";
			// 
			// pnlAbout
			// 
			this.pnlAbout.BackColor = System.Drawing.Color.White;
			this.pnlAbout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlAbout.Controls.Add(this.lblVersion);
			this.pnlAbout.Controls.Add(this.label1);
			this.pnlAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlAbout.Location = new System.Drawing.Point(0, 284);
			this.pnlAbout.Name = "pnlAbout";
			this.pnlAbout.Size = new System.Drawing.Size(490, 64);
			this.pnlAbout.TabIndex = 13;
			// 
			// lblVersion
			// 
			this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblVersion.ForeColor = System.Drawing.Color.LightSteelBlue;
			this.lblVersion.Location = new System.Drawing.Point(320, 32);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(160, 23);
			this.lblVersion.TabIndex = 15;
			this.lblVersion.Text = "[1.0.0.0]";
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.LightSteelBlue;
			this.label1.Location = new System.Drawing.Point(320, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 23);
			this.label1.TabIndex = 14;
			this.label1.Text = "Use Case Maker";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnlUseCasesContainer
			// 
			this.pnlUseCasesContainer.Controls.Add(this.lblUseCases);
			this.pnlUseCasesContainer.Controls.Add(this.lblUseCasesTitle);
			this.pnlUseCasesContainer.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlUseCasesContainer.Location = new System.Drawing.Point(0, 72);
			this.pnlUseCasesContainer.Name = "pnlUseCasesContainer";
			this.pnlUseCasesContainer.Size = new System.Drawing.Size(490, 24);
			this.pnlUseCasesContainer.TabIndex = 11;
			// 
			// lblUseCases
			// 
			this.lblUseCases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblUseCases.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblUseCases.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUseCases.Location = new System.Drawing.Point(120, 8);
			this.lblUseCases.Name = "lblUseCases";
			this.lblUseCases.Size = new System.Drawing.Size(362, 16);
			this.lblUseCases.TabIndex = 9;
			// 
			// lblUseCasesTitle
			// 
			this.lblUseCasesTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUseCasesTitle.Location = new System.Drawing.Point(8, 8);
			this.lblUseCasesTitle.Name = "lblUseCasesTitle";
			this.lblUseCasesTitle.Size = new System.Drawing.Size(100, 16);
			this.lblUseCasesTitle.TabIndex = 8;
			this.lblUseCasesTitle.Text = "[Use cases]";
			// 
			// pnlActorsContainer
			// 
			this.pnlActorsContainer.Controls.Add(this.lblActors);
			this.pnlActorsContainer.Controls.Add(this.lblActorsTitle);
			this.pnlActorsContainer.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlActorsContainer.Location = new System.Drawing.Point(0, 48);
			this.pnlActorsContainer.Name = "pnlActorsContainer";
			this.pnlActorsContainer.Size = new System.Drawing.Size(490, 24);
			this.pnlActorsContainer.TabIndex = 10;
			// 
			// lblActors
			// 
			this.lblActors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblActors.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblActors.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblActors.Location = new System.Drawing.Point(120, 8);
			this.lblActors.Name = "lblActors";
			this.lblActors.Size = new System.Drawing.Size(362, 16);
			this.lblActors.TabIndex = 7;
			// 
			// lblActorsTitle
			// 
			this.lblActorsTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblActorsTitle.Location = new System.Drawing.Point(8, 8);
			this.lblActorsTitle.Name = "lblActorsTitle";
			this.lblActorsTitle.Size = new System.Drawing.Size(100, 16);
			this.lblActorsTitle.TabIndex = 6;
			this.lblActorsTitle.Text = "[Actors]";
			// 
			// pnlPackagesContainer
			// 
			this.pnlPackagesContainer.Controls.Add(this.lblPackages);
			this.pnlPackagesContainer.Controls.Add(this.lblPackagesTitle);
			this.pnlPackagesContainer.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlPackagesContainer.Location = new System.Drawing.Point(0, 24);
			this.pnlPackagesContainer.Name = "pnlPackagesContainer";
			this.pnlPackagesContainer.Size = new System.Drawing.Size(490, 24);
			this.pnlPackagesContainer.TabIndex = 9;
			// 
			// lblPackages
			// 
			this.lblPackages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPackages.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblPackages.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPackages.Location = new System.Drawing.Point(120, 8);
			this.lblPackages.Name = "lblPackages";
			this.lblPackages.Size = new System.Drawing.Size(362, 16);
			this.lblPackages.TabIndex = 5;
			// 
			// lblPackagesTitle
			// 
			this.lblPackagesTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPackagesTitle.Location = new System.Drawing.Point(8, 8);
			this.lblPackagesTitle.Name = "lblPackagesTitle";
			this.lblPackagesTitle.Size = new System.Drawing.Size(100, 16);
			this.lblPackagesTitle.TabIndex = 4;
			this.lblPackagesTitle.Text = "[Packages]";
			// 
			// pnlFullPathContainer
			// 
			this.pnlFullPathContainer.Controls.Add(this.lblFullPath);
			this.pnlFullPathContainer.Controls.Add(this.lblFullPathTitle);
			this.pnlFullPathContainer.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlFullPathContainer.Location = new System.Drawing.Point(0, 0);
			this.pnlFullPathContainer.Name = "pnlFullPathContainer";
			this.pnlFullPathContainer.Size = new System.Drawing.Size(490, 24);
			this.pnlFullPathContainer.TabIndex = 8;
			// 
			// lblFullPath
			// 
			this.lblFullPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblFullPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblFullPath.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFullPath.Location = new System.Drawing.Point(120, 8);
			this.lblFullPath.Name = "lblFullPath";
			this.lblFullPath.Size = new System.Drawing.Size(362, 16);
			this.lblFullPath.TabIndex = 3;
			// 
			// lblFullPathTitle
			// 
			this.lblFullPathTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFullPathTitle.Location = new System.Drawing.Point(8, 8);
			this.lblFullPathTitle.Name = "lblFullPathTitle";
			this.lblFullPathTitle.Size = new System.Drawing.Size(100, 16);
			this.lblFullPathTitle.TabIndex = 2;
			this.lblFullPathTitle.Text = "[Full path]";
			// 
			// pgGlossary
			// 
			this.pgGlossary.Controls.Add(this.GList);
			this.pgGlossary.Controls.Add(this.btnChangeGlossaryItem);
			this.pgGlossary.Controls.Add(this.btnRemoveGlossaryItem);
			this.pgGlossary.Controls.Add(this.btnAddGlossaryItem);
			this.pgGlossary.Location = new System.Drawing.Point(4, 22);
			this.pgGlossary.Name = "pgGlossary";
			this.pgGlossary.Size = new System.Drawing.Size(490, 348);
			this.pgGlossary.TabIndex = 10;
			this.pgGlossary.Text = "[Glossary]";
			// 
			// GList
			// 
			this.GList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.GList.AutoScroll = true;
			this.GList.AutoScrollMinSize = new System.Drawing.Size(5, 5);
			this.GList.BackColor = System.Drawing.SystemColors.Window;
			this.GList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.GList.DataSource = null;
			this.GList.IndexBackColor = System.Drawing.SystemColors.Control;
			this.GList.IndexColumnWidth = 146;
			this.GList.IndexDataField = null;
			this.GList.Location = new System.Drawing.Point(8, 8);
			this.GList.Name = "GList";
			this.GList.RowHeight = 46;
			this.GList.SelectedIndex = -1;
			this.GList.Size = new System.Drawing.Size(340, 328);
			this.GList.TabIndex = 4;
			this.GList.TextBackColor = System.Drawing.SystemColors.Window;
			this.GList.TextColumnWidth = 189;
			this.GList.TextDataField = null;
			this.GList.UniqueIDDataField = null;
			this.GList.ItemTextChanged += new UseCaseMakerControls.ItemTextChangedEventHandler(this.GList_ItemTextChanged);
			this.GList.SelectedChanged += new UseCaseMakerControls.SelectedChangeEventHandler(this.GList_SelectedChanged);
			this.GList.MouseOverToken += new UseCaseMakerControls.MouseOverTokenEventHandler(this.GList_MouseOverToken);
			// 
			// btnChangeGlossaryItem
			// 
			this.btnChangeGlossaryItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnChangeGlossaryItem.Enabled = false;
			this.btnChangeGlossaryItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnChangeGlossaryItem.Location = new System.Drawing.Point(364, 40);
			this.btnChangeGlossaryItem.Name = "btnChangeGlossaryItem";
			this.btnChangeGlossaryItem.Size = new System.Drawing.Size(120, 23);
			this.btnChangeGlossaryItem.TabIndex = 2;
			this.btnChangeGlossaryItem.Text = "[Change]";
			this.btnChangeGlossaryItem.Click += new System.EventHandler(this.btnChangeGlossaryItem_Click);
			// 
			// btnRemoveGlossaryItem
			// 
			this.btnRemoveGlossaryItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveGlossaryItem.Enabled = false;
			this.btnRemoveGlossaryItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemoveGlossaryItem.Location = new System.Drawing.Point(364, 72);
			this.btnRemoveGlossaryItem.Name = "btnRemoveGlossaryItem";
			this.btnRemoveGlossaryItem.Size = new System.Drawing.Size(120, 23);
			this.btnRemoveGlossaryItem.TabIndex = 3;
			this.btnRemoveGlossaryItem.Text = "[Remove]";
			this.btnRemoveGlossaryItem.Click += new System.EventHandler(this.btnRemoveGlossaryItem_Click);
			// 
			// btnAddGlossaryItem
			// 
			this.btnAddGlossaryItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddGlossaryItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddGlossaryItem.Location = new System.Drawing.Point(364, 8);
			this.btnAddGlossaryItem.Name = "btnAddGlossaryItem";
			this.btnAddGlossaryItem.Size = new System.Drawing.Size(120, 23);
			this.btnAddGlossaryItem.TabIndex = 1;
			this.btnAddGlossaryItem.Text = "[Add]";
			this.btnAddGlossaryItem.Click += new System.EventHandler(this.btnAddGlossaryItem_Click);
			// 
			// pgHistory
			// 
			this.pgHistory.Controls.Add(this.btnRemoveHistoryItem);
			this.pgHistory.Controls.Add(this.lvHistory);
			this.pgHistory.Location = new System.Drawing.Point(4, 22);
			this.pgHistory.Name = "pgHistory";
			this.pgHistory.Size = new System.Drawing.Size(490, 348);
			this.pgHistory.TabIndex = 5;
			this.pgHistory.Text = "[History]";
			// 
			// btnRemoveHistoryItem
			// 
			this.btnRemoveHistoryItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveHistoryItem.Enabled = false;
			this.btnRemoveHistoryItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemoveHistoryItem.Location = new System.Drawing.Point(362, 8);
			this.btnRemoveHistoryItem.Name = "btnRemoveHistoryItem";
			this.btnRemoveHistoryItem.Size = new System.Drawing.Size(120, 23);
			this.btnRemoveHistoryItem.TabIndex = 1;
			this.btnRemoveHistoryItem.Text = "[Remove]";
			this.btnRemoveHistoryItem.Click += new System.EventHandler(this.btnRemoveHistoryItem_Click);
			// 
			// lvHistory
			// 
			this.lvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.chDate,
																						this.chType,
																						this.chAction,
																						this.chNotes});
			this.lvHistory.FullRowSelect = true;
			this.lvHistory.GridLines = true;
			this.lvHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvHistory.HideSelection = false;
			this.lvHistory.LabelWrap = false;
			this.lvHistory.Location = new System.Drawing.Point(8, 8);
			this.lvHistory.MultiSelect = false;
			this.lvHistory.Name = "lvHistory";
			this.lvHistory.Size = new System.Drawing.Size(346, 328);
			this.lvHistory.TabIndex = 0;
			this.lvHistory.View = System.Windows.Forms.View.Details;
			this.lvHistory.Layout += new System.Windows.Forms.LayoutEventHandler(this.lvHistory_Layout);
			this.lvHistory.SelectedIndexChanged += new System.EventHandler(this.lvHistory_SelectedIndexChanged);
			// 
			// chDate
			// 
			this.chDate.Text = "[Date]";
			this.chDate.Width = 100;
			// 
			// chType
			// 
			this.chType.Text = "[type]";
			this.chType.Width = 100;
			// 
			// chAction
			// 
			this.chAction.Text = "[Action]";
			this.chAction.Width = 86;
			// 
			// chNotes
			// 
			this.chNotes.Text = "[Notes]";
			this.chNotes.Width = 150;
			// 
			// pgDetails
			// 
			this.pgDetails.Controls.Add(this.OIList);
			this.pgDetails.Controls.Add(this.btnRemoveOpenIssue);
			this.pgDetails.Controls.Add(this.btnAddOpenIssue);
			this.pgDetails.Controls.Add(this.lblOpenIssuesTitle);
			this.pgDetails.Controls.Add(this.tbRelease);
			this.pgDetails.Controls.Add(this.lblReleaseTitle);
			this.pgDetails.Controls.Add(this.tbAssignedTo);
			this.pgDetails.Controls.Add(this.lblAssignedToTitle);
			this.pgDetails.Controls.Add(this.btnImplToHistory);
			this.pgDetails.Controls.Add(this.cmbImplementation);
			this.pgDetails.Controls.Add(this.lblImplTitle);
			this.pgDetails.Controls.Add(this.btnStatusToHistory);
			this.pgDetails.Controls.Add(this.cmbStatus);
			this.pgDetails.Controls.Add(this.lblStatusTitle);
			this.pgDetails.Controls.Add(this.cmbComplexity);
			this.pgDetails.Controls.Add(this.lblComplexityTitle);
			this.pgDetails.Controls.Add(this.cmbLevel);
			this.pgDetails.Controls.Add(this.lblLevelTitle);
			this.pgDetails.Controls.Add(this.tbPriority);
			this.pgDetails.Controls.Add(this.lblPriorityTitle);
			this.pgDetails.Location = new System.Drawing.Point(4, 22);
			this.pgDetails.Name = "pgDetails";
			this.pgDetails.Size = new System.Drawing.Size(490, 348);
			this.pgDetails.TabIndex = 3;
			this.pgDetails.Text = "[Details]";
			// 
			// OIList
			// 
			this.OIList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.OIList.AutoScroll = true;
			this.OIList.AutoScrollMinSize = new System.Drawing.Size(5, 5);
			this.OIList.BackColor = System.Drawing.SystemColors.Window;
			this.OIList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.OIList.DataSource = null;
			this.OIList.IndexBackColor = System.Drawing.SystemColors.Control;
			this.OIList.IndexColumnWidth = 50;
			this.OIList.IndexDataField = null;
			this.OIList.Location = new System.Drawing.Point(104, 232);
			this.OIList.Name = "OIList";
			this.OIList.RowHeight = 46;
			this.OIList.SelectedIndex = -1;
			this.OIList.Size = new System.Drawing.Size(252, 104);
			this.OIList.TabIndex = 34;
			this.OIList.TextBackColor = System.Drawing.SystemColors.Window;
			this.OIList.TextColumnWidth = 197;
			this.OIList.TextDataField = null;
			this.OIList.UniqueIDDataField = null;
			this.OIList.ItemTextChanged += new UseCaseMakerControls.ItemTextChangedEventHandler(this.OIList_ItemTextChanged);
			this.OIList.SelectedChanged += new UseCaseMakerControls.SelectedChangeEventHandler(this.OIList_SelectedChanged);
			this.OIList.ClickOnToken += new UseCaseMakerControls.ClickOnTokenEventHandler(this.OIList_ClickOnToken);
			this.OIList.MouseOverToken += new UseCaseMakerControls.MouseOverTokenEventHandler(this.OIList_MouseOverToken);
			// 
			// btnRemoveOpenIssue
			// 
			this.btnRemoveOpenIssue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveOpenIssue.Enabled = false;
			this.btnRemoveOpenIssue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemoveOpenIssue.Location = new System.Drawing.Point(362, 264);
			this.btnRemoveOpenIssue.Name = "btnRemoveOpenIssue";
			this.btnRemoveOpenIssue.Size = new System.Drawing.Size(120, 23);
			this.btnRemoveOpenIssue.TabIndex = 12;
			this.btnRemoveOpenIssue.Text = "[Remove]";
			this.btnRemoveOpenIssue.Click += new System.EventHandler(this.btnRemoveOpenIssue_Click);
			// 
			// btnAddOpenIssue
			// 
			this.btnAddOpenIssue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddOpenIssue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddOpenIssue.Location = new System.Drawing.Point(362, 232);
			this.btnAddOpenIssue.Name = "btnAddOpenIssue";
			this.btnAddOpenIssue.Size = new System.Drawing.Size(120, 23);
			this.btnAddOpenIssue.TabIndex = 10;
			this.btnAddOpenIssue.Text = "[Add]";
			this.btnAddOpenIssue.Click += new System.EventHandler(this.btnAddOpenIssue_Click);
			// 
			// lblOpenIssuesTitle
			// 
			this.lblOpenIssuesTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblOpenIssuesTitle.Location = new System.Drawing.Point(8, 232);
			this.lblOpenIssuesTitle.Name = "lblOpenIssuesTitle";
			this.lblOpenIssuesTitle.Size = new System.Drawing.Size(96, 40);
			this.lblOpenIssuesTitle.TabIndex = 33;
			this.lblOpenIssuesTitle.Text = "[Open issues]";
			// 
			// tbRelease
			// 
			this.tbRelease.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbRelease.Location = new System.Drawing.Point(104, 200);
			this.tbRelease.Name = "tbRelease";
			this.tbRelease.Size = new System.Drawing.Size(378, 20);
			this.tbRelease.TabIndex = 8;
			this.tbRelease.Text = "";
			this.tbRelease.TextChanged += new System.EventHandler(this.tbRelease_TextChanged);
			// 
			// lblReleaseTitle
			// 
			this.lblReleaseTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblReleaseTitle.Location = new System.Drawing.Point(8, 200);
			this.lblReleaseTitle.Name = "lblReleaseTitle";
			this.lblReleaseTitle.Size = new System.Drawing.Size(96, 16);
			this.lblReleaseTitle.TabIndex = 31;
			this.lblReleaseTitle.Text = "[Release]";
			// 
			// tbAssignedTo
			// 
			this.tbAssignedTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbAssignedTo.Location = new System.Drawing.Point(104, 168);
			this.tbAssignedTo.Name = "tbAssignedTo";
			this.tbAssignedTo.Size = new System.Drawing.Size(378, 20);
			this.tbAssignedTo.TabIndex = 7;
			this.tbAssignedTo.Text = "";
			this.tbAssignedTo.TextChanged += new System.EventHandler(this.tbAssignedTo_TextChanged);
			// 
			// lblAssignedToTitle
			// 
			this.lblAssignedToTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblAssignedToTitle.Location = new System.Drawing.Point(8, 168);
			this.lblAssignedToTitle.Name = "lblAssignedToTitle";
			this.lblAssignedToTitle.Size = new System.Drawing.Size(96, 16);
			this.lblAssignedToTitle.TabIndex = 29;
			this.lblAssignedToTitle.Text = "[Assigned to]";
			// 
			// btnImplToHistory
			// 
			this.btnImplToHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnImplToHistory.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnImplToHistory.Location = new System.Drawing.Point(362, 136);
			this.btnImplToHistory.Name = "btnImplToHistory";
			this.btnImplToHistory.Size = new System.Drawing.Size(120, 23);
			this.btnImplToHistory.TabIndex = 6;
			this.btnImplToHistory.Text = "[Add to history]";
			this.btnImplToHistory.Click += new System.EventHandler(this.btnImplToHistory_Click);
			// 
			// cmbImplementation
			// 
			this.cmbImplementation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbImplementation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbImplementation.Items.AddRange(new object[] {
																   "[Scheduled]",
																   "[Started]",
																   "[Partial]",
																   "[Complete]",
																   "[Deferred]"});
			this.cmbImplementation.Location = new System.Drawing.Point(104, 136);
			this.cmbImplementation.Name = "cmbImplementation";
			this.cmbImplementation.Size = new System.Drawing.Size(250, 21);
			this.cmbImplementation.TabIndex = 5;
			this.cmbImplementation.SelectedIndexChanged += new System.EventHandler(this.cmbImplementation_SelectedIndexChanged);
			// 
			// lblImplTitle
			// 
			this.lblImplTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblImplTitle.Location = new System.Drawing.Point(8, 136);
			this.lblImplTitle.Name = "lblImplTitle";
			this.lblImplTitle.Size = new System.Drawing.Size(96, 16);
			this.lblImplTitle.TabIndex = 26;
			this.lblImplTitle.Text = "[Implementation]";
			// 
			// btnStatusToHistory
			// 
			this.btnStatusToHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStatusToHistory.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStatusToHistory.Location = new System.Drawing.Point(362, 104);
			this.btnStatusToHistory.Name = "btnStatusToHistory";
			this.btnStatusToHistory.Size = new System.Drawing.Size(120, 23);
			this.btnStatusToHistory.TabIndex = 4;
			this.btnStatusToHistory.Text = "[Add to history]";
			this.btnStatusToHistory.Click += new System.EventHandler(this.btnStatusToHistory_Click);
			// 
			// cmbStatus
			// 
			this.cmbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbStatus.Items.AddRange(new object[] {
														   "[Named]",
														   "[Initial]",
														   "[Base]",
														   "[Complete]",
														   "[Deferred]",
														   "[Tested]",
														   "[Approved]"});
			this.cmbStatus.Location = new System.Drawing.Point(104, 104);
			this.cmbStatus.Name = "cmbStatus";
			this.cmbStatus.Size = new System.Drawing.Size(250, 21);
			this.cmbStatus.TabIndex = 3;
			this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
			// 
			// lblStatusTitle
			// 
			this.lblStatusTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblStatusTitle.Location = new System.Drawing.Point(8, 104);
			this.lblStatusTitle.Name = "lblStatusTitle";
			this.lblStatusTitle.Size = new System.Drawing.Size(96, 16);
			this.lblStatusTitle.TabIndex = 12;
			this.lblStatusTitle.Text = "[Status]";
			// 
			// cmbComplexity
			// 
			this.cmbComplexity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbComplexity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbComplexity.Items.AddRange(new object[] {
															   "[Low]",
															   "[Medium]",
															   "[High]"});
			this.cmbComplexity.Location = new System.Drawing.Point(104, 72);
			this.cmbComplexity.Name = "cmbComplexity";
			this.cmbComplexity.Size = new System.Drawing.Size(378, 21);
			this.cmbComplexity.TabIndex = 2;
			this.cmbComplexity.SelectedIndexChanged += new System.EventHandler(this.cmbComplexity_SelectedIndexChanged);
			// 
			// lblComplexityTitle
			// 
			this.lblComplexityTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblComplexityTitle.Location = new System.Drawing.Point(8, 72);
			this.lblComplexityTitle.Name = "lblComplexityTitle";
			this.lblComplexityTitle.Size = new System.Drawing.Size(96, 16);
			this.lblComplexityTitle.TabIndex = 10;
			this.lblComplexityTitle.Text = "[Complexity]";
			// 
			// cmbLevel
			// 
			this.cmbLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbLevel.Items.AddRange(new object[] {
														  "[Summary]",
														  "[User]",
														  "[Subfunction]"});
			this.cmbLevel.Location = new System.Drawing.Point(104, 40);
			this.cmbLevel.Name = "cmbLevel";
			this.cmbLevel.Size = new System.Drawing.Size(378, 21);
			this.cmbLevel.TabIndex = 1;
			this.cmbLevel.SelectedIndexChanged += new System.EventHandler(this.cmbLevel_SelectedIndexChanged);
			// 
			// lblLevelTitle
			// 
			this.lblLevelTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLevelTitle.Location = new System.Drawing.Point(8, 40);
			this.lblLevelTitle.Name = "lblLevelTitle";
			this.lblLevelTitle.Size = new System.Drawing.Size(96, 16);
			this.lblLevelTitle.TabIndex = 8;
			this.lblLevelTitle.Text = "[Level]";
			// 
			// tbPriority
			// 
			this.tbPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbPriority.Location = new System.Drawing.Point(104, 8);
			this.tbPriority.MaxLength = 3;
			this.tbPriority.Name = "tbPriority";
			this.tbPriority.Size = new System.Drawing.Size(378, 20);
			this.tbPriority.TabIndex = 0;
			this.tbPriority.Text = "";
			this.tbPriority.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPriority_KeyPress);
			this.tbPriority.Validating += new System.ComponentModel.CancelEventHandler(this.tbPriority_Validating);
			this.tbPriority.TextChanged += new System.EventHandler(this.tbPriority_TextChanged);
			// 
			// lblPriorityTitle
			// 
			this.lblPriorityTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPriorityTitle.Location = new System.Drawing.Point(8, 8);
			this.lblPriorityTitle.Name = "lblPriorityTitle";
			this.lblPriorityTitle.Size = new System.Drawing.Size(96, 16);
			this.lblPriorityTitle.TabIndex = 6;
			this.lblPriorityTitle.Text = "[Priority]";
			// 
			// pgFlowOfEvents
			// 
			this.pgFlowOfEvents.Controls.Add(this.UCList);
			this.pgFlowOfEvents.Controls.Add(this.btnInsertAltStep);
			this.pgFlowOfEvents.Controls.Add(this.btnInsertStep);
			this.pgFlowOfEvents.Controls.Add(this.btnAddAltStep);
			this.pgFlowOfEvents.Controls.Add(this.btnRemoveStep);
			this.pgFlowOfEvents.Controls.Add(this.btnAddStep);
			this.pgFlowOfEvents.Controls.Add(this.lblStepsTitle);
			this.pgFlowOfEvents.Location = new System.Drawing.Point(4, 22);
			this.pgFlowOfEvents.Name = "pgFlowOfEvents";
			this.pgFlowOfEvents.Size = new System.Drawing.Size(490, 348);
			this.pgFlowOfEvents.TabIndex = 1;
			this.pgFlowOfEvents.Text = "[Flow of events]";
			// 
			// UCList
			// 
			this.UCList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.UCList.AutoScroll = true;
			this.UCList.AutoScrollMinSize = new System.Drawing.Size(5, 5);
			this.UCList.BackColor = System.Drawing.SystemColors.Window;
			this.UCList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.UCList.DataSource = null;
			this.UCList.IndexBackColor = System.Drawing.SystemColors.Control;
			this.UCList.IndexColumnWidth = 50;
			this.UCList.IndexDataField = null;
			this.UCList.Location = new System.Drawing.Point(96, 8);
			this.UCList.Name = "UCList";
			this.UCList.RowHeight = 46;
			this.UCList.SelectedIndex = -1;
			this.UCList.Size = new System.Drawing.Size(260, 328);
			this.UCList.TabIndex = 10;
			this.UCList.TextBackColor = System.Drawing.SystemColors.Window;
			this.UCList.TextColumnWidth = 205;
			this.UCList.TextDataField = null;
			this.UCList.UniqueIDDataField = null;
			this.UCList.ItemTextChanged += new UseCaseMakerControls.ItemTextChangedEventHandler(this.UCList_ItemTextChanged);
			this.UCList.SelectedChanged += new UseCaseMakerControls.SelectedChangeEventHandler(this.UCList_SelectedChanged);
			this.UCList.ClickOnToken += new UseCaseMakerControls.ClickOnTokenEventHandler(this.UCList_ClickOnToken);
			this.UCList.MouseOverToken += new UseCaseMakerControls.MouseOverTokenEventHandler(this.UCList_MouseOverToken);
			// 
			// btnInsertAltStep
			// 
			this.btnInsertAltStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInsertAltStep.Enabled = false;
			this.btnInsertAltStep.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnInsertAltStep.Location = new System.Drawing.Point(362, 112);
			this.btnInsertAltStep.Name = "btnInsertAltStep";
			this.btnInsertAltStep.Size = new System.Drawing.Size(120, 23);
			this.btnInsertAltStep.TabIndex = 4;
			this.btnInsertAltStep.Text = "[Insert alternative]";
			this.btnInsertAltStep.Click += new System.EventHandler(this.btnInsertAltStep_Click);
			// 
			// btnInsertStep
			// 
			this.btnInsertStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInsertStep.Enabled = false;
			this.btnInsertStep.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnInsertStep.Location = new System.Drawing.Point(362, 40);
			this.btnInsertStep.Name = "btnInsertStep";
			this.btnInsertStep.Size = new System.Drawing.Size(120, 23);
			this.btnInsertStep.TabIndex = 2;
			this.btnInsertStep.Text = "[Insert]";
			this.btnInsertStep.Click += new System.EventHandler(this.btnInsertStep_Click);
			// 
			// btnAddAltStep
			// 
			this.btnAddAltStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddAltStep.Enabled = false;
			this.btnAddAltStep.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddAltStep.Location = new System.Drawing.Point(362, 80);
			this.btnAddAltStep.Name = "btnAddAltStep";
			this.btnAddAltStep.Size = new System.Drawing.Size(120, 23);
			this.btnAddAltStep.TabIndex = 3;
			this.btnAddAltStep.Text = "[Add alternative]";
			this.btnAddAltStep.Click += new System.EventHandler(this.btnAddAltStep_Click);
			// 
			// btnRemoveStep
			// 
			this.btnRemoveStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveStep.Enabled = false;
			this.btnRemoveStep.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemoveStep.Location = new System.Drawing.Point(362, 152);
			this.btnRemoveStep.Name = "btnRemoveStep";
			this.btnRemoveStep.Size = new System.Drawing.Size(120, 23);
			this.btnRemoveStep.TabIndex = 5;
			this.btnRemoveStep.Text = "[Remove]";
			this.btnRemoveStep.Click += new System.EventHandler(this.btnRemoveStep_Click);
			// 
			// btnAddStep
			// 
			this.btnAddStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddStep.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddStep.Location = new System.Drawing.Point(362, 8);
			this.btnAddStep.Name = "btnAddStep";
			this.btnAddStep.Size = new System.Drawing.Size(120, 23);
			this.btnAddStep.TabIndex = 1;
			this.btnAddStep.Text = "[Add]";
			this.btnAddStep.Click += new System.EventHandler(this.btnAddStep_Click);
			// 
			// lblStepsTitle
			// 
			this.lblStepsTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblStepsTitle.Location = new System.Drawing.Point(8, 8);
			this.lblStepsTitle.Name = "lblStepsTitle";
			this.lblStepsTitle.Size = new System.Drawing.Size(88, 16);
			this.lblStepsTitle.TabIndex = 9;
			this.lblStepsTitle.Text = "[Steps]";
			// 
			// pgProse
			// 
			this.pgProse.Controls.Add(this.tbProse);
			this.pgProse.Location = new System.Drawing.Point(4, 22);
			this.pgProse.Name = "pgProse";
			this.pgProse.Size = new System.Drawing.Size(490, 348);
			this.pgProse.TabIndex = 2;
			this.pgProse.Text = "[Prose]";
			// 
			// tbProse
			// 
			this.tbProse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbProse.CaseSensitive = false;
			this.tbProse.FilterAutoComplete = false;
			this.tbProse.Location = new System.Drawing.Point(8, 8);
			this.tbProse.MaxUndoRedoSteps = 50;
			this.tbProse.Name = "tbProse";
			this.tbProse.Size = new System.Drawing.Size(476, 328);
			this.tbProse.TabIndex = 0;
			this.tbProse.Text = "";
			this.tbProse.ClickOnToken += new UseCaseMakerControls.ClickOnTokenEventHandler(this.tbProse_ClickOnToken);
			this.tbProse.ItemTextChanged += new UseCaseMakerControls.ItemTextChangedEventHandler(this.tbProse_ItemTextChanged);
			this.tbProse.MouseOverToken += new UseCaseMakerControls.MouseOverTokenEventHandler(this.tbProse_MouseOverToken);
			// 
			// pgAttributes
			// 
			this.pgAttributes.Controls.Add(this.btnOpenRelatedDoc);
			this.pgAttributes.Controls.Add(this.btnRemoveRelatedDoc);
			this.pgAttributes.Controls.Add(this.btnAddRelatedDoc);
			this.pgAttributes.Controls.Add(this.lvRelatedDocs);
			this.pgAttributes.Controls.Add(this.lblRelatedDocsTitle);
			this.pgAttributes.Controls.Add(this.tbNotes);
			this.pgAttributes.Controls.Add(this.lblNotesTitle);
			this.pgAttributes.Controls.Add(this.tbDescription);
			this.pgAttributes.Controls.Add(this.lblDescriptionTitle);
			this.pgAttributes.Location = new System.Drawing.Point(4, 22);
			this.pgAttributes.Name = "pgAttributes";
			this.pgAttributes.Size = new System.Drawing.Size(490, 348);
			this.pgAttributes.TabIndex = 4;
			this.pgAttributes.Text = "[Attributes]";
			// 
			// btnOpenRelatedDoc
			// 
			this.btnOpenRelatedDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenRelatedDoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOpenRelatedDoc.Location = new System.Drawing.Point(362, 296);
			this.btnOpenRelatedDoc.Name = "btnOpenRelatedDoc";
			this.btnOpenRelatedDoc.Size = new System.Drawing.Size(120, 23);
			this.btnOpenRelatedDoc.TabIndex = 5;
			this.btnOpenRelatedDoc.Text = "[Open]";
			this.btnOpenRelatedDoc.Click += new System.EventHandler(this.btnOpenRelatedDoc_Click);
			// 
			// btnRemoveRelatedDoc
			// 
			this.btnRemoveRelatedDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveRelatedDoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemoveRelatedDoc.Location = new System.Drawing.Point(362, 264);
			this.btnRemoveRelatedDoc.Name = "btnRemoveRelatedDoc";
			this.btnRemoveRelatedDoc.Size = new System.Drawing.Size(120, 23);
			this.btnRemoveRelatedDoc.TabIndex = 4;
			this.btnRemoveRelatedDoc.Text = "[Remove]";
			this.btnRemoveRelatedDoc.Click += new System.EventHandler(this.btnRemoveRelatedDoc_Click);
			// 
			// btnAddRelatedDoc
			// 
			this.btnAddRelatedDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddRelatedDoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddRelatedDoc.Location = new System.Drawing.Point(362, 232);
			this.btnAddRelatedDoc.Name = "btnAddRelatedDoc";
			this.btnAddRelatedDoc.Size = new System.Drawing.Size(120, 23);
			this.btnAddRelatedDoc.TabIndex = 3;
			this.btnAddRelatedDoc.Text = "[Add]";
			this.btnAddRelatedDoc.Click += new System.EventHandler(this.btnAddRelatedDoc_Click);
			// 
			// lvRelatedDocs
			// 
			this.lvRelatedDocs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvRelatedDocs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.columnHeader1});
			this.lvRelatedDocs.FullRowSelect = true;
			this.lvRelatedDocs.GridLines = true;
			this.lvRelatedDocs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvRelatedDocs.HideSelection = false;
			this.lvRelatedDocs.Location = new System.Drawing.Point(104, 232);
			this.lvRelatedDocs.Name = "lvRelatedDocs";
			this.lvRelatedDocs.Size = new System.Drawing.Size(250, 104);
			this.lvRelatedDocs.TabIndex = 2;
			this.lvRelatedDocs.View = System.Windows.Forms.View.Details;
			this.lvRelatedDocs.Layout += new System.Windows.Forms.LayoutEventHandler(this.lvRelatedDocs_Layout);
			this.lvRelatedDocs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvRelatedDocs_MouseMove);
			this.lvRelatedDocs.SelectedIndexChanged += new System.EventHandler(this.lvRelatedDocs_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 200;
			// 
			// lblRelatedDocsTitle
			// 
			this.lblRelatedDocsTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblRelatedDocsTitle.Location = new System.Drawing.Point(8, 232);
			this.lblRelatedDocsTitle.Name = "lblRelatedDocsTitle";
			this.lblRelatedDocsTitle.Size = new System.Drawing.Size(88, 40);
			this.lblRelatedDocsTitle.TabIndex = 37;
			this.lblRelatedDocsTitle.Text = "[Related Docs]";
			// 
			// tbNotes
			// 
			this.tbNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbNotes.Location = new System.Drawing.Point(104, 120);
			this.tbNotes.Multiline = true;
			this.tbNotes.Name = "tbNotes";
			this.tbNotes.Size = new System.Drawing.Size(378, 104);
			this.tbNotes.TabIndex = 1;
			this.tbNotes.Text = "";
			this.tbNotes.TextChanged += new System.EventHandler(this.tbNotes_TextChanged);
			// 
			// lblNotesTitle
			// 
			this.lblNotesTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblNotesTitle.Location = new System.Drawing.Point(8, 120);
			this.lblNotesTitle.Name = "lblNotesTitle";
			this.lblNotesTitle.Size = new System.Drawing.Size(88, 16);
			this.lblNotesTitle.TabIndex = 10;
			this.lblNotesTitle.Text = "[Notes]";
			// 
			// tbDescription
			// 
			this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbDescription.Location = new System.Drawing.Point(104, 8);
			this.tbDescription.Multiline = true;
			this.tbDescription.Name = "tbDescription";
			this.tbDescription.Size = new System.Drawing.Size(378, 104);
			this.tbDescription.TabIndex = 0;
			this.tbDescription.Text = "";
			this.tbDescription.TextChanged += new System.EventHandler(this.tbDescription_TextChanged);
			// 
			// lblDescriptionTitle
			// 
			this.lblDescriptionTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblDescriptionTitle.Location = new System.Drawing.Point(8, 8);
			this.lblDescriptionTitle.Name = "lblDescriptionTitle";
			this.lblDescriptionTitle.Size = new System.Drawing.Size(88, 16);
			this.lblDescriptionTitle.TabIndex = 8;
			this.lblDescriptionTitle.Text = "[Description]";
			// 
			// pgRequirements
			// 
			this.pgRequirements.Controls.Add(this.RList);
			this.pgRequirements.Controls.Add(this.btnRemoveRequirement);
			this.pgRequirements.Controls.Add(this.btnAddRequirement);
			this.pgRequirements.Location = new System.Drawing.Point(4, 22);
			this.pgRequirements.Name = "pgRequirements";
			this.pgRequirements.Size = new System.Drawing.Size(490, 348);
			this.pgRequirements.TabIndex = 6;
			this.pgRequirements.Text = "[Requirements]";
			// 
			// RList
			// 
			this.RList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.RList.AutoScroll = true;
			this.RList.AutoScrollMinSize = new System.Drawing.Size(5, 5);
			this.RList.BackColor = System.Drawing.SystemColors.Window;
			this.RList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.RList.DataSource = null;
			this.RList.IndexBackColor = System.Drawing.SystemColors.Control;
			this.RList.IndexColumnWidth = 50;
			this.RList.IndexDataField = null;
			this.RList.Location = new System.Drawing.Point(8, 8);
			this.RList.Name = "RList";
			this.RList.RowHeight = 46;
			this.RList.SelectedIndex = -1;
			this.RList.Size = new System.Drawing.Size(348, 328);
			this.RList.TabIndex = 3;
			this.RList.TextBackColor = System.Drawing.SystemColors.Window;
			this.RList.TextColumnWidth = 293;
			this.RList.TextDataField = null;
			this.RList.UniqueIDDataField = null;
			this.RList.ItemTextChanged += new UseCaseMakerControls.ItemTextChangedEventHandler(this.RList_ItemTextChanged);
			this.RList.SelectedChanged += new UseCaseMakerControls.SelectedChangeEventHandler(this.RList_SelectedChange);
			this.RList.ClickOnToken += new UseCaseMakerControls.ClickOnTokenEventHandler(this.RList_ClickOnToken);
			this.RList.MouseOverToken += new UseCaseMakerControls.MouseOverTokenEventHandler(this.RList_MouseOverToken);
			// 
			// btnRemoveRequirement
			// 
			this.btnRemoveRequirement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveRequirement.Enabled = false;
			this.btnRemoveRequirement.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemoveRequirement.Location = new System.Drawing.Point(362, 40);
			this.btnRemoveRequirement.Name = "btnRemoveRequirement";
			this.btnRemoveRequirement.Size = new System.Drawing.Size(120, 23);
			this.btnRemoveRequirement.TabIndex = 2;
			this.btnRemoveRequirement.Text = "[Remove]";
			this.btnRemoveRequirement.Click += new System.EventHandler(this.btnRemoveRequirement_Click);
			// 
			// btnAddRequirement
			// 
			this.btnAddRequirement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddRequirement.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddRequirement.Location = new System.Drawing.Point(362, 8);
			this.btnAddRequirement.Name = "btnAddRequirement";
			this.btnAddRequirement.Size = new System.Drawing.Size(120, 23);
			this.btnAddRequirement.TabIndex = 1;
			this.btnAddRequirement.Text = "[Add]";
			this.btnAddRequirement.Click += new System.EventHandler(this.btnAddRequirement_Click);
			// 
			// pgUCGeneral
			// 
			this.pgUCGeneral.Controls.Add(this.lblUCName);
			this.pgUCGeneral.Controls.Add(this.btnUCNameChange);
			this.pgUCGeneral.Controls.Add(this.btnSetPrimaryActor);
			this.pgUCGeneral.Controls.Add(this.btnRemoveActor);
			this.pgUCGeneral.Controls.Add(this.btnAddActor);
			this.pgUCGeneral.Controls.Add(this.lvActors);
			this.pgUCGeneral.Controls.Add(this.lblUCActorsTitle);
			this.pgUCGeneral.Controls.Add(this.tbPostconditions);
			this.pgUCGeneral.Controls.Add(this.lblPostconditionsTitle);
			this.pgUCGeneral.Controls.Add(this.tbPreconditions);
			this.pgUCGeneral.Controls.Add(this.lblPreconditionsTitle);
			this.pgUCGeneral.Controls.Add(this.lblUCNameTitle);
			this.pgUCGeneral.Controls.Add(this.lblUCOwner);
			this.pgUCGeneral.Controls.Add(this.lblUCOwnerTitle);
			this.pgUCGeneral.Controls.Add(this.lblUCID);
			this.pgUCGeneral.Controls.Add(this.lblUCIDTitle);
			this.pgUCGeneral.Location = new System.Drawing.Point(4, 22);
			this.pgUCGeneral.Name = "pgUCGeneral";
			this.pgUCGeneral.Size = new System.Drawing.Size(490, 348);
			this.pgUCGeneral.TabIndex = 0;
			this.pgUCGeneral.Text = "[General (UC)]";
			// 
			// lblUCName
			// 
			this.lblUCName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblUCName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblUCName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCName.Location = new System.Drawing.Point(104, 40);
			this.lblUCName.Name = "lblUCName";
			this.lblUCName.Size = new System.Drawing.Size(250, 20);
			this.lblUCName.TabIndex = 28;
			// 
			// btnUCNameChange
			// 
			this.btnUCNameChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUCNameChange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnUCNameChange.Location = new System.Drawing.Point(362, 40);
			this.btnUCNameChange.Name = "btnUCNameChange";
			this.btnUCNameChange.Size = new System.Drawing.Size(120, 23);
			this.btnUCNameChange.TabIndex = 0;
			this.btnUCNameChange.Text = "[Change]";
			this.btnUCNameChange.Click += new System.EventHandler(this.btnUCNameChange_Click);
			// 
			// btnSetPrimaryActor
			// 
			this.btnSetPrimaryActor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSetPrimaryActor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSetPrimaryActor.Location = new System.Drawing.Point(362, 280);
			this.btnSetPrimaryActor.Name = "btnSetPrimaryActor";
			this.btnSetPrimaryActor.Size = new System.Drawing.Size(120, 23);
			this.btnSetPrimaryActor.TabIndex = 6;
			this.btnSetPrimaryActor.Text = "[Set primary]";
			this.btnSetPrimaryActor.Click += new System.EventHandler(this.btnSetPrimaryActor_Click);
			// 
			// btnRemoveActor
			// 
			this.btnRemoveActor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveActor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemoveActor.Location = new System.Drawing.Point(362, 248);
			this.btnRemoveActor.Name = "btnRemoveActor";
			this.btnRemoveActor.Size = new System.Drawing.Size(120, 23);
			this.btnRemoveActor.TabIndex = 5;
			this.btnRemoveActor.Text = "[Remove]";
			this.btnRemoveActor.Click += new System.EventHandler(this.btnRemoveActor_Click);
			// 
			// btnAddActor
			// 
			this.btnAddActor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddActor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddActor.Location = new System.Drawing.Point(362, 216);
			this.btnAddActor.Name = "btnAddActor";
			this.btnAddActor.Size = new System.Drawing.Size(120, 23);
			this.btnAddActor.TabIndex = 4;
			this.btnAddActor.Text = "[Add]";
			this.btnAddActor.Click += new System.EventHandler(this.btnAddActor_Click);
			// 
			// lvActors
			// 
			this.lvActors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvActors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.chActorName,
																					   this.chActorPrimary});
			this.lvActors.FullRowSelect = true;
			this.lvActors.GridLines = true;
			this.lvActors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvActors.HideSelection = false;
			this.lvActors.LabelWrap = false;
			this.lvActors.Location = new System.Drawing.Point(104, 216);
			this.lvActors.MultiSelect = false;
			this.lvActors.Name = "lvActors";
			this.lvActors.Size = new System.Drawing.Size(250, 120);
			this.lvActors.TabIndex = 3;
			this.lvActors.View = System.Windows.Forms.View.Details;
			this.lvActors.Layout += new System.Windows.Forms.LayoutEventHandler(this.lvActors_Layout);
			this.lvActors.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvActors_MouseMove);
			this.lvActors.SelectedIndexChanged += new System.EventHandler(this.lvActors_SelectedIndexChanged);
			// 
			// chActorName
			// 
			this.chActorName.Text = "[Name]";
			this.chActorName.Width = 184;
			// 
			// chActorPrimary
			// 
			this.chActorPrimary.Text = "[Primary]";
			this.chActorPrimary.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblUCActorsTitle
			// 
			this.lblUCActorsTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCActorsTitle.Location = new System.Drawing.Point(8, 216);
			this.lblUCActorsTitle.Name = "lblUCActorsTitle";
			this.lblUCActorsTitle.Size = new System.Drawing.Size(88, 16);
			this.lblUCActorsTitle.TabIndex = 22;
			this.lblUCActorsTitle.Text = "[Actors]";
			// 
			// tbPostconditions
			// 
			this.tbPostconditions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbPostconditions.Location = new System.Drawing.Point(104, 144);
			this.tbPostconditions.Multiline = true;
			this.tbPostconditions.Name = "tbPostconditions";
			this.tbPostconditions.Size = new System.Drawing.Size(378, 64);
			this.tbPostconditions.TabIndex = 2;
			this.tbPostconditions.Text = "";
			this.tbPostconditions.TextChanged += new System.EventHandler(this.tbPostconditions_TextChanged);
			// 
			// lblPostconditionsTitle
			// 
			this.lblPostconditionsTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPostconditionsTitle.Location = new System.Drawing.Point(8, 144);
			this.lblPostconditionsTitle.Name = "lblPostconditionsTitle";
			this.lblPostconditionsTitle.Size = new System.Drawing.Size(88, 16);
			this.lblPostconditionsTitle.TabIndex = 20;
			this.lblPostconditionsTitle.Text = "[Postconditions]";
			// 
			// tbPreconditions
			// 
			this.tbPreconditions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbPreconditions.Location = new System.Drawing.Point(104, 72);
			this.tbPreconditions.Multiline = true;
			this.tbPreconditions.Name = "tbPreconditions";
			this.tbPreconditions.Size = new System.Drawing.Size(378, 64);
			this.tbPreconditions.TabIndex = 1;
			this.tbPreconditions.Text = "";
			this.tbPreconditions.TextChanged += new System.EventHandler(this.tbPreconditions_TextChanged);
			// 
			// lblPreconditionsTitle
			// 
			this.lblPreconditionsTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPreconditionsTitle.Location = new System.Drawing.Point(8, 72);
			this.lblPreconditionsTitle.Name = "lblPreconditionsTitle";
			this.lblPreconditionsTitle.Size = new System.Drawing.Size(88, 16);
			this.lblPreconditionsTitle.TabIndex = 18;
			this.lblPreconditionsTitle.Text = "[Preconditions]";
			// 
			// lblUCNameTitle
			// 
			this.lblUCNameTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCNameTitle.Location = new System.Drawing.Point(8, 40);
			this.lblUCNameTitle.Name = "lblUCNameTitle";
			this.lblUCNameTitle.Size = new System.Drawing.Size(80, 16);
			this.lblUCNameTitle.TabIndex = 4;
			this.lblUCNameTitle.Text = "[Name]";
			// 
			// lblUCOwner
			// 
			this.lblUCOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblUCOwner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblUCOwner.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCOwner.Location = new System.Drawing.Point(320, 8);
			this.lblUCOwner.Name = "lblUCOwner";
			this.lblUCOwner.Size = new System.Drawing.Size(162, 20);
			this.lblUCOwner.TabIndex = 3;
			// 
			// lblUCOwnerTitle
			// 
			this.lblUCOwnerTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCOwnerTitle.Location = new System.Drawing.Point(208, 8);
			this.lblUCOwnerTitle.Name = "lblUCOwnerTitle";
			this.lblUCOwnerTitle.Size = new System.Drawing.Size(96, 16);
			this.lblUCOwnerTitle.TabIndex = 2;
			this.lblUCOwnerTitle.Text = "[Owning package]";
			// 
			// lblUCID
			// 
			this.lblUCID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblUCID.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCID.Location = new System.Drawing.Point(104, 8);
			this.lblUCID.Name = "lblUCID";
			this.lblUCID.Size = new System.Drawing.Size(80, 20);
			this.lblUCID.TabIndex = 1;
			// 
			// lblUCIDTitle
			// 
			this.lblUCIDTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUCIDTitle.Location = new System.Drawing.Point(8, 8);
			this.lblUCIDTitle.Name = "lblUCIDTitle";
			this.lblUCIDTitle.Size = new System.Drawing.Size(80, 16);
			this.lblUCIDTitle.TabIndex = 0;
			this.lblUCIDTitle.Text = "[ID]";
			// 
			// pgAGeneral
			// 
			this.pgAGeneral.Controls.Add(this.btnANameChange);
			this.pgAGeneral.Controls.Add(this.lblAName);
			this.pgAGeneral.Controls.Add(this.lblANameTitle);
			this.pgAGeneral.Controls.Add(this.lblAOwner);
			this.pgAGeneral.Controls.Add(this.lblAOwnerTitle);
			this.pgAGeneral.Controls.Add(this.lblAID);
			this.pgAGeneral.Controls.Add(this.lblAIDTitle);
			this.pgAGeneral.Location = new System.Drawing.Point(4, 22);
			this.pgAGeneral.Name = "pgAGeneral";
			this.pgAGeneral.Size = new System.Drawing.Size(490, 348);
			this.pgAGeneral.TabIndex = 7;
			this.pgAGeneral.Text = "[General (A)]";
			// 
			// btnANameChange
			// 
			this.btnANameChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnANameChange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnANameChange.Location = new System.Drawing.Point(362, 40);
			this.btnANameChange.Name = "btnANameChange";
			this.btnANameChange.Size = new System.Drawing.Size(120, 23);
			this.btnANameChange.TabIndex = 0;
			this.btnANameChange.Text = "[Change]";
			this.btnANameChange.Click += new System.EventHandler(this.btnANameChange_Click);
			// 
			// lblAName
			// 
			this.lblAName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblAName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblAName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblAName.Location = new System.Drawing.Point(104, 40);
			this.lblAName.Name = "lblAName";
			this.lblAName.Size = new System.Drawing.Size(250, 20);
			this.lblAName.TabIndex = 11;
			// 
			// lblANameTitle
			// 
			this.lblANameTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblANameTitle.Location = new System.Drawing.Point(8, 40);
			this.lblANameTitle.Name = "lblANameTitle";
			this.lblANameTitle.Size = new System.Drawing.Size(80, 16);
			this.lblANameTitle.TabIndex = 10;
			this.lblANameTitle.Text = "[Name]";
			// 
			// lblAOwner
			// 
			this.lblAOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblAOwner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblAOwner.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblAOwner.Location = new System.Drawing.Point(320, 8);
			this.lblAOwner.Name = "lblAOwner";
			this.lblAOwner.Size = new System.Drawing.Size(162, 20);
			this.lblAOwner.TabIndex = 9;
			// 
			// lblAOwnerTitle
			// 
			this.lblAOwnerTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblAOwnerTitle.Location = new System.Drawing.Point(208, 8);
			this.lblAOwnerTitle.Name = "lblAOwnerTitle";
			this.lblAOwnerTitle.Size = new System.Drawing.Size(96, 16);
			this.lblAOwnerTitle.TabIndex = 8;
			this.lblAOwnerTitle.Text = "[Owning package]";
			// 
			// lblAID
			// 
			this.lblAID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblAID.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblAID.Location = new System.Drawing.Point(104, 8);
			this.lblAID.Name = "lblAID";
			this.lblAID.Size = new System.Drawing.Size(80, 20);
			this.lblAID.TabIndex = 7;
			// 
			// lblAIDTitle
			// 
			this.lblAIDTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblAIDTitle.Location = new System.Drawing.Point(8, 8);
			this.lblAIDTitle.Name = "lblAIDTitle";
			this.lblAIDTitle.Size = new System.Drawing.Size(80, 16);
			this.lblAIDTitle.TabIndex = 6;
			this.lblAIDTitle.Text = "[ID]";
			// 
			// pgPGeneral
			// 
			this.pgPGeneral.Controls.Add(this.btnPNameChange);
			this.pgPGeneral.Controls.Add(this.lblPName);
			this.pgPGeneral.Controls.Add(this.lblPNameTitle);
			this.pgPGeneral.Controls.Add(this.lblPOwner);
			this.pgPGeneral.Controls.Add(this.lblPOwnerTitle);
			this.pgPGeneral.Controls.Add(this.lblPID);
			this.pgPGeneral.Controls.Add(this.lblPIDTitle);
			this.pgPGeneral.Location = new System.Drawing.Point(4, 22);
			this.pgPGeneral.Name = "pgPGeneral";
			this.pgPGeneral.Size = new System.Drawing.Size(490, 348);
			this.pgPGeneral.TabIndex = 9;
			this.pgPGeneral.Text = "[General(P)]";
			// 
			// btnPNameChange
			// 
			this.btnPNameChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPNameChange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPNameChange.Location = new System.Drawing.Point(362, 40);
			this.btnPNameChange.Name = "btnPNameChange";
			this.btnPNameChange.Size = new System.Drawing.Size(120, 23);
			this.btnPNameChange.TabIndex = 0;
			this.btnPNameChange.Text = "[Change]";
			this.btnPNameChange.Click += new System.EventHandler(this.btnPNameChange_Click);
			// 
			// lblPName
			// 
			this.lblPName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblPName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPName.Location = new System.Drawing.Point(104, 40);
			this.lblPName.Name = "lblPName";
			this.lblPName.Size = new System.Drawing.Size(250, 20);
			this.lblPName.TabIndex = 17;
			// 
			// lblPNameTitle
			// 
			this.lblPNameTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPNameTitle.Location = new System.Drawing.Point(8, 40);
			this.lblPNameTitle.Name = "lblPNameTitle";
			this.lblPNameTitle.Size = new System.Drawing.Size(80, 16);
			this.lblPNameTitle.TabIndex = 16;
			this.lblPNameTitle.Text = "[Name]";
			// 
			// lblPOwner
			// 
			this.lblPOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPOwner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblPOwner.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPOwner.Location = new System.Drawing.Point(320, 8);
			this.lblPOwner.Name = "lblPOwner";
			this.lblPOwner.Size = new System.Drawing.Size(162, 20);
			this.lblPOwner.TabIndex = 15;
			// 
			// lblPOwnerTitle
			// 
			this.lblPOwnerTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPOwnerTitle.Location = new System.Drawing.Point(208, 8);
			this.lblPOwnerTitle.Name = "lblPOwnerTitle";
			this.lblPOwnerTitle.Size = new System.Drawing.Size(96, 16);
			this.lblPOwnerTitle.TabIndex = 14;
			this.lblPOwnerTitle.Text = "[Owning package]";
			// 
			// lblPID
			// 
			this.lblPID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblPID.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPID.Location = new System.Drawing.Point(104, 8);
			this.lblPID.Name = "lblPID";
			this.lblPID.Size = new System.Drawing.Size(80, 20);
			this.lblPID.TabIndex = 13;
			// 
			// lblPIDTitle
			// 
			this.lblPIDTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPIDTitle.Location = new System.Drawing.Point(8, 8);
			this.lblPIDTitle.Name = "lblPIDTitle";
			this.lblPIDTitle.Size = new System.Drawing.Size(80, 16);
			this.lblPIDTitle.TabIndex = 12;
			this.lblPIDTitle.Text = "[ID]";
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 402);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(628, 22);
			this.statusBar.TabIndex = 5;
			// 
			// openModelFileDialog
			// 
			this.openModelFileDialog.DefaultExt = "ucm";
			this.openModelFileDialog.Filter = "UseCaseMaker Model|*.ucm";
			// 
			// saveModelFileDialog
			// 
			this.saveModelFileDialog.DefaultExt = "ucm";
			this.saveModelFileDialog.Filter = "UseCaseMaker Model|*.ucm";
			// 
			// selectDocFileDialog
			// 
			this.selectDocFileDialog.DefaultExt = "*.*";
			this.selectDocFileDialog.Filter = "All files|*.*";
			// 
			// elementTokenCtxMenu
			// 
			this.elementTokenCtxMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								this.mnuCtxETGoToDefinition});
			// 
			// mnuCtxETGoToDefinition
			// 
			this.mnuCtxETGoToDefinition.Index = 0;
			this.mnuCtxETGoToDefinition.Text = "[Go to definition]";
			this.mnuCtxETGoToDefinition.Click += new System.EventHandler(this.mnuCtxETGoToDefinition_Click);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(628, 424);
			this.Controls.Add(this.splLeft);
			this.Controls.Add(this.tabUseCase);
			this.Controls.Add(this.pnlModelBrowser);
			this.Controls.Add(this.toolBar);
			this.Controls.Add(this.statusBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.MinimumSize = new System.Drawing.Size(630, 450);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Use Case Maker";
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.LocationChanged += new System.EventHandler(this.frmMain_LocationChanged);
			this.pnlModelBrowser.ResumeLayout(false);
			this.tabUseCase.ResumeLayout(false);
			this.pgMain.ResumeLayout(false);
			this.pnlAbout.ResumeLayout(false);
			this.pnlUseCasesContainer.ResumeLayout(false);
			this.pnlActorsContainer.ResumeLayout(false);
			this.pnlPackagesContainer.ResumeLayout(false);
			this.pnlFullPathContainer.ResumeLayout(false);
			this.pgGlossary.ResumeLayout(false);
			this.pgHistory.ResumeLayout(false);
			this.pgDetails.ResumeLayout(false);
			this.pgFlowOfEvents.ResumeLayout(false);
			this.pgProse.ResumeLayout(false);
			this.pgAttributes.ResumeLayout(false);
			this.pgRequirements.ResumeLayout(false);
			this.pgUCGeneral.ResumeLayout(false);
			this.pgAGeneral.ResumeLayout(false);
			this.pgPGeneral.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/**
		 * Punto di ingresso principale dell'applicazione.
		 */
		[STAThread]
		static void Main(string [] args) 
		{
			if(args.Length > 0)
			{
				Application.Run(new frmMain(args[0]));
			}
			else
			{
				Application.Run(new frmMain(string.Empty));
			}
		}

		#region Public Properties
		/**
		 * @brief Modello in uso
		 * 
		 * Ritorna il modello correntemente in uso dall'applicazione
		 */
		public Model Model
		{
			get
			{
				return this.model;
			}
		}
		#endregion

		/**
		 * @brief Localizzazione dei controlli
		 * 
		 * Utilizza la classe Localizer per impostare il testo dei 
		 * controlli dell'applicazione nella lingua in uso dall'utente
		 * 
		 * @param languageFilePath Percorso al file xml contenente la lingua
		 */
		private void LocalizeControls(string languageFilePath)
		{
			this.localizer.Load(languageFilePath);

			this.localizer.LocalizeControls(this);
		}

		/**
		 * @brief Gestione delle modifiche utente
		 * 
		 * Traccia lo stato delle modifiche effettuate dall'utente.
		 * Attiva il comando salva (nel menu e nella toolbar) quando
		 * un elemento del business dell'applicazione è stato modificato
		 * 
		 * @param value Il comando salva è attivo (vero) o disattivo (falso)
		 */
		private void SetModified(bool value)
		{
			if(this.modifiedLocked)
			{
				return;
			}

			if(value == true)
			{
				mnuFileSave.Enabled = true;
				toolBar.Buttons[2].Enabled = true;
			}
			else
			{
				mnuFileSave.Enabled = false;
				toolBar.Buttons[2].Enabled = false;
			}
			this.modified = value;
		}

		/**
		 * @brief Gestione delle modifiche utente
		 * 
		 * Blocca le variazioni dello stato delle modifiche durante
		 * il caricamento e/o il ridisegno dei controlli dell'applicazione
		 */
		private void LockModified()
		{
			this.modifiedLocked = true;
		}

		/**
		 * @brief Gestione delle modifiche utente
		 * 
		 * Sblocca le variazioni dello stato delle modifiche
		 */
		private void UnlockModified()
		{
			this.modifiedLocked = false;
		}

		/**
		 * @brief Gestione del modello
		 * 
		 * Chiude il modello correntemente in uso.
		 * Se gli elementi del business risultano modificati,
		 * richiede il salvataggio prima della chiusura
		 */
		private void CloseModel()
		{
			if(this.modified)
			{
				// [Save current model?]
				if(MessageBox.Show(
					this.localizer.GetValue("UserMessages","saveModel"),
					Application.ProductName,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if(this.modelFilePath != string.Empty)
					{
						this.SaveModel(false);
					}
					else
					{
						this.SaveModel(true);
					}
				}
			}	
		}

		/**
		 * @brief Visualizzazione del modello
		 * 
		 * Costruisce la vista ad albero che riflette la struttura del modello
		 * correntemente in uso
		 */
		private void BuildView(object element)
		{
			if(element.GetType() == typeof(Model))
			{
				Model model = (Model)element;
				AddElement(model,null,false);
				foreach(Actor actor in model.Actors)
				{
					actor.Owner = model;
					AddElement(actor,actor.Owner,false);
				}
				foreach(UseCase useCase in model.UseCases)
				{
					useCase.Owner = model;
					AddElement(useCase,useCase.Owner,false);
				}
				foreach(Package subPackage in model.Packages)
				{
					subPackage.Owner = model;
					BuildView(subPackage);
				}
				foreach(GlossaryItem gi in model.Glossary)
				{
					gi.Owner = model;
					string sub = "\"" + gi.Name + "\"";
					sub = sub.Replace(" ","\t");
					sub = sub.Replace(".","\v");
					HighlightDescriptor hd = new 
						HighlightDescriptor(sub,Color.Green,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
					this.hdc.Add(hd);
				}
			}
			if(element.GetType() == typeof(Package))
			{
				Package package = (Package)element;
				AddElement(package,package.Owner,false);
				foreach(Actor actor in package.Actors)
				{
					actor.Owner = package;
					AddElement(actor,actor.Owner,false);
				}
				foreach(UseCase useCase in package.UseCases)
				{
					useCase.Owner = package;
					AddElement(useCase,useCase.Owner,false);
				}
				foreach(Package subPackage in package.Packages)
				{
					subPackage.Owner = package;
					BuildView(subPackage);
				}
			}
		}
		
		/**
		 * @brief Visualizzazione del modello
		 * 
		 * Aggiorna gli elementi del modello riflettendo lo stato
		 * dell business e le modifiche apportate dall'utente
		 */
		private void UpdateView()
		{
			if(this.lockUpdate)
			{
				return;
			}

			LockWindowUpdate(tabUseCase.Handle);
			this.LockModified();

			tabUseCase.TabPages.Clear();
			TreeNode node = tvModelBrowser.SelectedNode;

			// Cerca il nodo selezionato
			this.currentElement = model.FindElementByUniqueID((String)node.Tag);
			if(currentElement != null)
			{
				if(this.currentElement.GetType() == typeof(Model))
				{
					// ToolBar controls
					tbBtnAddActor.Enabled = false;
					tbBtnAddPackage.Enabled = true;
					tbBtnAddUseCase.Enabled = false;
					tbBtnRemoveActor.Enabled = false;
					tbBtnRemovePackage.Enabled = false;
					tbBtnRemoveUseCase.Enabled = false;

					// Menu items
					mnuEditAddActor.Enabled = false;
					mnuEditAddPackage.Enabled = true;
					mnuEditAddUseCase.Enabled = false;
					mnuEditRemoveActor.Enabled = false;
					mnuEditRemovePackage.Enabled = false;
					mnuEditRemoveUseCase.Enabled = false;

					// Model browser context menu items
					mnuCtxMBAddActor.Enabled = false;
					mnuCtxMBAddPackage.Enabled = true;
					mnuCtxMBAddUseCase.Enabled = false;
					mnuCtxMBRemoveActor.Enabled = false;
					mnuCtxMBRemovePackage.Enabled = false;
					mnuCtxMBRemoveUseCase.Enabled = false;
					
					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = true;
					pnlActorsContainer.Visible = true;
					pnlUseCasesContainer.Visible = true;
					lblPackages.Text = model.Packages.Count.ToString();
					lblActors.Text = model.Actors.Count.ToString();
					lblUseCases.Text = model.UseCases.Count.ToString();

					tabUseCase.TabPages.Add(pgPGeneral);
					lblPOwner.Text = String.Empty;
					lblPID.Text = model.ElementID;
					lblPName.Text = model.Name;

					tabUseCase.TabPages.Add(pgAttributes);
					tbDescription.Text = model.Attributes.Description;
					tbNotes.Text = model.Attributes.Notes;
					lvRelatedDocs.Items.Clear();
					foreach(RelatedDocument rd in model.Attributes.RelatedDocuments)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = rd.FileName;
						this.lvRelatedDocs.Items.Add(lvi);
					}
					btnRemoveRelatedDoc.Enabled = false;
					btnOpenRelatedDoc.Enabled = false;

					tabUseCase.TabPages.Add(pgRequirements);
					RList.DataSource = model.Requirements;
					RList.IndexDataField = "Name";
					RList.TextDataField = "Description";
					RList.UniqueIDDataField = "UniqueID";
					RList.DataBind();
					if(model.Requirements.Count > 0)
					{
						RList.SelectedIndex = 0;
					}

					tabUseCase.TabPages.Add(pgGlossary);
					GList.Items.Clear();
					GList.DataSource = model.Glossary;
					GList.IndexDataField = "Name";
					GList.TextDataField = "Description";
					GList.UniqueIDDataField = "UniqueID";
					GList.DataBind();
					if(model.Glossary.Count > 0)
					{
						GList.SelectedIndex = 0;
					}
				}
				else if(this.currentElement.GetType() == typeof(Package))
				{
					Package package = (Package)this.currentElement;

					// ToolBar controls
					tbBtnAddActor.Enabled = true;
					tbBtnAddPackage.Enabled = true;
					tbBtnAddUseCase.Enabled = true;
					tbBtnRemoveActor.Enabled = false;
					tbBtnRemovePackage.Enabled = true;
					tbBtnRemoveUseCase.Enabled = false;

					// Menu items
					mnuEditAddActor.Enabled = true;
					mnuEditAddPackage.Enabled = true;
					mnuEditAddUseCase.Enabled = true;
					mnuEditRemoveActor.Enabled = false;
					mnuEditRemovePackage.Enabled = true;
					mnuEditRemoveUseCase.Enabled = false;

					// Model browser context menu items
					mnuCtxMBAddActor.Enabled = true;
					mnuCtxMBAddPackage.Enabled = true;
					mnuCtxMBAddUseCase.Enabled = true;
					mnuCtxMBRemoveActor.Enabled = false;
					mnuCtxMBRemovePackage.Enabled = true;
					mnuCtxMBRemoveUseCase.Enabled = false;

					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = true;
					pnlActorsContainer.Visible = true;
					pnlUseCasesContainer.Visible = true;
					lblPackages.Text = package.Packages.Count.ToString();
					lblActors.Text = package.Actors.Count.ToString();
					lblUseCases.Text = package.UseCases.Count.ToString();

					tabUseCase.TabPages.Add(pgPGeneral);
					lblPOwner.Text = package.Owner.Name;
					lblPID.Text = package.ElementID;
					lblPName.Text = package.Name;

					tabUseCase.TabPages.Add(pgAttributes);
					tbDescription.Text = package.Attributes.Description;
					tbNotes.Text = package.Attributes.Notes;
					lvRelatedDocs.Items.Clear();
					foreach(RelatedDocument rd in package.Attributes.RelatedDocuments)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = rd.FileName;
						this.lvRelatedDocs.Items.Add(lvi);
					}
					btnRemoveRelatedDoc.Enabled = false;
					btnOpenRelatedDoc.Enabled = false;

					tabUseCase.TabPages.Add(pgRequirements);
					RList.DataSource = package.Requirements;
					RList.IndexDataField = "Name";
					RList.TextDataField = "Description";
					RList.UniqueIDDataField = "UniqueID";
					RList.DataBind();
					if(package.Requirements.Count > 0)
					{
						RList.SelectedIndex = 0;
					}
				}
				else if(this.currentElement.GetType() == typeof(Actors))
				{
					// ToolBar controls
					tbBtnAddActor.Enabled = true;
					tbBtnAddPackage.Enabled = false;
					tbBtnAddUseCase.Enabled = false;
					tbBtnRemoveActor.Enabled = false;
					tbBtnRemovePackage.Enabled = false;
					tbBtnRemoveUseCase.Enabled = false;

					// Menu items
					mnuEditAddActor.Enabled = true;
					mnuEditAddPackage.Enabled = false;
					mnuEditAddUseCase.Enabled = false;
					mnuEditRemoveActor.Enabled = false;
					mnuEditRemovePackage.Enabled = false;
					mnuEditRemoveUseCase.Enabled = false;

					// Model browser context menu items
					mnuCtxMBAddActor.Enabled = true;
					mnuCtxMBAddPackage.Enabled = false;
					mnuCtxMBAddUseCase.Enabled = false;
					mnuCtxMBRemoveActor.Enabled = false;
					mnuCtxMBRemovePackage.Enabled = false;
					mnuCtxMBRemoveUseCase.Enabled = false;
					
					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = false;
					pnlActorsContainer.Visible = true;
					pnlUseCasesContainer.Visible = false;
					lblActors.Text = ((Actors)this.currentElement).Owner.Actors.Count.ToString();
				}
				else if(this.currentElement.GetType() == typeof(UseCases))
				{
					// ToolBar controls
					tbBtnAddActor.Enabled = false;
					tbBtnAddPackage.Enabled = false;
					tbBtnAddUseCase.Enabled = true;
					tbBtnRemoveActor.Enabled = false;
					tbBtnRemovePackage.Enabled = false;
					tbBtnRemoveUseCase.Enabled = false;

					// Menu items
					mnuEditAddActor.Enabled = false;
					mnuEditAddPackage.Enabled = false;
					mnuEditAddUseCase.Enabled = true;
					mnuEditRemoveActor.Enabled = false;
					mnuEditRemovePackage.Enabled = false;
					mnuEditRemoveUseCase.Enabled = false;

					// Model browser context menu items
					mnuCtxMBAddActor.Enabled = false;
					mnuCtxMBAddPackage.Enabled = false;
					mnuCtxMBAddUseCase.Enabled = true;
					mnuCtxMBRemoveActor.Enabled = false;
					mnuCtxMBRemovePackage.Enabled = false;
					mnuCtxMBRemoveUseCase.Enabled = false;

					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = false;
					pnlActorsContainer.Visible = false;
					pnlUseCasesContainer.Visible = true;
					lblUseCases.Text = ((UseCases)this.currentElement).Owner.UseCases.Count.ToString();
				}
				else if(this.currentElement.GetType() == typeof(Actor))
				{
					Actor actor = (Actor)this.currentElement;

					// ToolBar controls
					tbBtnAddActor.Enabled = false;
					tbBtnAddPackage.Enabled = false;
					tbBtnAddUseCase.Enabled = false;
					tbBtnRemoveActor.Enabled = true;
					tbBtnRemovePackage.Enabled = false;
					tbBtnRemoveUseCase.Enabled = false;

					// Menu items
					mnuEditAddActor.Enabled = false;
					mnuEditAddPackage.Enabled = false;
					mnuEditAddUseCase.Enabled = false;
					mnuEditRemoveActor.Enabled = true;
					mnuEditRemovePackage.Enabled = false;
					mnuEditRemoveUseCase.Enabled = false;

					// Model browser context menu items
					mnuCtxMBAddActor.Enabled = false;
					mnuCtxMBAddPackage.Enabled = false;
					mnuCtxMBAddUseCase.Enabled = false;
					mnuCtxMBRemoveActor.Enabled = true;
					mnuCtxMBRemovePackage.Enabled = false;
					mnuCtxMBRemoveUseCase.Enabled = false;

					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = false;
					pnlActorsContainer.Visible = false;
					pnlUseCasesContainer.Visible = false;

					tabUseCase.TabPages.Add(pgAGeneral);
					lblAOwner.Text = actor.Owner.Name;
					lblAID.Text = actor.ElementID;
					lblAName.Text = actor.Name;
					
					tabUseCase.TabPages.Add(pgAttributes);
					tbDescription.Text = actor.Attributes.Description;
					tbNotes.Text = actor.Attributes.Notes;
					lvRelatedDocs.Items.Clear();
					foreach(RelatedDocument rd in actor.Attributes.RelatedDocuments)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = rd.FileName;
						this.lvRelatedDocs.Items.Add(lvi);
					}
					btnRemoveRelatedDoc.Enabled = false;
					btnOpenRelatedDoc.Enabled = false;

				}
				else if(this.currentElement.GetType() == typeof(UseCase))
				{
					UseCase useCase = (UseCase)this.currentElement;

					// ToolBar controls
					tbBtnAddActor.Enabled = false;
					tbBtnAddPackage.Enabled = false;
					tbBtnAddUseCase.Enabled = false;
					tbBtnRemoveActor.Enabled = false;
					tbBtnRemovePackage.Enabled = false;
					tbBtnRemoveUseCase.Enabled = true;

					// Menu items
					mnuEditAddActor.Enabled = false;
					mnuEditAddPackage.Enabled = false;
					mnuEditAddUseCase.Enabled = false;
					mnuEditRemoveActor.Enabled = false;
					mnuEditRemovePackage.Enabled = false;
					mnuEditRemoveUseCase.Enabled = true;

					// Model browser context menu items
					mnuCtxMBAddActor.Enabled = false;
					mnuCtxMBAddPackage.Enabled = false;
					mnuCtxMBAddUseCase.Enabled = false;
					mnuCtxMBRemoveActor.Enabled = false;
					mnuCtxMBRemovePackage.Enabled = false;
					mnuCtxMBRemoveUseCase.Enabled = true;

					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = false;
					pnlActorsContainer.Visible = false;
					pnlUseCasesContainer.Visible = false;

					tabUseCase.TabPages.Add(pgUCGeneral);
					lblUCOwner.Text = useCase.Owner.Name;
					lblUCID.Text = useCase.ElementID;
					lblUCName.Text = useCase.Name;
					tbPreconditions.Text = useCase.Preconditions;
					tbPostconditions.Text = useCase.Postconditions;
					lvActors.Items.Clear();
					foreach(ActiveActor aactor in useCase.ActiveActors)
					{
						Actor actor = (Actor)model.FindElementByUniqueID(aactor.ActorUniqueID);
						ListViewItem lvi = new ListViewItem();
						lvi.Text = actor.Name;
						if(aactor.IsPrimary)
						{
							lvi.SubItems.Add("X");
						}
						else
						{
							lvi.SubItems.Add("");
						}
						this.lvActors.Items.Add(lvi);
					}
					btnRemoveActor.Enabled = false;
					btnSetPrimaryActor.Enabled = false;

					tabUseCase.TabPages.Add(pgDetails);
					OIList.DataSource = useCase.OpenIssues;
					OIList.IndexDataField = "Name";
					OIList.TextDataField = "Description";
					OIList.UniqueIDDataField = "UniqueID";
					OIList.DataBind();
					if(useCase.OpenIssues.Count > 0)
					{
						OIList.SelectedIndex = 0;
					}
					cmbLevel.SelectedIndex = (int)useCase.Level;
					cmbComplexity.SelectedIndex = (int)useCase.Complexity;
					cmbStatus.SelectedIndex = (int)useCase.Status;
					cmbImplementation.SelectedIndex = (int)useCase.Implementation;
					tbPriority.Text = useCase.Priority.ToString();
					tbAssignedTo.Text = useCase.AssignedTo;
					tbRelease.Text = useCase.Release;
					
					tabUseCase.TabPages.Add(pgAttributes);
					tbDescription.Text = useCase.Attributes.Description;
					tbNotes.Text = useCase.Attributes.Notes;
					lvRelatedDocs.Items.Clear();
					foreach(RelatedDocument rd in useCase.Attributes.RelatedDocuments)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = rd.FileName;
						this.lvRelatedDocs.Items.Add(lvi);
					}
					btnRemoveRelatedDoc.Enabled = false;
					btnOpenRelatedDoc.Enabled = false;
					
					tabUseCase.TabPages.Add(pgFlowOfEvents);
					UCList.DataSource = useCase.Steps;
					UCList.IndexDataField = "Name";
					UCList.TextDataField = "Description";
					UCList.UniqueIDDataField = "UniqueID";
					UCList.DataBind();
					if(useCase.Steps.Count > 0)
					{
						UCList.SelectedIndex = 0;
					}
					
					tabUseCase.TabPages.Add(pgProse);
					tbProse.Text = useCase.Prose;
					tbProse.ParseNow();

					tabUseCase.TabPages.Add(pgHistory);
					lvHistory.Items.Clear();
					foreach(HistoryItem hi in useCase.HistoryItems)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = hi.LocalizatedDateValue;
						if(hi.Type == HistoryItem.HistoryType.Implementation)
						{
							lvi.SubItems.Add(this.localizer.GetValue("Globals","Implementation"));
							lvi.SubItems.Add((string)cmbImplementation.Items[hi.Action]);
							lvi.SubItems.Add(hi.Notes);
						}
						else
						{
							lvi.SubItems.Add(this.localizer.GetValue("Globals","Status"));
							lvi.SubItems.Add((string)cmbStatus.Items[hi.Action]);
							lvi.SubItems.Add(hi.Notes);
						}
						this.lvHistory.Items.Add(lvi);
					}
					btnRemoveHistoryItem.Enabled = false;
				}			
			}

			IdentificableObjectCollection coll = (this.currentElement as IdentificableObjectCollection);
			if(coll != null)
			{
				lblFullPath.Text = ((IdentificableObjectCollection)this.currentElement).Path;
			}
			else
			{
				lblFullPath.Text = ((IdentificableObject)this.currentElement).Path;
			}

			this.UnlockModified();

			LockWindowUpdate(new IntPtr(0));
			tabUseCase.Invalidate();
		}

		/**
		 * @brief Gestione del modello
		 * 
		 * Trova il nodo selezionato dall'utente usando il valore
		 * univoco assegnato al nodo stesso dal business.
		 * Il metodo agisce in modo ricorsivo.
		 * @param parent Nodo genitore da cui avviare la ricerca
		 * @param parentUniqueID Identificativo univoco del nodo da ricercare
		 */
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

		/**
		 * @brief Gestione del modello
		 * 
		 * Aggiunge un elemento alla struttura del modello tramite
		 * il business dell'applicazione.
		 * @param element Elemento da aggiungere
		 * @param owner Elemento a cui appartiene l'elemento da aggiungere
		 * @addToParent Flag che stabilisce se l'elemento è gia collegato al genitore
		 */
		private void AddElement(object element, object owner, bool addToParent)
		{
			String ownerUniqueID = String.Empty;
			string sub = null;

			if(element.GetType() == typeof(Model))
			{
				Model model = (Model)element;
				tvModelBrowser.Nodes.Clear();
				TreeNode node = new TreeNode(model.Name + " (" + model.ElementID + ")");
				node.Tag = model.UniqueID;
				tvModelBrowser.Nodes.Add(node);
				tvModelBrowser.SelectedNode = node;
				TreeNode ownerNode = node;
				node = new TreeNode(this.localizer.GetValue("Globals","Actors"),1,1);
				node.Tag = model.Actors.UniqueID;
				ownerNode.Nodes.Add(node);
				node = new TreeNode(this.localizer.GetValue("Globals","UseCases"),2,2);
				node.Tag = model.UseCases.UniqueID;
				ownerNode.Nodes.Add(node);
				sub = "\"" + model.Path + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				HighlightDescriptor hd = new HighlightDescriptor(sub,Color.DarkGray,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
				sub = "\"" + model.Name + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				hd = new HighlightDescriptor(sub,Color.Red,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
			}

			if(element.GetType() == typeof(Package))
			{
				Package package = (Package)element;
				ownerUniqueID = ((Package)owner).UniqueID;
				if(addToParent)
				{
					((Package)owner).AddPackage(package);
				}
				TreeNode node = new TreeNode(package.Name + " (" + package.ElementID + ")");
				node.Tag = package.UniqueID;
				TreeNode ownerNode = this.FindNode(null,ownerUniqueID);
				if(ownerNode != null)
				{
					ownerNode.Nodes.Add(node);
					tvModelBrowser.SelectedNode = node;
					ownerNode = node;
					node = new TreeNode(this.localizer.GetValue("Globals","Actors"),1,1);
					node.Tag = package.Actors.UniqueID;
					ownerNode.Nodes.Add(node);
					node = new TreeNode(this.localizer.GetValue("Globals","UseCases"),2,2);
					node.Tag = package.UseCases.UniqueID;
					ownerNode.Nodes.Add(node);
				}
				sub = "\"" + package.Path + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				HighlightDescriptor hd = new HighlightDescriptor(sub,Color.DarkGray,null,DescriptorType.Word,DescriptorRecognition.WholeWord,false);
				this.hdc.Add(hd);
				sub = "\"" + package.Name + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				hd = new HighlightDescriptor(sub,Color.Red,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
			}

			if(element.GetType() == typeof(Actor))
			{
				Actor actor = (Actor)element;
				Package package = (Package)owner;
				if(addToParent)
				{
					package.AddActor(actor);
				}
				TreeNode node = new TreeNode(actor.Name + " (" + actor.ElementID + ")",3,3);
				node.Tag = actor.UniqueID;
				TreeNode ownerNode = this.FindNode(null,package.UniqueID);
				if(ownerNode != null)
				{
					foreach(TreeNode subNode in ownerNode.Nodes)
					{
						if((String)subNode.Tag == package.Actors.UniqueID)
						{
							subNode.Nodes.Add(node);
							tvModelBrowser.SelectedNode = node;
							break;
						}
					}
				}
				sub = "\"" + actor.Path + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				HighlightDescriptor hd = new HighlightDescriptor(sub,Color.DarkGray,null,DescriptorType.Word,DescriptorRecognition.WholeWord,false);
				this.hdc.Add(hd);
				sub = "\"" + actor.Name + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				hd = new HighlightDescriptor(sub,Color.Red,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
			}

			if(element.GetType() == typeof(UseCase))
			{
				UseCase useCase = (UseCase)element;
				Package package = (Package)owner;
				if(addToParent)
				{
					package.AddUseCase(useCase);
				}
				TreeNode node = new TreeNode(useCase.Name + " (" + useCase.ElementID + ")",4,4);
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
				sub = "\"" + useCase.Path + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				HighlightDescriptor hd = new HighlightDescriptor(sub,Color.DarkGray,null,DescriptorType.Word,DescriptorRecognition.WholeWord,false);
				this.hdc.Add(hd);
				sub = "\"" + useCase.Name + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				hd = new HighlightDescriptor(sub,Color.Red,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
			}

			this.SetModified(true);
		}

		/**
		 * @ingroup user_interface
		 * @brief Gestione del modello
		 * 
		 * Crea un nuovo modello contenente il solo package
		 * principale modificando il business dell'applicazione.
		 * 
		 * @note Il metodo richiama frmMain::CloseModel nel caso
		 * in cui vi sia un modello già in uso che necessita
		 * di essere salvato
		 */
		private void NewModel()
		{
			this.CloseModel();

			System.Char [] separators = {' ','\r','\n',',','.','-','+','\\','\'','?','!'};
			foreach(System.Char c in separators)
			{
				this.separators.Add(c);
			}

			this.hdc.Clear();

			UCList.Items.Clear();
			UCList.Separators = this.separators;
			UCList.HighlightDescriptors = this.hdc;

			OIList.Items.Clear();
			OIList.Separators = this.separators;
			OIList.HighlightDescriptors = this.hdc;

			RList.Items.Clear();
			RList.Separators = this.separators;
			RList.HighlightDescriptors = this.hdc;

			tbProse.CaseSensitive = true;
			tbProse.Separators = this.separators;
			tbProse.HighlightDescriptors = this.hdc;

			this.lockUpdate = true;
			model = new Model(this.localizer.GetValue("Globals","NewModel"),defaultMPrefix,1);
			BuildView(model);
			this.lockUpdate = false;

			this.UpdateView();

			// Cambia il titolo della finestra
			int sub = this.Text.IndexOf("-");
			if(sub != -1)
			{
				this.Text = this.Text.Substring(0,sub - 1) + " - [" + model.Name + "]";
			}
			else
			{
				this.Text += " - [" + model.Name + "]";
			}

			this.SetModified(false);
		}

		/**
		 * @ingroup user_interface
		 * @brief Gestione del modello
		 * 
		 * Apre un modello da file.
		 * 
		 * @note Il metodo richiama frmMain::CloseModel nel caso
		 * in cui vi sia un modello già in uso che necessita
		 * di essere salvato
		 */
		private void OpenModel()
		{
			this.CloseModel();

			if(Directory.Exists(this.appSettings.ModelFilePath))
			{
				openModelFileDialog.InitialDirectory = this.appSettings.ModelFilePath;
			}

			// [Open Model]
			openModelFileDialog.Title = this.localizer.GetValue("UserMessages","openModel");
			openModelFileDialog.FileName = string.Empty;
			if(openModelFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.appSettings.ModelFilePath = Path.GetDirectoryName(openModelFileDialog.FileName);
				this.modelFilePath = Path.GetDirectoryName(openModelFileDialog.FileName);
				this.modelFileName = Path.GetFileName(openModelFileDialog.FileName);

				this.appSettings.AddToRecentFileList(openModelFileDialog.FileName);
				this.UpdateRecentFileList();

				System.Char [] separators = {' ','\r','\n',',','.','-','+','\\','\'','?','!'};
				foreach(System.Char c in separators)
				{
					this.separators.Add(c);
				}

				this.hdc.Clear();

				UCList.Items.Clear();
				UCList.Separators = this.separators;
				UCList.HighlightDescriptors = this.hdc;

				OIList.Items.Clear();
				OIList.Separators = this.separators;
				OIList.HighlightDescriptors = this.hdc;

				RList.Items.Clear();
				RList.Separators = this.separators;
				RList.HighlightDescriptors = this.hdc;

				tbProse.CaseSensitive = true;
				tbProse.Separators = this.separators;
				tbProse.HighlightDescriptors = this.hdc;

				this.lockUpdate = true;
				LockWindowUpdate(tvModelBrowser.Handle);
				this.Cursor = Cursors.WaitCursor;
				XmlDocument doc = new XmlDocument();
				try
				{
					doc.Load(openModelFileDialog.FileName);
					model = new Model();
					UseCaseMakerLibrary.XmlSerializer.XmlDeserialize(
						doc,
						"UCM-Document",
						"", // "http://www.magosoftware.it/" + this.GetType().Namespace
						this.GetType().Assembly.GetName().Version.ToString(2),
						model);
					BuildView(model);
				}
				catch(XmlException e)
				{
					MessageBox.Show(this,e.Message,Application.ProductName);
					this.NewModel();
				}
				catch(XmlSerializerException e)
				{
					MessageBox.Show(this,e.Message,Application.ProductName);
					this.NewModel();
				}
				finally
				{
					this.lockUpdate = false;
					LockWindowUpdate(new IntPtr(0));
					tvModelBrowser.Invalidate();
				}
				UpdateView();
				this.Cursor = Cursors.Default;

				// Cambia il titolo della finestra
				int sub = this.Text.IndexOf("-");
				if(sub != -1)
				{
					this.Text = this.Text.Substring(0,sub - 1) + " - " + this.modelFileName;
				}
				else
				{
					this.Text += " - " + this.modelFileName;
				}
			}

			this.SetModified(false);
		}

		/**
		 * @brief Gestione del modello
		 * 
		 * Apre un modello da file sulla base della selezione utente
		 * dal menu File - elementi recenti
		 * 
		 * @note Il metodo richiama this::CloseModel nel caso
		 * in cui vi sia un modello già in uso che necessita
		 * di essere salvato
		 */
		private void OpenRecentModel(string modelFilePath)
		{
			this.CloseModel();

			if(!File.Exists(modelFilePath))
			{
				MessageBox.Show(this,this.localizer.GetValue("UserMessages","cannotOpenFile"));
				return;
			}

			this.appSettings.ModelFilePath = Path.GetDirectoryName(modelFilePath);
			this.modelFilePath = Path.GetDirectoryName(modelFilePath);
			this.modelFileName = Path.GetFileName(modelFilePath);

			System.Char [] separators = {' ','\r','\n',',','.','-','+','\\','\'','?','!'};
			foreach(System.Char c in separators)
			{
				this.separators.Add(c);
			}

			this.hdc.Clear();

			UCList.Items.Clear();
			UCList.Separators = this.separators;
			UCList.HighlightDescriptors = this.hdc;

			OIList.Items.Clear();
			OIList.Separators = this.separators;
			OIList.HighlightDescriptors = this.hdc;

			RList.Items.Clear();
			RList.Separators = this.separators;
			RList.HighlightDescriptors = this.hdc;

			tbProse.CaseSensitive = true;
			tbProse.Separators = this.separators;
			tbProse.HighlightDescriptors = this.hdc;

			this.lockUpdate = true;
			LockWindowUpdate(tvModelBrowser.Handle);
			this.Cursor = Cursors.WaitCursor;
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(modelFilePath);
				model = new Model();
				UseCaseMakerLibrary.XmlSerializer.XmlDeserialize(
					doc,
					"UCM-Document",
					"", // "http://www.magosoftware.it/" + this.GetType().Namespace
					Application.ProductVersion,
					model);
				BuildView(model);
			}
			catch(XmlException e)
			{
				MessageBox.Show(this,e.Message,Application.ProductName);
				this.NewModel();
			}
			catch(XmlSerializerException e)
			{
				MessageBox.Show(this,e.Message,Application.ProductName);
				this.NewModel();
			}
			finally
			{
				this.lockUpdate = false;
				LockWindowUpdate(new IntPtr(0));
				tvModelBrowser.Invalidate();
			}
			UpdateView();
			this.Cursor = Cursors.Default;

			// Cambia il titolo della finestra
			int sub = this.Text.IndexOf("-");
			if(sub != -1)
			{
				this.Text = this.Text.Substring(0,sub - 1) + " - " + this.modelFileName;
			}
			else
			{
				this.Text += " - " + this.modelFileName;
			}

			this.SetModified(false);
		}

		private void SaveModel(bool showSaveAsDialog)
		{
			if(Directory.Exists(this.appSettings.ModelFilePath))
			{
				saveModelFileDialog.InitialDirectory = this.appSettings.ModelFilePath;
			}

			if(showSaveAsDialog)
			{
				// [Save Model As]
				saveModelFileDialog.Title = this.localizer.GetValue("UserMessages","saveModelAs");
				saveModelFileDialog.FileName = ((this.modelFileName == string.Empty)
					? model.Name : Path.GetFileNameWithoutExtension(this.modelFileName));
				if(saveModelFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					this.appSettings.AddToRecentFileList(saveModelFileDialog.FileName);
					this.UpdateRecentFileList();

					XmlDocument doc = UseCaseMakerLibrary.XmlSerializer.XmlSerialize(
						"UCM-Document",
						"", /* "http://www.magosoftware.it/" + this.GetType().Namespace */
						this.GetType().Assembly.GetName().Version.ToString(2),
						model,
						true);
					doc.Save(saveModelFileDialog.FileName);
					this.appSettings.ModelFilePath = Path.GetDirectoryName(saveModelFileDialog.FileName);
					this.modelFilePath = Path.GetDirectoryName(saveModelFileDialog.FileName);
					this.modelFileName = Path.GetFileName(saveModelFileDialog.FileName);
					this.SetModified(false);
				}
			}
			else
			{
				XmlDocument doc = UseCaseMakerLibrary.XmlSerializer.XmlSerialize(
					"UCM-Document",
					"", /* "http://www.magosoftware.it/" + this.GetType().Namespace */
					this.GetType().Assembly.GetName().Version.ToString(2),
					model,
					true);
				doc.Save(Path.Combine(this.modelFilePath,this.modelFileName));
				this.SetModified(false);
			}

			// Cambia il titolo della finestra
			int sub = this.Text.IndexOf("-");
			if(sub != -1)
			{
				this.Text = this.Text.Substring(0,sub - 1) + " - " + this.modelFileName;
			}
			else
			{
				this.Text += " - " + this.modelFileName;
			}
		}

		private void UpdateRecentFileList()
		{
			mnuFileRecent1.Visible = false;
			mnuFileRecent2.Visible = false;
			mnuFileRecent3.Visible = false;
			mnuFileRecent4.Visible = false;
			mnuFileSep3.Visible = false;

			if(this.appSettings.RecentFile1 != null)
			{
				mnuFileRecent1.Text = this.appSettings.RecentFile1;
				mnuFileRecent1.Visible = true;
				mnuFileSep3.Visible = true;
			}
			if(this.appSettings.RecentFile2 != null)
			{
				mnuFileRecent2.Text = this.appSettings.RecentFile2;
				mnuFileRecent2.Visible = true;
				mnuFileSep3.Visible = true;
			}
			if(this.appSettings.RecentFile3 != null)
			{
				mnuFileRecent3.Text = this.appSettings.RecentFile3;
				mnuFileRecent3.Visible = true;
				mnuFileSep3.Visible = true;
			}
			if(this.appSettings.RecentFile4 != null)
			{
				mnuFileRecent4.Text = this.appSettings.RecentFile4;
				mnuFileRecent4.Visible = true;
				mnuFileSep3.Visible = true;
			}
		}

		private void OnAfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			this.UpdateView();
		}

		private void mnuFileOpen_Click(object sender, System.EventArgs e)
		{
			this.OpenModel();
		}

		private void RemoveHighlightDescriptor(string hdName, string hdUserID)
		{
			for(int i = this.hdc.Count - 1; i >= 0; i--)
			{
				HighlightDescriptor hd = this.hdc[i];
				if(hd.Token == hdName || hd.Token == hdUserID)
				{
					this.hdc.RemoveAt(i);
				}
			}
		}

		private string GetElementInfo(object element)
		{
			string elementInfo = string.Empty;

			if(element.GetType() == typeof(Model))
			{
				Model root = (Model)element;

				elementInfo = this.localizer.GetValue("Globals","Model") + ": " + root.Name + "\r\n" +
					this.localizer.GetValue("Globals","Identifier") + ": " + root.Path + "\r\n" +
					this.localizer.GetValue("Globals","Owner") + ":\r\n" +
					this.localizer.GetValue("Globals","Description") + ": " + root.Attributes.Description;
			}
			if(element.GetType() == typeof(Package))
			{
				Package package = (Package)element;

				elementInfo = this.localizer.GetValue("Globals","Package") + ": " + package.Name + "\r\n" +
					this.localizer.GetValue("Globals","Identifier") + ": " + package.Path + "\r\n" +
					this.localizer.GetValue("Globals","Owner") + ": " + package.Owner.Name + "\r\n" +
					this.localizer.GetValue("Globals","Description") + ": " + package.Attributes.Description;
			}
			if(element.GetType() == typeof(Actor))
			{
				Actor actor = (Actor)element;

				elementInfo = this.localizer.GetValue("Globals","Actor") + ": " + actor.Name + "\r\n" +
					this.localizer.GetValue("Globals","Identifier") + ": " + actor.Path + "\r\n" +
					this.localizer.GetValue("Globals","Owner") + ": " + actor.Owner.Name + "\r\n" +
					this.localizer.GetValue("Globals","Description") + ": " + actor.Attributes.Description;
			}
			if(element.GetType() == typeof(UseCase))
			{
				UseCase useCase = (UseCase)element;

				elementInfo = this.localizer.GetValue("Globals","UseCase") + ": " + useCase.Name + "\r\n" +
					this.localizer.GetValue("Globals","Identifier") + ": " + useCase.Path + "\r\n" +
					this.localizer.GetValue("Globals","Owner") + ": " + useCase.Owner.Name + "\r\n" +
					this.localizer.GetValue("Globals","Description") + ": " + useCase.Attributes.Description;
			}
			if(element.GetType() == typeof(GlossaryItem))
			{
				GlossaryItem gi = (GlossaryItem)element;

				elementInfo = this.localizer.GetValue("Globals","GlossaryItem") + ": " + gi.Name + "\r\n" +
					this.localizer.GetValue("Globals","Description") + ": " + gi.Description;
			}

			return elementInfo;
		}

		private void AddPackage()
		{
			frmCreator frm = new frmCreator(this.localizer,this.localizer.GetValue("Globals","Package"));
			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				Package owner = (Package)this.currentElement;
				Package package = owner.NewPackage(frm.tbName.Text,defaultPPrefix,owner.Packages.GetNextFreeID());
				this.AddElement(package,owner,true);
			}
			frm.Dispose();
		}

		private void AddActor()
		{
			frmCreator frm = new frmCreator(this.localizer,this.localizer.GetValue("Globals","Actor"));
			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				Package owner = null;
				IdentificableObjectCollection coll = (this.currentElement as IdentificableObjectCollection);
				if(coll == null)
				{
					owner = (Package)this.currentElement;
				}
				else
				{
					owner = coll.Owner;
				}
				Actor actor = owner.NewActor(frm.tbName.Text,defaultAPrefix,owner.Actors.GetNextFreeID());
				this.AddElement(actor,owner,true);
			}
			frm.Dispose();
		}

		private void AddUseCase()
		{
			frmCreator frm = new frmCreator(this.localizer,this.localizer.GetValue("Globals","UseCase"));
			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				Package owner = null;
				IdentificableObjectCollection coll = (this.currentElement as IdentificableObjectCollection);
				if(coll == null)
				{
					owner = (Package)this.currentElement;
				}
				else
				{
					owner = coll.Owner;
				}
				UseCase useCase = owner.NewUseCase(frm.tbName.Text,defaultUCPrefix,owner.UseCases.GetNextFreeID());
				this.AddElement(useCase,owner,true);
			}
			frm.Dispose();
		}

		private void ElementNameChange(Label oldNameLabel)
		{
			IdentificableObject ia = (IdentificableObject)this.currentElement;
			oldNameLabel.Text = this.ElementNameChange(ia);
			tvModelBrowser.SelectedNode.Text = ia.Name + " (" + ia.ElementID + ")";
		}

		private string ElementNameChange(IdentificableObject ia)
		{
			string type = string.Empty;

			if(ia.GetType() == typeof(Model))
			{
				type = this.localizer.GetValue("Globals","Model");
			}
			if(ia.GetType() == typeof(Package))
			{
				type = this.localizer.GetValue("Globals","Package");
			}
			if(ia.GetType() == typeof(Actor))
			{
				type = this.localizer.GetValue("Globals","Actor");
			}
			if(ia.GetType() == typeof(UseCase))
			{
				type = this.localizer.GetValue("Globals","UseCase");
			}
			if(ia.GetType() == typeof(GlossaryItem))
			{
				type = this.localizer.GetValue("Globals","GlossaryItem");
			}

			frmNameChanger frm = new frmNameChanger(this.localizer,type);
			frm.lblOldName.Text = ia.Name;

			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				string token = "\"" + frm.lblOldName.Text + "\"";
				token = token.Replace(" ","\t");
				token = token.Replace(".","\v");

				RemoveHighlightDescriptor(token,string.Empty);

				string sub = "\"" + frm.tbNewName.Text + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				HighlightDescriptor hd = new 
					HighlightDescriptor(sub,Color.Red,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);

				if(!frm.cbNoReplace.Checked)
				{
					model.ReplaceElementName(
						frm.lblOldName.Text,
						"\"",
						"\"",
						frm.tbNewName.Text,
						"\"",
						"\"");
					if(this.currentElement.GetType() == typeof(UseCase))
					{
						this.UpdateView();
						tabUseCase.SelectedTab = this.pgUCGeneral;
					}
				}

				ia.Name = frm.tbNewName.Text;
				this.SetModified(true);
			}
			frm.Dispose();
			return ia.Name;
		}

		private void ElementDelete()
		{
			string type = string.Empty;

			if(this.currentElement.GetType() == typeof(Model))
			{
				type = this.localizer.GetValue("Globals","Model");
			}
			if(this.currentElement.GetType() == typeof(Package))
			{
				type = this.localizer.GetValue("Globals","Package");
			}
			if(this.currentElement.GetType() == typeof(Actor))
			{
				type = this.localizer.GetValue("Globals","Actor");
			}
			if(this.currentElement.GetType() == typeof(UseCase))
			{
				type = this.localizer.GetValue("Globals","UseCase");
			}

			frmDeleter frm = new frmDeleter(this.localizer,type);

			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				RecursiveRemoveHighligthDescriptor(this.currentElement);
				Package owner = null;
				if(this.currentElement.GetType() == typeof(Package))
				{
					owner = ((Package)this.currentElement).Owner;
					owner.RemovePackage(
						(Package)this.currentElement,
						"\"",
						"\"",
						"{",
						"}",
						frm.cbDontMark.Checked);
				}
				if(this.currentElement.GetType() == typeof(Actor))
				{
					owner = ((Actor)this.currentElement).Owner;
					owner.RemoveActor(
						(Actor)this.currentElement,
						"\"",
						"\"",
						"{",
						"}",
						frm.cbDontMark.Checked);
				}
				if(this.currentElement.GetType() == typeof(UseCase))
				{
					owner = ((UseCase)this.currentElement).Owner;
					owner.RemoveUseCase(
						(UseCase)this.currentElement,
						"\"",
						"\"",
						"{",
						"}",
						frm.cbDontMark.Checked);
				}
				tvModelBrowser.SelectedNode.Remove();
			}

			frm.Dispose();
			this.SetModified(true);
		}

		private void RecursiveRemoveHighligthDescriptor(object element)
		{
			if(element.GetType() == typeof(Package))
			{
				Package package = (Package)element;

				foreach(Actor actor in package.Actors)
				{
					RecursiveRemoveHighligthDescriptor(actor);
				}
				foreach(UseCase useCase in package.UseCases)
				{
					RecursiveRemoveHighligthDescriptor(useCase);
				}
				foreach(Package subPackage in package.Packages)
				{
					RecursiveRemoveHighligthDescriptor(subPackage);
				}
				string token = "\"" + package.Name + "\"";
				token = token.Replace(" ","\t");
				token = token.Replace(".","\v");
				string userID = "\"" + package.Path + "\"";
				userID = userID.Replace(" ","\t");
				userID = userID.Replace(".","\v");
				RemoveHighlightDescriptor(token,userID);
			}
			if(element.GetType() == typeof(Actor))
			{
				Actor actor = (Actor)element;
				string token = "\"" + actor.Name + "\"";
				token = token.Replace(" ","\t");
				token = token.Replace(".","\v");
				string userID = "\"" + actor.Path + "\"";
				userID = userID.Replace(" ","\t");
				userID = userID.Replace(".","\v");
				RemoveHighlightDescriptor(token,userID);
			}
			if(element.GetType() == typeof(UseCase))
			{
				UseCase useCase = (UseCase)element;
				string token = "\"" + useCase.Name + "\"";
				token = token.Replace(" ","\t");
				token = token.Replace(".","\v");
				string userID = "\"" + useCase.Path + "\"";
				userID = userID.Replace(" ","\t");
				userID = userID.Replace(".","\v");
				RemoveHighlightDescriptor(token,userID);
			}
		}

		private void mnuFileNew_Click(object sender, System.EventArgs e)
		{
			this.NewModel();
		}

		private void mnuFileSave_Click(object sender, System.EventArgs e)
		{
			if(this.modelFileName == string.Empty)
			{
				this.SaveModel(true);
			}
			else
			{
				this.SaveModel(false);
			}
		}

		private void tbDescription_TextChanged(object sender, System.EventArgs e)
		{
			CommonAttributes attributes = null;

			if(this.currentElement.GetType() == typeof(Model))
			{
				attributes = ((Model)this.currentElement).Attributes;
			}
			if(this.currentElement.GetType() == typeof(Package))
			{
				attributes = ((Package)this.currentElement).Attributes;
			}
			if(this.currentElement.GetType() == typeof(Actor))
			{
				attributes = ((Actor)this.currentElement).Attributes;
			}
			if(this.currentElement.GetType() == typeof(UseCase))
			{
				attributes = ((UseCase)this.currentElement).Attributes;
			}

			attributes.Description = tbDescription.Text;
			this.SetModified(true);
		}

		private void tbNotes_TextChanged(object sender, System.EventArgs e)
		{
			CommonAttributes attributes = null;

			if(this.currentElement.GetType() == typeof(Model))
			{
				attributes = ((Model)this.currentElement).Attributes;
			}
			if(this.currentElement.GetType() == typeof(Package))
			{
				attributes = ((Package)this.currentElement).Attributes;
			}
			if(this.currentElement.GetType() == typeof(Actor))
			{
				attributes = ((Actor)this.currentElement).Attributes;
			}
			if(this.currentElement.GetType() == typeof(UseCase))
			{
				attributes = ((UseCase)this.currentElement).Attributes;
			}

			attributes.Notes = tbNotes.Text;
			this.SetModified(true);
		}

		private void btnAddStep_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			int currentSelectedIndex = UCList.SelectedIndex;
			IndexedListItem ili = null;

			if(currentSelectedIndex != -1)
			{
				ili = UCList.Items[UCList.SelectedIndex];
				Step previousStep = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
				currentSelectedIndex = useCase.AddStep(previousStep,Step.StepType.Default);
			}
			else
			{
				currentSelectedIndex = useCase.AddStep(null,Step.StepType.Default);
			}

			UCList.DataBind();

			UCList.SelectedIndex = currentSelectedIndex;
			this.SetModified(true);
		}

		private void btnAddAltStep_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			int currentSelectedIndex = UCList.SelectedIndex;
			IndexedListItem ili = null;

			ili = UCList.Items[UCList.SelectedIndex];
			Step previousStep = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
			currentSelectedIndex = useCase.AddStep(previousStep,Step.StepType.Alternative);

			UCList.DataBind();

			UCList.SelectedIndex = currentSelectedIndex;
			this.SetModified(true);
		}

		private void btnInsertStep_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			int currentSelectedIndex = UCList.SelectedIndex;
			IndexedListItem ili = null;

			ili = UCList.Items[UCList.SelectedIndex];
			Step previousStep = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
			currentSelectedIndex = useCase.InsertStep(previousStep,Step.StepType.Default);

			UCList.DataBind();

			UCList.SelectedIndex = currentSelectedIndex;
			this.SetModified(true);
		}


		private void btnInsertAltStep_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			int currentSelectedIndex = UCList.SelectedIndex;
			IndexedListItem ili = null;

			ili = UCList.Items[UCList.SelectedIndex];
			Step previousStep = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
			currentSelectedIndex = useCase.InsertStep(previousStep,Step.StepType.Alternative);

			UCList.DataBind();

			UCList.SelectedIndex = currentSelectedIndex;
			this.SetModified(true);
		}

		private void btnRemoveStep_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			int currentSelectedIndex = UCList.SelectedIndex;

			IndexedListItem ili = UCList.Items[currentSelectedIndex];
			Step step = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
			useCase.RemoveStep(step);

			UCList.DataBind();

			if(currentSelectedIndex < UCList.Items.Count)
			{
				UCList.SelectedIndex = currentSelectedIndex;
			}
			else
			{
				if(UCList.Items.Count > 0)
				{
					UCList.SelectedIndex = UCList.Items.Count - 1;
				}
				else
				{
					btnAddStep.Enabled = true;
					btnAddAltStep.Enabled = false;
					btnInsertStep.Enabled = false;
					btnInsertAltStep.Enabled = false;
					btnRemoveStep.Enabled = false;
				}
			}
			this.SetModified(true);
		}

		private void UCList_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
		{
			UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

			object element = null;

			element = model.FindElementByName(e.Token);
			if(element == null)
			{
				element = model.FindElementByPath(e.Token);
			}
			if(element != null)
			{
				rtb.ToolTip.SetToolTip(rtb,this.GetElementInfo(element));
			}
		}

		private void UCList_ItemTextChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			IndexedListItem item = (IndexedListItem)sender;
			Step step = (Step)useCase.FindStepByUniqueID((String)item.Tag);
			if(step != null)
			{
				step.Description = item.Text;
			}
			this.SetModified(true);
		}

		private void UCList_SelectedChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			Step step = (Step)useCase.FindStepByUniqueID((String)((IndexedListItem)sender).Tag);
			switch(step.Type)
			{
				case Step.StepType.Default:
					btnAddStep.Enabled = true;
					btnInsertStep.Enabled = true;
					btnAddAltStep.Enabled = true;
					btnInsertAltStep.Enabled = false;
					btnRemoveStep.Enabled = true;
					break;
				case Step.StepType.Alternative:
					btnAddStep.Enabled = true;
					btnInsertStep.Enabled = false;
					btnAddAltStep.Enabled = true;
					btnInsertAltStep.Enabled = true;
					btnRemoveStep.Enabled = true;
					break;
				case Step.StepType.AlternativeChild:
					btnAddStep.Enabled = true;
					btnInsertStep.Enabled = true;
					btnAddAltStep.Enabled = true;
					btnInsertAltStep.Enabled = false;
					btnRemoveStep.Enabled = true;
					break;
			}
		}


		private void tbProse_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
		{
			UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

			object element = null;

			element = model.FindElementByName(e.Token);
			if(element == null)
			{
				element = model.FindElementByPath(e.Token);
			}
			if(element != null)
			{
				rtb.ToolTip.SetToolTip(rtb,this.GetElementInfo(element));
			}
		}

		private void tbProse_ItemTextChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			UseCaseMakerControls.LinkEnabledRTB item = (UseCaseMakerControls.LinkEnabledRTB)sender;
			useCase.Prose = item.Text;
			this.SetModified(true);
		}

		private void btnAddOpenIssue_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			int currentSelectedIndex = useCase.AddOpenIssue();

			OIList.DataBind();

			OIList.SelectedIndex = currentSelectedIndex;
			this.SetModified(true);
		}

		private void btnRemoveOpenIssue_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			int currentSelectedIndex = OIList.SelectedIndex;

			IndexedListItem ili = OIList.Items[currentSelectedIndex];
			OpenIssue openIssue = (OpenIssue)useCase.FindOpenIssueByUniqueID((String)ili.Tag);
			useCase.RemoveOpenIssue(openIssue);

			OIList.DataBind();

			if(currentSelectedIndex < OIList.Items.Count)
			{
				OIList.SelectedIndex = currentSelectedIndex;
			}
			else
			{
				if(OIList.Items.Count > 0)
				{
					OIList.SelectedIndex = OIList.Items.Count - 1;
				}
				else
				{
					btnAddOpenIssue.Enabled = true;
					btnRemoveOpenIssue.Enabled = false;
				}
			}
			this.SetModified(true);
		}

		private void OIList_ItemTextChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			IndexedListItem item = (IndexedListItem)sender;
			OpenIssue openIssue = (OpenIssue)useCase.FindOpenIssueByUniqueID((String)item.Tag);
			if(openIssue != null)
			{
				openIssue.Description = item.Text;
			}
			this.SetModified(true);
		}

		private void OIList_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
		{
			UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

			object element = null;

			element = model.FindElementByName(e.Token);
			if(element == null)
			{
				element = model.FindElementByPath(e.Token);
			}
			if(element != null)
			{
				rtb.ToolTip.SetToolTip(rtb,this.GetElementInfo(element));
			}	
		}

		private void OIList_SelectedChanged(object sender, System.EventArgs e)
		{
			btnAddOpenIssue.Enabled = true;
			btnRemoveOpenIssue.Enabled = true;
		}

		private void btnAddRequirement_Click(object sender, System.EventArgs e)
		{
			Package package = (Package)this.currentElement;
			int currentSelectedIndex = package.AddRequrement();

			RList.DataBind();

			RList.SelectedIndex = currentSelectedIndex;
			this.SetModified(true);
		}

		private void btnRemoveRequirement_Click(object sender, System.EventArgs e)
		{
			Package package = (Package)this.currentElement;
			int currentSelectedIndex = RList.SelectedIndex;

			IndexedListItem ili = RList.Items[currentSelectedIndex];
			Requirement requirement = (Requirement)package.FindRequirementByUniqueID((String)ili.Tag);
			package.RemoveRequirement(requirement);

			RList.DataBind();

			if(currentSelectedIndex < RList.Items.Count)
			{
				RList.SelectedIndex = currentSelectedIndex;
			}
			else
			{
				if(RList.Items.Count > 0)
				{
					RList.SelectedIndex = RList.Items.Count - 1;
				}
				else
				{
					btnAddRequirement.Enabled = true;
					btnRemoveRequirement.Enabled = false;
				}
			}
			this.SetModified(true);
		}

		private void RList_ItemTextChanged(object sender, System.EventArgs e)
		{
			Package package = (Package)this.currentElement;
			IndexedListItem item = (IndexedListItem)sender;
			Requirement requirement = (Requirement)package.FindRequirementByUniqueID((String)item.Tag);
			if(requirement != null)
			{
				requirement.Description = item.Text;
			}
			this.SetModified(true);
		}

		private void RList_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
		{
			UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

			object element = null;

			element = model.FindElementByName(e.Token);
			if(element == null)
			{
				element = model.FindElementByPath(e.Token);
			}
			if(element != null)
			{
				rtb.ToolTip.SetToolTip(rtb,this.GetElementInfo(element));
			}			
		}

		private void RList_SelectedChange(object sender, System.EventArgs e)
		{
			btnAddRequirement.Enabled = true;
			btnRemoveRequirement.Enabled = true;		
		}

		private void btnANameChange_Click(object sender, System.EventArgs e)
		{
			ElementNameChange(lblAName);
		}

		private void btnUCNameChange_Click(object sender, System.EventArgs e)
		{
			ElementNameChange(lblUCName);
		}

		private void btnPNameChange_Click(object sender, System.EventArgs e)
		{
			ElementNameChange(lblPName);
		}

		private void btnAddGlossaryItem_Click(object sender, System.EventArgs e)
		{
			frmCreator frm = new frmCreator(this.localizer,this.localizer.GetValue("Globals","GlossaryItem"));
			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				Model model = (Model)this.currentElement;
				GlossaryItem gi = 
					model.NewGlossaryItem(frm.tbName.Text,defaultGPrefix,model.Glossary.GetNextFreeID());
				model.Glossary.Add(gi);

				IndexedListItem ili = new IndexedListItem();
				ili.Index = frm.tbName.Text;
				ili.Text = string.Empty;
				ili.Tag = gi.UniqueID;
				GList.Items.Add(ili);
				GList.SelectedIndex = GList.Items.Count - 1;

				string sub = "\"" + gi.Name + "\"";
				sub = sub.Replace(" ","\t");
				sub = sub.Replace(".","\v");
				HighlightDescriptor hd = new 
					HighlightDescriptor(sub,Color.Green,null,DescriptorType.Word,DescriptorRecognition.WholeWord,true);
				this.hdc.Add(hd);
				this.SetModified(true);
			}
			frm.Dispose();
		}

		private void btnChangeGlossaryItem_Click(object sender, System.EventArgs e)
		{
			Model model = (Model)this.currentElement;

			IndexedListItem ili = GList.Items[GList.SelectedIndex];
			ili.Index = this.ElementNameChange((IdentificableObject)model.GetGlossaryItem((String)ili.Tag));
		}

		private void btnRemoveGlossaryItem_Click(object sender, System.EventArgs e)
		{
			Model model = (Model)this.currentElement;
			int currentSelectedIndex = GList.SelectedIndex;

			IndexedListItem ili = GList.Items[currentSelectedIndex];
			GlossaryItem gi = model.GetGlossaryItem((String)ili.Tag);

			frmDeleter frm = new frmDeleter(this.localizer,this.localizer.GetValue("Globals","GlossaryItem"));

			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				model.RemoveGlossaryItem(
					gi,
					"\"",
					"\"",
					"{",
					"}",
					frm.cbDontMark.Checked);

				string token = "\"" + gi.Name + "\"";
				token = token.Replace(" ","\t");
				token = token.Replace(".","\v");
				RemoveHighlightDescriptor(token,null);

				GList.DataBind();

				if(currentSelectedIndex < GList.Items.Count)
				{
					GList.SelectedIndex = currentSelectedIndex;
				}
				else
				{
					if(GList.Items.Count > 0)
					{
						GList.SelectedIndex = GList.Items.Count - 1;
					}
					else
					{
						btnAddGlossaryItem.Enabled = true;
						btnRemoveGlossaryItem.Enabled = false;
						btnChangeGlossaryItem.Enabled = false;
					}
				}
				this.SetModified(true);
			}
			frm.Dispose();
		}

		private void GList_ItemTextChanged(object sender, System.EventArgs e)
		{
			Model model = (Model)this.currentElement;
			IndexedListItem item = (IndexedListItem)sender;
			GlossaryItem gi = (GlossaryItem)model.GetGlossaryItem((String)item.Tag);
			if(gi != null)
			{
				gi.Description = item.Text;
			}
			this.SetModified(true);		
		}

		private void GList_SelectedChanged(object sender, System.EventArgs e)
		{
			btnAddGlossaryItem.Enabled = true;
			btnRemoveGlossaryItem.Enabled = true;
			btnChangeGlossaryItem.Enabled = true;
		}

		private void GList_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
		{
			UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

			object element = null;

			element = model.FindElementByName(e.Token);
			if(element == null)
			{
				element = model.FindElementByPath(e.Token);
			}
			if(element != null)
			{
				rtb.ToolTip.SetToolTip(rtb,this.GetElementInfo(element));
			}		
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(toolBar.Buttons.IndexOf(e.Button))
			{
					// New model
				case 0:
					NewModel();
					break;
					// Open model
				case 1:
					OpenModel();
					break;
					// Save model
				case 2:
					if(this.modelFileName == string.Empty)
					{
						SaveModel(true);
					}
					else
					{
						SaveModel(false);
					}
					break;
					// Add package
				case 4:
					AddPackage();
					break;
					// Remove package
				case 5:
					ElementDelete();
					break;
					// Add actor
				case 7:
					AddActor();
					break;
					// Remove actor
				case 8:
					ElementDelete();
					break;
					// Add use case
				case 10:
					AddUseCase();
					break;
					// Remove use case
				case 11:
					ElementDelete();
					break;
			}
		}

		private void tbPreconditions_TextChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			useCase.Preconditions = tbPreconditions.Text;
			this.SetModified(true);
		}

		private void tbPostconditions_TextChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			useCase.Postconditions = tbPostconditions.Text;
			this.SetModified(true);
		}

		private void btnAddActor_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;

			String [] actors = model.GetActorNames();

			frmActorChooser frm = new frmActorChooser(actors,this.localizer);

			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				String actorName = (String)frm.lbActors.SelectedItem;
				Actor actor = (Actor)model.FindElementByName(actorName);
				if(useCase.ActiveActors.FindByUniqueID(actor.UniqueID) != null)
				{
					// [Actor already present!]
					MessageBox.Show(this,this.localizer.GetValue("UserMessages","actorAlreadyPresent"));
					return;
				}

				useCase.AddActiveActor(actor);
				this.UpdateView();
				tabUseCase.SelectedTab = pgUCGeneral;
				this.SetModified(true);
			}
		}

		private void btnRemoveActor_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			Actor actor = (Actor)model.FindElementByName(lvActors.SelectedItems[0].Text);
			useCase.RemoveActiveActor(actor);
			this.UpdateView();
			tabUseCase.SelectedTab = pgUCGeneral;
			this.SetModified(true);
		}

		private void btnSetPrimaryActor_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;

			foreach(ActiveActor aactor in useCase.ActiveActors)
			{
				aactor.IsPrimary = false;
			}

			ActiveActor selectedAActor = (ActiveActor)useCase.ActiveActors[lvActors.SelectedIndices[0]];
			selectedAActor.IsPrimary = true;
			this.UpdateView();
			tabUseCase.SelectedTab = pgUCGeneral;
			this.SetModified(true);
		}

		private void tbPriority_TextChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			if(tbPriority.Text != string.Empty)
			{
				useCase.Priority = Convert.ToInt32(tbPriority.Text);
			}
			this.SetModified(true);
		}

		private void tbPriority_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if((e.KeyChar >= '0' && e.KeyChar <= '9')
				|| e.KeyChar == 0x1E || e.KeyChar == 0x08 || e.KeyChar == 0x1B)
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
		}

		private void tbPriority_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;

			if(tbPriority.Text == string.Empty)
			{
				tbPriority.Text = useCase.Priority.ToString();
			}
			if(Convert.ToInt32(tbPriority.Text) == 0)
			{
				tbPriority.Text = Convert.ToString(1);
			}
			tbPriority.Text = useCase.Priority.ToString();
		}

		private void tbAssignedTo_TextChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			useCase.AssignedTo = tbAssignedTo.Text;
			this.SetModified(true);
		}

		private void tbRelease_TextChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			useCase.Release = tbRelease.Text;
			this.SetModified(true);
		}

		private void cmbLevel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			Boolean found = false;

			Array _values = Enum.GetValues(typeof(UseCase.LevelValue));
			foreach(Int32 _value in _values)
			{
				if(_value == cmbLevel.SelectedIndex)
				{
					useCase.Level = (UseCase.LevelValue)cmbLevel.SelectedIndex;
					found = true;
				}
			}

			if(!found)
			{
				cmbLevel.SelectedIndex = (int)useCase.Level;
			}
			this.SetModified(true);
		}

		private void cmbComplexity_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			Boolean found = false;

			Array _values = Enum.GetValues(typeof(UseCase.ComplexityValue));
			foreach(Int32 _value in _values)
			{
				if(_value == cmbComplexity.SelectedIndex)
				{
					useCase.Complexity = (UseCase.ComplexityValue)cmbComplexity.SelectedIndex;
					found = true;
				}
			}

			if(!found)
			{
				cmbComplexity.SelectedIndex = (int)useCase.Complexity;
			}
			this.SetModified(true);
		}

		private void cmbStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			Boolean found = false;

			Array _values = Enum.GetValues(typeof(UseCase.StatusValue));
			foreach(Int32 _value in _values)
			{
				if(_value == cmbStatus.SelectedIndex)
				{
					useCase.Status = (UseCase.StatusValue)cmbStatus.SelectedIndex;
					found = true;
				}
			}

			if(!found)
			{
				cmbStatus.SelectedIndex = (int)useCase.Status;
			}
			this.SetModified(true);
		}

		private void cmbImplementation_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;
			Boolean found = false;

			Array _values = Enum.GetValues(typeof(UseCase.ImplementationValue));
			foreach(Int32 _value in _values)
			{
				if(_value == cmbImplementation.SelectedIndex)
				{
					useCase.Implementation = (UseCase.ImplementationValue)cmbImplementation.SelectedIndex;
					found = true;
				}
			}

			if(!found)
			{
				cmbImplementation.SelectedIndex = (int)useCase.Implementation;
			}
			this.SetModified(true);
		}

		private void lvActors_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(lvActors.SelectedIndices.Count == 0)
			{
				btnRemoveActor.Enabled = false;
				btnSetPrimaryActor.Enabled = false;
			}
			else
			{
				btnRemoveActor.Enabled = true;
				btnSetPrimaryActor.Enabled = true;
			}
		}

		private void lvRelatedDocs_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(lvRelatedDocs.SelectedIndices.Count == 0)
			{
				btnRemoveRelatedDoc.Enabled = false;
				btnOpenRelatedDoc.Enabled = false;
			}
			else
			{
				btnRemoveRelatedDoc.Enabled = true;
				btnOpenRelatedDoc.Enabled = true;
			}		
		}

		private void btnAddRelatedDoc_Click(object sender, System.EventArgs e)
		{
			if(selectDocFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				CommonAttributes attributes = null;
				if(this.currentElement.GetType() == typeof(Model) ||
					this.currentElement.GetType() == typeof(Package))
				{
					attributes = ((Package)this.currentElement).Attributes;
				}
				if(this.currentElement.GetType() == typeof(UseCase))
				{
					attributes = ((UseCase)this.currentElement).Attributes;
				}
				if(this.currentElement.GetType() == typeof(Actor))
				{
					attributes = ((Actor)this.currentElement).Attributes;
				}

				foreach(RelatedDocument rd in attributes.RelatedDocuments)
				{
					if(rd.FileName == selectDocFileDialog.FileName)
					{
						// [File already present!]
						MessageBox.Show(this,this.localizer.GetValue("UserMessages","fileAlreadyPresent"));
						return;
					}
				}
				RelatedDocument newRd = new RelatedDocument();
				newRd.FileName = selectDocFileDialog.FileName;
				attributes.RelatedDocuments.Add(newRd);

				this.UpdateView();
				tabUseCase.SelectedTab = pgAttributes;
				this.SetModified(true);
			}
		}

		private void btnRemoveRelatedDoc_Click(object sender, System.EventArgs e)
		{
			CommonAttributes attributes = null;
			if(this.currentElement.GetType() == typeof(Model) ||
				this.currentElement.GetType() == typeof(Package))
			{
				attributes = ((Package)this.currentElement).Attributes;
			}
			if(this.currentElement.GetType() == typeof(UseCase))
			{
				attributes = ((UseCase)this.currentElement).Attributes;
			}
			if(this.currentElement.GetType() == typeof(Actor))
			{
				attributes = ((Actor)this.currentElement).Attributes;
			}

			for(int index = attributes.RelatedDocuments.Count - 1; index >= 0; index--)
			{
				RelatedDocument rd = (RelatedDocument)attributes.RelatedDocuments[index];
				if(rd.FileName == lvRelatedDocs.SelectedItems[0].Text)
				{
					attributes.RelatedDocuments.RemoveAt(index);
				}
			}

			this.UpdateView();
			tabUseCase.SelectedTab = pgAttributes;
			this.SetModified(true);
		}

		private void lvRelatedDocs_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListViewItem lvi = (ListViewItem)lvRelatedDocs.GetItemAt(e.X,e.Y);
			if(lvi != null)
			{
				lvRelatedDocsTooltip.Active = true;
				lvRelatedDocsTooltip.SetToolTip(lvRelatedDocs,lvi.Text);
			}
			else
			{
				lvRelatedDocsTooltip.Active = false;
			}
		}

		private void lvActors_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListViewItem lvi = (ListViewItem)lvActors.GetItemAt(e.X,e.Y);
			if(lvi != null)
			{
				lvActorsTooltip.Active = true;
				lvActorsTooltip.SetToolTip(lvActors,lvi.Text);
			}
			else
			{
				lvActorsTooltip.Active = false;
			}		
		}

		private void btnOpenRelatedDoc_Click(object sender, System.EventArgs e)
		{
			Process process = new Process();
			process.StartInfo.FileName = lvRelatedDocs.SelectedItems[0].Text;
			try
			{
				process.Start();
			}
			catch(Win32Exception ex)
			{
				// [Cannot open file!]
				MessageBox.Show(this,this.localizer.GetValue("UserMessages","cannotOpenFile") + "\r\n" + 
					ex.Message);
			}
		}

		private void tvModelBrowser_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				TreeNode tv = (TreeNode)tvModelBrowser.GetNodeAt(e.X,e.Y);
				if(tv != null)
				{
					tvModelBrowser.SelectedNode = tv;
					this.UpdateView();
				}
			}
		}

		private void mnuEditAddPackage_Click(object sender, System.EventArgs e)
		{
			this.AddPackage();
		}

		private void mnuEditRemovePackage_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuEditAddActor_Click(object sender, System.EventArgs e)
		{
			this.AddActor();
		}

		private void mnuEditRemoveActor_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuEditAddUseCase_Click(object sender, System.EventArgs e)
		{
			this.AddUseCase();
		}

		private void mnuEditRemoveUseCase_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuFileSaveAs_Click(object sender, System.EventArgs e)
		{
			this.SaveModel(true);
		}

		private void mnuFileExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void mnuCtxMBAddPackage_Click(object sender, System.EventArgs e)
		{
			this.AddPackage();
		}

		private void mnuCtxMBRemovePackage_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuCtxMBAddActor_Click(object sender, System.EventArgs e)
		{
			this.AddActor();
		}

		private void mnuCtxMBRemoveActor_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		private void mnuCtxMBAddUseCase_Click(object sender, System.EventArgs e)
		{
			this.AddUseCase();
		}

		private void mnuCtxMBRemoveUseCase_Click(object sender, System.EventArgs e)
		{
			this.ElementDelete();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			// [Do you really want to exit?]
			if(MessageBox.Show(
				this,
				this.localizer.GetValue("UserMessages","exitConfirm"),
				Application.ProductName,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question) == DialogResult.No)
			{
				e.Cancel = true;
			}
			else
			{
				this.CloseModel();
			}
			this.appSettings.SaveSettings();
			base.OnClosing (e);
		}

		private void btnStatusToHistory_Click(object sender, System.EventArgs e)
		{
			frmHistoryNotes frm = new frmHistoryNotes(this.localizer);

			if(frm.ShowDialog() == DialogResult.OK)
			{
				UseCase useCase = (UseCase)this.currentElement;
				useCase.AddHistoryItem(
					DateTime.Now,
					HistoryItem.HistoryType.Status,
					cmbStatus.SelectedIndex,
					frm.tbNotes.Text.Replace("\r\n"," "));
				this.UpdateView();
				tabUseCase.SelectedTab = pgDetails;
				this.SetModified(true);
			}
			frm.Dispose();
		}

		private void btnImplToHistory_Click(object sender, System.EventArgs e)
		{
			frmHistoryNotes frm = new frmHistoryNotes(this.localizer);

			if(frm.ShowDialog() == DialogResult.OK)
			{
				UseCase useCase = (UseCase)this.currentElement;
				useCase.AddHistoryItem(
					DateTime.Now,
					HistoryItem.HistoryType.Implementation,
					cmbImplementation.SelectedIndex,
					frm.tbNotes.Text.Replace("\r\n"," "));
				this.UpdateView();
				tabUseCase.SelectedTab = pgDetails;
				this.SetModified(true);
			}
			frm.Dispose();		
		}

		private void btnRemoveHistoryItem_Click(object sender, System.EventArgs e)
		{
			UseCase useCase = (UseCase)this.currentElement;

			useCase.RemoveHistoryItem(lvHistory.SelectedIndices[0]);

			this.UpdateView();
			tabUseCase.SelectedTab = pgHistory;
			this.SetModified(true);
		}

		private void lvHistory_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(lvHistory.SelectedIndices.Count == 0)
			{
				btnRemoveHistoryItem.Enabled = false;
			}
			else
			{
				btnRemoveHistoryItem.Enabled = true;
			}		
		}

		private void lvHistory_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
		{
			lvHistory.Columns[3].Width =
				lvHistory.ClientRectangle.Width -
				lvHistory.Columns[0].Width -
				lvHistory.Columns[1].Width -
				lvHistory.Columns[2].Width;		
		}

		private void lvActors_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
		{
			lvActors.Columns[0].Width = lvActors.ClientRectangle.Width - lvActors.Columns[1].Width;
		}

		private void lvRelatedDocs_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
		{
			lvRelatedDocs.Columns[0].Width = lvRelatedDocs.ClientRectangle.Width;		
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			this.appSettings.LoadSettings();

			this.SuspendLayout();
			
			try
			{
				LocalizeControls(this.appSettings.UILanguageFilePath);
			}
			catch(Exception)
			{
				MessageBox.Show(
					this,
					"Cannot load the localization file!",
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			}

			if(this.appSettings.IsMaximized)
			{
				this.WindowState = FormWindowState.Maximized;
			}
			else
			{
				this.Location = new Point(this.appSettings.Left,this.appSettings.Top);
				this.Size = new Size(this.appSettings.Width,this.appSettings.Height);
				this.CenterToScreen();
			}
			this.splLeft.SplitPosition = this.appSettings.SplitPosition;
			this.UpdateRecentFileList();
			
			this.ResumeLayout();

			lblVersion.Text = Application.ProductVersion;
			
			if(this.modelFilePath == string.Empty)
			{
				this.NewModel();
			}
			else
			{
				string sPath = string.Empty;
				if(this.modelFilePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
				{
					sPath = this.modelFilePath + this.modelFileName;
				}
				else
				{
					sPath = this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName;
				}
				this.OpenRecentModel(sPath);
				this.appSettings.AddToRecentFileList(sPath);
				this.UpdateRecentFileList();
			}
		}

		private void frmMain_Resize(object sender, System.EventArgs e)
		{
			this.appSettings.Left = this.Left;
			this.appSettings.Top = this.Top;
			this.appSettings.Width = this.Width;
			this.appSettings.Height = this.Height;
			if(this.WindowState == FormWindowState.Maximized)
			{
				this.appSettings.IsMaximized = true;
			}
			else
			{
				this.appSettings.IsMaximized = false;
			}
		}

		private void frmMain_LocationChanged(object sender, System.EventArgs e)
		{
			this.appSettings.Left = this.Left;
			this.appSettings.Top = this.Top;
		}

		private void mnuToolsHtmlExport_Click(object sender, System.EventArgs e)
		{
			if(this.modelFilePath == string.Empty || this.modified)
			{
				// [Model must be saved before exporting...]
				MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","saveBeforeExport"),
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			if(Directory.Exists(this.appSettings.HtmlFilesPath))
			{
				folderBrowserDialog.SelectedPath = this.appSettings.HtmlFilesPath;
			}
			// [Select destination folder for generated HTML files]
			folderBrowserDialog.Description = this.localizer.GetValue("UserMessages","selectHTMLdestination");
			if(folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				try
				{
					string [] filesToCopy = Directory.GetFiles(this.appSettings.ReportsFilesPath,"*.*");
					foreach(string file in filesToCopy)
					{
						File.Copy(file,folderBrowserDialog.SelectedPath + Path.DirectorySeparatorChar + 
							file.Substring(file.LastIndexOf(Path.DirectorySeparatorChar)+1),true);
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				try
				{
					HTMLConverter hc = new HTMLConverter(
						folderBrowserDialog.SelectedPath,
						folderBrowserDialog.SelectedPath,
						this.localizer);
					this.Cursor = Cursors.WaitCursor;
					hc.BuildNavigator(this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName);
					hc.BuildPages(this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName);
					this.appSettings.HtmlFilesPath = folderBrowserDialog.SelectedPath;
				}
				catch(Exception ex)
				{
					this.Cursor = Cursors.Default;
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				this.Cursor = Cursors.Default;

				try
				{
					string [] filesToDelete = Directory.GetFiles(folderBrowserDialog.SelectedPath,"*.xsl");
					foreach(string file in filesToDelete)
					{
						File.Delete(file);
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				// [Export complete. Do you want to see the HTML files?]
				if(MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","htmlExportCompleted"),
					Application.ProductName,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Information) == DialogResult.Yes)
				{
					Process process = new Process();
					process.StartInfo.FileName = folderBrowserDialog.SelectedPath + Path.DirectorySeparatorChar 
						+ "index.htm";
					try
					{
						process.Start();
					}
					catch(Win32Exception ex)
					{
						// [Cannot open file!]
						MessageBox.Show(this,this.localizer.GetValue("UserMessages","cannotOpenFile") + 
							"\r\n" + ex.Message);
					}
				}
			}
		}

		private void mnuXMIExport_Click(object sender, System.EventArgs e)
		{
			if(this.modelFilePath == string.Empty || this.modified)
			{
				// [Model must be saved before exporting...]
				MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","saveBeforeExport"),
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			SaveFileDialog saveXMIFileDialog = new SaveFileDialog();
			saveXMIFileDialog.Title = this.localizer.GetValue("UserMessages","exportAsXMI");
			saveXMIFileDialog.FileName = ((this.modelFileName == string.Empty)
				? model.Name : Path.GetFileNameWithoutExtension(this.modelFileName));
			saveXMIFileDialog.DefaultExt = "xml";
			saveXMIFileDialog.Filter = "XML Files|*.xml";
			if(saveXMIFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				XMIConverter xmi = new XMIConverter(
					this.appSettings.ReportsFilesPath,
					saveXMIFileDialog.FileName);

				this.Cursor = Cursors.WaitCursor;

				try
				{
					xmi.Transform(this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName);
				}
				catch(Exception ex)
				{
					this.Cursor = Cursors.Default;
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				this.Cursor = Cursors.Default;
				
				// [Export complete.]
				MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","xmiExportCompleted"),
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
			}
		}

		private void mnuToolsPDFExport_Click(object sender, System.EventArgs e)
		{
			if(this.modelFilePath == string.Empty || this.modified)
			{
				// [Model must be saved before exporting...]
				MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","saveBeforeExport"),
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}

			SaveFileDialog savePDFFileDialog = new SaveFileDialog();
			savePDFFileDialog.Title = this.localizer.GetValue("UserMessages","exportAsPDF");
			savePDFFileDialog.FileName = ((this.modelFileName == string.Empty)
				? model.Name : Path.GetFileNameWithoutExtension(this.modelFileName));
			savePDFFileDialog.DefaultExt = "pdf";
			savePDFFileDialog.Filter = "PDF Files|*.pdf";
			if(savePDFFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				PDFConverter pdf = new PDFConverter(
					this.appSettings.ReportsFilesPath,
					savePDFFileDialog.FileName,
					this.localizer);

				this.Cursor = Cursors.WaitCursor;

				try
				{
					pdf.Transform(this.modelFilePath + Path.DirectorySeparatorChar + this.modelFileName);
				}
				catch(Exception ex)
				{
					this.Cursor = Cursors.Default;
					MessageBox.Show(
						this,
						ex.GetType().Name + ":\r\n" + ex.Message,
						Application.ProductName,
						MessageBoxButtons.OK,
						MessageBoxIcon.Stop);
					return;
				}

				this.Cursor = Cursors.Default;

				// [Export complete. Do you want to see the PDF file?]
				if(MessageBox.Show(
					this,
					this.localizer.GetValue("UserMessages","pdfExportCompleted"),
					Application.ProductName,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Information) == DialogResult.Yes)
				{
					Process process = new Process();
					process.StartInfo.FileName = savePDFFileDialog.FileName;
					try
					{
						process.Start();
					}
					catch(Win32Exception ex)
					{
						// [Cannot open file!]
						MessageBox.Show(this,this.localizer.GetValue("UserMessages","cannotOpenFile") + 
							"\r\n" + ex.Message);
					}
				}
			}		
		}

		private void splLeft_SplitterMoving(object sender, System.Windows.Forms.SplitterEventArgs e)
		{
			if(tabUseCase.Width < 494)
			{
				tabUseCase.Width = 494;
				splLeft.SplitPosition = tabUseCase.Left - splLeft.Width;
			}
			this.appSettings.SplitPosition = e.SplitX;
		}

		private void mnuFileRecent1_Click(object sender, System.EventArgs e)
		{
			this.OpenRecentModel(mnuFileRecent1.Text);
		}

		private void mnuFileRecent2_Click(object sender, System.EventArgs e)
		{
			this.OpenRecentModel(mnuFileRecent2.Text);
		}

		private void mnuFileRecent3_Click(object sender, System.EventArgs e)
		{
			this.OpenRecentModel(mnuFileRecent3.Text);
		}

		private void mnuFileRecent4_Click(object sender, System.EventArgs e)
		{
			this.OpenRecentModel(mnuFileRecent4.Text);
		}

		private void mnuToolsOptions_Click(object sender, System.EventArgs e)
		{
			frmOptions frm = new frmOptions(this.appSettings,this.localizer);
			if(frm.ShowDialog(this) == DialogResult.OK)
			{
				if(frm.SelectedLanguage != string.Empty
					&& frm.SelectedLanguage != this.appSettings.UILanguage)
				{
					this.appSettings.UILanguage = frm.SelectedLanguage;
					this.appSettings.SaveSettings();
					this.LocalizeControls(this.appSettings.UILanguageFilePath);
					TabPage selectedTab = tabUseCase.SelectedTab;
					this.UpdateView();
					tabUseCase.SelectedTab = selectedTab;
				}
			}
		}

		private void mnuHelpAbout_Click(object sender, System.EventArgs e)
		{
			frmAbout frm = new frmAbout();
			frm.ShowDialog(this);
		}

		private void EnableElementTokenContextMenu(LinkEnabledRTB parent, Point location)
		{
			parent.ToolTip.Active = false;
			elementTokenCtxMenu.Show(parent,location);
			parent.ToolTip.Active = true;
		}

		private void mnuCtxETGoToDefinition_Click(object sender, System.EventArgs e)
		{
			ContextMenu ctxMenu = (ContextMenu)((MenuItem)sender).Parent;
			LinkEnabledRTB rtb = (LinkEnabledRTB)ctxMenu.SourceControl;
			string token = rtb.LastTokenClicked;
			IdentificableObject io = (IdentificableObject)model.FindElementByName(token);
			if(io is GlossaryItem)
			{
				tvModelBrowser.SelectedNode = tvModelBrowser.Nodes[0];
				tvModelBrowser.Nodes[0].EnsureVisible();
				tabUseCase.SelectedTab = pgGlossary;
				foreach(IndexedListItem ili in GList.Items)
				{
					if(token ==  ili.Index)
					{
						ili.Selected = true;
					}
				}
			}
			else
			{
				TreeNode node = this.FindNode(null,io.UniqueID);
				node.EnsureVisible();
				tvModelBrowser.SelectedNode = node;
			}
		}

		private void OIList_ClickOnToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				EnableElementTokenContextMenu(e.Item,new Point(e.X,e.Y));
			}		
		}

		private void UCList_ClickOnToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				EnableElementTokenContextMenu(e.Item,new Point(e.X,e.Y));
			}		
		}

		private void tbProse_ClickOnToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				EnableElementTokenContextMenu(e.Item,new Point(e.X,e.Y));
			}		
		}

		private void RList_ClickOnToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				EnableElementTokenContextMenu(e.Item,new Point(e.X,e.Y));
			}		
		}
	}
}
