using MTGCollectionManager.Models.Cards;
using MTGCollectionManager.Models.Decks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGCollectionManager.Services.Decks
{
    public interface IDeckService
    {
        Task<bool> AddDeckAsync(DeckAdd model);

        Task<IEnumerable<ViewDeck>> GetDeckListAsync();

        Task<DeleteDeck> GetDeckByIdAsync(int deckId);

        Task<bool> UpdateDeckAsync(UpdateDeck model);

        Task<bool> DeleteDeckAsync(DeleteDeck model);

        void SetUserId(string userId);
    }
}
