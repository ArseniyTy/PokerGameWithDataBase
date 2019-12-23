using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DatabaseService.Entities.Models;

namespace DatabaseService.Entities
{
    public class PokerGameContext : DbContext
    {
        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<GameSessionModel> PlayerGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerModel>().HasKey(p => p.Name);

            //One-to-many
            modelBuilder.Entity<PlayerModel>()
                .HasMany(p => p.Games)
                .WithOne(g => g.Player)
                .HasForeignKey(g => g.PlayerName);
        }
    }
}
