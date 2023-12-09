using System;
using WebApp.Strategy.Enums;

namespace WebApp.Strategy.Entities
{
    public class Settings
    {
        public static string claimDatabaseType = "databasetype";
        public EDatabaseType DatabaseType;
        public EDatabaseType GetDefaultDatabaseType => EDatabaseType.MySql;
    }
}

