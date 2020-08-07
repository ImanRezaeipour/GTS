using System;
using System.Configuration;

namespace GTS.Clock.Infrastructure.CalculatorSchedulerFramework.Configuration
{
    public sealed class CalculatorSchedulerCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CalculatorSchedulerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CalculatorSchedulerElement)element).SchedulerName;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return CalculatorSchedulerConstants.ConfigurationElementName; }
        }

        public CalculatorSchedulerElement this[int index]
        {
            get { return (CalculatorSchedulerElement)this.BaseGet(index); }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new CalculatorSchedulerElement this[string schedulerName]
        {
            get { return (CalculatorSchedulerElement)this.BaseGet(schedulerName); }
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
