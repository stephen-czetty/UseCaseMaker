using System;
using System.Diagnostics.CodeAnalysis;

namespace UseCaseMakerLibrary.Annotations
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [ExcludeFromCodeCoverage]
    public class PathReferenceAttribute : Attribute
    {
        public PathReferenceAttribute()
        {
        }

        [UsedImplicitly]
        public PathReferenceAttribute([PathReference] string basePath)
        {
            this.BasePath = basePath;
        }

        [UsedImplicitly]
        public string BasePath { get; private set; }
    }
}