using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    public class Step : IdentificableObject
    {
        #region Public Constants and Enumerators
        public enum StepType
        {
            Default = 0,
            Alternative = 1,
            AlternativeChild = 2
        }
        #endregion

        #region Constructors
        internal Step()
        {
            Dependency = new DependencyItem();
            Type = StepType.Default;
            ChildID = -1;
            Description = "";
        }

        #endregion

        #region Public Properties

        public string Description { get; set; }

        public int ChildID { get; set; }

        public StepType Type { get; set; }

        public DependencyItem Dependency { get; set; }

        [XmlAttribute]
        public override string Name
        {
            get
            {
                return this.ID +
                    ((Prefix != "") ? "." + Prefix : "") +
                    ((ChildID != -1) ? "." + ChildID : "");
            }

            set
            {
                base.Name = value;
            }
        }
        #endregion
    }
}
