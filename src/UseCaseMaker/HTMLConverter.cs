using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Net;
using System.Reflection;
using UseCaseMakerLibrary.Contracts;
using UseCaseMakerLibrary.Services;

namespace UseCaseMaker
{
	/// <summary>
	/// Descrizione di riepilogo per HTMLConverter.
	/// </summary>
	public class HTMLConverter
	{
		private string stylesheetFilesPath = string.Empty;
		private string htmlFilesPath = string.Empty;
		private ILocalizationService localizationService = null;

		public HTMLConverter(
			string stylesheetFilesPath,
			string htmlFilesPath,
			ILocalizationService localizationService)
		{
			this.stylesheetFilesPath = stylesheetFilesPath;
			this.htmlFilesPath = htmlFilesPath;
			this.localizationService = localizationService;
		}

		public void BuildNavigator(string modelFilePath)
		{
			XmlResolver resolver = new XmlUrlResolver();
			resolver.Credentials = CredentialCache.DefaultCredentials;
			XmlDocument doc = new XmlDocument();
			doc.XmlResolver = resolver;
			doc.Load(modelFilePath);
			XslTransform transform = new XslTransform();
			transform.Load(this.stylesheetFilesPath + Path.DirectorySeparatorChar + "ModelTree.xsl",resolver);
			StreamWriter sw = new StreamWriter(this.htmlFilesPath + Path.DirectorySeparatorChar + "ModelTree.htm",false);
			XsltArgumentList al = new XsltArgumentList();
			al.AddParam("modelBrowser","",this.localizationService.GetValue("Globals","ModelBrowser"));
			al.AddParam("glossary","",this.localizationService.GetValue("Globals","Glossary"));
			transform.Transform(doc,al,sw,null);
			sw.Close();

			transform.Load(this.stylesheetFilesPath + Path.DirectorySeparatorChar + "HomePage.xsl",resolver);
			sw = new StreamWriter(this.htmlFilesPath + Path.DirectorySeparatorChar + "main.htm",false);
			al = new XsltArgumentList();
			AssemblyName an = this.GetType().Assembly.GetName();
			al.AddParam("version","",an.Version.ToString(3));
			transform.Transform(doc,al,sw,null);
			sw.Close();
		}

		public void BuildPages(string modelFilePath)
		{
			XmlResolver resolver = new XmlUrlResolver();
			resolver.Credentials = CredentialCache.DefaultCredentials;
			XmlDocument doc = new XmlDocument();
			doc.XmlResolver = resolver;
			doc.Load(modelFilePath);
			XmlNode modelNode = doc.SelectSingleNode("//Model");
			this.RecurseNode(doc, resolver, modelNode,"Package.xsl");
		}

		private void RecurseNode(XmlDocument doc, XmlResolver resolver, XmlNode elementNode, string xsltName)
		{
			this.ElementToHTMLPage(doc,resolver,elementNode,xsltName);

			foreach(XmlNode childNode in elementNode)
			{
				if(childNode.Name == "Glossary")
				{
					this.ElementToHTMLPage(doc,resolver,childNode,"Glossary.xsl");
				}
				if(childNode.Name == "Packages")
				{
					foreach(XmlNode packageNode in childNode.ChildNodes)
					{
						this.RecurseNode(doc,resolver,packageNode,"Package.xsl");
					}
				}
				if(childNode.Name == "Actors")
				{
					foreach(XmlNode actorNode in childNode.ChildNodes)
					{
						this.RecurseNode(doc,resolver,actorNode,"Actor.xsl");
					}
				}
				if(childNode.Name == "UseCases")
				{
					foreach(XmlNode useCaseNode in childNode.ChildNodes)
					{
						this.RecurseNode(doc,resolver,useCaseNode,"UseCase.xsl");
					}
				}
			}
		}

