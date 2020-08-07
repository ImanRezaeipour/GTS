using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GTS.Clock.Infrastructure.Validation.Configuration
{
    public sealed class UIValidationMappingCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new UIValidationMappingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UIValidationMappingElement)element).InterfaceShortTypeName;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "repositoryMapping"; }
        }

        public UIValidationMappingElement this[int index]
        {
            get { return (UIValidationMappingElement)this.BaseGet(index); }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new UIValidationMappingElement this[string interfaceShortTypeName]
        {
            get { return (UIValidationMappingElement)this.BaseGet(interfaceShortTypeName); }
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
