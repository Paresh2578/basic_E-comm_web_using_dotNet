using bulkyApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace bulkyApp.Data.Interface
{
    public interface CardInterface
    {
        List<CardModel> GetUserWiseCardList(string userID);

        CardModel GetCardById(int cardId);

        JsonResult AddCard(CardModel card, int productID);

        void RemoveToCard(int cardID);

        int ChangeQuantity(int newQuantity, int CardID);
    }
}
