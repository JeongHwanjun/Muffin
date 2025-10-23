using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

// 메타데이터를 읽어오는 역할임
public class MetadataReader
{
    public static List<CakeMetaData> ReadMetaData()
    {
        if (!Directory.Exists(CakeStorageUtil.MetaDataPath))
        {
            Debug.LogError("Reading MetaData : File Does Not Exist");
            return null;
        }

        string json;
        List<CakeMetaData> cakeMetaData = null;

        try
        {
            json = File.ReadAllText(Path.Combine(CakeStorageUtil.MetaDataPath, "Metadata.json"));
            cakeMetaData = JsonConvert.DeserializeObject<List<CakeMetaData>>(json);

            Debug.LogFormat("Read Data : {0}", json);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }

        return cakeMetaData;
    }
}
