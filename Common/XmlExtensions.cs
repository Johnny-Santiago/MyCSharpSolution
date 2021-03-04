using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Common
{
    public static class XmlExtensions
    {
        public static string ToXml(this DataTable dt)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            MemoryStream s = new MemoryStream();
            dt.WriteXml(s, true);
            s.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(s);
            string xml = sr.ReadToEnd();
            sr.Close();

            dt.WriteXml(@"c:\UserPrivileges.xml", XmlWriteMode.IgnoreSchema);

            return xml;
        }
    }
}