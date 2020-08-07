using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.TranslatorFramework.Configuration
{
    public sealed class GTSSTranslatorCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new GTSSTranslatorElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GTSSTranslatorElement)element).GTSSTranslatorPersianName;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return GTSSTranslatorMappingConstants.ConfigurationElementName; }
        }

        public GTSSTranslatorElement this[int index]
        {
            get { return (GTSSTranslatorElement)this.BaseGet(index); }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new GTSSTranslatorElement this[string interfaceShortTypeName]
        {
            get { return (GTSSTranslatorElement)this.BaseGet(interfaceShortTypeName); }
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
