using System;
using System.Diagnostics.CodeAnalysis;

namespace UseCaseMakerLibrary.Annotations
{
    /// <summary>
    /// This attribute is intended to mark publicly available API which should not be removed and so is treated as used.
    /// </summary>
    [MeansImplicitUse]
    [ExcludeFromCodeCoverage]
    public sealed class PublicAPIAttribute : Attribute
    {
        public PublicAPIAttribute()
        {
        }

        // ReSharper disable UnusedParameter.Local
#pragma warning disable UnusedMember.Global
        public PublicAPIAttribute(string comment)
        {
        }
#pragma warning restore UnusedMember.Global
        // ReSharper restore UnusedParameter.Local
    }
}