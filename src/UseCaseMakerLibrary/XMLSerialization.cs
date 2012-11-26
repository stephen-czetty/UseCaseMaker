using System;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	public interface IXMLNodeSerializable
	{
	}

    public interface IXmlCollectionSerializable<T>
    {
        void Add(T item);

        T this[int idx] { get; set; }
    }

    public class XmlSerializerException : Exception
	{
		public XmlSerializerException(string message) : base(message)
		{
		}
	}

	/// <summary>
	/// Descrizione di riepilogo per XMLNodeRetriever.
	/// </summary>
	public static class XmlSerializer
	{
		static public XmlDocument XmlSerialize(
			string mainNodeName,
			string namespaceURI,
			string version,
			object instance,
			bool deep)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(mainNodeName));
			Contract.Requires<ArgumentNullException>(instance != null);
			Contract.Ensures(Contract.Result<XmlDocument>() != null);

			XmlDocument document = new XmlDocument();
			XmlDeclaration decl = document.CreateXmlDeclaration("1.0","UTF-8","");
			XmlElement mainNode = document.CreateElement(string.Empty, mainNodeName, namespaceURI);
			XmlAttribute attr = document.CreateAttribute("Version");
			attr.Value = version;
			mainNode.SetAttributeNode(attr);
			document.AppendChild(mainNode);
			document.InsertBefore(decl,document.DocumentElement);
			XmlNode node = XmlSerializer.XmlSerialize(document,instance,string.Empty,true);
			mainNode.AppendChild(document.ImportNode(node,true));
			return document;
		}

		private static XmlNode XmlSerialize(XmlDocument document, object instance, string propertyName, bool deep)
		{
			Contract.Requires<ArgumentNullException>(document != null);
			Contract.Requires<ArgumentNullException>(instance != null);
			Contract.Ensures(Contract.Result<XmlNode>() != null);

			string namespaceURI = document.DocumentElement.NamespaceURI;

			int i;

			XmlElement mainNode = document.CreateElement(string.Empty, propertyName == string.Empty ? instance.GetType().Name : propertyName, namespaceURI);

			XmlAttribute instanceType = document.CreateAttribute("Type");
			instanceType.Value = instance.GetType().ToString();
			mainNode.SetAttributeNode(instanceType);

			PropertyInfo[] pi = instance.GetType().GetProperties();

			for(i = 0; i < pi.Length; i++)
			{
				if (pi[i].IsDefined(typeof (XmlIgnoreAttribute), false))
					continue;

			    string name = pi[i].Name == "UniqueId" ? "UniqueID" : pi[i].Name;
			    name = name == "Id" ? "ID" : name;
				XmlElement propertyNode = document.CreateElement(string.Empty,name,namespaceURI);
				if(typeof(IXMLNodeSerializable).IsAssignableFrom(pi[i].PropertyType)
				   && pi[i].GetValue(instance,null) != null)
				{
					if(deep)
					{
						XmlNode methodNode = XmlSerialize(document,pi[i].GetValue(instance,null),pi[i].Name,deep);
						propertyNode = (XmlElement)document.ImportNode(methodNode,true);
						mainNode.AppendChild(propertyNode);
					}
					if(typeof(ICollection).IsAssignableFrom(pi[i].PropertyType))
					{
						IEnumerator ie = ((ICollection)pi[i].GetValue(instance,null)).GetEnumerator();
						while(ie.MoveNext())
						{
							object element = null;
							Type [] parameters = new Type[1];
							parameters[0] = ie.Current.GetType();
							PropertyInfo Item = pi[i].PropertyType.GetProperty("Item",parameters);
							if(Item == null)
							{
								element = ie.Current;
							}
							else
							{
								MethodInfo GetItem = Item.GetGetMethod(false);
								object [] GetItemParameters = new object[1];
								GetItemParameters[0] = ie.Current;
								element = GetItem.Invoke(pi[i].GetValue(instance,null),GetItemParameters);
							}
							if(element is IXMLNodeSerializable)
							{
								XmlNode methodNode = XmlSerialize(document,element,string.Empty,deep);
								propertyNode.AppendChild(document.ImportNode(methodNode,true));
								mainNode.AppendChild(propertyNode);
							}
						}
					}
				}
				else
				{
					if(pi[i].IsDefined(typeof(XmlAttributeAttribute),false))
					{
						XmlAttribute xmlAttribute = document.CreateAttribute(name);
						xmlAttribute.Value = pi[i].GetValue(instance,null).ToString();
						mainNode.SetAttributeNode(xmlAttribute);
					}
					else
					{
						propertyNode.InnerText = (pi[i].GetValue(instance,null) == null)
						                         	? string.Empty : pi[i].GetValue(instance,null).ToString();
						XmlAttribute propertyType = document.CreateAttribute("Type");
						propertyType.Value = pi[i].PropertyType.ToString();
						propertyNode.SetAttributeNode(propertyType);
						mainNode.AppendChild(propertyNode);
					}
				}
			}
			
			return mainNode;
		}

		static public void XmlDeserialize(
			XmlDocument document,
			string mainNodeName,
			string namespaceURI,
			string version,
			object instance)
		{
			Contract.Requires<NullReferenceException>(document != null);
			Contract.Requires<NullReferenceException>(instance != null);

			// Check document and instance
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(document.NameTable);
			nsmgr.AddNamespace(string.Empty, namespaceURI);
			XmlNode node = document.DocumentElement;
			if (node.Name != mainNodeName)
				throw new XmlSerializerException("Invalid document!");

			XmlAttribute attr = node.Attributes["Version"];
			if (attr == null)
				throw new XmlSerializerException("Invalid document!");

			string [] currentVersion = version.Split('.');
			string [] fileVersion = attr.Value.Split('.');
			if (fileVersion.Length != 2)
				throw new XmlSerializerException("Invalid document!");

			if (fileVersion[0].CompareTo(currentVersion[0]) > 0)
				throw new XmlSerializerException("Incompatible version!");

			if (fileVersion[0] == currentVersion[0])
				if (fileVersion[1].CompareTo(currentVersion[1]) > 0)
					throw new XmlSerializerException("Incompatible version!");

			XmlDeserialize(node.FirstChild, instance);
		}

		private static void XmlDeserialize(XmlNode fromNode, object instance)
		{
			Contract.Requires<ArgumentNullException>(fromNode != null);
			Contract.Requires<ArgumentNullException>(instance != null);

			if (fromNode.Attributes["Type"] == null)
				throw new XmlSerializerException("Type attribute not found!");

			foreach(XmlNode node in fromNode.ChildNodes)
			{
			    if (node.Attributes["Type"] == null)
			        throw new XmlSerializerException("Type attribute not found!");

			    try
			    {
			        PropertyInfo pi = instance.GetType().GetProperty(node.Name, Type.GetType(node.Attributes["Type"].Value));
			        if (typeof(ICollection).IsAssignableFrom(pi.PropertyType))
			        {
			            foreach (XmlNode itemNode in node.ChildNodes)
			            {
			                if (itemNode.GetType() == typeof(XmlElement))
			                {
			                    if (itemNode.Attributes["Type"] == null)
			                        throw new XmlSerializerException("Type attribute not found!");

			                    Type itemType = instance.GetType().Assembly.GetType(itemNode.Attributes["Type"].Value);
			                    ConstructorInfo ctor =
			                        itemType.GetConstructor(
			                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
			                            null,
			                            Type.EmptyTypes,
			                            null);
			                    object item = ctor.Invoke(new object[0]); // Default constructor
			                    XmlDeserialize(itemNode, item);

			                    MethodInfo add = pi.PropertyType.GetMethod("Add", new[] { itemType });
			                    object[] addParameters = new Object[1];
			                    addParameters[0] = item;
			                    add.Invoke(pi.GetValue(instance, null), addParameters);

			                }
			            }
			        }

			        if (!typeof(IXMLNodeSerializable).IsAssignableFrom(pi.PropertyType))
			        {
			            foreach (XmlNode nodeValue in node.ChildNodes)
			            {
			                if (nodeValue.GetType() == typeof(XmlText) && nodeValue.Value != null)
			                    pi.SetValue(
			                        instance,
			                        pi.PropertyType.IsEnum
			                            ? Enum.Parse(pi.PropertyType, nodeValue.Value, true)
			                            : Convert.ChangeType(nodeValue.Value, pi.PropertyType),
			                        null);
			            }
			        }
			        else
			        {
			            if (pi.GetValue(instance, null) == null)
			                pi.SetValue(instance, Convert.ChangeType(node.Value, pi.PropertyType), null);

			            if (node.HasChildNodes)
			                XmlDeserialize(node, pi.GetValue(instance, null));
			        }
			    }
			    catch (NullReferenceException)
			    {
			        return;
			    }
			}
		    foreach(XmlAttribute attr in fromNode.Attributes)
				{
					try
					{
					    string name = attr.Name == "UniqueID" ? "UniqueId" : attr.Name;
					    name = name == "ID" ? "Id" : name;
						PropertyInfo pi = instance.GetType().GetProperty(name);
						if (pi.CanWrite && pi.IsDefined(typeof (XmlAttributeAttribute), false))
							pi.SetValue(instance, Convert.ChangeType(attr.Value, pi.PropertyType), null);
					}
					catch(NullReferenceException)
					{
					}
				}
			
		}
	}
}
