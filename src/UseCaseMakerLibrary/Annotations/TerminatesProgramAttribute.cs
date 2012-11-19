using System;

namespace UseCaseMakerLibrary.Annotations
{
    /// <summary>
    /// Indicates that the marked method unconditionally terminates control flow execution.
    /// For example, it could unconditionally throw exception
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class TerminatesProgramAttribute : Attribute
    {
    }
}