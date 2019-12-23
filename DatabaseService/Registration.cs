using System;

namespace DatabaseService
{
    public static class Registration
    {
        public static void LogIn(string name, int money, string password)
        {
            //запись в бд
        }
        public static bool SignIn(string name, string password)
        {
            //чтение из бд, если есть, то войти, нет вернуть false
            return false;
        }
    }
}
