/**
 * Author: dyx1001
 * Email: dyx1001@126.com
 * License: MIT
 * Git URL: https://github.com/dyx1001/BigBlueButtonAPI.NET
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BigBlueButtonAPI.Common
{
    public class MetaData : Dictionary<string, string>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement || reader.Read() == false) return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    this[reader.Name] = reader.ReadElementContentAsString();
                } else
                {
                    reader.Read();
                }
            }
            reader.ReadEndElement();
        }

        /// <summary>
        /// We need to read xml only.
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }


}