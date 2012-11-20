using System;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Services
{
    /// <summary>
    /// Service for getting localized strings
    /// </summary>
    public class LocalizationService : ILocalizationService
    {
        #region Public Properties

        /// <summary>
        /// Gets the language document.
        /// </summary>
        public XmlDocument LanguageDocument { get; private set; }

        #endregion

        #region Public Methods
        /// <summary>
        /// Loads the specified language file path.
        /// </summary>
        /// <param name="languageFilePath">The language file path.</param>
        public void Load(string languageFilePath)
        {
            this.LanguageDocument = new XmlDocument();
            this.LanguageDocument.Load(languageFilePath);
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The localized value
        /// </returns>
        public string GetValue(string section, string name)
        {
            XmlNode node = LanguageDocument.SelectSingleNode("//" + section + "/" + name);
            return node == null ? string.Empty : node.InnerText;
        }

        /// <summary>
        /// Gets the node set.
        /// </summary>
        /// <param name="parentName">Name of the parent.</param>
        /// <param name="childsName">Name of the child.</param>
        /// <returns>
        /// The node set
        /// </returns>
        public XPathNodeIterator GetNodeSet(string parentName, string childsName)
        {
            XPathNavigator nav = this.LanguageDocument.CreateNavigator();
            XPathNodeIterator ni = nav.Select("//" + parentName + "/" + childsName);
            return ni;
        }

        /// <summary>
        /// Localizes the controls in a form.
        /// </summary>
        /// <param name="form">The form.</param>
        public void LocalizeControls(Form form)
        {
            XmlNode controlsNode = this.LanguageDocument.SelectSingleNode("//" + form.Name + "/Controls");
            if (controlsNode == null)
                return;

            foreach (XmlNode node in controlsNode.ChildNodes)
            {
                if (node.HasChildNodes && node.FirstChild.NodeType != XmlNodeType.Text)
                {
                    FieldInfo fi = form.GetType().GetField(
                        node.Name,
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (fi == null)
                        throw new Exception("Unrecognized control: " + node.Name);

                    PropertyInfo items = fi.FieldType.GetProperty("Items");
                    MethodInfo clear = items.PropertyType.GetMethod("Clear");
                    clear.Invoke(items.GetValue(fi.GetValue(form), null), null);
                    foreach (XmlNode itemNode in node.ChildNodes)
                    {
                        MethodInfo add = items.PropertyType.GetMethod("Add");
                        var addParams = new object[1];
                        addParams[0] = itemNode.InnerText;
                        add.Invoke(items.GetValue(fi.GetValue(form), null), addParams);
                    }

                    if (node.Attributes != null && node.Attributes["ToolTipText"] != null)
                    {
                        PropertyInfo pi = fi.FieldType.GetProperty("ToolTipText");
                        pi.SetValue(fi.GetValue(form), node.Attributes["ToolTipText"].Value, null);
                    }
                }
                else if (node.NodeType != XmlNodeType.Comment)
                {
                    if (node.InnerText != string.Empty)
                    {
                        if (node.Name.ToUpper() == "SELF")
                            form.Text = node.InnerText;
                        else
                        {
                            FieldInfo fi = form.GetType().GetField(
                                node.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                            if (fi == null)
                                throw new Exception("Unrecognized control: " + node.Name);
                            PropertyInfo pi = fi.FieldType.GetProperty("Text");
                            pi.SetValue(fi.GetValue(form), node.InnerText, null);
                        }
                    }

                    if (node.Attributes != null && node.Attributes["ToolTipText"] != null)
                    {
                        FieldInfo fi = form.GetType().GetField(
                            node.Name, 
                            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                        if (fi == null)
                            throw new Exception("Unrecognized control: " + node.Name);
                        PropertyInfo pi = fi.FieldType.GetProperty("ToolTipText");
                        pi.SetValue(fi.GetValue(form), node.Attributes["ToolTipText"].Value, null);
                    }
                }
            }
        }
        #endregion
    }
}
