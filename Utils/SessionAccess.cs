using Microsoft.AspNetCore.Http;
using NuGet.Configuration;
using System;

namespace bulkyApp.Utils
{
    public class SessionAccess
    {
        public static  bool IsLogIn(HttpContext httpContext)
        {
            //httpContext.Session.SetString("IsLogIn", "true");
            string? name = httpContext.Session.GetString("IsLogIn");
            if (name != null && name == "true")
                return true;
            else
                return false;
        }

        public static void setLogInAndRoleDataAndUserID(string? role,string userID , HttpContext httpContext)
        {
            httpContext.Session.SetString("IsLogIn", "true");
            httpContext.Session.SetString("role", role??"");
            httpContext.Session.SetString("userID", userID);
        }
        public static string getRole(HttpContext httpContext)
        {
            string? role = httpContext.Session.GetString("role");
            return role ?? string.Empty;
        }

        public static string getUserID(HttpContext httpContext)
        {
            return httpContext.Session.GetString("userID")!;
        }

        public static void setCardLength(HttpContext httpContext , int cardLen)
        {
            httpContext.Session.SetString("cardLen" , cardLen.ToString());
        }

        public static string getCardLength(HttpContext httpContext)
        {
            string? cardLen = httpContext.Session.GetString("cardLen");
            return cardLen ?? "0";
        }

        public static void reducCardLen(HttpContext httpContext)
        {
            int updateCardLen = Convert.ToInt32(getCardLength(httpContext)) - 1;
            setCardLength(httpContext, updateCardLen);
        }
        public static void incressCardLen(HttpContext httpContext)
        {
            int updateCardLen = Convert.ToInt32(getCardLength(httpContext)) + 1;
            setCardLength(httpContext, updateCardLen);
        }




        public static void clearSeesion(HttpContext httpContext)
        {
            httpContext.Session.Clear();
        }

    }
}
