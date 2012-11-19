using System;

namespace UseCaseMakerLibrary.Annotations
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method, Inherited = true)]
    public sealed class RazorSectionAttribute : Attribute
    {
    }
}