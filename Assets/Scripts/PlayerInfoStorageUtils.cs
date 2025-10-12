using System.IO;

public static class PlayerInfoStorageUtils
{
  public static string BasePath => Path.Combine(Directory.GetCurrentDirectory(), "PlayerInfo");
  public static string PlayerInfoPath => Path.Combine(BasePath, "PlayerInfo.json");
}