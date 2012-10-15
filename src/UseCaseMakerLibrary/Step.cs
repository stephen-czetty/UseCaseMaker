using System;

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

        #region Class Members
        private String description = String.Empty;
        private Int32 childID = -1;
        private StepType type = StepType.Default;
        private DependencyItem dependencyItem = new DependencyItem();
        #endregion

        #region Constructors
        internal Step()
        {
        }
        #endregion

        #region Public Properties
        public String Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public Int32 ChildID
        {
            get
            {
                return this.childID;
            }
            set
            {
                this.childID = value;
            }
        }

        public StepType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        public DependencyItem Dependency
        {
            get
            {
                return this.dependencyItem;
            }
        }
        #endregion
    }
}
