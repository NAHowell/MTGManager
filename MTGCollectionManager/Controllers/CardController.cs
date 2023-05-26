using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MTGCollectionManager.Models;
using MTGCollectionManager.Models.Cards;
using MTGCollectionManager.Services.Cards;
using System.Diagnostics;
using System.Security.Claims;

namespace MTGCollectionManager.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
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

            _cardService.SetUserId(userId);
            return true;
        }


        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService())
            {
                return View("Error");
            }

            var cards = await _cardService.GetCardListAsync();

            return View(cards);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CardAdd model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is invalid";
                return View(model);
            }

            if (!SetUserIdInService())
            {
                return View("~/Views/Card/NotLoggedIn");
            }

            if (await _cardService.AddCardAsync(model))
            {
                return Redirect(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewPage()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var card = await _cardService.GetCardByIdAsync(Id);

            UpdateCard update = new UpdateCard();

            update.Id = Id;
            /*update.CardName = card.CardName;
            update.Quantity = card.Quantity;*/

            return View(update);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCard model)
        {
            if (!SetUserIdInService())
            {
                return View("~/Views/Card/NotLoggedIn");
            }

            if (model == null || !ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is invalid";
                return View(model);
            }

            if (_cardService.UpdateCardAsync(model).Result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
            
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var card = await _cardService.GetCardByIdAsync(Id);

            return View(card);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCard model)
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

            if (_cardService.DeleteCardAsync(model).Result)
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
