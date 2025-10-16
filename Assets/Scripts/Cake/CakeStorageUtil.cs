using System.IO;
using UnityEngine;

public static class CakeStorageUtil
{
    public static string BasePath => Path.Combine(Directory.GetCurrentDirectory(), "CakeData");
    public static string CakeRecipePath => Path.Combine(BasePath, "Recipes");
    public static string MetaDataPath => Path.Combine(CakeRecipePath, "MetaData");
    public static string CakeImagePath => Path.Combine(BasePath, "CakeImages");
}