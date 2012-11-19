using System;

namespace UseCaseMakerLibrary.Annotations
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcViewAttribute : PathReferenceAttribute
    {
    }
}