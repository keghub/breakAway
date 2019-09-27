using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BreakAway.Entities
{
    public class DbSetTable<T> : ITable<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public DbSetTable(DbSet<T> dbSet)
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
            return _dbSet.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dbSet.AsEnumerable().GetEnumerator();
        }

        public Type ElementType
        {
            get { return _dbSet.AsQueryable().ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return _dbSet.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _dbSet.AsQueryable().Provider; }
        }

        public override string ToString()
        {
            return _dbSet.ToString();
        }
    }
}