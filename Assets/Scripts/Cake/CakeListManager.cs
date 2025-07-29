using System.IO;
using UnityEngine;

/* 케이크 메타데이터 파일을 관리하는 클래스 */
public static class CakeListManager
{
    private static string metaDataPath = CakeStorageUtil.MetaDataPath;
    public static CakeSummaryList LoadList()
    {
        if (!File.Exists(metaDataPath))
            return new CakeSummaryList();

        string json = File.ReadAllText(metaDataPath);
        return JsonUtility.FromJson<CakeSummaryList>(json);
    }

    public static void SaveList(CakeSummaryList list)
    {
        string json = JsonUtility.ToJson(list, true);
        File.WriteAllText(metaDataPath, json);
    }

    public static void AddCake(CakeSummary newCake)
    {
        var list = LoadList();
        list.cakes.Add(newCake);
        SaveList(list);
    }

    public static void RemoveCake(string id)
    {
        var list = LoadList();
        list.cakes.RemoveAll(c => c.id == id);
        SaveList(list);
    }
}
