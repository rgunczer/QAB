using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

#pragma warning disable 0618


namespace VM
{
    class ActionXMLRead : ActionBase
    {
        private string m_xml = null;
        private string m_nodeValue = null;

		private static readonly string Xslt = @"
<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0'>  
  <xsl:template match='*'>
    <xsl:element name='{local-name()}'>
      <xsl:apply-templates select='@* | node()'/>
    </xsl:element>
  </xsl:template>  
  <xsl:template match='@* | text() | comment() | processing-instruction()'><xsl:copy/></xsl:template>
</xsl:stylesheet>";

        private void LoadXML(string path)
        {
			try
			{
				using(StreamReader reader = File.OpenText(path))
					m_xml = reader.ReadToEnd();
			}
            catch 
			{				
				throw;
			}
        }

        private void ReadNodeValue(string xpath)
        {
			m_nodeValue = null;

			try
			{
				m_nodeValue = Read(m_xml, xpath);
			}
            catch
			{
                throw;				
			}
        }
        
        public override EnumActionResult Execute()
        {
            LoadXML(_vm.variables.Get( m_params[1] ));
            ReadNodeValue( _vm.variables.Get( m_params[3] ) );

            _vm.variables.Update(m_params[5], m_nodeValue);

            return EnumActionResult.OK;				
		}

		private string Read(string xml, string xpath)
		{
			XmlDocument doc = new XmlDocument();
			string xmlNoNamespaces = RemoveNamespaces(xml);
			doc.Load(new StringReader(xmlNoNamespaces));
			XPathNavigator navi = doc.CreateNavigator();
			XPathExpression expr = navi.Compile(xpath);
			XPathNodeIterator iterator = navi.Select(expr);
			iterator.MoveNext();
			return iterator.Current.Value;
		}

		private string RemoveNamespaces(string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(new StringReader(xml));
			XPathNavigator navi = doc.CreateNavigator();
			XslTransform myXslTransform = new XslTransform();
			string strippedXml = null;
			using (MemoryStream ms = new MemoryStream())
			using (XmlTextWriter writer = new XmlTextWriter(ms, null))
			{
				myXslTransform.Load(XmlReader.Create(new StringReader(Xslt)));
				myXslTransform.Transform(navi, null, writer);

				ms.Position = 0L;
				using (StreamReader stream = new StreamReader(ms))
					strippedXml = stream.ReadToEnd();
			}

			return FormatXml(strippedXml);		
        }

		private string FormatXml(string xml)
		{
			StringBuilder sb = new StringBuilder();
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Indent = true,
				ConformanceLevel = ConformanceLevel.Auto,
				OmitXmlDeclaration = true
			};
			XmlDocument doc = new XmlDocument();
			doc.Load(new StringReader(xml));

			using (XmlWriter writer = XmlWriter.Create(sb, settings))
			{
				doc.Save(writer);
			}
			return sb.ToString();

		}
    }
}
