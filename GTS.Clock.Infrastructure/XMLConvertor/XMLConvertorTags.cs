using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.CustomeXMLConvertor
{
    public enum XMLCategory 
    {
       Default, Category, Category2, Category3, Category4, Category5, Category6, Category7, Category8, Category9
    };
    public class XMLConvertorElementAttribute:Attribute 
    {
        string nodeName = "";
        XMLCategory category = XMLCategory.Default;
        public string NodeName
        {
            get { return nodeName; }
        }
        public XMLCategory Category
        {
            get { return category; }
            set { category = value; }
        }
        public XMLConvertorElementAttribute(string elementName) 
        {
            nodeName = elementName;
        }
    }
    public class XMLConvertorRootAttribute : Attribute
    {
        string rootName = "";
        public string RootName
        {
            get { return rootName; }
        }
        public XMLConvertorRootAttribute()
        {
            rootName = "";          
        }
        public XMLConvertorRootAttribute(string rootTag)
        {
            rootName = rootTag;
        }
        
    }
    class XMLConvertorAttributeAttribute : Attribute
    {
        string attribute = "";
        public string Attribute
        {
            get { return attribute; }
        }

        private XMLConvertorAttributeAttribute(string attrib) 
        {
            attribute = attrib;
        }
    }

   
}
