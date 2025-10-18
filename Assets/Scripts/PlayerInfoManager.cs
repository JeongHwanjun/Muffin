using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

// 게임 시작하자마자 playerData를 읽어와서 초기화해야함 ㅇㅇ
// 또한 저장도 담당함
public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] [Tooltip("IngredientGroup의 Tier를 등록해야 합니다.")]private List<TierPair> ingredientTierDict;
    [SerializeField] [Tooltip("모든 IngredientGroup을 등록해야 합니다.")]private List<IngredientGroup> allIngredientGroups;
    private PlayerData playerData;

    public static PlayerInfoManager _instance;
    void Awake()
    {
        // 씬 내에 하나만 존재함을 보장함
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        playerData = PlayerData.Instance;
        // Initialize;
        if (File.Exists(PlayerInfoStorageUtils.PlayerInfoPath)) // 플레이어 파일이 존재하면 그걸 읽어옴
        {
            ReadPlayerData();
        }
        else // 없으면 그냥 에디터에서 지정된 데이터로 초기화함
        {
            InitializeFromEditor();
        }

        Debug.Log("CakeNumLimit : " + playerData.cakeNumLimit);
    }

    void InitializeFromEditor()
    {
        playerData.ingredientTierDict = new();
        foreach (var tierPair in ingredientTierDict)
        {
            playerData.ingredientTierDict[tierPair.key] = tierPair.value;
        }
        Debug.Log("InitializeFromEditor");
    }

    void OnApplicationQuit()
    {
        SavePlayerData();
    }

    void SavePlayerData()
    {
        try
        {
            var serializableData = new PlayerDataSerializable(playerData);
            string json = JsonConvert.SerializeObject(serializableData, Formatting.Indented);

            if(!Directory.Exists(PlayerInfoStorageUtils.BasePath)) Directory.CreateDirectory(PlayerInfoStorageUtils.BasePath);
            File.WriteAllText(PlayerInfoStorageUtils.PlayerInfoPath, json);
            Debug.Log("SavePlayerData");
        }
        catch (Exception e)
        {
            Debug.LogError($"[SavePlayerData] {e}");
        }
    }
    void ReadPlayerData()
    {
        try
        {
            string json = File.ReadAllText(PlayerInfoStorageUtils.PlayerInfoPath);
            var serializableData = JsonConvert.DeserializeObject<PlayerDataSerializable>(json);
            serializableData.ApplyTo(playerData, allIngredientGroups);
            Debug.Log("ReadPlayerData");
        }
        catch (Exception e)
        {
            Debug.LogError($"[ReadPlayerData] {e}");
            InitializeFromEditor();
        }
    }
}

[Serializable]
public class TierPair
{
    public IngredientGroup key;
    public int value;
}
