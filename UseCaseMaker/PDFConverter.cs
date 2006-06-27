using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Net;
using ApacheFop;

namespace UseCaseMaker
{
	/**
	 * @brief Descrizione di riepilogo per PDFConverter.
	 */
	public class PDFConverter
	{
		#region Enumerators and Constants
		// Public
		// Private
		// Protected
		#endregion

		#region Class Members
		// Public
		// Private
		private string stylesheetFilesPath = string.Empty;
		private string pdfFilesPath = string.Empty;
		private Localizer localizer = null;
		// Protected
		#endregion

		#region Constructors
		/**
		 * @brief Costruttore di default per XMIConverter
		 */
		public PDFConverter(
			string stylesheetFilesPath,
			string pdfFilesPath,
			Localizer localizer)
		{
			this.stylesheetFilesPath = stylesheetFilesPath;
			this.pdfFilesPath = pdfFilesPath;
			this.localizer = localizer;
		}
		#endregion

		#region Public Properties
		#endregion

		#region Private Properties
		#endregion

		#region Protected Properties
		#endregion

		#region Public Methods
		public void Transform(string modelFilePath)
		{
			XmlResolver resolver = new XmlUrlResolver();
			resolver.Credentials = CredentialCache.DefaultCredentials;
			XmlDocument doc = new XmlDocument();
			doc.XmlResolver = resolver;
			doc.Load(modelFilePath);
			//			XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
			//			nsmgr.AddNamespace(string.Empty, "http://www.magosoftware.it/UseCaseMaker");
			XslTransform transform = new XslTransform();
			transform.Load(this.stylesheetFilesPath + Path.DirectorySeparatorChar + "PDFExport.xsl",resolver);
			StringWriter sw = new StringWriter();

			XsltArgumentList al = new XsltArgumentList();
			AssemblyName an = this.GetType().Assembly.GetName();
			al.AddParam("version","",an.Version.ToString(2));
			AssemblyCopyrightAttribute copyright = (AssemblyCopyrightAttribute)this.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute),false)[0];
			al.AddParam("copyright","",copyright.Copyright);
			al.AddParam("pathToLogo","",this.stylesheetFilesPath + Path.DirectorySeparatorChar + "Mago_logo.jpg");
			al.AddParam("description","",this.localizer.GetValue("Globals","Description"));
			al.AddParam("notes","",this.localizer.GetValue("Globals","Notes"));
			al.AddParam("relatedDocs","",this.localizer.GetValue("Globals","RelatedDocuments"));
			al.AddParam("model","",this.localizer.GetValue("Globals","Model"));
			al.AddParam("actor","",this.localizer.GetValue("Globals","Actor"));
			al.AddParam("useCase","",this.localizer.GetValue("Globals","UseCase"));
			al.AddParam("package","",this.localizer.GetValue("Globals","Package"));
			al.AddParam("actors","",this.localizer.GetValue("Globals","Actors"));
			al.AddParam("useCases","",this.localizer.GetValue("Globals","UseCases"));
			al.AddParam("packages","",this.localizer.GetValue("Globals","Packages"));
			al.AddParam("summary","",this.localizer.GetValue("Globals","Summary"));
			al.AddParam("glossary","",this.localizer.GetValue("Globals","Glossary"));
			al.AddParam("glossaryItem","",this.localizer.GetValue("Globals","GlossaryItem"));
			al.AddParam("preconditions","",this.localizer.GetValue("Globals","Preconditions"));
			al.AddParam("postconditions","",this.localizer.GetValue("Globals","Postconditions"));
			al.AddParam("openIssues","",this.localizer.GetValue("Globals","OpenIssues"));
			al.AddParam("flowOfEvents","",this.localizer.GetValue("Globals","FlowOfEvents"));
			al.AddParam("prose","",this.localizer.GetValue("Globals","Prose"));
			al.AddParam("requirements","",this.localizer.GetValue("Globals","Requirements"));
			al.AddParam("details","",this.localizer.GetValue("Globals","Details"));
			al.AddParam("priority","",this.localizer.GetValue("Globals","Priority"));
			al.AddParam("status","",this.localizer.GetValue("Globals","Status"));
			al.AddParam("level","",this.localizer.GetValue("Globals","Level"));
			al.AddParam("complexity","",this.localizer.GetValue("Globals","Complexity"));
			al.AddParam("implementation","",this.localizer.GetValue("Globals","Implementation"));
			al.AddParam("assignedTo","",this.localizer.GetValue("Globals","AssignedTo"));
			al.AddParam("release","",this.localizer.GetValue("Globals","Release"));
			al.AddParam("activeActors","",this.localizer.GetValue("Globals","ActiveActors"));
			al.AddParam("primary","",this.localizer.GetValue("Globals","Primary"));
			al.AddParam("history","",this.localizer.GetValue("Globals","History"));
			al.AddParam("statusNodeSet","",this.localizer.GetNodeSet("cmbStatus","Item"));
			al.AddParam("levelNodeSet","",this.localizer.GetNodeSet("cmbLevel","Item"));
			al.AddParam("complexityNodeSet","",this.localizer.GetNodeSet("cmbComplexity","Item"));
			al.AddParam("implementationNodeSet","",this.localizer.GetNodeSet("cmbImplementation","Item"));
			al.AddParam("historyTypeNodeSet","",this.localizer.GetNodeSet("HistoryType","Item"));

			transform.Transform(doc,al,sw,null);
			sw.Close();

			this.FO2PDF(sw.ToString(), this.pdfFilesPath);
		}
		#endregion

		#region Private Methods
		private void FO2PDF(string xmldocFo, string strFilename)
		{
			// Run the full FO doc through the engine to create a pdf
			Engine e = new Engine();
			sbyte[] sPdf = e.Run(xmldocFo);

			int sz = sPdf.Length;
			byte[] pdf = new byte[sz];
			for(int i=0; i<sz; i++)
			{
				pdf[i] = (byte) sPdf[i];
			}

			//Write output file
			FileStream fs = new FileStream(strFilename, FileMode.Create);
			BinaryWriter sw = new BinaryWriter(fs);
			sw.Write(pdf);
			sw.Close();
			fs.Close();
		}
		#endregion

		#region Protected Methods
		#endregion
	}
}

