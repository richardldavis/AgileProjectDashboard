namespace ProjectDashboard.Domain.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    public static class XmlHelper
    {
        public static XmlNode AddNode(XmlDocument document, XmlNode parent, string name, object value)
        {
            return AddNode(document, parent, name, value, null);
        }

        public static XmlNode AddNode(XmlDocument document, XmlNode parent, string name, object value, string ns)
        {
            var text = value != null ? value.ToString() : string.Empty;
            var child = document.CreateNode(XmlNodeType.Element, name, ns);
            if (value != null)
            {
                child.InnerText = text;
            }

            parent.AppendChild(child);
            return child;
        }

        public static XmlAttribute AddAttribute(XmlDocument parent, XmlNode node, string name, object value)
        {
            var text = value != null ? value.ToString() : string.Empty;
            var attribute = parent.CreateAttribute(name);
            if (value != null)
            {
                attribute.InnerText = text;
            }

            node.Attributes.Append(attribute);
            return attribute;
        }

        public static string GetText(this XmlNode node, string child)
        {
            return NodeText(node, child);
        }

        public static string GetText(this XmlNode node, string child, XmlNamespaceManager nsmgr)
        {
            return NodeText(node, child, nsmgr);
        }

        public static string NodeText(XmlNode node, string child)
        {
            return NodeText(node, child, null);
        }

        public static string NodeText(XmlNode node, string child, XmlNamespaceManager nsmgr)
        {
            string value = null;
            var childNode = node.SelectSingleNode(child, nsmgr);
            if (childNode != null)
            {
                value = childNode.InnerText;
            }

            return value;
        }

        public static List<string> NodeValues(XmlNode node, string xpath, XmlNamespaceManager nsmgr)
        {
            var childNodeList = node.SelectNodes(xpath, nsmgr);
            return (from XmlNode childNode in childNodeList where childNode != null select childNode.InnerText).ToList();
        }

        public static string AttributeValue(XmlNode node, string attribute)
        {
            string value = null;
            var attr = node.Attributes[attribute];
            if (attr != null)
            {
                value = attr.InnerText;
            }

            return value;
        }
    }
}
