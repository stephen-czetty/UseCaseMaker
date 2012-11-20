﻿using System.IO;
using System.Xml;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Services
{
    public class LegacyXmlSerializer : ISerializer<IModel>
    {
        private const string Version = "1.0";

        /// <summary>
        /// De-serialize the input text
        /// </summary>
        /// <param name="inputDataStream">The input data stream.</param>
        /// <returns>A deserialized object of type <see cref="UseCaseMakerLibrary.Model"/>.</returns>
        public IModel DeSerialize(TextReader inputDataStream)
        {
            var doc = new XmlDocument();
            doc.Load(inputDataStream);
            var model = new Model();
            XmlSerializer.XmlDeserialize(doc, "UCM-Document", "", Version, model);
            return model;
        }

        /// <summary>
        /// Serializes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="outputDataStream">The output data stream.</param>
        public void Serialize(IModel data, TextWriter outputDataStream)
        {
            XmlDocument doc = XmlSerializer.XmlSerialize("UCM-Document", "", Version, data, true);
            doc.Save(outputDataStream);
        }
    }
}