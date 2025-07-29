using System;
using System.IO;
using UnityEngine;

public class CakeSerializer : MonoBehaviour
{
    public RecipeEventManager recipeEventManager;
    public CakeBuilder cakeBuilder;

    void Start()
    {
        recipeEventManager.OnSaveCake += SaveCake;
    }

    public void SaveCake()
    {
        try
        {
            CakeData newCake = cakeBuilder.BuildCake();
            string json = JsonUtility.ToJson(cakeBuilder.BuildCake());
            Debug.Log("CakeData Serialized : " + json.ToString());
            
            File.WriteAllText(Path.Combine(CakeStorageUtil.SavePath, newCake.cakeName), json);
            Debug.Log("케이크 데이터 저장 완료: " + CakeStorageUtil.SavePath);
        }
        catch(Exception e)
        {
            Debug.LogErrorFormat("CakeSerializer : {0}", e);
        }
    }
}
