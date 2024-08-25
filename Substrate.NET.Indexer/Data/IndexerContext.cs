using Microsoft.EntityFrameworkCore;
using Substrate.NET.Indexer.Model;
using System;

namespace Substrate.NET.Indexer.Data
{
    public class IndexerContext : DbContext
    {
        public DbSet<DbBlock> Blocks { get; set; }
        public DbSet<DbEvent> Events { get; set; }

        public IndexerContext(DbContextOptions<IndexerContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define BlockNumber as the primary key
            modelBuilder.Entity<DbBlock>()
                .HasKey(b => b.BlockNumber);

            // Define the relationship between DbBlock and DbEvent
            modelBuilder.Entity<DbBlock>()
                .HasMany(b => b.Events)
                .WithOne(e => e.Block)
                .HasForeignKey(e => e.BlockNumber);

            // Configure DbEvent's primary key
            modelBuilder.Entity<DbEvent>()
                .HasKey(e => e.EventId);
        }
    }
}