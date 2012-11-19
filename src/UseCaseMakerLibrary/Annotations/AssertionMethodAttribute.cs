using System;

namespace UseCaseMakerLibrary.Annotations
{
    /// <summary>
    /// Indicates that the marked method is assertion method, i.e. it halts control flow if one of the conditions is satisfied. 
    /// To set the condition, mark one of the parameters with <see cref="AssertionConditionAttribute"/> attribute
    /// </summary>
    /// <seealso cref="AssertionConditionAttribute"/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AssertionMethodAttribute : Attribute
    {
    }
}