using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BreakAway.Entities
{
    public class DbSetTable<T> : ITable<T> where T : class
    {
        private readonly IDbSet<T> _dbSet;

        public DbSetTable(IDbSet<T> dbSet)
        {
            if (dbSet == null)
            {
                throw new ArgumentNullException("dbSet");
            }

            _dbSet = dbSet;
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _dbSet.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dbSet.GetEnumerator();
        }

        public Type ElementType
        {
            get { return _dbSet.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return _dbSet.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _dbSet.Provider; }
        }

        public override string ToString()
        {
            return _dbSet.ToString();
        }
    }
}