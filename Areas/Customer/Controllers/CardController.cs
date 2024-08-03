using bulkyApp.CV;
using bulkyApp.Data.Interface;
using bulkyApp.Models;
using bulkyApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bulkyApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [IsLogIn]
    public class CardController : Controller
    {
        private readonly CardInterface _ICard;
        public CardController(CardInterface ICard)
        {
            _ICard = ICard;
        }
        public IActionResult cardList()
        {
            string usrID = SessionAccess.getUserID(HttpContext);
            List<CardModel> cardList = _ICard.GetUserWiseCardList(usrID);
            return View("CardList", cardList);
        }

        public IActionResult Detele(int id)
        {
            _ICard.RemoveToCard(id);
            return RedirectToAction("cardList");
        }

        [Route("/ChangeQuantity/{cardID}/{newQuantity}")]
        public IActionResult ChangeQuantity(int cardID, int newQuantity)
        {
            _ICard.ChangeQuantity(newQuantity, cardID);
            return RedirectToAction("cardList");

        }


        [Route("/addToDart/{productID}/{quentity}")]
        public IActionResult AddToCard(int productID, int quentity)
        {
            string userID = SessionAccess.getUserID(HttpContext)?? "0";

            CardModel card = new CardModel { Quentity = quentity, ProuctID = productID, UserID = Convert.ToInt32(userID) };
            ApiResponseModel response = _ICard.AddCard(card, productID).Value as ApiResponseModel;

                if (response.success)
                    TempData["success"] = response.msg;
                else
                    TempData["error"] = response.msg;

            // Redirect to the Details action with the product ID
            return RedirectToAction("Details","Home", new { productId = productID });
        }
    }
}
