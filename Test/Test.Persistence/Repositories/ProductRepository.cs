﻿using Microsoft.EntityFrameworkCore;
using Test.Application.Contracts.Persistence;
using Test.Domain;

namespace Test.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly TestDbContext _context;

        public ProductRepository(TestDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
