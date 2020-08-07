using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.TranslatorFramework.Configuration
{
    public sealed class GTSSDictionaryCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new GTSSDictionaryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GTSSDictionaryElement)element).GTSSDictionaryFullTypeName;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return GTSSTranslatorMappingConstants.GTSSDictionaryElementName; }
        }

        public GTSSDictionaryElement this[int index]
        {
            get { return (GTSSDictionaryElement)this.BaseGet(index); }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new GTSSDictionaryElement this[string GTSSDictionaryFullTypeName]
        {
            get { return (GTSSDictionaryElement)this.BaseGet(GTSSDictionaryFullTypeName); }
        }

        public bool ContainsKey(string keyName)
        {
            bool result = false;
            object[] keys = this.BaseGetAllKeys();
            foreach (object key in keys)
            {
                if ((string)key == keyName)
                {
                    result = true;
                    break;

                }
            }
            return result;
        }
    }
}
