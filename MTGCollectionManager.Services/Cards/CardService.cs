using Microsoft.EntityFrameworkCore;
using MTGCollectionManager.Data;
using MTGCollectionManager.Data.Entities;
using MTGCollectionManager.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Services.Cards
{
    public class CardService : ICardService
    {
        private readonly ApplicationDbContext _context;

        public CardService(ApplicationDbContext context)
        {
            _context = context;
        }

        private string _userId;

        public void SetUserId(string userId) => _userId = userId;

        public async Task<bool> AddCardAsync(CardAdd model)
        {
            var card = new CardEntity
            {
                CardName = model.CardName,
                Quantity = model.Quantity,
                OwnerId = _userId
            };

            _context.Cards.Add(card);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ViewCard>> GetCardListAsync()
        {
            var card = _context
                .Cards
                .Where(n => n.OwnerId == _userId)
                .Select(n =>
                    new ViewCard
                    {
                        CardName = n.CardName,
                        Quantity = n.Quantity,
                        Id = n.Id

                    });
            return await card.ToListAsync();
        }

        public async Task<DeleteCard> GetCardByIdAsync(int CardId)
        {
            var entity = await _context
                .Cards
                .FirstOrDefaultAsync(n => n.Id == CardId && n.OwnerId == _userId);

            if (entity is null)
                return null;

            var detail = new DeleteCard
            {
                Id = entity.Id,
                CardName = entity.CardName,
                Quantity = entity.Quantity
            };

            return detail;
        }

        public async Task<bool> UpdateCardAsync(UpdateCard model)
        {
            if (model == null) return false;

            var entity = await _context.Cards.FindAsync(model.Id);

            if (entity?.OwnerId != _userId) return false;

            entity.CardName = model.CardName;
            entity.Quantity = model.Quantity;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteCardAsync(DeleteCard model)
        {
            if (model == null)
            {
                return false;
            }

            var entity = await _context.Cards.FindAsync(model.Id);

            if (entity?.OwnerId != _userId) return false;

            _context.Cards.Remove(entity);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
