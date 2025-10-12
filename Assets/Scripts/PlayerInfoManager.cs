using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 게임 시작하자마자 playerData를 읽어와서 초기화해야함 ㅇㅇ
// 또한 저장도 담당함
public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField]
    private List<TierPair> ingredientTierDict;
    private PlayerData playerData;
    void Awake()
    {
        playerData = PlayerData.Instance;
        // Initialize;
        if (File.Exists(PlayerInfoStorageUtils.PlayerInfoPath)) // 플레이어 파일이 존재하면 그걸로 초기화함
        {
            // Read Files;
        }
        else // 없으면 그냥 에디터에서 지정된 데이터로 진행함
        {
            playerData.ingredientTierDict = new();
            foreach(var ingredientTier in ingredientTierDict)
            {
                playerData.ingredientTierDict.Add(ingredientTier.key, ingredientTier.value);
            }
        }
        
    }
}

[Serializable]
public class TierPair
{
    public IngredientGroup key;
    public int value;
}
