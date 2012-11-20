using System;
using System.Xml.Serialization;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary
{
    public class Model : Package, IModel
    {
        #region Constructors
        public Model() : base()
        {
            this.Glossary = new GlossaryItems(this);
        }

        public Model(String name, String prefix, Int32 id) : base(name,prefix,id)
        {
            this.Glossary = new GlossaryItems(this);
        }

        #endregion

        #region Public Properties
        [XmlArray]
        [XmlArrayItem("GlossaryItem")]
        public GlossaryItems Glossary { get; private set; }

        #endregion

        #region Public Methods
        #region Glossary Handling
        public GlossaryItem NewGlossaryItem(String name, String prefix, Int32 id)
        {
            GlossaryItem gi = new GlossaryItem(name,prefix,id,this);
            return gi;
        }

        public void AddGlossaryItem(GlossaryItem gi)
        {
            gi.Owner = this;
            this.Glossary.Add(gi);
        }

        public void RemoveGlossaryItem(
            GlossaryItem gi,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean doNotMarkOccurrences)
        {
            if(!doNotMarkOccurrences)
            {
                this.ChangeReferences(
                    gi.Name,
                    oldNameStartTag,
                    oldNameEndTag,
                    gi.Name,
                    newNameStartTag,
                    newNameEndTag,
                    false);
            }

            foreach(GlossaryItem tmpGi in this.Glossary)
            {
                if(tmpGi.ID > gi.ID)
                {
                    tmpGi.ID -= 1;
                }
            }
            this.Glossary.Remove(gi);
        }

        public GlossaryItem GetGlossaryItem(String uniqueID)
        {
            return Glossary.FindByUniqueID(uniqueID);
        }
        #endregion // Packages Handling

        public IIdentificableObject FindElementByUniqueId(String uniqueId)
        {
            IIdentificableObject element = null;

            if(this.UniqueID == uniqueId)
            {
                return this;
            }

            element = this.Glossary.FindByUniqueID(uniqueId);
            if(element != null)
            {
                return element;
            }

            if(this.Requirements.UniqueID == uniqueId)
            {
                return this.Requirements;
            }

            if(this.Actors.UniqueID == uniqueId)
            {
                return this.Actors;
            }
            element = this.Actors.FindByUniqueID(uniqueId);
            if(element != null)
            {
                return element;
            }

            if(this.UseCases.UniqueID == uniqueId)
            {
                return this.UseCases;
            }
            element = this.UseCases.FindByUniqueID(uniqueId);
            if(element != null)
            {
                return element;
            }

            return this.Packages.FindElementByUniqueID(uniqueId);
        }

        public IIdentificableObject FindElementByName(String name)
        {
            IIdentificableObject element = null;

            if(this.Name == name)
            {
                return this;
            }

            element = this.Glossary.FindByName(name);
            if(element != null)
            {
                return element;
            }

            if(this.Actors.Name == name)
            {
                return this.Actors;
            }
            element = this.Actors.FindByName(name);
            if(element != null)
            {
                return element;
            }

            if(this.UseCases.Name == name)
            {
                return this.UseCases;
            }
            element = this.UseCases.FindByName(name);
            if(element != null)
            {
                return element;
            }

            return this.Packages.FindElementByName(name);
        }

        public IIdentificableObject FindElementByPath(String path)
        {
            IIdentificableObject element = null;

            if(this.Path == path)
            {
                return this;
            }

            element = this.Glossary.FindByPath(path);
            if(element != null)
            {
                return element;
            }

            if(this.Actors.Path == path)
            {
                return this.Actors;
            }
            element = this.Actors.FindByPath(path);
            if(element != null)
            {
                return element;
            }

            if(this.UseCases.Path == path)
            {
                return this.UseCases;
            }
            element = this.UseCases.FindByPath(path);
            if(element != null)
            {
                return element;
            }

            return this.Packages.FindElementByPath(path);
        }
        #endregion
    }
}
