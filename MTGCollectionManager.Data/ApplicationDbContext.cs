using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MTGCollectionManager.Data.Entities;

namespace MTGCollectionManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CardEntity> Cards { get; set; }

        public DbSet<DeckEntity> Decks { get; set; }

        public DbSet<WishListEntity> WishListDb { get; set; }
    }
}