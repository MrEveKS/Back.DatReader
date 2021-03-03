﻿using DatReader.Models;
using Microsoft.EntityFrameworkCore;

namespace DatReader.Database
{
    public class DatDbContext : CoreDbContext
    {
        public DatDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Header> Headers { get; set; }
        public DbSet<CoordinateInformation> CoordinateInformations { get; set; }
        public DbSet<IpIntervalsInformation> IpIntervalsInformations { get; set; }
    }
}
