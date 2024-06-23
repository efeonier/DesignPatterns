using WebApp.Strategy.Enums;

namespace WebApp.Strategy.Models;

public class SettingsModel
{
    public const string ClaimDatabaseType = "databasetype";
    public EDatabaseType DatabaseType { get; set; }
    public static EDatabaseType GetDefaultDatabaseType => EDatabaseType.SqlServer;
}