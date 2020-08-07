using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace GTS.Clock.Infrastructure.CustomeXMLConvertor
{
    public class XMLConvertor
    {
        #region DataStructure
        public class XMLElement 
        {
            string _tagName = "";
            List<ElementNode> _nodeList = new List<ElementNode>();
            public List<ElementNode> Nodes 
            {
                get { return _nodeList; }
                set { _nodeList = value; }
            }
            public string TagName 
            {
                get { return _tagName; }
                set { _tagName = value; }
            }
        }
        public class ElementNode 
        {
            string _tagName = "";
            object _value = "";
            public ElementNode(string tagName, object objectValue) 
            {
                _value = objectValue;
                _tagName = tagName;
            }
            public string TagName 
            {
                get { return _tagName; }
            }
            public object Value
            {
                get { return _value; }
            }
        }
        struct XMLNodePair 
        {
            Hashtable elementNodes;
            public string xmlNodeName ;
            public string xmlNodeText ;
            public XMLNodePair(string name, string vlaue) 
            {
                xmlNodeName = name;
                xmlNodeText = vlaue;
                elementNodes = new Hashtable();
            }
        }
        #endregion

        XMLCategory xmlCategory = XMLCategory.Default;
        XmlDocument doc;
        //private string _classFullName = "";
        private Type objType;
        private object obj;
        Hashtable xmlElements = new Hashtable();
        string xmlRootName = "";     
        bool isList = false;
        /// <summary>
        /// Get a class object and convert it to xml document
        /// </summary>
        /// <param name="classFullName">class full name contains it's namespace</param>
        public XMLConvertor(object theObject)
        {
            Initialize(theObject);    
        }
        public XMLConvertor(object theObject,XMLCategory categoty)
        {
            Initialize(theObject);
            xmlCategory = categoty;
        }
        private void Initialize(object theObject) 
        {
            doc = new XmlDocument();
            isList = false;
            obj = theObject;
            if (theObject is IList)
            {
                isList = true;
                IList myList = (IList)theObject;
                objType = myList.GetType().GetGenericArguments()[0];

            }
            else
            {
                objType = theObject.GetType();
            }
        }
        public XmlDocument ConvertToXML()
        {

            SearchAttributes(obj, objType, isList);

            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);// Create the root element
            XmlElement root = doc.CreateElement(xmlRootName);
            doc.AppendChild(root);
           

            //ruleNode.SetAttribute("Id", rule.ID.ToString());
            foreach (string key in xmlElements.Keys)
            {
                CustomeXMLConvertor.XMLConvertor.XMLElement xmlElemnt = (CustomeXMLConvertor.XMLConvertor.XMLElement)xmlElements[key];

                XmlElement elementNode = doc.CreateElement(xmlElemnt.TagName);                
                foreach (ElementNode nodeElem in xmlElemnt.Nodes) 
                {
                    XmlElement innerNode = doc.CreateElement(nodeElem.TagName);
                    if (nodeElem.Value != null)
                    {
                        innerNode.InnerText = nodeElem.Value.ToString();
                    }
                    else 
                    {
                        innerNode.InnerText = "";
                    }
                    elementNode.AppendChild(innerNode);
                }
                root.AppendChild(elementNode);
            }
           
            return doc;
        }

        private  void SearchAttributes(object theObject,Type theObjectType,bool isObjectList)
        {
            #region root and MainElement Tag Name Setting
            foreach (object attribute in theObjectType.GetCustomAttributes(true))
            {
                if (attribute.GetType() == typeof(XMLConvertorRootAttribute))
                {
                    xmlRootName = ((XMLConvertorRootAttribute)attribute).RootName;                  
                }
            }
            if (xmlRootName.Length == 0 || xmlRootName.Equals(theObjectType.Name))
            {
                xmlRootName = theObjectType.Name + "List";
            }
           
           
            #endregion
                     
            Guid guid = new Guid();            
            if (!isObjectList)
            {
                CustomeXMLConvertor.XMLConvertor.XMLElement xmlElement = new CustomeXMLConvertor.XMLConvertor.XMLElement();
                xmlElement.TagName = theObjectType.Name;

                #region FieldSearch
                foreach (FieldInfo fInfo in theObjectType.GetFields())
                {
                    foreach (Attribute attr in Attribute.GetCustomAttributes(fInfo))
                    {
                        if (attr.GetType() == typeof(XMLConvertorElementAttribute))
                        {
                            if (((XMLConvertorElementAttribute)attr).Category.Equals(xmlCategory))
                            {
                                object fValue = fInfo.GetValue(obj);
                                ElementNode node = new ElementNode(((XMLConvertorElementAttribute)attr).NodeName, fValue);
                            }
                        }
                    }
                }
                #endregion

                #region Property Search
                foreach (PropertyInfo pInfo in theObjectType.GetProperties())
                {
                    foreach (Attribute attr in Attribute.GetCustomAttributes(pInfo))
                    {
                        if (attr.GetType() == typeof(XMLConvertorElementAttribute))
                        {
                            if (((XMLConvertorElementAttribute)attr).Category.Equals(xmlCategory))
                            {
                                object pValue = pInfo.GetValue(obj, null);
                                ElementNode node = new ElementNode(((XMLConvertorElementAttribute)attr).NodeName, pValue);
                                xmlElement.Nodes.Add(node);
                            }
                        }
                    }
                }
                #endregion

                xmlElements.Add(Guid.NewGuid().ToString(), xmlElement);
            }
            else             
            {                               
                IList myList = (IList)theObject;
                foreach (object item in myList) 
                {
                    Type itemType = item.GetType();
                    CustomeXMLConvertor.XMLConvertor.XMLElement xmlElement = new CustomeXMLConvertor.XMLConvertor.XMLElement();
                    xmlElement.TagName = itemType.Name;

                    #region FieldSearch
                    foreach (FieldInfo fInfo in theObjectType.GetFields())
                    {
                        foreach (Attribute attr in Attribute.GetCustomAttributes(fInfo))
                        {
                            if (attr.GetType() == typeof(XMLConvertorElementAttribute))
                            {
                                if (((XMLConvertorElementAttribute)attr).Category.Equals(xmlCategory))
                                {
                                    object fValue = fInfo.GetValue(item);
                                    ElementNode node = new ElementNode(((XMLConvertorElementAttribute)attr).NodeName, fValue);
                                }
                            }
                        }
                    }
                    #endregion

                    #region Property Search
                    foreach (PropertyInfo pInfo in theObjectType.GetProperties())
                    {
                        foreach (Attribute attr in Attribute.GetCustomAttributes(pInfo))
                        {
                            if (attr.GetType() == typeof(XMLConvertorElementAttribute))
                            {
                                if (((XMLConvertorElementAttribute)attr).Category.Equals(xmlCategory))
                                {
                                    object pValue = pInfo.GetValue(item, null);
                                    ElementNode node = new ElementNode(((XMLConvertorElementAttribute)attr).NodeName, pValue);
                                    xmlElement.Nodes.Add(node);
                                }
                            }
                        }
                    }
                    #endregion

                    xmlElements.Add(Guid.NewGuid().ToString(), xmlElement);
                }

            }        
        }

        public void ShowAttributes() 
        {
            if (xmlElements.Count > 0) 
            {
                foreach(string key in xmlElements.Keys ) 
                {
                   // XMLNodePair pair = (XMLNodePair)xmlElements[key];
                    //Console.WriteLine("Item Name:\"{0}\" and XML Element Name is \"{1}\" and inner Text is :\"{2}\"", key, pair.xmlNodeName, pair.xmlNodeText);
                }
            }
        }
    }
}
