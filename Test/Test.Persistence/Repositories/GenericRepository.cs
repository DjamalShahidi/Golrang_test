﻿using Microsoft.EntityFrameworkCore;
using Test.Application.Contracts.Persistence;

namespace Test.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TestDbContext _context;

        internal DbSet<T> dbSet;

        public GenericRepository(TestDbContext context)
        {
            this._context = context;
            dbSet = context.Set<T>();

        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            var enity=await GetAsync(id);

            return enity != null;
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }
    }
}
