using Microsoft.AspNetCore.Mvc;
using MTGCollectionManager.Models;
using MTGCollectionManager.Models.Cards;
using MTGCollectionManager.Models.Decks;
using MTGCollectionManager.Services.Decks;
using System.Diagnostics;
using System.Security.Claims;

namespace MTGCollectionManager.Controllers
{
    public class DeckController : Controller
    {
        private readonly IDeckService _deckService;

        public DeckController(IDeckService deckService)
        {
            _deckService = deckService;
        }

        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            if (userIdClaim == null) return null;

            return userIdClaim;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;

            _deckService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService())
            {
                return View(Error);
            }

            var deck = await _deckService.GetDeckListAsync();

            return View(deck);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeckAdd model)
        {
            if(!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is invalid";
                return View(model);
            }

            if (!SetUserIdInService())
            {
                return View("~/Views/Deck/NotLoggedIn");
            }

            if (await _deckService.AddDeckAsync(model))
            {
                return Redirect(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var card = await _deckService.GetDeckByIdAsync(Id);

            UpdateDeck update = new UpdateDeck();

            update.Id = Id;
            /*update.CardName = card.CardName;
            update.Quantity = card.Quantity;*/

            return View(update);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateDeck model)
        {
            if (!SetUserIdInService())
            {
                return View("~/Views/Deck/NotLoggedIn");
            }

            if (model == null || !ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is invalid";
                return View(model);
            }

            if (_deckService.UpdateDeckAsync(model).Result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var card = await _deckService.GetDeckByIdAsync(Id);

            return View(card);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteDeck model)
        {
            if (!SetUserIdInService())
            {
                return View("~/Views/Card/NotLoggedIn");
            }

            if (!ModelState.IsValid || model == null)
            {
                TempData["ErrorMsg"] = "Model State is invalid";
                return View(model);
            }

            if (_deckService.DeleteDeckAsync(model).Result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
