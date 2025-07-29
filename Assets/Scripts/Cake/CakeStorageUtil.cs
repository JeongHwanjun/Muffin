using System.IO;
using UnityEngine;

public static class CakeStorageUtil
{
    public static string SavePath => Path.Combine(Application.persistentDataPath, "CakeData");
    public static string MetaDataPath => Path.Combine(Application.persistentDataPath, "CakeData", "MetaData.json");
}