using Microsoft.EntityFrameworkCore;
using MTGCollectionManager.Data;
using MTGCollectionManager.Data.Entities;
using MTGCollectionManager.Models.Cards;
using MTGCollectionManager.Models.WishList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Services.WishList
{
    public class WishListService : IWishListService
    {
        private readonly ApplicationDbContext _context;

        public WishListService(ApplicationDbContext context)
        {
            _context = context;
        }

        private string _userId;

        public void SetUserId(string userId) => _userId = userId;


        public async Task<bool> AddWishListAsync(WIshListAdd model)
        {
            var wish = new WishListEntity
            {
                CardName = model.CardName,
                Quantity = model.Quantity,
                OwnerId = _userId
            };

            _context.WishListDb.Add(wish);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ViewWishList>> GetWishListAsync()
        {
            var wishList = _context
                .WishListDb
                .Where(n => n.OwnerId == _userId)
                .Select(n =>
                    new ViewWishList
                    {
                        CardName = n.CardName,
                        Quantity = n.Quantity,
                        id = n.Id

                    });
            return await wishList.ToListAsync();
        }

        public async Task<DeleteWishList> GetWishByIdAsync(int WishId)
        {
            var entity = await _context
                .WishListDb
                .FirstOrDefaultAsync(n => n.Id == WishId && n.OwnerId == _userId);

            if (entity is null)
                return null;

            var detail = new DeleteWishList
            {
                Id = entity.Id,
                CardName = entity.CardName,
                Quantity = entity.Quantity
            };

            return detail;
        }

        public async Task<bool> UpdateWishListAsync(UpdateWishList model)
        {
            if (model == null) return false;

            var entity = await _context.WishListDb.FindAsync(model.Id);

            if (entity?.OwnerId != _userId) return false;

            entity.CardName = model.CardName;
            entity.Quantity = model.Quantity;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteWishListAsync(DeleteWishList model)
        {
            if (model == null)
            {
                return false;
            }

            var entity = await _context.WishListDb.FindAsync(model.Id);

            if (entity?.OwnerId != _userId) return false;

            _context.WishListDb.Remove(entity);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
