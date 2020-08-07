using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GTS.Clock.Infrastructure.NHibernateFramework
{
    public class NhibernateFilters
    {
        private IList<NhibernateFilter> filters = new List<NhibernateFilter>();
        
        public void Add(NhibernateFilter filter)
        {
            filters.Add(filter);
        }

        public void Remove(NhibernateFilter filter)
        {           
            if (filters.Contains(filter))
            {               
                filters.Remove(filter);
            }
        }

        public bool HasFilter(string filterName) 
        {
            if (filters.Where(x => x.FilterName.Equals(filterName)).Count() > 0) 
            {
                return true;
            }
            return false;
        }

        public NhibernateFilter GetFilter(string filterName) 
        {
            if (HasFilter(filterName)) 
            {
                return filters.Where(x => x.FilterName.Equals(filterName)).First();
            }
            return new NhibernateFilter();
        }

        public void Clear()
        {
            this.filters.Clear();
        }
    }
}
