using bulkyApp.Data.Interface;
using bulkyApp.Models;
using bulkyApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;

namespace bulkyApp.Data.Repository
{
    public class CardRepo : CardInterface
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CardRepo(ApplicationDbContext db , IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        JsonResult CardInterface.AddCard(CardModel card, int productID)
        {
            CardModel? exitCard = _db.cards.Where(c => c.ProuctID == productID).FirstOrDefault();

            if (exitCard != null)
            {
                return new JsonResult(new ApiResponseModel { success = false, msg = "Already added" });
            }
            else
            {
                _db.cards.Add(card);
                _db.SaveChanges();
                SessionAccess.incressCardLen(_httpContextAccessor.HttpContext);
                return new JsonResult(new ApiResponseModel { success = true, msg = "Successfully added" });
            }
        }

        int CardInterface.ChangeQuantity(int newQuantity, int CardID)
        {
            CardModel card = _db.cards.Find(CardID)!;
            card.Quentity = newQuantity;

            _db.cards.Update(card);
            _db.SaveChanges();

            return card.Quentity;
        }

        CardModel CardInterface.GetCardById(int cardId)
        {
            return _db.cards.Find(cardId)!;
        }

        List<CardModel> CardInterface.GetUserWiseCardList(string userID)
        {
            return _db.cards.Include(e => e.Product).Include(e=> e.User).Where(c => c.UserID.ToString() == userID).ToList();
        }

        void CardInterface.RemoveToCard(int cardID)
        {
            _db.cards.Remove(_db.cards.Find(cardID)!);
            _db.SaveChanges();
            SessionAccess.reducCardLen(_httpContextAccessor.HttpContext);
        }
    }
}
