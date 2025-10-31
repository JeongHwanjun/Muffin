using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CakeDataReader : MonoBehaviour
{
    public StatRegistry statRegistry; // CakeData 역직렬화에 활용
    List<string> cakePaths;
    List<CakeData> cakeDatas;
    private OpeningReadyEventManager openingReadyEventManager;
    void Start()
    {
        openingReadyEventManager = OpeningReadyEventManager.Instance;
        openingReadyEventManager.OnEnlistCake += EnlistCake;
        openingReadyEventManager.OnDeleteCake += DeleteCake;
        cakePaths = new();
    }
    public List<CakeData> ReadCakeDatas()
    {
        if (cakePaths.Count <= 0) return null;
        cakeDatas = new();
        foreach (string path in cakePaths)
        {
            if (!File.Exists(path))
            {
                Debug.LogWarning("File does not exist : " + path);
                continue;
            }
            string json = File.ReadAllText(path);
            Debug.Log("Data read : " + json);

            cakeDatas.Add(CakeData.FromSerializable(JObject.Parse(json), statRegistry));
        }
        return cakeDatas;
    }

    void EnlistCake(string path) { cakePaths.Add(path); }
    void DeleteCake(string path) { cakePaths.RemoveAll(p => string.Equals(p, path)); } // O(n) n<=5
}
