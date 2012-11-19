using System;
using System.Diagnostics.CodeAnalysis;

namespace UseCaseMakerLibrary.Annotations
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    [ExcludeFromCodeCoverage]
    public sealed class AspMvcControllerAttribute : Attribute
    {
        [UsedImplicitly]
        public string AnonymousProperty { get; private set; }

        public AspMvcControllerAttribute()
        {
        }

        public AspMvcControllerAttribute(string anonymousProperty)
        {
            this.AnonymousProperty = anonymousProperty;
        }
    }
}