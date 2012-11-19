using System;
using System.Diagnostics.CodeAnalysis;

namespace UseCaseMakerLibrary.Annotations
{
    /// <summary>
    /// Indicates that marked method builds string by format pattern and (optional) arguments. 
    /// Parameter, which contains format string, should be given in constructor.
    /// The format string should be in <see cref="string.Format(IFormatProvider,string,object[])"/> -like form
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    [ExcludeFromCodeCoverage]
    public sealed class StringFormatMethodAttribute : Attribute
    {
        /// <summary>
        /// Initializes new instance of StringFormatMethodAttribute
        /// </summary>
        /// <param name="formatParameterName">Specifies which parameter of an annotated method should be treated as format-string</param>
        public StringFormatMethodAttribute(string formatParameterName)
        {
            this.FormatParameterName = formatParameterName;
        }

        /// <summary>
        /// Gets format parameter name
        /// </summary>
        [UsedImplicitly]
        public string FormatParameterName { get; private set; }
    }
}