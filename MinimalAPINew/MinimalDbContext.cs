﻿using Microsoft.EntityFrameworkCore;
using MinimalAPINew.Models;

namespace MinimalAPINew
{
    public class MinimalDbContext : DbContext
    {
        public MinimalDbContext(DbContextOptions options) : base (options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
