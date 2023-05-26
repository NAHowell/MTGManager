using MTGCollectionManager.Models.Cards;
using MTGCollectionManager.Models.WishList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Services.WishList
{
    public interface IWishListService
    {
        Task<bool> AddWishListAsync(WIshListAdd model);

        Task<IEnumerable<ViewWishList>> GetWishListAsync();

        Task<DeleteWishList> GetWishByIdAsync(int WishId);

        Task<bool> UpdateWishListAsync(UpdateWishList model);

        Task<bool> DeleteWishListAsync(DeleteWishList model);

        void SetUserId(string  userId);
    }
}
