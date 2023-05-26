using Microsoft.EntityFrameworkCore;
using MTGCollectionManager.Data;
using MTGCollectionManager.Data.Entities;
using MTGCollectionManager.Models.Cards;
using MTGCollectionManager.Models.Decks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Services.Decks
{
    public class DeckService : IDeckService
    {
        private readonly ApplicationDbContext _context;

        public DeckService(ApplicationDbContext context)
        {
            _context = context;
        }

        private string _userId;

        public void SetUserId(string userId) => _userId = userId;

        public async Task<bool> AddDeckAsync(DeckAdd model)
        {
            var deck = new DeckEntity
            {
                Name = model.Name,
                Format = model.Format,
                OwnerId = _userId
            };

            _context.Decks.Add(deck);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ViewDeck>> GetDeckListAsync()
        {
            var deck = _context
                .Decks
                .Where(n => n.OwnerId == _userId)
                .Select(n =>
                    new ViewDeck
                    {
                        Name = n.Name,
                        Format = n.Format,
                        Id = n.Id
                    });
            return await deck.ToListAsync();
        }

        public async Task<DeleteDeck> GetDeckByIdAsync(int deckId)
        {
            var entity = await _context
                .Decks
                .FirstOrDefaultAsync(n => n.Id == deckId && n.OwnerId == _userId);

            if (entity is null)
                return null;

            var detail = new DeleteDeck
            {
                Id = entity.Id,
                Name = entity.Name,
                Format = entity.Format
            };

            return detail;
        }

        public async Task<bool> UpdateDeckAsync(UpdateDeck model)
        {
            if (model == null) return false;

            var entity = await _context.Decks.FindAsync(model.Id);

            if (entity?.OwnerId != _userId) return false;

            entity.Name = model.Name;
            entity.Format = model.Format;

            return await _context.SaveChangesAsync() == 1;
        }

        

        public async Task<bool> DeleteDeckAsync(DeleteDeck model)
        {
            if (model == null)
            {
                return false;
            }

            var entity = await _context.Decks.FindAsync(model.Id);

            if (entity?.OwnerId != _userId) return false;

            _context.Decks.Remove(entity);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
