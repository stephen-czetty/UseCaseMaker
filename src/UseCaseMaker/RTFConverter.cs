using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Collections.Specialized;
using System.Net;
using UseCaseMakerLibrary.Contracts;
using UseCaseMakerLibrary.Services;
using iTextSharp.text;
using iTextSharp.text.rtf;
using iTextSharp.text.xml;

namespace UseCaseMaker
{
	/**
	 * @brief Descrizione di riepilogo per PDFConverter.
	 */
	public class RTFConverter
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
		private string rtfFilesPath = string.Empty;
		private ILocalizationService localizationService = null;
		// Protected
		#endregion

		#region Constructors
		/**
		 * @brief Costruttore di default per XMIConverter
		 */
		public RTFConverter(
			string stylesheetFilesPath,
			string rtfFilesPath,
			ILocalizationService localizationService)
		{
			this.stylesheetFilesPath = stylesheetFilesPath;
			this.rtfFilesPath = rtfFilesPath;
			this.localizationService = localizationService;
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
			StreamReader sr;
			string foDoc;
			MemoryStream ms = new MemoryStream();
			XmlResolver resolver = new XmlUrlResolver();
			resolver.Credentials = CredentialCache.DefaultCredentials;
			XmlDocument doc = new XmlDocument();
			doc.XmlResolver = resolver;
			doc.Load(modelFilePath);
			XslTransform transform = new XslTransform();
			transform.Load(this.stylesheetFilesPath + Path.DirectorySeparatorChar + "PdfRtfExport.xsl",resolver);

			XsltArgumentList al = new XsltArgumentList();
			AssemblyName an = this.GetType().Assembly.GetName();
			al.AddParam("version","",an.Version.ToString(3));
			al.AddParam("outputType","","withoutLink");
			al.AddParam("description","",this.localizationService.GetValue("Globals","Description"));
			al.AddParam("notes","",this.localizationService.GetValue("Globals","Notes"));
			al.AddParam("relatedDocs","",this.localizationService.GetValue("Globals","RelatedDocuments"));
			al.AddParam("model","",this.localizationService.GetValue("Globals","Model"));
			al.AddParam("actor","",this.localizationService.GetValue("Globals","Actor"));
			al.AddParam("goals","",this.localizationService.GetValue("Globals","Goals"));
			al.AddParam("useCase","",this.localizationService.GetValue("Globals","UseCase"));
			al.AddParam("package","",this.localizationService.GetValue("Globals","Package"));
			al.AddParam("actors","",this.localizationService.GetValue("Globals","Actors"));
			al.AddParam("useCases","",this.localizationService.GetValue("Globals","UseCases"));
			al.AddParam("packages","",this.localizationService.GetValue("Globals","Packages"));
			al.AddParam("summary","",this.localizationService.GetValue("Globals","Summary"));
			al.AddParam("glossary","",this.localizationService.GetValue("Globals","Glossary"));
			al.AddParam("glossaryItem","",this.localizationService.GetValue("Globals","GlossaryItem"));
			al.AddParam("preconditions","",this.localizationService.GetValue("Globals","Preconditions"));
			al.AddParam("postconditions","",this.localizationService.GetValue("Globals","Postconditions"));
			al.AddParam("openIssues","",this.localizationService.GetValue("Globals","OpenIssues"));
			al.AddParam("flowOfEvents","",this.localizationService.GetValue("Globals","FlowOfEvents"));
			al.AddParam("prose","",this.localizationService.GetValue("Globals","Prose"));
			al.AddParam("requirements","",this.localizationService.GetValue("Globals","Requirements"));
			al.AddParam("details","",this.localizationService.GetValue("Globals","Details"));
			al.AddParam("priority","",this.localizationService.GetValue("Globals","Priority"));
			al.AddParam("status","",this.localizationService.GetValue("Globals","Status"));
			al.AddParam("level","",this.localizationService.GetValue("Globals","Level"));
			al.AddParam("complexity","",this.localizationService.GetValue("Globals","Complexity"));
			al.AddParam("implementation","",this.localizationService.GetValue("Globals","Implementation"));
			al.AddParam("assignedTo","",this.localizationService.GetValue("Globals","AssignedTo"));
			al.AddParam("release","",this.localizationService.GetValue("Globals","Release"));
			al.AddParam("activeActors","",this.localizationService.GetValue("Globals","ActiveActors"));
			al.AddParam("primary","",this.localizationService.GetValue("Globals","Primary"));
			al.AddParam("history","",this.localizationService.GetValue("Globals","History"));
			al.AddParam("statusNodeSet","",this.localizationService.GetNodeSet("cmbStatus","Item"));
			al.AddParam("levelNodeSet","",this.localizationService.GetNodeSet("cmbLevel","Item"));
			al.AddParam("complexityNodeSet","",this.localizationService.GetNodeSet("cmbComplexity","Item"));
			al.AddParam("implementationNodeSet","",this.localizationService.GetNodeSet("cmbImplementation","Item"));
			al.AddParam("historyTypeNodeSet","",this.localizationService.GetNodeSet("HistoryType","Item"));

			transform.Transform(doc,al,ms,resolver);
			ms.Position = 0;
			sr = new StreamReader(ms,Encoding.UTF8);
			foDoc = sr.ReadToEnd();
			sr.Close();
			ms.Close();

			this.XmlToRtf(foDoc,this.rtfFilesPath);
		}
		#endregion

		#region Private Methods
		public void XmlToRtf(string xmlDoc, string strFilename) 
		{        
			Document document = new Document();
			MemoryStream ms = new MemoryStream();
			Phrase headerPhrase;
			Phrase footerPhrase;
        
			// iTextSharp
			RtfWriter2 writer = RtfWriter2.GetInstance(document, ms);
			
			footerPhrase = new Phrase("",new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA,8));
			RtfHeaderFooter footer = new RtfHeaderFooter(footerPhrase,true);
			footer.SetAlignment("center");
			writer.Footer = footer;
			
			AssemblyName an = this.GetType().Assembly.GetName();
			headerPhrase = new Phrase(
				"Use Case Maker " + an.Version.ToString(3),
				new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA,8));
			RtfHeaderFooter header = new RtfHeaderFooter(headerPhrase,false);
			header.SetAlignment("right");
			writer.Header = header;

			StringReader sr = new StringReader(xmlDoc);
			XmlTextReader reader = new XmlTextReader(sr);
			ITextHandler xmlHandler = new ITextHandler(document);

			try
			{
				xmlHandler.Parse(reader);
			}
			catch(Exception e)
			{
				ms.Close();
				throw e;
			}
			finally
			{
				reader.Close();
				sr.Close();
			}

			//Write output file
			FileStream fs = new FileStream(strFilename, FileMode.Create);
			BinaryWriter bw = new BinaryWriter(fs);
			bw.Write(ms.ToArray());
			bw.Close();
			fs.Close();
			ms.Close();
		}
		#endregion

		#region Protected Methods
		#endregion
	}
}

