using System;

namespace UseCaseMakerLibrary.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AspMvcSupressViewErrorAttribute : Attribute
    {
    }
}