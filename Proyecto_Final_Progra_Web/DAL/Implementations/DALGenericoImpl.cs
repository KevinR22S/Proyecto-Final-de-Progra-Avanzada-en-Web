using DAL.Intefaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Implementations
{
    public class DALGenericoImpl<TEntity> : IDALGenerico<TEntity> where TEntity : class
    {
        private readonly ProyectoFinalWebContext _context;

        public DALGenericoImpl(ProyectoFinalWebContext context)
        {
            _context = context;
        }

        public bool Add(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public bool Remove(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Attach(entity);
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
