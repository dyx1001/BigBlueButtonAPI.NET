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
    public class PlaybackPreviewImages:List<PlaybackPreviewImage>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement || reader.Read() == false) return;

            var formatter = new XmlSerializer(typeof(PlaybackPreviewImage));
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "images")
                    {
                        reader.Read();
                    }
                    else
                    {
                        this.Add((PlaybackPreviewImage) formatter.Deserialize(reader));
                    }
                }
                else
                {
                    reader.Read();
                }
            }
            //images
            reader.ReadEndElement();
            //preview
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