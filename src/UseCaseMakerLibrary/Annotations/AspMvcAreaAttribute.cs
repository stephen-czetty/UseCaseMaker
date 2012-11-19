using System;
using System.Diagnostics.CodeAnalysis;

namespace UseCaseMakerLibrary.Annotations
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [ExcludeFromCodeCoverage]
    public sealed class AspMvcAreaAttribute : PathReferenceAttribute
    {
        [UsedImplicitly]
        public string AnonymousProperty { get; private set; }

        [UsedImplicitly]
        public AspMvcAreaAttribute()
        {
        }

        public AspMvcAreaAttribute(string anonymousProperty)
        {
            this.AnonymousProperty = anonymousProperty;
        }
    }
}