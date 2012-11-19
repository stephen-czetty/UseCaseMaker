using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace UseCaseMakerLibrary.Contracts
{
    [ContractClass(typeof(SerializerContract<>))]
    public interface ISerializer<T>
        where T : class
    {
        /// <summary>
        /// De-serialize the input text
        /// </summary>
        /// <param name="inputDataStream">The input data stream.</param>
        /// <typeparam name="T">The type that will be deserialized</typeparam>
        /// <returns>A deserialized object of type {T}</returns>
        [Annotations.Pure]
        T DeSerialize(TextReader inputDataStream);

        /// <summary>
        /// Serializes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="outputDataStream">The output data stream.</param>
        void Serialize(T data, TextWriter outputDataStream);
    }

    [ContractClassFor(typeof(ISerializer<>))]
    internal abstract class SerializerContract<T> : ISerializer<T>
        where T : class
    {
        [Annotations.Pure]
        public T DeSerialize(TextReader inputDataStream)
        {
            Contract.Requires<ArgumentNullException>(inputDataStream != null);
            Contract.Ensures(Contract.Result<T>() != null);
            return default(T);
        }

        public void Serialize(T data, TextWriter outputDataStream)
        {
            Contract.Requires<ArgumentNullException>(data != null);
            Contract.Requires<ArgumentNullException>(outputDataStream != null);
        }
    }
}