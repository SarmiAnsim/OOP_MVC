using EntityDataBases.Storage.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Storage
{
    public class AutoPartsStoreDataContext : DbContext
    {
        public AutoPartsStoreDataContext(DbContextOptions<AutoPartsStoreDataContext> options) : base(options)
        {

        }
        public DbSet<CarModel> CarsModels { get; set; }
        public DbSet<CategoryParts> CategorysParts { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<OrdersParts> OrdersParts { get; set; }
        public DbSet<Storage.Entity.Storage> Storages { get; set; }
    }
}
