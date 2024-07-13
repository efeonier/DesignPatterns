using WebApp.StrategyPattern.Enums;

namespace WebApp.StrategyPattern.Models;

public class SettingsModel
{
    public const string ClaimDatabaseType = "databasetype";
    public EDatabaseType DatabaseType { get; set; }
    public static EDatabaseType GetDefaultDatabaseType => EDatabaseType.SqlServer;
}
