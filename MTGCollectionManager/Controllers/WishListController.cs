using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MTGCollectionManager.Models;
using MTGCollectionManager.Models.Cards;
using MTGCollectionManager.Models.WishList;
using MTGCollectionManager.Services.Cards;
using MTGCollectionManager.Services.WishList;
using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;

namespace MTGCollectionManager.Controllers
{
    public class WishListController : Controller
    {
        private readonly IWishListService _wishListService;

        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
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

            _wishListService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService())
            {
                return View("~/Views/WishList/NotLoggedIn");
            }

            var wish = await _wishListService.GetWishListAsync();

            return View(wish);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WIshListAdd model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is invalid";
                return View(model);
            }

            if (!SetUserIdInService())
            {
                return View("~/Views/WishList/NotLoggedIn");
            }

            if (await _wishListService.AddWishListAsync(model))
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
            var card = await _wishListService.GetWishByIdAsync(Id);

            UpdateWishList update = new UpdateWishList();

            update.Id = Id;
            /*update.CardName = card.CardName;
            update.Quantity = card.Quantity;*/

            return View(update);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateWishList model)
        {
            if (!SetUserIdInService())
            {
                return View("~/Views/WishList/NotLoggedIn");
            }

            if (model == null || !ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is invalid";
                return View(model);
            }

            if (_wishListService.UpdateWishListAsync(model).Result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var wish = await _wishListService.GetWishByIdAsync(Id);

            return View(wish);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteWishList model)
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

            if (_wishListService.DeleteWishListAsync(model).Result)
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