		private void ElementToHTMLPage(
			XmlDocument src,
			XmlResolver resolver,
			XmlNode currentNode,
			string xslFileName
			)
		{
			XsltArgumentList al = new XsltArgumentList();
			al.AddParam("elementUniqueID","",currentNode.Attributes["UniqueID"].InnerText);
			if(currentNode.Name == "Glossary")
			{
				al.AddParam("glossary","",this.localizationService.GetValue("Globals","Glossary"));
				al.AddParam("glossaryItem","",this.localizationService.GetValue("Globals","GlossaryItem"));
				al.AddParam("description","",this.localizationService.GetValue("Globals","Description"));
			}
			if(currentNode.Name == "Model" || currentNode.Name == "Package")
			{
				if(currentNode.Name == "Model")
				{
					al.AddParam("elementType","",this.localizationService.GetValue("Globals","Model"));
				}
				else
				{
					al.AddParam("elementType","",this.localizationService.GetValue("Globals","Package"));
				}
				al.AddParam("actors","",this.localizationService.GetValue("Globals","Actors"));
				al.AddParam("useCases","",this.localizationService.GetValue("Globals","UseCases"));
				al.AddParam("packages","",this.localizationService.GetValue("Globals","Packages"));
				al.AddParam("description","",this.localizationService.GetValue("Globals","Description"));
				al.AddParam("notes","",this.localizationService.GetValue("Globals","Notes"));
				al.AddParam("relatedDocs","",this.localizationService.GetValue("Globals","RelatedDocuments"));
				al.AddParam("requirements","",this.localizationService.GetValue("Globals","Requirements"));
			}
			if(currentNode.Name == "Actor")
			{
				al.AddParam("elementType","",this.localizationService.GetValue("Globals","Actor"));
				al.AddParam("description","",this.localizationService.GetValue("Globals","Description"));
				al.AddParam("notes","",this.localizationService.GetValue("Globals","Notes"));
				al.AddParam("relatedDocs","",this.localizationService.GetValue("Globals","RelatedDocuments"));
				al.AddParam("goals","",this.localizationService.GetValue("Globals","Goals"));
			}
			if(currentNode.Name == "UseCase")
			{
				al.AddParam("statusNodeSet","",this.localizationService.GetNodeSet("cmbStatus","Item"));
				al.AddParam("levelNodeSet","",this.localizationService.GetNodeSet("cmbLevel","Item"));
				al.AddParam("complexityNodeSet","",this.localizationService.GetNodeSet("cmbComplexity","Item"));
				al.AddParam("implementationNodeSet","",this.localizationService.GetNodeSet("cmbImplementation","Item"));
				al.AddParam("historyTypeNodeSet","",this.localizationService.GetNodeSet("HistoryType","Item"));

				al.AddParam("elementType","",this.localizationService.GetValue("Globals","UseCase"));
				al.AddParam("preconditions","",this.localizationService.GetValue("Globals","Preconditions"));
				al.AddParam("postconditions","",this.localizationService.GetValue("Globals","Postconditions"));
				al.AddParam("openIssues","",this.localizationService.GetValue("Globals","OpenIssues"));
				al.AddParam("flowOfEvents","",this.localizationService.GetValue("Globals","FlowOfEvents"));
				al.AddParam("prose","",this.localizationService.GetValue("Globals","Prose"));
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
				al.AddParam("description","",this.localizationService.GetValue("Globals","Description"));
				al.AddParam("notes","",this.localizationService.GetValue("Globals","Notes"));
				al.AddParam("relatedDocs","",this.localizationService.GetValue("Globals","RelatedDocuments"));
			}

			XslTransform transform = new XslTransform();
			transform.Load(this.stylesheetFilesPath + Path.DirectorySeparatorChar + xslFileName,resolver);
			StreamWriter sw = new StreamWriter(htmlFilesPath + Path.DirectorySeparatorChar + currentNode.Attributes["UniqueID"].Value + ".htm",false);
			transform.Transform(src,al,sw,null);
			sw.Close();
		}
	}
}
