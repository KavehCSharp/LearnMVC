using System.Diagnostics.CodeAnalysis;
using CrudImageRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudImageRepository.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}