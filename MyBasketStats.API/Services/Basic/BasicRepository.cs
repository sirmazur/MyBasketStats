﻿using Microsoft.EntityFrameworkCore;
using MyBasketStats.API.DbContexts;
using System.Reflection.Metadata.Ecma335;

namespace MyBasketStats.API.Services.Basic
{
    public class BasicRepository<TEntity> : IBasicRepository<TEntity> where TEntity : class
    {

        private readonly MyBasketStatsContext _context;
        public BasicRepository(MyBasketStatsContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<(bool,TEntity?)> CheckIfIdExistsAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            return (entity != null,entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
