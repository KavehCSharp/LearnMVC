using System.Diagnostics.CodeAnalysis;
using CrudRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudRepository.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}