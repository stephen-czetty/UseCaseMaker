using System;
using System.Diagnostics.CodeAnalysis;

namespace UseCaseMakerLibrary.Annotations
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    [ExcludeFromCodeCoverage]
    public sealed class AspMvcActionAttribute : Attribute
    {
        [UsedImplicitly]
        public string AnonymousProperty { get; private set; }

        public AspMvcActionAttribute()
        {
        }

        public AspMvcActionAttribute(string anonymousProperty)
        {
            this.AnonymousProperty = anonymousProperty;
        }
    }
}