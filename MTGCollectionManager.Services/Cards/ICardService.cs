using MTGCollectionManager.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Services.Cards
{
    public interface ICardService
    {
        Task<bool> AddCardAsync(CardAdd model);

        Task<IEnumerable<ViewCard>> GetCardListAsync();

        Task<DeleteCard> GetCardByIdAsync(int CardId);

        Task<bool> UpdateCardAsync(UpdateCard model);

        Task<bool> DeleteCardAsync(DeleteCard model);

        void SetUserId(string userId);
    }
}
