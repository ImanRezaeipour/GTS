using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GTS.Clock.Infrastructure.NHibernateFramework
{
    public class NhibernateFilter : IComparable<NhibernateFilter>
    {
        private Hashtable items = new Hashtable();

        public string FilterName
        {
            get;
            set;
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        /// <summary>
        /// Add , Update
        /// </summary>      
        public void Add(string key, object value)
        {
            if (items.ContainsKey(key))
            {
                items[key] = value;
            }
            else
            {
                items.Add(key, value);
            }
        }

        public void Remove(string key)
        {
            if (items.ContainsKey(key))
            {
                items.Remove(key);
            }
        }

        public object GetItem(string key)
        {
            if (items.ContainsKey(key))
            {
                return items[key];
            }
            return null;
        }

        public int CompareTo(NhibernateFilter _filter) 
        {
            return this.FilterName.CompareTo(_filter.FilterName);           
        }
    }
}
