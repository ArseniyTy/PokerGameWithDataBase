using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DatabaseService.Entities.Models;

namespace DatabaseService.Entities
{
    /// <summary>
    /// Poker game database context
    /// </summary>
    public class PokerGameContext : DbContext
    {
        /// <summary>
        /// Table of players
        /// </summary>
        public DbSet<PlayerModel> Players { get; set; }
        /// <summary>
        /// Table of game statistic history of the Player
        /// </summary>
        public DbSet<GameSessionModel> PlayerGames { get; set; }

        /// <summary>
        /// Creates(connects to) the context.
        /// </summary>
        public PokerGameContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=pokerGameDb;Trusted_Connection=True;");
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
