using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.Report.BusinessFramework
{
    public sealed class BusinessMappingCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new BusinessMappingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BusinessMappingElement)element).InterfaceKey;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return BusinessMappingConstants.ConfigurationElementName; }
        }

        public BusinessMappingElement this[int index]
        {
            get { return (BusinessMappingElement)this.BaseGet(index); }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new BusinessMappingElement this[string interfaceShortTypeName]
        {
            get { return (BusinessMappingElement)this.BaseGet(interfaceShortTypeName); }
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
